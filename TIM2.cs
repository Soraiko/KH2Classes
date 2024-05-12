using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using GL = OpenTK.Graphics.OpenGL;
using static dbkg.Texture2D;
using System.Reflection;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace dbkg
{
    /* Knowledge provided by Disgustor THANKS */
    public class TIM2
    {
        public unsafe struct dataTransfer
        {
            public static byte[] gsBuffer;
            public int binOffset {get { fixed (byte* a = &data[offset + 0x74]) return (*(int*)a); } set { fixed (byte* a = &data[offset + 0x74]) (*(int*)a) = value; }}

            public const int SIZE = 0x90;
            public byte[] data;
            public int offset;

            static dataTransfer()
            {
                gsBuffer = new byte[1024 * 1024 * 4];
            }

            public dataTransfer(ref byte[] data, int offset)
            {
                this.data = data;
                this.offset = offset;
            }
        }

        public struct gsInfo
        {
            public const int SIZE = 0xA0;
            public byte[] data;
            public int offset;

            public gsInfo(ref byte[] data, int offset)
            {
                this.data = data;
                this.offset = offset;
            }
        }

        public unsafe struct texAnimTexa
        {
            public short unk1            { get { fixed (byte* a = &data[offset+0x00]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x00]) (*(short*)a)=value; } }
            public short texIndex        { get { fixed (byte* a = &data[offset+0x02]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x02]) (*(short*)a) = value; } }
            public short frameStride     { get { fixed (byte* a = &data[offset+0x04]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x04]) (*(short*)a) = value; } }
            public short bitsPerPixel    { get { fixed (byte* a = &data[offset+0x06]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x06]) (*(short*)a) = value; } }
            public short minSlotIdx      { get { fixed (byte* a = &data[offset+0x08]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x08]) (*(short*)a) = value; } }
            public short maxSlotIdx      { get { fixed (byte* a = &data[offset+0x0A]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x0A]) (*(short*)a) = value; } }
            public short numAnims        { get { fixed (byte* a = &data[offset+0x0C]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x0C]) (*(short*)a) = value; } }
            public short numSprites      { get { fixed (byte* a = &data[offset+0x0E]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x0E]) (*(short*)a) = value; } }
            public short uOff            { get { fixed (byte* a = &data[offset+0x10]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x10]) (*(short*)a) = value; } }
            public short vOff            { get { fixed (byte* a = &data[offset+0x12]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x12]) (*(short*)a) = value; } }
            public short rrw             { get { fixed (byte* a = &data[offset+0x14]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x14]) (*(short*)a) = value; } }
            public short rrh             { get { fixed (byte* a = &data[offset+0x16]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x16]) (*(short*)a) = value; } }
            public int slotTableOffset   { get { fixed (byte* a = &data[offset+0x18]) return (*(int*)a);   } set { fixed (byte* a = &data[offset+0x18]) (*(int*)a) = value;   } }
            public int animTableOffset   { get { fixed (byte* a = &data[offset+0x1C]) return (*(int*)a);   } set { fixed (byte* a = &data[offset+0x1C]) (*(int*)a) = value;   } }
            public int spriteImageOffset { get { fixed (byte* a = &data[offset+0x20]) return (*(int*)a);   } set { fixed (byte* a = &data[offset+0x20]) (*(int*)a) = value;   } }
            public int defAnimIdx        { get { fixed (byte* a = &data[offset+0x24]) return (*(int*)a);   } set { fixed (byte* a = &data[offset+0x24]) (*(int*)a) = value;   } }

            public const int SIZE = 0x28;
            public byte[] data;
            public int offset;

            public texAnimTexa(ref byte[] data, int offset)
            {
                this.data = data;
                this.offset = offset;
            }
        }

        public unsafe struct texAnimFrameEntry
        {
            public short frameControl   { get { fixed (byte* a = &data[offset + 0x00]) return   (short)(((*(short*)a) & 0b0000000000001111) >> 0); } 
                                          set { fixed (byte* a = &data[offset + 0x00])        (*(short*)a) = (short)((*(short*)a & 0b1111111111110000) + ((value & 0b0000000000001111) << 0)); } }
            public short loopOffset     { get { fixed (byte* a = &data[offset + 0x00]) return   (short)(((*(short*)a) & 0b1111111111110000) >> 4); } 
                                          set { fixed (byte* a = &data[offset + 0x00])        (*(short*)a) = (short)((*(short*)a & 0b0000000000001111) + ((value & 0b0000111111111111) << 4)); } }
            public short minLen         { get { fixed (byte* a = &data[offset + 0x02]) return (*(short*)a); } 
                                          set { fixed (byte* a = &data[offset + 0x02])        (*(short*)a) = value; } }
            public short maxLen         { get { fixed (byte* a = &data[offset + 0x04]) return (*(short*)a); } 
                                          set { fixed (byte* a = &data[offset + 0x04])        (*(short*)a) = value; } }
            public short spriteIndex    { get { fixed (byte* a = &data[offset + 0x06]) return (*(short*)a); } 
                                          set { fixed (byte* a = &data[offset + 0x06])        (*(short*)a) = value; } }

            public const int SIZE = 0x08;
            public byte[] data;
            public int offset;

            public texAnimFrameEntry(ref byte[] data, int offset)
            {
                this.data = new byte[SIZE];
                this.offset = offset;
            }
        }

        public unsafe struct textAnimUvsc
        {
            public int   index  { get { fixed (byte* a = &data[offset + 0x00]) return (*(int*)a); } }
            public float uSpeed { get { fixed (byte* a = &data[offset + 0x04]) return (*(float*)a); } }
            public float vSpeed { get { fixed (byte* a = &data[offset + 0x08]) return (*(float*)a); } }

            const int SIZE = 0x0C;
            byte[] data;
            int offset;

            public textAnimUvsc(ref byte[] data, int offset)
            {
                this.data = data;
                this.offset = offset;
            }
        }

        public List<Texture>[] Textures = new List<Texture>[] {new List<Texture>(0)};
        public List<TexturePatch>[] Patches = new List<TexturePatch>[] { new List<TexturePatch>(0) };
        public List<textAnimUvsc>[] UVScrolls = new List<textAnimUvsc>[] { new List<textAnimUvsc>(0) };


        public TIM2(byte[] data)
        {
            if (data.Length == 0)
                return;

            int magic = BitConverter.ToInt32(data, 0x00);

            if (magic == -1)
            {
                int timsCount = BitConverter.ToInt32(data, 0x04);
                List<int> timOffsets = new List<int>(0);
                for (int i = 0; i < timsCount; i++)
                {
                    timOffsets.Add(BitConverter.ToInt32(data, 0x08 + i * sizeof(int)));
                }

                Textures = new List<Texture>[timsCount];
                Patches = new List<TexturePatch>[timsCount];
                UVScrolls = new List<textAnimUvsc>[timsCount];

                timOffsets.Add(data.Length);
                for (int i = 0; i < timsCount; i++)
                {
                    byte[] copy = new byte[timOffsets[i + 1] - timOffsets[i]];
                    Array.Copy(data, timOffsets[i], copy, 0, copy.Length);
                    TIM2 t = new TIM2(copy);
                    Textures[i] = t.Textures[0];
                    Patches[i] = t.Patches[0];
                    UVScrolls[i] = t.UVScrolls[0];
                }
                /* END */
            }
            else
            {
                int colorsCount = BitConverter.ToInt32(data, 0x04);
                int textureTransferCount = BitConverter.ToInt32(data, 0x08);
                int gsInfoCount = BitConverter.ToInt32(data, 0x0C);
                int offsetDataOffset = BitConverter.ToInt32(data, 0x10);
                int dataTransferOffset = BitConverter.ToInt32(data, 0x14);
                int gsInfoOffset = BitConverter.ToInt32(data, 0x18);
                int pictureOffset = BitConverter.ToInt32(data, 0x1C);
                int paletteOffset = BitConverter.ToInt32(data, 0x20);

                if (offsetDataOffset > data.Length) return;
                if (dataTransferOffset > data.Length) return;
                if (gsInfoOffset > data.Length) return;
                if (pictureOffset > data.Length) return;
                if (paletteOffset > data.Length) return;

                byte[] offsetData = new byte[gsInfoCount];
                    Array.Copy(data, offsetDataOffset, offsetData, 0, gsInfoCount);

                dataTransfer clutTransfer = new dataTransfer(ref data, dataTransferOffset);
                dataTransfer[] textureTransfer = new dataTransfer[textureTransferCount];
                    for (int i = 0; i < textureTransferCount; i++)
                        textureTransfer[i] = new dataTransfer(ref data, dataTransferOffset + dataTransfer.SIZE/*0x90*/ + i * dataTransfer.SIZE);
                
                gsInfo[] gsInfoList = new gsInfo[gsInfoCount];
                    for (int i = 0; i < gsInfoCount; i++)
                        gsInfoList[i] = new gsInfo(ref data, gsInfoOffset + i * gsInfo.SIZE/*0x0A*/);


                byte[] pictureData = new byte[paletteOffset - pictureOffset];
                    Array.Copy(data, pictureOffset, pictureData, 0, pictureData.Length);

                byte[] paletteData = new byte[4 * colorsCount];
                    Array.Copy(data, paletteOffset, paletteData, 0, paletteData.Length);


                EncodeDataTransfer(data, clutTransfer);
                for (int i = 0; i < offsetData.Length; i++)
                {
                    EncodeDataTransfer(data, textureTransfer[offsetData[i]]);
                    Textures[0].Add(DecodeDataTransfer(data, textureTransfer[offsetData[i]], gsInfoList[i]));
                }


                /* Texture Anims. */

                List<texAnimTexa> texas = new List<texAnimTexa>(0);
                List<textAnimUvsc> uvscs = new List<textAnimUvsc>(0);

                int textAnimsCursor = paletteOffset + paletteData.Length;
                while (textAnimsCursor + 4 <= data.Length &&
                    Encoding.ASCII.GetString(data, textAnimsCursor, 4) == "_DMY") /* Or else, equals _KN5 */
                {
                    textAnimsCursor += 0x08;
                    string magicTest = Encoding.ASCII.GetString(data, textAnimsCursor, 4);

                    switch (magicTest)
                    {
                        case "TEXA":
                            do
                            {
                                int length = BitConverter.ToInt32(data, textAnimsCursor + 0x04);
                                textAnimsCursor += 0x08;
                                texas.Add(new texAnimTexa(ref data, textAnimsCursor));
                                textAnimsCursor += length;
                            }
                            while (Encoding.ASCII.GetString(data, textAnimsCursor, 4) == magicTest);
                        break;
                        case "UVSC":
                            do
                            {
                                int length = BitConverter.ToInt32(data, textAnimsCursor + 0x04);
                                textAnimsCursor += 0x08;
                                uvscs.Add(new textAnimUvsc(ref data, textAnimsCursor));
                                textAnimsCursor += length;
                            }
                            while (Encoding.ASCII.GetString(data, textAnimsCursor, 4) == magicTest);
                        break;
                    }
                }


                foreach (texAnimTexa texa in texas)
                {
                    byte[] spriteImage = new byte[(texa.numSprites * texa.rrw * texa.rrh) / (Convert.ToString(byte.MaxValue, 2).Length / texa.bitsPerPixel)];
                    List<short> slotTableOffsets = new List<short>(0);
                    List<texAnimFrameEntry> texAnimFrameEntries = new List<texAnimFrameEntry>(0);

                    Array.Copy(data, texa.offset + texa.spriteImageOffset, spriteImage, 0, spriteImage.Length);
                    var slotTableOffsets_asByteArray = new byte[(texa.maxSlotIdx - texa.minSlotIdx + 1) * sizeof(short)];
                    Array.Copy(data, texa.offset + texa.animTableOffset, slotTableOffsets_asByteArray, 0, slotTableOffsets_asByteArray.Length);
                    for (int i = 0; i < slotTableOffsets_asByteArray.Length; i += sizeof(short))
                    {
                        short currTableOffset = BitConverter.ToInt16(slotTableOffsets_asByteArray, i);
                        slotTableOffsets.Add(currTableOffset);
                    }
                    var animTableOffsets_asByteArray = new byte[(texa.numAnims) * sizeof(int)];
                    Array.Copy(data, texa.offset + texa.animTableOffset, animTableOffsets_asByteArray, 0, animTableOffsets_asByteArray.Length);
                    for (int i = 0; i < animTableOffsets_asByteArray.Length; i += sizeof(int))
                    {
                        int currTableOffset = BitConverter.ToInt32(animTableOffsets_asByteArray, i);
                        texAnimFrameEntries.Add(new texAnimFrameEntry(ref data, texa.offset + currTableOffset));
                    }
                    Patches[0].Add(GetPatch(spriteImage, Textures[0], texa));
                }

                UVScrolls[0].AddRange(uvscs);
                /* END */
            }
        }

        public enum BlitTransmissionOrder
        {
            UpperLeftLowerRight = 0,
            LowerLeftUpperRight = 1,
            UpperRightLowerLeft = 2,
            LowerRightUpperLeft = 3
        }

        public const int DECODE_BUFFER_MIN_SIZE = 0x2000;
        public void EncodeDataTransfer(byte[] timBytes, dataTransfer dt)
        {
            //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=101
            /* BITBLTBUF */
            uint _____source =   BitConverter.ToUInt32(dt.data, dt.offset+0x20);
            uint destination =   BitConverter.ToUInt32(dt.data, dt.offset+0x24);
            
            uint sBufferPointer =        (_____source & 0b00000000000000000011111111111111) >> 0;
            uint sBufferWidth =          (_____source & 0b00000000001111110000000000000000) >> 16;
            uint sPixStorageMode =       (_____source & 0b00111111000000000000000000000000) >> 24;

            uint dBufferPointer =        (destination & 0b00000000000000000011111111111111) >> 0;
            uint dBufferWidth =          (destination & 0b00000000001111110000000000000000) >> 16;
            uint dPixStorageMode =       (destination & 0b00111111000000000000000000000000) >> 24;


            //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=133
            /* TRXPOS */
            _____source =       BitConverter.ToUInt32(dt.data, dt.offset + 0x30);
            destination =       BitConverter.ToUInt32(dt.data, dt.offset + 0x34);

            uint sUpperLeftX = (_____source & 0b00000000000000000000011111111111) >> 0;
            uint sUpperLeftY = (_____source & 0b00000111111111110000000000000000) >> 16;

            uint dUpperLeftX = (destination & 0b00000000000000000000011111111111) >> 0;
            uint dUpperLeftY = (destination & 0b00000111111111110000000000000000) >> 16;
            var DIR = (BlitTransmissionOrder)(
                              (destination & 0b00011000000000000000000000000000) >> 27);

            //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=134
            /* TRXREG */
            uint rrw = BitConverter.ToUInt32(dt.data, dt.offset + 0x40) & 0b00000000000000000000111111111111;
            uint rrh = BitConverter.ToUInt32(dt.data, dt.offset + 0x44) & 0b00000000000000000000111111111111;

            //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=113
            /* TRXDIR */
            uint xdir = BitConverter.ToUInt32(dt.data, dt.offset + 0x50) & 0b00000000000000000000000000000010;
            uint eop = BitConverter.ToUInt16(dt.data, dt.offset + 0x60);
            uint clutBufferTextureSize =  (eop & 0b00000000000000000111111111111111) * 0x10;
            uint dbh = clutBufferTextureSize / DECODE_BUFFER_MIN_SIZE / dBufferWidth;


            byte[] decodeBuffer = new byte[Math.Max(DECODE_BUFFER_MIN_SIZE, clutBufferTextureSize)];
            uint copySize = clutBufferTextureSize;
            if (dt.binOffset + copySize > timBytes.Length)
                copySize = (uint)(timBytes.Length - dt.binOffset);

            Array.Copy(timBytes, dt.binOffset, decodeBuffer, 0, copySize);


            byte[] binDec;
            switch (dPixStorageMode)
            {
                case 0:
                    binDec = OpenKh.Kh2.Ps2.Encode32(decodeBuffer, dBufferWidth, dbh);
                    break;
                case 19:
                    binDec = OpenKh.Kh2.Ps2.Encode8(decodeBuffer, dBufferWidth / 2, clutBufferTextureSize / DECODE_BUFFER_MIN_SIZE / (dBufferWidth / 2));
                    break;
                case 20:
                    binDec = OpenKh.Kh2.Ps2.Encode4(decodeBuffer, dBufferWidth / 2, clutBufferTextureSize / DECODE_BUFFER_MIN_SIZE / Math.Max(1, dBufferWidth / 2));
                    break;
                default:
                    throw new NotImplementedException("This destination pixel storage mode ("+ dPixStorageMode+") is not implemented yet.");
            }

            Array.Copy(binDec, 0, dataTransfer.gsBuffer, 256 * dBufferPointer, clutBufferTextureSize);

        }


        public Texture DecodeDataTransfer(byte[] timBytes, dataTransfer dt, gsInfo gsI)
        {
                //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=131
                /* TEXFLUSH */
            uint xdir = BitConverter.ToUInt32(gsI.data, gsI.offset + 0x20) & 0b00000000000000000000000000000010;
                //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=113
                /* MIPTBP1_1 */
            ulong mipmap1_context1 = BitConverter.ToUInt64(gsI.data, gsI.offset + 0x30);
                //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=113
                /* MIPTBP2_1 */
            ulong mipmap2_context1 = BitConverter.ToUInt64(gsI.data, gsI.offset + 0x40);
                //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=128

                /* TEX2_1 (STORAGE/CLUT) */
            uint pixelStorageInfo = BitConverter.ToUInt32(gsI.data, gsI.offset + 0x50);
            uint clutInfo = BitConverter.ToUInt32(gsI.data, gsI.offset + 0x54);

            uint pixStorageMode =(pixelStorageInfo & 0b00000011111100000000000000000000) >> 20;
            uint clutBufferOffset =      (clutInfo & 0b00000000000001111111111111100000) >> 5;
            uint clutPixStorageMode =    (clutInfo & 0b00000000011110000000000000000000) >> 19;
            uint clutStorageMode =       (clutInfo & 0b00000000100000000000000000000000) >> 23;
            uint clutEntryOffset =       (clutInfo & 0b00011111000000000000000000000000) >> 24;
            uint clutBufferLoadControl = (clutInfo & 0b11100000000000000000000000000000) >> 29;

                //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=62
                /* TEXT1_1 (LOD) */
            ulong LODInfo = BitConverter.ToUInt64(gsI.data, gsI.offset + 0x60);
            int calculationMethod =        (int)((LODInfo & 0b0000000000000000000000000000000000000000000000000000000000000001) >> 0);
            int maxMipLevel =              (int)((LODInfo & 0b0000000000000000000000000000000000000000000000000000000000011100) >> 2);
            var magFilter =     MagFilters[(int)((LODInfo & 0b0000000000000000000000000000000000000000000000000000000000100000) >> 5)];
            var minFilter =     MinFilters[(int)((LODInfo & 0b0000000000000000000000000000000000000000000000000000000111000000) >> 6)];
            int baseAddressSpec =          (int)((LODInfo & 0b0000000000000000000000000000000000000000000000000000001000000000) >> 9);
            int lValue =                   (int)((LODInfo & 0b0000000000000000000000000000000000000000000110000000000000000000) >> 19);
            int kValue =                   (int)((LODInfo & 0b0000000000000000000011111111111100000000000000000000000000000000) >> 32);


                //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=125
                /* TEXT0_1 (TEXTURE INFO) */
            ulong texInfo = BitConverter.ToUInt64(gsI.data, gsI.offset + 0x70);
            int texBasePointer =                         (int)((texInfo & 0b0000000000000000000000000000000000000000000000000011111111111111) >> 0);
            int texBufferWidth =                         (int)((texInfo & 0b0000000000000000000000000000000000000000000011111100000000000000) >> 14);
            var pixelStorageMode = (TexturePixelStorageFormat)((texInfo & 0b0000000000000000000000000000000000000011111100000000000000000000) >> 20);
            int textureWidth =                           (int)((texInfo & 0b0000000000000000000000000000000000111100000000000000000000000000) >> 26);
            int textureHeight =                          (int)((texInfo & 0b0000000000000000000000000000001111000000000000000000000000000000) >> 30);
            int textureColorComponent =                  (int)((texInfo & 0b0000000000000000000000000000010000000000000000000000000000000000) >> 34);
            int textureFunction =                        (int)((texInfo & 0b0000000000000000000000000001100000000000000000000000000000000000) >> 35);
            int textureClutBufferBasePointer =           (int)((texInfo & 0b0000000000000111111111111110000000000000000000000000000000000000) >> 37);
            int textureClutPixelStorageFormat =          (int)((texInfo & 0b0000000001111000000000000000000000000000000000000000000000000000) >> 51);
            int textureClutStorageMode =                 (int)((texInfo & 0b0000000010000000000000000000000000000000000000000000000000000000) >> 55);
            int textureClutEntryOffset =                 (int)((texInfo & 0b0001111100000000000000000000000000000000000000000000000000000000) >> 56);
            int textureClutBufferLoadControl =           (int)((texInfo & 0b1110000000000000000000000000000000000000000000000000000000000000) >> 61);


            //https://usermanual.wiki/Pdf/GSUsersManual.1012076781/view#page=102
            /* TEXTURE WRAP MODE */
            ulong textureWrapMode = BitConverter.ToUInt64(gsI.data, gsI.offset + 0x80);
            var wrapS = TextureWrapModes[((textureWrapMode & 0b0000000000000000000000000000000000000000000000000000000000000011) >> 0)];
            var wrapT = TextureWrapModes[((textureWrapMode & 0b0000000000000000000000000000000000000000000000000000000000001100) >> 2)];
            int minu =              (int)((textureWrapMode & 0b0000000000000000000000000000000000000000000000000011111111110000) >> 4);
            int maxu =              (int)((textureWrapMode & 0b0000000000000000000000000000000000000000111111111100000000000000) >> 14);
            int minv =              (int)((textureWrapMode & 0b0000000000000000000000000000001111111111000000000000000000000000) >> 24);
            int maxv =              (int)((textureWrapMode & 0b0000000000000000000011111111110000000000000000000000000000000000) >> 34);

            int sizeTextureBuffer = (1 << textureWidth) * (1 << textureHeight);
            byte[] textureBuffer = new byte[Math.Max(DECODE_BUFFER_MIN_SIZE, sizeTextureBuffer)]; // needs at least 8kb
            Array.Copy(dataTransfer.gsBuffer, 256 * texBasePointer, textureBuffer, 0, Math.Min(dataTransfer.gsBuffer.Length - 256 * texBasePointer, Math.Min(textureBuffer.Length, sizeTextureBuffer)));
            byte[] clutBuffer = new byte[DECODE_BUFFER_MIN_SIZE];
            Array.Copy(dataTransfer.gsBuffer, 256 * textureClutBufferBasePointer, clutBuffer, 0, clutBuffer.Length);

            Bitmap bmp = null;
            switch (pixelStorageMode)
            {
                case TexturePixelStorageFormat.PSMT8:
                    bmp = Decode8(textureBuffer, clutBuffer, texBufferWidth, 1 << textureWidth, 1 << textureHeight);
                break;
                case TexturePixelStorageFormat.bPSMT4:
                    bmp = Decode4Ps(textureBuffer, clutBuffer, texBufferWidth, 1 << textureWidth, 1 << textureHeight, textureClutEntryOffset);
                break;
            }
            Texture output = new Texture();
            output.Image = bmp;
            output.MagFilter = magFilter;
            output.MinFilter = minFilter;
            output.WrapS = wrapS;
            output.WrapT = wrapT;
            output.RepeatBounds(minu, maxu, minv, maxv);
            return output;
        }

        readonly static sbyte[] tbl = new sbyte[] { 0, 0, 6, 6, -2, -2, 4, 4, -4, -4, 2, 2, -6, -6, 0, 0, 16, 16, 22, 22, 14, 14, 20, 20, 12, 12, 18, 18, 10, 10, 16, 16, 32, 32, 38, 38, 30, 30, 36, 36, 28, 28, 34, 34, 26, 26, 32, 32, 48, 48, 54, 54, 46, 46, 52, 52, 44, 44, 50, 50, 42, 42, 48, 48, -48, -48, -42, -42, -50, -50, -44, -44, -52, -52, -46, -46, -54, -54, -48, -48, -32, -32, -26, -26, -34, -34, -28, -28, -36, -36, -30, -30, -38, -38, -32, -32, -16, -16, -10, -10, -18, -18, -12, -12, -20, -20, -14, -14, -22, -22, -16, -16, 0, 0, 6, 6, -2, -2, 4, 4, -4, -4, 2, 2, -6, -6, 0, 0, };

        static int repl(int t)
        {
            return (t & 0x80) | ((t & 0x7F) + tbl[t & 0x7F]);
        }

        public static Bitmap Decode8(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
        {
            Bitmap bmp = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
            tbw /= 2;
            byte[] bin = OpenKh.Kh2.Ps2.Decode8(picbin, tbw, Math.Max(1, picbin.Length / DECODE_BUFFER_MIN_SIZE / tbw));
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            int buffSize = bd.Stride * bd.Height;
            System.Runtime.InteropServices.Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
            bmp.UnlockBits(bd);

            ColorPalette pals = bmp.Palette;
            int psi = 0;

            byte[] palb2 = new byte[1024];
            for (int t = 0; t < 256; t++)
            {
                int toi = repl(t);
                Array.Copy(palbin, 4 * t + 0, palb2, 4 * toi + 0, 4);
            }
            Array.Copy(palb2, 0, palbin, 0, 1024);

            for (int pi = 0; pi < 256; pi++)
            {
                pals.Entries[pi] = Color.FromArgb(
                    palbin[psi + 4 * pi + 3],
                    palbin[psi + 4 * pi + 0],
                    palbin[psi + 4 * pi + 1],
                    palbin[psi + 4 * pi + 2]);
            }
            bmp.Palette = pals;
            return bmp;
        }

        public static Bitmap Decode4Ps(byte[] picbin, byte[] palbin, int tbw, int cx, int cy, int csa)
        {
            Bitmap bmp = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
            tbw = Math.Max(1, tbw / 2);

            byte[] bin = OpenKh.Kh2.Ps2.Decode4(picbin, tbw, Math.Max(1, picbin.Length / 8192 / tbw));
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

            int buffSize = bd.Stride * bd.Height;
            System.Runtime.InteropServices.Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
            bmp.UnlockBits(bd);

            ColorPalette pals = bmp.Palette;
            int psi = 64 * csa;
            byte[] palb2 = new byte[1024];
            for (int t = 0; t < 256; t++)
            {
                int toi = repl(t);
                Array.Copy(palbin, 4 * t + 0, palb2, 4 * toi + 0, 4);
            }

            for (int pi = 0; pi < 16; pi++)
            {
                pals.Entries[pi] = Color.FromArgb(
                    palb2[psi + 4 * pi + 3],
                    palb2[psi + 4 * pi + 0],
                    palb2[psi + 4 * pi + 1],
                    palb2[psi + 4 * pi + 2]
                    );
            }
            bmp.Palette = pals;
            return bmp;
        }

        public GL.TextureWrapMode[] TextureWrapModes = new GL.TextureWrapMode[]
        {
            GL.TextureWrapMode.Repeat,
            GL.TextureWrapMode.Clamp,
            GL.TextureWrapMode.Clamp,
            GL.TextureWrapMode.Repeat
        };

        public class Texture
        {
            public Bitmap Image;
            public GL.TextureWrapMode WrapS;
            public GL.TextureWrapMode WrapT;
            public GL.TextureMagFilter MagFilter;
            public GL.TextureMinFilter MinFilter;

            public void RepeatBounds(int minu, int maxu, int minv, int maxv)
            {
                int left =   Math.Min(minu,maxu);
                int right =  Math.Max(minu, maxu);
                int top =    Math.Min(minv, maxv);
                int bottom = Math.Max(minv, maxv);

                Rectangle rect = new System.Drawing.Rectangle(left, top, right - left + 1, bottom - top + 1);

                Bitmap output = new Bitmap(this.Image.Width, this.Image.Height);
                Graphics gr = Graphics.FromImage(output);
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetWrapMode(WrapMode.Tile);

                TextureBrush myTBrush = new TextureBrush(this.Image, rect, imageAttr);
                myTBrush.TranslateTransform((int)rect.X, (int)rect.Y);
                
                if (WrapS== GL.TextureWrapMode.Repeat && WrapT != GL.TextureWrapMode.Repeat)
                {
                    gr.FillRectangle(myTBrush, rect.X, rect.Y, this.Image.Width, rect.Height);
                    gr.FillRectangle(myTBrush, rect.X - this.Image.Width, rect.Y, this.Image.Width, rect.Height);
                }
                else if (WrapS != GL.TextureWrapMode.Repeat && WrapT == GL.TextureWrapMode.Repeat)
                {
                    gr.FillRectangle(myTBrush, rect.X, rect.Y, rect.Width, this.Image.Height);
                    gr.FillRectangle(myTBrush, rect.X, rect.Y - this.Image.Height, rect.Width, this.Image.Height);
                }
                else if (WrapS == GL.TextureWrapMode.Repeat && WrapT == GL.TextureWrapMode.Repeat)
                {
                    gr.FillRectangle(myTBrush, rect.X, rect.Y, this.Image.Width, this.Image.Height);
                    gr.FillRectangle(myTBrush, rect.X - this.Image.Width, rect.Y, this.Image.Width, this.Image.Height);
                    gr.FillRectangle(myTBrush, rect.X + this.Image.Width, rect.Y, this.Image.Width, this.Image.Height);

                    gr.FillRectangle(myTBrush, rect.X, rect.Y - this.Image.Height, this.Image.Width, this.Image.Height);
                    gr.FillRectangle(myTBrush, rect.X, rect.Y + this.Image.Height, this.Image.Width, this.Image.Height);
                }
                else
                {
                    gr.DrawImage(this.Image, rect, rect, GraphicsUnit.Pixel);
                    if (WrapS == GL.TextureWrapMode.Clamp)
                    {
                        gr.DrawImage(this.Image, new RectangleF(0, rect.Y, rect.X, rect.Height), new RectangleF(rect.X, rect.Y, 0.1f, rect.Height), GraphicsUnit.Pixel);
                        gr.DrawImage(this.Image, new RectangleF(rect.X + rect.Width, rect.Y, this.Image.Width - rect.X - rect.Width, rect.Height), new RectangleF(rect.X + rect.Width - 1, rect.Y, 0.1f, rect.Height), GraphicsUnit.Pixel);
                    }
                    if (WrapT == GL.TextureWrapMode.Clamp)
                    {
                        gr.DrawImage(this.Image, new RectangleF(rect.X, 0, rect.Width, rect.Y), new RectangleF(rect.X, rect.Y, rect.Width, 0.1f), GraphicsUnit.Pixel);
                        gr.DrawImage(this.Image, new RectangleF(rect.X, rect.Y + rect.Height, rect.Width, this.Image.Height - rect.Y - rect.Height), new RectangleF(rect.X, rect.Y + rect.Height - 1, rect.Width, 0.1f), GraphicsUnit.Pixel);
                    }
                    gr.FillRectangle(new SolidBrush(output.GetPixel(rect.X, rect.Y)), 0, 0, rect.X, rect.Y);
                    gr.FillRectangle(new SolidBrush(output.GetPixel(rect.X + rect.Width - 1, rect.Y)), rect.X + rect.Width, 0, this.Image.Width - rect.X - rect.Width, rect.Y);
                    gr.FillRectangle(new SolidBrush(output.GetPixel(rect.X, rect.Y+rect.Height-1)), 0, rect.Y+rect.Height, rect.X, this.Image.Height-rect.Y-rect.Height);
                    gr.FillRectangle(new SolidBrush(output.GetPixel(rect.X + rect.Width - 1, rect.Y + rect.Height - 1)), rect.X + rect.Width, rect.Y + rect.Height, this.Image.Width - rect.X - rect.Width, this.Image.Height - rect.Y - rect.Height);
                }

                output.Palette = this.Image.Palette;
                this.Image = output;
            }
        }



        public class TexturePatch
        {
            public Bitmap Image;
            public int PatchedTextureIndex;
            public Rectangle DestinationRectangle;
            public int SpriteCount;
        }

        public static TexturePatch GetPatch(byte[] pixels, List<Texture> textures, texAnimTexa texa)
        {
            Bitmap bmp = new Bitmap(texa.rrw, texa.rrh * texa.numSprites, texa.bitsPerPixel == 8 ? PixelFormat.Format8bppIndexed : PixelFormat.Format4bppIndexed);
            

            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

            int buffSize = bd.Stride * bd.Height;
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bd.Scan0, buffSize);
            bmp.UnlockBits(bd);

            bmp.Palette = textures[texa.texIndex].Image.Palette;

            TexturePatch patch = new TexturePatch();
            patch.Image = bmp;
            patch.PatchedTextureIndex = texa.texIndex;
            patch.DestinationRectangle = new Rectangle(texa.uOff, texa.vOff, texa.rrw, texa.rrh);
            patch.SpriteCount = texa.numSprites;
            return patch;
        }
    }
}
