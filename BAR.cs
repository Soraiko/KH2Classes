
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Collections;
using OpenTK.Graphics.ES30;
using System.Globalization;
using System.Windows.Forms;

namespace dbkg
{
    public class BAR
    {
        public const string MAGIC_CODE = "BAR\x1";

        static byte[] buffer = new byte[4];

        public int Reserved;
        public int HeaderSize;
        public BarEntry Entry;

        public unsafe struct BarEntry
        {
            public static BarEntry Empty;

            public BAR Parent;
            public int Index;

            public byte[] Data;

            public short Type           { get { fixed (byte* a = &Data[0x00]) return (*(short*)a); } }
            public short DuplicateFlag  { get { fixed (byte* a = &Data[0x02]) return (*(short*)a); } }
            public string Name          { get { fixed (byte* a = &Data[0x04]) return new string((sbyte*)a, 0, 4); } }
            public int Offset           { get { fixed (byte* a = &Data[0x08]) return (*(int*)a); } }
            public int Size           { get { fixed (byte* a = &Data[0x0C]) return (*(int*)a); } }

            public long AbsoluteOffset;
            public long AbsoluteSize;

            public BarEntry(BAR parent, int index, byte[] entryBytes, long absoluteOffset, long absoluteSize)
            {
                this.Parent = parent;
                this.Index = index;
                this.Data = entryBytes;
                this.AbsoluteOffset = absoluteOffset;
                this.AbsoluteSize = absoluteSize;
            }
            public new string ToString()
            {
                return this.Data == null ? "[null]" : "Type "+this.Type.ToString("X4") + " "+this.Name.TrimEnd('\x0') + " @" + this.Offset.ToString("X8")+" #"+this.Size.ToString("X") + " (#"+this.AbsoluteSize.ToString("X")+")";
            }
        }

        public BARCollection<BAR> Files;
        public byte[] Data;

        public static byte[] ReadBytes(Stream stream, int length)
        {
            stream.Read(buffer, 0, length);
            return buffer;
        }

        public new string ToString()
        {
            return this.Entry.ToString();
        }

        public BAR(string filename):this(new FileStream(filename, FileMode.Open))
        {

        }

        public BAR(Stream stream) : this(stream, new BarEntry(null, -1, null, stream.Position, stream.Length))
        {
            this.DeabsoluteDataAddresses(stream, this);
        }

        public unsafe BAR(Stream stream, BarEntry entry)
        {
            this.Entry = entry;
            stream.Position = entry.AbsoluteOffset;
            this.Files = new BARCollection<BAR>(0);
            this.NamePairs = new BARCollection<BAR>(0);

            var hasMagicCode = Encoding.ASCII.GetString(ReadBytes(stream, 4)) == MAGIC_CODE;

            if (hasMagicCode && (entry.Data == null || (entry.DuplicateFlag == 0 && entry.Size > 0)))
            {
                int child_count = BitConverter.ToInt32(ReadBytes(stream, 4), 0);
                stream.Position += 4;
                this.Reserved = BitConverter.ToInt32(ReadBytes(stream, 4), 0);
                this.HeaderSize = 0x10 + child_count * 0x10;
                if (child_count > 0 && entry.AbsoluteOffset + this.HeaderSize <= stream.Length)
                {
                    List<BarEntry> entries = new List<BarEntry>(0);
                    for (int i = 0; i < child_count; i++)
                    {
                        byte[] entryBytes = new byte[16];
                        stream.Position = entry.AbsoluteOffset + 0x10 + i * 0x10;
                        stream.Read(entryBytes, 0, 16);
                        entries.Add(new BarEntry(this, i, entryBytes, entry.AbsoluteOffset, 0));
                    }
                    BarEntry earliest = BarEntry.Empty;

                    for (int i = 0; i < entries.Count; i++)
                    {
                        BarEntry entry_ = entries[i];
                        bool relyToSize = false;
                        long closest = long.MaxValue;

                        for (int j = 0; j < entries.Count; j++)
                        {
                            long diff = entries[j].Offset - entry_.Offset;
                            if (diff == 0 && (i != j))
                                relyToSize = true;
                            if (diff > 0 && diff < closest)
                                closest = diff;
                        }

                        if (stream is ProcessStream)
                            entry_.AbsoluteOffset = entry_.Offset;
                        else
                            entry_.AbsoluteOffset += entry_.Offset;

                        if (closest == long.MaxValue)
                        {
                            if (stream is ProcessStream)
                            {
                                if (entry.Data == null)
                                    closest = entry_.Size;
                                else
                                    closest = entry.AbsoluteOffset + entry.AbsoluteSize - entry_.AbsoluteOffset;
                            }
                            else
                                closest = entry.AbsoluteSize - entry_.Offset;
                        }

                        if (relyToSize == false || (entry_.DuplicateFlag == 0 && entry_.Size > 0))
                            entry_.AbsoluteSize = closest;

                        if (entry_.AbsoluteOffset > entry.AbsoluteOffset && (earliest.Data == null || entry_.AbsoluteOffset < earliest.AbsoluteOffset))
                            earliest = entry_;

                        entries[i] = entry_;
                    }
                    for (int h = 0; h < entries.Count; h++)
                    {
                        BarEntry entry_ = entries[h];
                        BAR child = new BAR(stream, entry_);
                        this.Files.Add(child);
                    }
                    if (entries.Count == 0)
                    {
                        if (stream is ProcessStream == false)
                            this.HeaderSize = (int)entry.AbsoluteSize;
                    }
                    else
                        this.HeaderSize = (int)(earliest.AbsoluteOffset - entry.AbsoluteOffset);

                    if (entry.Data == null)
                        stream.Close();
                    return;
                }
            }

            stream.Position = entry.AbsoluteOffset;
            this.Data = new byte[entry.AbsoluteSize];
            if (entry.Data == null || entry.Size > 0)
                stream.Read(this.Data, 0, (int)entry.AbsoluteSize);

            if (entry.Data == null)
                stream.Close();
		}

        public unsafe void DeabsoluteDataAddresses(Stream stream, BAR recursiveBar)
        {
            if (stream is ProcessStream)
            {
                if (recursiveBar.Entry.Data != null)
                switch (recursiveBar.Entry.Type)
                {
                    case 9:
                        Array.Copy(new byte[0x30], 0, recursiveBar.Data, 0, 0x30);
                        if (recursiveBar.Entry.Parent != null && recursiveBar.Entry.Index+1 < recursiveBar.Entry.Parent.Files.Count &&
                                recursiveBar.Entry.Parent.Files[recursiveBar.Entry.Index + 1].Entry.Type == 0x10)
                        fixed (byte* a = &recursiveBar.Entry.Data[0x04])
                        fixed (byte* b = &recursiveBar.Entry.Parent.Files[recursiveBar.Entry.Index + 1].Entry.Data[0x04])
                            (*(int*)a) = (*(int*)b);
                        int hrcOffset = 0x90+BitConverter.ToInt32(recursiveBar.Data, 0x90 + 0x18);
                        int flagOffset = 0x90+BitConverter.ToInt32(recursiveBar.Data, 0x90 + 0x1C);
                        for (int i= hrcOffset;i< flagOffset;i+=0x40)
                        {
                            short boneIndex = BitConverter.ToInt16(recursiveBar.Data, i);
                            Array.Copy(BitConverter.GetBytes((int)boneIndex), 0, recursiveBar.Data, i, 4);

                            short parentIndex = BitConverter.ToInt16(recursiveBar.Data, i+4);
                            Array.Copy(BitConverter.GetBytes((int)parentIndex), 0, recursiveBar.Data, i+4, 4);
                        }
                    break;
                    case 4:
                        Array.Copy(new byte[0x30], 0, recursiveBar.Data, 0, 0x30);
                        if (BitConverter.ToInt16(recursiveBar.Data, 0x90) == 3)
                        {
                            int bonesCounts = BitConverter.ToInt16(recursiveBar.Data, 0xA0);
                            int bonesOff = 0x90 + BitConverter.ToInt32(recursiveBar.Data, 0xA4);
                            for (int i = 0; i < bonesCounts; i++)
                            {
                                short boneIndex = BitConverter.ToInt16(recursiveBar.Data, bonesOff + i * 0x40);
                                Array.Copy(BitConverter.GetBytes((int)boneIndex), 0, recursiveBar.Data, bonesOff + i * 0x40, 4);

                                short parentIndex = BitConverter.ToInt16(recursiveBar.Data, bonesOff + 0x04 + i * 0x40);
                                Array.Copy(BitConverter.GetBytes((int)parentIndex), 0, recursiveBar.Data, bonesOff + 0x04 + i * 0x40, 4);
                            }
                        }
                    break;
                    case 7:
                        int tim_header_6s = global::System.BitConverter.ToInt32(recursiveBar.Data, 0x14);
                        int tim_header_6s_count = global::System.BitConverter.ToInt32(recursiveBar.Data, 0x08) + 1;
                        for (int i = 0; i < tim_header_6s_count; i++)
                        {
                            int address = global::System.BitConverter.ToInt32(recursiveBar.Data, tim_header_6s + i * 0x90 + 0x74);
                            Array.Copy(global::System.BitConverter.GetBytes(address - recursiveBar.Entry.AbsoluteOffset), 0, recursiveBar.Data, tim_header_6s + i * 0x90 + 0x74, 4);
                        }
                    break;
                }
            }
            foreach (BAR child in recursiveBar.Files)
            {
                DeabsoluteDataAddresses(stream, child);
            }
            var rBar = recursiveBar;
            if (rBar.Entry.Parent != null)
            for (int i = rBar.Entry.Parent.Files.IndexOf(rBar) + 1; i > -1 && i < rBar.Entry.Parent.Files.Count;i++)
            {
                BAR sibling = rBar.Entry.Parent.Files[i];
                if (sibling.Entry.Type != rBar.Entry.Type && ("tim_`" + rBar.Entry.Name).Contains(sibling.Entry.Name) && rBar.NamePairs.GetAllOfType(sibling.Entry.Type).Count == 0)
                {
                    rBar.NamePairs.Add(sibling);
                    sibling.NamePairs.Add(rBar);
                }
                else
                    break;
            }
        }

		public void Save(Stream stream, bool firstRecursirveStep)
        {
            if (this.Data != null)
            {
				stream.Write(this.Data, 0, this.Data.Length);
			}
			else
            {
                if (firstRecursirveStep || (this.Entry.DuplicateFlag == 0 && this.Entry.AbsoluteSize > 0))
                {
                    long headerPosition = stream.Position;

                    stream.Write(Encoding.ASCII.GetBytes(MAGIC_CODE), 0, MAGIC_CODE.Length);
                    stream.Write(BitConverter.GetBytes(this.Files.Count), 0, sizeof(int));
                    stream.Write(new byte[4], 0, 4);
                    stream.Write(BitConverter.GetBytes(this.Reserved), 0, sizeof(int));

                    foreach (BAR child in this.Files)
                    {
                        stream.Write(BitConverter.GetBytes(child.Entry.Type), 0, sizeof(short));
                        stream.Write(BitConverter.GetBytes(child.Entry.DuplicateFlag), 0, sizeof(short));
                        string name = child.Entry.Name;
                        while (name.Length < 4) name += "\0";
                        stream.Write(Encoding.ASCII.GetBytes(name), 0, name.Length);
                        stream.Write(BitConverter.GetBytes(0), 0, sizeof(int));
                        stream.Write(BitConverter.GetBytes(child.Entry.Size), 0, sizeof(int));
                    }
                    byte[] headerReservedBytes = new byte[this.HeaderSize - (0x10 + 0x10 * this.Files.Count)];
                    stream.Write(headerReservedBytes, 0, headerReservedBytes.Length);
                    List<long> offsets = new List<long>(0);

                    foreach (BAR child in this.Files)
                    {
                        stream.Position = headerPosition + 0x10 + offsets.Count * 0x10 + 0x08;
                        long offset = stream.Length - headerPosition;

                        if (child.Entry.DuplicateFlag == 1 || child.Entry.Size == 0)
                        {
                            if (child.Entry.Offset == 0)
                            {
                                for (int i = 0; i < offsets.Count; i++)
                                    if (this.Files[i].Entry.DuplicateFlag == 0 && this.Files[i].Entry.Name == child.Entry.Name)
                                    {
                                        offset = offsets[i];
                                        break;
                                    }
                            }
                            else
                            {
                                for (int i = 0; i < offsets.Count; i++)
                                    if (this.Files[i].Entry.Size > 0 && this.Files[i].Entry.Offset == child.Entry.Offset)
                                    {
                                        offset = offsets[i];
                                        break;
                                    }
                            }
                        }
                        stream.Write(BitConverter.GetBytes((int)offset), 0, sizeof(int));
                        stream.Position = stream.Length;
                        child.Save(stream, false);
                        offsets.Add(offset);
                    }
                }
            }
            if (firstRecursirveStep)
            stream.Close();
		}

        public BARCollection<BAR> NamePairs;

    }
    public class BARCollection<T> : List<BAR>
    {
        public BARCollection(int count) : base(count)
        {

        }

        public new void Add(BAR item)
        {
            base.Add(item);
        }
        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }
        public BARCollection<BAR> GetAllOfType(short type)
        {
            BARCollection<BAR> output = new BARCollection<BAR>(0);
            foreach (BAR child in this)
            {
                if (child.Entry.Data != null && child.Entry.Type == type)
                {
                    output.Add(child);
                }
            }
            return output;
        }
    }
}
