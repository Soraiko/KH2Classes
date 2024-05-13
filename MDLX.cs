#define keepVifDataPosition
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using BC = System.BitConverter;
using static dbkg.MDLX.objectModelPartEntry;
using System.Drawing;
using System.Windows.Forms;

namespace dbkg
{
    public class MDLX:BAR
    {
        public unsafe struct objectModelPartEntry
        {
            public unsafe struct SKLRenderAttribute
            {
                public const int SIZE = 0x04;
                byte[] data;
                int offset;

                const uint v00 = 0b00000000000000000000000000000001;
                const uint v01 = 0b00000000000000000000000000000010;
                const uint v02 = 0b00000000000000000000000000000100;
                const uint v03 = 0b00000000000000000000000000111000;
                const uint v04 = 0b00000000000000000000011111000000;
                const uint v05 = 0b00000000000000001111100000000000;
                const uint v06 = 0b00000000000111110000000000000000;
                const uint v07 = 0b00000000001000000000000000000000;
                const uint v08 = 0b00000111110000000000000000000000;
                const uint v09 = 0b00001000000000000000000000000000;
                const uint v10 = 0b00010000000000000000000000000000;
                const uint v11 = 0b00100000000000000000000000000000;
                const uint v12 = 0b01000000000000000000000000000000;
                const uint v13 = 0b10000000000000000000000000000000;

                public bool drawAlphaPhase	{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v00) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v00) + (((value?1:0) << 0) & v00));  } }
                public bool alpha			{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v01) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v01) + (((value?1:0) << 1) & v01));  } }
                public bool multi			{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v02) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v02) + (((value?1:0) << 2) & v02));  } }
                public uint reserved1		{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v03) >> 3; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v03) + (((value)     << 3) & v03));  } }
                public uint part			{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v04) >> 6; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v04) + (((value)     << 6) & v04));  } }
                public uint mesh			{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v05) >> 11;} set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v05) + (((value)     << 11) & v05)); } }
                public uint priority		{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v06) >> 16;} set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v06) + (((value)     << 16) & v06)); } }
                public bool alphaEx			{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v07) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v07) + (((value?1:0) << 21) & v07)); } }
                public uint uvScroll		{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v08) >> 22;} set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v08) + (((value)     << 22) & v08)); } }
                public bool alphaAdd		{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v09) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v09) + (((value?1:0) << 27) & v09)); } }
                public bool alphaSub		{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v10) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v10) + (((value?1:0) << 28) & v10)); } }
                public bool specular		{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v11) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v11) + (((value?1:0) << 29) & v11)); } }
                public bool noLight			{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v12) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v12) + (((value?1:0) << 30) & v12)); } }
                public bool reserved2		{ get { fixed (byte* a = &data[offset]) return (*(uint*)a & v13) >  0; } set { fixed (byte* a = &data[offset]) *(uint*)a = (uint)((*(uint*)a & ~v13) + (((value?1:0) << 31) & v13)); } }
            
                public SKLRenderAttribute(ref byte[] data, int offset)
                {
                    this.data = data;
                    this.offset = offset;
                }
            }
            
            public unsafe struct SKLBone
            {
                const int SIZE = 0x120;
                public int offset;
                public byte[] data;

                public Vector4 bbox_min     { get { fixed (byte* a = &data[offset+0x00]) return new Vector4((*(float*)(a + 0 * sizeof(float))), (*(float*)(a + 1 * sizeof(float))), (*(float*)(a + 2 * sizeof(float))), (*(float*)(a + 3 * sizeof(float)))); } set { fixed (byte* a = &data[offset+0x00]) { (*(float*)(a + 0 * sizeof(float))) = value.X; (*(float*)(a + 1 * sizeof(float))) = value.Y; (*(float*)(a + 2 * sizeof(float))) = value.Z; (*(float*)(a + 3 * sizeof(float))) = value.W; } }}
                public Vector4 bbox_max     { get { fixed (byte* a = &data[offset+0x10]) return new Vector4((*(float*)(a + 0 * sizeof(float))), (*(float*)(a + 1 * sizeof(float))), (*(float*)(a + 2 * sizeof(float))), (*(float*)(a + 3 * sizeof(float)))); } set { fixed (byte* a = &data[offset+0x10]) { (*(float*)(a + 0 * sizeof(float))) = value.X; (*(float*)(a + 1 * sizeof(float))) = value.Y; (*(float*)(a + 2 * sizeof(float))) = value.Z; (*(float*)(a + 3 * sizeof(float))) = value.W; } }}
                public Vector4 ik_bone_bias { get { fixed (byte* a = &data[offset+0x20]) return new Vector4((*(float*)(a + 0 * sizeof(float))), (*(float*)(a + 1 * sizeof(float))), (*(float*)(a + 2 * sizeof(float))), (*(float*)(a + 3 * sizeof(float)))); } set { fixed (byte* a = &data[offset+0x20]) { (*(float*)(a + 0 * sizeof(float))) = value.X; (*(float*)(a + 1 * sizeof(float))) = value.Y; (*(float*)(a + 2 * sizeof(float))) = value.Z; (*(float*)(a + 3 * sizeof(float))) = value.W; } }}
                                      
                int PART_LF_HEAD        { get { fixed (byte* a = &data[offset+0x030]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x030])        (*(int*)a)=value; } }
                int PART_RF_HEAD        { get { fixed (byte* a = &data[offset+0x034]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x034])        (*(int*)a)=value; } }
                int PART_LB_HEAD        { get { fixed (byte* a = &data[offset+0x038]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x038])        (*(int*)a)=value; } }
                int PART_RB_HEAD        { get { fixed (byte* a = &data[offset+0x03C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x03C])        (*(int*)a)=value; } }
                int PART_LF_NECK        { get { fixed (byte* a = &data[offset+0x040]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x040])        (*(int*)a)=value; } }
                int PART_RF_NECK        { get { fixed (byte* a = &data[offset+0x044]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x044])        (*(int*)a)=value; } }
                int PART_LB_NECK        { get { fixed (byte* a = &data[offset+0x048]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x048])        (*(int*)a)=value; } }
                int PART_RB_NECK        { get { fixed (byte* a = &data[offset+0x04C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x04C])        (*(int*)a)=value; } }
                int PART_LF_CHEST       { get { fixed (byte* a = &data[offset+0x050]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x050])        (*(int*)a)=value; } }
                int PART_RF_CHEST       { get { fixed (byte* a = &data[offset+0x054]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x054])        (*(int*)a)=value; } }
                int PART_LB_CHEST       { get { fixed (byte* a = &data[offset+0x058]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x058])        (*(int*)a)=value; } }
                int PART_RB_CHEST       { get { fixed (byte* a = &data[offset+0x05C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x05C])        (*(int*)a)=value; } }
                int PART_LF_HIP         { get { fixed (byte* a = &data[offset+0x060]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x060])        (*(int*)a)=value; } }
                int PART_RF_HIP         { get { fixed (byte* a = &data[offset+0x064]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x064])        (*(int*)a)=value; } }
                int PART_LB_HIP         { get { fixed (byte* a = &data[offset+0x068]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x068])        (*(int*)a)=value; } }
                int PART_RB_HIP         { get { fixed (byte* a = &data[offset+0x06C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x06C])        (*(int*)a)=value; } }
                int PART_LF_COLLAR      { get { fixed (byte* a = &data[offset+0x070]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x070])        (*(int*)a)=value; } }
                int PART_RF_COLLAR      { get { fixed (byte* a = &data[offset+0x074]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x074])        (*(int*)a)=value; } }
                int PART_LB_COLLAR      { get { fixed (byte* a = &data[offset+0x078]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x078])        (*(int*)a)=value; } }
                int PART_RB_COLLAR      { get { fixed (byte* a = &data[offset+0x07C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x07C])        (*(int*)a)=value; } }
                int PART_LF_UPARM       { get { fixed (byte* a = &data[offset+0x080]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x080])        (*(int*)a)=value; } }
                int PART_RF_UPARM       { get { fixed (byte* a = &data[offset+0x084]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x084])        (*(int*)a)=value; } }
                int PART_LB_UPARM       { get { fixed (byte* a = &data[offset+0x088]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x088])        (*(int*)a)=value; } }
                int PART_RB_UPARM       { get { fixed (byte* a = &data[offset+0x08C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x08C])        (*(int*)a)=value; } }
                int PART_LF_FOARM       { get { fixed (byte* a = &data[offset+0x090]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x090])        (*(int*)a)=value; } }
                int PART_RF_FOARM       { get { fixed (byte* a = &data[offset+0x094]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x094])        (*(int*)a)=value; } }
                int PART_LB_FOARM       { get { fixed (byte* a = &data[offset+0x098]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x098])        (*(int*)a)=value; } }
                int PART_RB_FOARM       { get { fixed (byte* a = &data[offset+0x09C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x09C])        (*(int*)a)=value; } }
                int PART_LF_HAND        { get { fixed (byte* a = &data[offset+0x0A0]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0A0])        (*(int*)a)=value; } }
                int PART_RF_HAND        { get { fixed (byte* a = &data[offset+0x0A4]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0A4])        (*(int*)a)=value; } }
                int PART_LB_HAND        { get { fixed (byte* a = &data[offset+0x0A8]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0A8])        (*(int*)a)=value; } }
                int PART_RB_HAND        { get { fixed (byte* a = &data[offset+0x0AC]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0AC])        (*(int*)a)=value; } }
                int PART_LF_FEMUR       { get { fixed (byte* a = &data[offset+0x0B0]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0B0])        (*(int*)a)=value; } }
                int PART_RF_FEMUR       { get { fixed (byte* a = &data[offset+0x0B4]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0B4])        (*(int*)a)=value; } }
                int PART_LB_FEMUR       { get { fixed (byte* a = &data[offset+0x0B8]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0B8])        (*(int*)a)=value; } }
                int PART_RB_FEMUR       { get { fixed (byte* a = &data[offset+0x0BC]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0BC])        (*(int*)a)=value; } }
                int PART_LF_TIBIA       { get { fixed (byte* a = &data[offset+0x0C0]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0C0])        (*(int*)a)=value; } }
                int PART_RF_TIBIA       { get { fixed (byte* a = &data[offset+0x0C4]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0C4])        (*(int*)a)=value; } }
                int PART_LB_TIBIA       { get { fixed (byte* a = &data[offset+0x0C8]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0C8])        (*(int*)a)=value; } }
                int PART_RB_TIBIA       { get { fixed (byte* a = &data[offset+0x0CC]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0CC])        (*(int*)a)=value; } }
                int PART_LF_FOOT        { get { fixed (byte* a = &data[offset+0x0D0]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0D0])        (*(int*)a)=value; } }
                int PART_RF_FOOT        { get { fixed (byte* a = &data[offset+0x0D4]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0D4])        (*(int*)a)=value; } }
                int PART_LB_FOOT        { get { fixed (byte* a = &data[offset+0x0D8]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0D8])        (*(int*)a)=value; } }
                int PART_RB_FOOT        { get { fixed (byte* a = &data[offset+0x0DC]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0DC])        (*(int*)a)=value; } }
                int PART_LF_TOES        { get { fixed (byte* a = &data[offset+0x0E0]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0E0])        (*(int*)a)=value; } }
                int PART_RF_TOES        { get { fixed (byte* a = &data[offset+0x0E4]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0E4])        (*(int*)a)=value; } }
                int PART_LB_TOES        { get { fixed (byte* a = &data[offset+0x0E8]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0E8])        (*(int*)a)=value; } }
                int PART_RB_TOES        { get { fixed (byte* a = &data[offset+0x0EC]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0EC])        (*(int*)a)=value; } }
                int PART_WEAPON_L_LINK  { get { fixed (byte* a = &data[offset+0x0F0]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0F0])        (*(int*)a)=value; } }
                int PART_WEAPON_L       { get { fixed (byte* a = &data[offset+0x0F4]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0F4])        (*(int*)a)=value; } }
                int PART_WEAPON_R_LINK  { get { fixed (byte* a = &data[offset+0x0F8]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0F8])        (*(int*)a)=value; } }
                int PART_WEAPON_R       { get { fixed (byte* a = &data[offset+0x0FC]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x0FC])        (*(int*)a)=value; } }
                int PART_SPECIAL0       { get { fixed (byte* a = &data[offset+0x100]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x100])        (*(int*)a)=value; } }
                int PART_SPECIAL1       { get { fixed (byte* a = &data[offset+0x104]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x104])        (*(int*)a)=value; } }
                int PART_SPECIAL2       { get { fixed (byte* a = &data[offset+0x108]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x108])        (*(int*)a)=value; } }
                int PART_SPECIAL3       { get { fixed (byte* a = &data[offset+0x10C]) return (*(int*)a); } set { fixed (byte* a = &data[offset+0x10C])        (*(int*)a)=value; } }
            
                public Vector4 distanceFromSkeleton { get { fixed (byte* a = &data[offset+0x110]) return new Vector4((*(float*)(a + 0 * sizeof(float))), (*(float*)(a + 1 * sizeof(float))), (*(float*)(a + 2 * sizeof(float))), (*(float*)(a + 3 * sizeof(float)))); } set { fixed (byte* a = &data[offset+0x110]) { (*(float*)(a + 0 * sizeof(float))) = value.X; (*(float*)(a + 1 * sizeof(float))) = value.Y; (*(float*)(a + 2 * sizeof(float))) = value.Z; (*(float*)(a + 3 * sizeof(float))) = value.W; } }}
            
                public SKLBone(ref byte[] data, int offset)
                {
                    this.data = data;
                    this.offset = offset;
                }
            }
            
            public unsafe struct SKLHRC
            {
                public const int SIZE = 0x40;
                public int index;
                public int offset;
                public byte[] data;

                public short id             { get { fixed (byte* a = &data[offset+0x00]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x00]) (*(short*)a)=value; } }
                public short sibling_id     { get { fixed (byte* a = &data[offset+0x02]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x02]) (*(short*)a)=value; }}
                public short parent_id      { get { fixed (byte* a = &data[offset+0x04]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x04]) (*(short*)a)=value; }}
                public short child_id       { get { fixed (byte* a = &data[offset+0x06]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x06]) (*(short*)a)=value; }}
                public int reserved         { get { fixed (byte* a = &data[offset+0x08]) return (*(int*)a);   } set { fixed (byte* a = &data[offset+0x08]) (*(int*)a)=value; } }

                const uint v00 = 0b00000000000000000000000000000001;
                const uint v01 = 0b00000000000000000000000000000010;
                const uint v02 = 0b11111111111111111111111111111100;

                public bool no_envelope   { get { fixed (byte* a = &data[offset+0x0C]) return    (*(int*)a & v00) > 0; } set { fixed (byte* a = &data[offset+0x0C]) *(uint*)a = (uint)((*a & ~v00)+(((value?1:0)<<0) & v00)); } }
                public bool not_joint     { get { fixed (byte* a = &data[offset+0x0C]) return    (*(int*)a & v01) > 0; } set { fixed (byte* a = &data[offset+0x0C]) *(uint*)a = (uint)((*a & ~v01)+(((value?1:0)<<1) & v01)); } }
                public uint reserved2     { get { fixed (byte* a = &data[offset+0x0C]) return ((*(uint*)a) & v02) >>2; } set { fixed (byte* a = &data[offset+0x0C]) *(uint*)a = (uint)((*a & ~v02)+(((value    )<<2) & v02)); } }
            
                public Vector4 scale { get { fixed (byte* a = &data[offset+0x10]) return new Vector4((*(float*)(a + 0 * sizeof(float))), (*(float*)(a + 1 * sizeof(float))), (*(float*)(a + 2 * sizeof(float))), (*(float*)(a + 3 * sizeof(float)))); } set { fixed (byte* a = &data[offset+0x10]) { (*(float*)(a + 0 * sizeof(float))) = value.X; (*(float*)(a + 1 * sizeof(float))) = value.Y; (*(float*)(a + 2 * sizeof(float))) = value.Z; (*(float*)(a + 3 * sizeof(float))) = value.W; } }}
                public Vector4 rot   { get { fixed (byte* a = &data[offset+0x20]) return new Vector4((*(float*)(a + 0 * sizeof(float))), (*(float*)(a + 1 * sizeof(float))), (*(float*)(a + 2 * sizeof(float))), (*(float*)(a + 3 * sizeof(float)))); } set { fixed (byte* a = &data[offset+0x20]) { (*(float*)(a + 0 * sizeof(float))) = value.X; (*(float*)(a + 1 * sizeof(float))) = value.Y; (*(float*)(a + 2 * sizeof(float))) = value.Z; (*(float*)(a + 3 * sizeof(float))) = value.W; } }}
                public Vector4 trans { get { fixed (byte* a = &data[offset+0x30]) return new Vector4((*(float*)(a + 0 * sizeof(float))), (*(float*)(a + 1 * sizeof(float))), (*(float*)(a + 2 * sizeof(float))), (*(float*)(a + 3 * sizeof(float)))); } set { fixed (byte* a = &data[offset+0x30]) { (*(float*)(a + 0 * sizeof(float))) = value.X; (*(float*)(a + 1 * sizeof(float))) = value.Y; (*(float*)(a + 2 * sizeof(float))) = value.Z; (*(float*)(a + 3 * sizeof(float))) = value.W; } }}
                public Matrix4 Matrix;

                public SKLHRC(int firstEntryOffset, int entryIndex, ref byte[] entryBytes)
                {
                    this.index = entryIndex;
                    this.data = entryBytes;
                    this.offset = firstEntryOffset + entryIndex * SIZE;
                    this.Matrix = Matrix4.Identity;
                }
                public new string ToString()
                {
                    return this.Matrix.ToString();
                }
            }

            public const int SIZE = 0x20;
            public int index;
            public int offset;
            public byte[] data;

            public SKLRenderAttribute attribute;
            public short textureIndex { get { fixed (byte* a = &data[offset + 0x04]) return (*(short*)a);  } set { fixed (byte* a = &data[offset + 0x04]) (*(short*)a)=value;} }
            public int polygonNum     { get { fixed (byte* a = &data[offset + 0x08]) return (*(int*)a);  } set { fixed (byte* a = &data[offset + 0x08]) (*(int*)a)=value;} }
            public short hasVB        { get { fixed (byte* a = &data[offset + 0x0C]) return (*(short*)a);} set { fixed (byte* a = &data[offset + 0x0C]) (*(short*)a)=value;} }
            public short alt          { get { fixed (byte* a = &data[offset + 0x0E]) return (*(short*)a);} set { fixed (byte* a = &data[offset + 0x0E]) (*(short*)a)=value;} }
            public int offDmaSrcChain { get { fixed (byte* a = &data[offset + 0x10]) return (*(int*)a);  } set { fixed (byte* a = &data[offset + 0x10]) (*(int*)a)=value; }}
            public int offMatrixRef   { get { fixed (byte* a = &data[offset + 0x14]) return (*(int*)a);  } set { fixed (byte* a = &data[offset + 0x14]) (*(int*)a)=value; }}
            public int totalQwc       { get { fixed (byte* a = &data[offset + 0x18]) return (*(int*)a);  } set { fixed (byte* a = &data[offset + 0x18]) (*(int*)a)=value; }}
            public int unk5           { get { fixed (byte* a = &data[offset + 0x1C]) return (*(int*)a);  } set { fixed (byte* a = &data[offset + 0x1C]) (*(int*)a)=value; }}


            public objectModelPartEntry(int firstEntryOffset, int entryIndex, ref byte[] entryBytes)
            {
                this.offset = firstEntryOffset + SIZE * entryIndex;
                this.index = entryIndex;
                this.data = entryBytes;
                this.attribute = new SKLRenderAttribute(ref this.data, this.offset + 0x00);
            }
        }


        public unsafe struct backgroundModelPartEntry
        {
            public const int SIZE = 0x10;
            public int index;
            public int offset;
            public byte[] data;

            public int firstDmaTagOffset { get { fixed (byte* a = &data[offset+0x00]) return (*(int*)a);   } set { fixed (byte* a = &data[offset+0x00]) (*(int*)a)=value; } }
            public short textureIndex    { get { fixed (byte* a = &data[offset+0x04]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x04]) (*(short*)a)=value; } }
            public short renderingGroup  { get { fixed (byte* a = &data[offset+0x08]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x08]) (*(short*)a)=value; } }
            public short transparentFlag { get { fixed (byte* a = &data[offset+0x0A]) return (*(short*)a); } set { fixed (byte* a = &data[offset+0x0A]) (*(short*)a)=value; } }
            
            const byte v00 = 0b00000001;
            const byte v01 = 0b00000010;
            const byte v02 = 0b00111100;
            const byte v03 = 0b11000000;
            public bool unk1             { get { fixed (byte* a = &data[offset+0x0C]) return (*a & v00) > 0; } set { fixed (byte* a = &data[offset+0x0C]) *a = (byte)((*a&~v00) + (((value?1:0)<<0)&v00)); } }
            public bool enableUvsc       { get { fixed (byte* a = &data[offset+0x0C]) return (*a & v01) > 0; } set { fixed (byte* a = &data[offset+0x0C]) *a = (byte)((*a&~v01) + (((value?1:0)<<1)&v01)); } }
            public int uvscIdx           { get { fixed (byte* a = &data[offset+0x0C]) return (*a & v02)>> 2; } set { fixed (byte* a = &data[offset+0x0C]) *a = (byte)((*a&~v02) + (((value    )<<2)&v02)); } }
            public int unk2              { get { fixed (byte* a = &data[offset+0x0C]) return (*a & v03)>> 6; } set { fixed (byte* a = &data[offset+0x0C]) *a = (byte)((*a&~v03) + (((value    )<<6)&v03)); } }
            
            public byte vd               { get { fixed (byte* a = &data[offset+0x0D]) return (*a); } set { fixed (byte* a = &data[offset+0x0D]) (*a)=value; } }
            public byte ve               { get { fixed (byte* a = &data[offset+0x0E]) return (*a); } set { fixed (byte* a = &data[offset+0x0E]) (*a)=value; } }
            public byte vf               { get { fixed (byte* a = &data[offset+0x0F]) return (*a); } set { fixed (byte* a = &data[offset+0x0F]) (*a)=value; } }

            public backgroundModelPartEntry(int firstEntryOffset, int entryIndex, ref byte[] entryBytes)
            {
                this.index = entryIndex;
                this.data = entryBytes;
                this.offset = firstEntryOffset + entryIndex * SIZE;
            }
        }

        public unsafe struct VifCode
        {
            public new string ToString()
            {
                return this.Command.ToString() + " "+BC.ToString(data, offset,4);
            }

            public const int SIZE = 0x04;
            public int offset;
            public byte[] data;
            
            const uint v00 = 0b00000000000000001111111111111111;
            const uint v01 = 0b00000000111111110000000000000000;
            public const byte usn =  0b01000000;
            const uint v02 = 0b11111111000000000000000000000000;

            public ushort imm     { get { fixed (byte* a = &data[offset+0x00]) return (*(ushort*)a);  } set { fixed (byte* a = &data[offset+0x00]) *(ushort*)a = value; } }
            public byte num             { get { return            data[offset+0x02]; } set { data[offset+0x02] =       value; } }
            public byte cmd             { get { return            data[offset+0x03]; } set { data[offset+0x03] =       value; } }
            public CMD Command
            {
                get
                {
                    return (CMD)Math.Min((int)CMD.UNPACK, this.cmd & 0b01111111);
                }
            }
            public enum CMD
            {
                NOP = 0,
                STCYCL = 1,
                OFFSET = 2,
                BASE = 3,
                ITOP = 4,
                STMOD = 5,
                MSKPATH3 = 6,
                MARK = 7,
                FLUSHE = 16,
                FLUSH = 17,
                FLUSHA = 19,
                MSCAL = 20,
                MSCNT = 23,
                MSCALF = 21,
                STMASK = 32,
                STROW = 48,
                STCOL = 49,
                MPG = 74,
                DIRECT = 80,
                DIRECTHL = 81,
                UNPACK = 96
            };

            public VifCode(ref byte[] data, int offset)
            {
                this.data = data;
                this.offset = offset;
            }
        }

        public unsafe struct sourceChainDmaTag
        {
            public enum ID
            {
                REFE = 0,
                CNT = 1,
                NEXT = 2,
                REF = 3,
                REFS = 4,
                CALL = 5,
                RET = 6,
                END = 7
            };

            public enum PC
            {
                NOTHING = 0,
                RESERVED = 1,
                DISABLED = 2,
                ENABLED = 3
            };

            public const int SIZE = 0x10;
            public int offset;
            public byte[] data;
            
            const uint v00 = 0b00000000000000001111111111111111;
            const uint v01 = 0b00000011111111110000000000000000;
            const uint v02 = 0b00001100000000000000000000000000;
            const uint v03 = 0b01110000000000000000000000000000;
            const uint v04 = 0b10000000000000000000000000000000;

            const uint v05 = 0b01111111111111111111111111111111;
            const uint v06 = 0b10000000000000000000000000000000;

            public uint qwc               { get { fixed (byte* a = &data[offset+0x00]) return ((*(uint*)a) & v00) >>0;  } set { fixed (byte* a = &data[offset+0x00]) *(uint*)a = (uint)((*a & ~v00)+(((value    )<<0 ) & v00)); } }
            public uint pad               { get { fixed (byte* a = &data[offset+0x00]) return ((*(uint*)a) & v01) >>16; } set { fixed (byte* a = &data[offset+0x00]) *(uint*)a = (uint)((*a & ~v01)+(((value    )<<16) & v01)); } }
            public PC priorityControl     { get { fixed (byte* a = &data[offset+0x00]) return (PC)(((*(uint*)a) & v02) >>26); } set { fixed (byte* a = &data[offset+0x00]) *(uint*)a = (uint)((*a & ~v02)+((((uint)value)<<26) & v02)); } }
            public ID id                  { get { fixed (byte* a = &data[offset+0x00]) return (ID)(((*(uint*)a) & v03) >>28); } set { fixed (byte* a = &data[offset+0x00]) *(uint*)a = (uint)((*a & ~v03)+((((uint)value)<<28) & v03)); } }
            public bool interruptRequest  { get { fixed (byte* a = &data[offset+0x00]) return ((*(uint*)a) & v04) > 0;  } set { fixed (byte* a = &data[offset+0x00]) *(uint*)a = (uint)((*a & ~v04)+(((value?1:0)<<31) & v04)); } }
            
            public uint addr              { get { fixed (byte* a = &data[offset+0x04]) return ((*(uint*)a) & v05) >>0;  } set { fixed (byte* a = &data[offset+0x04]) *(uint*)a = (uint)((*a & ~v05)+(((value    )<<0 ) & v05)); } }
            public bool isSprAddr         { get { fixed (byte* a = &data[offset+0x04]) return ((*(uint*)a) & v06) > 0;  } set { fixed (byte* a = &data[offset+0x04]) *(uint*)a = (uint)((*a & ~v06)+(((value?1:0)<<31 ) & v06)); } }
            
            public VifCode command1;
            public VifCode command2;

            public sourceChainDmaTag(ref byte[] entryBytes, int index, int offset)
            {
                this.offset = offset + index * SIZE;
                this.data = entryBytes;
                this.command1  = new VifCode(ref entryBytes, offset + index * SIZE + 0x08);
                this.command2 = new VifCode(ref entryBytes, offset + index * SIZE + 0x0C);
            }
        }


        public List<KH2Model> Models;

        public class KH2Model
        {
            public new string ToString()
            {
                return this.Name.TrimEnd('\x0')+" ("+this.Type.ToString()+")";
            }
            public enum MODEL_TYPE
            {
                MULTI  = -1,
                BG     =  2,
                SKL    =  3,
                SHADOW =  4
            }

            public enum MODEL_SUBTYPE
            {
                CHARAOBJ =  0 + MODEL_TYPE.SKL * 0x10,
                BGOBJ    =  1 + MODEL_TYPE.SKL * 0x10,
                MAPBG    =  0 + MODEL_TYPE.BG  * 0x10,
                SKYBG    =  1 + MODEL_TYPE.BG  * 0x10
            }
            
            public static byte[] MaskRegister = new byte[0x10];
            public static List<int> ColRegister = new List<int>(0);
#if keepVifDataPosition
public static List<int> ColRegister_ptr = new List<int>(0);
#endif
            public static class VUMemory
            {
#if keepVifDataPosition
public static byte[] Data_ptr = new byte[0x1030];
#endif
                public static byte[] Data = new byte[0x1030];
                public static void InitializeMarkers()
                {
                    for (int i=0;i<Data.Length;i+=4)
                    {
                        Array.Copy(BC.GetBytes(0), 0, Data_ptr, i, sizeof(int));
                        Array.Copy(BC.GetBytes(0x676B6264), 0, Data, i, sizeof(int));
                    }
                }
                public static void RemoveMarkers()
                {
                    for (int i=0;i<Data.Length;i+=sizeof(int))
                        if (BC.ToInt32(Data,i) == 0x676B6264)
                            Array.Copy(new byte[4], 0, Data, i, sizeof(int));
                }
            }

            
            public class Part
            {
                public int TextureIndex;

                public Part()
                {
                    this.TextureIndex = -1;
                }

                public struct TVector
                {
                    public Vector3 WorldPosition;
                    public struct Influence
                    {
                        public short Index;
                        public float Weight;
                        public Influence(short index, float weight)
                        {
                            this.Index = index;
                            this.Weight = weight;
                        }
                    }
                    public Influence[] Influences;
                    public TVector(float x, float y, float z)
                    {
                        this.WorldPosition = new Vector3(x,y,z);
                        this.Influences = null;
                    }
                }

                public List<TVector> Vertices = new List<TVector>(0);
                List<int> vertexIndices = new List<int>(0);
                List<int> trianglestripFlags = new List<int>(0);

                List<Vector2> texcoords = new List<Vector2>(0);
                List<Color> colors = new List<Color>(0);

                public List<int> Indices = new List<int>(0);
                public List<Color> Colors = new List<Color>(0);
                public List<Vector2> TextureCoordinates = new List<Vector2>(0);


                unsafe struct StripElement
                {
                    public int v;
                    public int uv;
                    public int c;
                    public StripElement(int v)
                    {
                        this.v = v;
                        this.uv = -1;
                        this.c = -1;
                    }
                }
                
                public Part Finalize()
                {
                    List<StripElement> triBuff = new List<StripElement>();
                    
                    for (int i=0;i<trianglestripFlags.Count;i++)
                    {
                        StripElement strip = new StripElement(vertexIndices[i]);
                        if (i<this.texcoords.Count)
                        strip.uv = i;
                        if (i<this.colors.Count)
                        strip.c = i;

                        if (triBuff.Count == 3)
                            triBuff.RemoveAt(0);

                        triBuff.Add(strip);

                        int stripFlag = trianglestripFlags[i];
                        List<int> stripIndices = new List<int>(0);

                        if (stripFlag == 0x20 || stripFlag == 0x00)
                            stripIndices.AddRange(new int[] {0,1,2});
                        
                        if (stripFlag == 0x30 || stripFlag == 0x00)
                            stripIndices.AddRange(new int[] {2,1,0});

                        foreach (int stripIndex in stripIndices)
                        {
                            this.Indices.Add(triBuff[stripIndex].v);
                            if (triBuff[stripIndex].uv > -1)
                                this.TextureCoordinates.Add(this.texcoords[triBuff[stripIndex].uv]);
                            if (triBuff[stripIndex].c > -1)
                                this.Colors.Add(this.colors[triBuff[stripIndex].c]);
                        }
                    }

                    return this;
                }
                
                public void AppendFromVUMem(KH2Model model)
                {
				    int type = BC.ToInt32(VUMemory.Data, 0x00);

				    int cnt_uvIndStrip = BC.ToInt32(VUMemory.Data, 0x10);
				    int off_uvIndStrip = BC.ToInt32(VUMemory.Data, 0x14) * 0x10;
				    int off_infRunLength = BC.ToInt32(VUMemory.Data, 0x18) * 0x10;
				    int off_matrices = BC.ToInt32(VUMemory.Data, 0x1C) * 0x10;

				    int cnt_colors = 0;
				    int off_color = 0;
				    int cnt_infMulti = 0;
				    int off_infMulti = 0;

				    int cnt_verts;
				    int off_verts;
				    int cnt_infRunLength;
                    
				    if (type == 2)
				    {
					    cnt_verts = BC.ToInt32(VUMemory.Data, 0x20);
					    off_verts = BC.ToInt32(VUMemory.Data, 0x24) * 0x10;
					    cnt_infRunLength = BC.ToInt32(VUMemory.Data, 0x2C);
				    }
				    else
				    {
					    cnt_colors = BC.ToInt32(VUMemory.Data, 0x20);
					    off_color = BC.ToInt32(VUMemory.Data, 0x24) * 0x10;
					    cnt_infMulti = BC.ToInt32(VUMemory.Data, 0x28);
					    off_infMulti = BC.ToInt32(VUMemory.Data, 0x2C) * 0x10;

					    cnt_verts = BC.ToInt32(VUMemory.Data, 0x30);
					    off_verts = BC.ToInt32(VUMemory.Data, 0x34) * 0x10;
					    cnt_infRunLength = BC.ToInt32(VUMemory.Data, 0x3C);
				    }

                    List<SKLHRC> matrices = new List<SKLHRC>(0);
                    int vertex_cursor = off_verts;
                    int matrices_cursor = off_matrices;
                    int infMulti_cursor = off_infMulti + cnt_infMulti*4;
                    int uvIndStrip_cursor = off_uvIndStrip;
                    int color_cursor = off_color;

                    int vextex_origin = this.Vertices.Count;

                    if (cnt_infRunLength == 0)
                    {
                        for (int i=0;i<cnt_verts;i++)
                        {
                            Vector4 v4 = new Vector4(
                                BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4),
                                BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4),
                                BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4),
                                BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4));

                            this.Vertices.Add(new TVector(v4.X,v4.Y,v4.Z));
                        }
                    }

                    /* run length decoding */
                    for (int i=0;i<cnt_infRunLength;i++)
                    {
                        int currLength = BC.ToInt32(VUMemory.Data,off_infRunLength+i*sizeof(int));
                        int index = BC.ToInt16(VUMemory.Data,matrices_cursor);
                        for (int j=0;j<currLength;j++)
                        {
                            if (cnt_infMulti == 0)
                            {
                                Vector4 v4 = Vector4.Transform(new Vector4(
                                    BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4),
                                    BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4),
                                    BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4),
                                    BC.ToSingle(VUMemory.Data, (vertex_cursor+=4)-4)), 
                                    model.Skeleton[index].Matrix);
                                TVector v = new TVector(v4.X, v4.Y, v4.Z);
                                v.Influences = new TVector.Influence[] {new TVector.Influence(model.Skeleton[index].id, v4.W)};
                                this.Vertices.Add(v);
                            }
                            matrices.Add(model.Skeleton[index]);
                        }
                        matrices_cursor+=0x40;
                    }
                    
                    for (int i=0;i<cnt_infMulti;i++)
                    {
                        infMulti_cursor += (~((infMulti_cursor-1) & 15) & 15);
                        int currCount = BC.ToInt32(VUMemory.Data, off_infMulti+i*sizeof(int));

                        for (int j=0;j<currCount;j++)
                        {
                            TVector v = new TVector();
                            TVector.Influence[] influences = new TVector.Influence[i+1];
                            for (int k=0;k<influences.Length;k++)
                            {
                                int vInd = BC.ToInt32(VUMemory.Data, (infMulti_cursor+=4)-4);
                                int vPos = off_verts+vInd*0x10;
                                Vector4 v4 = new Vector4(
                                    BC.ToSingle(VUMemory.Data, (vPos+=4)-4),
                                    BC.ToSingle(VUMemory.Data, (vPos+=4)-4),
                                    BC.ToSingle(VUMemory.Data, (vPos+=4)-4),
                                    BC.ToSingle(VUMemory.Data, (vPos+=4)-4));
                                v.WorldPosition += Vector4.Transform(v4, matrices[vInd].Matrix).Xyz;
                                influences[k] = new TVector.Influence(matrices[vInd].id,v4.W);
                            }
                            v.Influences = influences;
                            this.Vertices.Add(v);
                        }
                    }
                    
                    for (int i=0;i<cnt_colors;i++)
                    {
                        int r = BC.ToInt32(VUMemory.Data, (color_cursor+=4)-4);
                        int g = BC.ToInt32(VUMemory.Data, (color_cursor+=4)-4);
                        int b = BC.ToInt32(VUMemory.Data, (color_cursor+=4)-4);
                        int a = BC.ToInt32(VUMemory.Data, (color_cursor+=4)-4);
                        this.colors.Add(Color.FromArgb(a,r,g,b));
                    }

                    for (int i=0;i<cnt_uvIndStrip;i++)
                    {
                        int u = 0;
                        int v = 0;

                        if (type != 2)
                        {
                            u = BC.ToInt32(VUMemory.Data, (uvIndStrip_cursor+=4)-4);
                            v = BC.ToInt32(VUMemory.Data, (uvIndStrip_cursor+=4)-4);
						    if (v > 0x2000) v -= ushort.MaxValue;
                        }
                        
                        int ind = BC.ToInt32(VUMemory.Data, (uvIndStrip_cursor+=4)-4);
                        int stripFlag = BC.ToInt32(VUMemory.Data, (uvIndStrip_cursor+=4)-4);

                        this.vertexIndices.Add(vextex_origin+ind);
                        this.trianglestripFlags.Add(stripFlag);
					    if (type != 2)
                        {
                            this.texcoords.Add(new Vector2(u/(float)0x1000,v/(float)0x1000));
                        }
                        //File.WriteAllBytes("VUMemory.bin",VUMemory.Data);
                        //File.WriteAllBytes("VUMemory_ptr.bin",VUMemory.Data_ptr);
                        
                        if (type == 2)
                        {
                            uvIndStrip_cursor+=sizeof(int);
                            uvIndStrip_cursor+=sizeof(int);
                        }
                    }
                }
            }

            public string Name;
            public MODEL_TYPE Type;
            TIM2 TIM;
            public int Index = 0;

            public List<TIM2.Texture> Textures {get {return this.TIM == null? null : this.TIM.Textures[Index];}}
            public List<TIM2.TexturePatch> TexturePatches {get {return this.TIM == null? null : this.TIM.Patches[Index];}}
            public List<TIM2.textAnimUvsc> UVScrolls {get {return this.TIM == null? null : this.TIM.UVScrolls[Index];}}

            public SKLBone BoneInfo;
            public SKLHRC[] Skeleton;
            public List<Part> Meshes;
            public List<KH2Model> Models;
            
            public KH2Model()
            {
                this.Name = "";
                this.Meshes = new List<Part>(0);
                this.Models = new List<KH2Model>();
            }

            public KH2Model(BAR inputFile):this()
            {
                this.Name = inputFile.Entry.Name;
                var timsCollection = inputFile.NamePairs.GetAllOfType(7);
                if (timsCollection.Count > 0)
                    this.TIM = new TIM2(timsCollection[0].Data);
                
                var data = inputFile.Data;
                if (data.Length < 0xA0) return;

                int pos = 0x90;
                if (BC.ToInt32(data, (pos+=4)-4) == (int)MODEL_TYPE.MULTI)
                {
                    pos += (~((pos-1) & 15) & 15);
                    List<int> offsets = new List<int>(0);
                    
                    while (pos + sizeof(int) < data.Length)
                    {
                        int offset = BC.ToInt32(data, pos);
                        pos += sizeof(int);
                        if (offset > 0 && offset< data.Length)
                            offsets.Add(offset);
                        else
                            break;
                    }

                    if (offsets.Count > 0)
                        offsets.Add(data.Length);

                    for (int i=0; i<offsets.Count-1; i++)
                    {
                        KH2Model model = new KH2Model();
                        this.Type = MODEL_TYPE.MULTI;
                        model.TIM = this.TIM;
                        model.Index = i;
                        model.ReadModel(ref data, 0x90 + offsets[i], offsets[i + 1] - offsets[i]);
                        this.Models.Add(model);
                    }
                }
                else
                {
                    this.ReadModel(ref data, 0, data.Length);
                }
            }


            void ReadModel(ref byte[] data, int offset, int length)
            {
                int origin = offset+0x90;
                int cursor = origin;
                this.Type = (MODEL_TYPE)BC.ToInt32(data, (cursor+=4)-4);
                MODEL_SUBTYPE subtype = (MODEL_SUBTYPE)((int)this.Type * 0x10 + BC.ToInt32(data, (cursor+=4)-4));
                int attribute = BC.ToInt32(data, (cursor+=4)-4);
                int nextOffset = BC.ToInt32(data, (cursor+=4)-4);

                short numMatrices, modelCount = numMatrices = BC.ToInt16(data, (cursor+=2)-2);
                short texture_num,shadowCount = texture_num = BC.ToInt16(data, (cursor+=2)-2);
                int matricesOffset = BC.ToInt32(data, (cursor+=4)-4);
                    int textureCount = matricesOffset & 0xFFFF;
                    int octalTreeCount = matricesOffset>> 16;
                int bone_offset, offVifPacketRenderingGroup  = bone_offset = BC.ToInt32(data, (cursor+=4)-4);
                int numModelParts, offDmaChainIndexRemapTable  = numModelParts = BC.ToInt32(data, (cursor+=4)-4);

                List<objectModelPartEntry> objectModelParts = new List<objectModelPartEntry>(0);
                List<backgroundModelPartEntry> backgroundModelEntries = new List<backgroundModelPartEntry>(0);
                

                switch (this.Type)
                {
                    case MODEL_TYPE.SKL:
                    case MODEL_TYPE.SHADOW:
                        for (int i=0;i< numModelParts;i++)
                            objectModelParts.Add(new objectModelPartEntry(cursor, i, ref data));
                        if (this.Type == MODEL_TYPE.SKL)
                        {
                            if (bone_offset > 0)
                                this.BoneInfo = new SKLBone(ref data, origin + bone_offset);
                            if (numMatrices > 0)
                            {
                                this.Skeleton = new SKLHRC[numMatrices];
                                for (int i = 0; i < numMatrices; i++)
                                {
                                    SKLHRC sklHrc = new SKLHRC(origin + matricesOffset, i, ref data);
                                    sklHrc.Matrix = 
                                        Matrix4.CreateScale(sklHrc.scale.Xyz)*
                                        Matrix4.CreateRotationX(sklHrc.rot.X)*
                                        Matrix4.CreateRotationY(sklHrc.rot.Y)*
                                        Matrix4.CreateRotationZ(sklHrc.rot.Z)*
                                        Matrix4.CreateTranslation(sklHrc.trans.Xyz);
                                    this.Skeleton[i] = sklHrc;
                                }
                                for (int i = 0; i < numMatrices; i++)
                                {
                                    if (this.Skeleton[i].parent_id < 0) continue;
                                    this.Skeleton[i].Matrix *= this.Skeleton[this.Skeleton[i].parent_id].Matrix;
                                }
                            }
                        }
                    break;
                    case MODEL_TYPE.BG:
                        for (int i = 0; i < modelCount; i++)
                            backgroundModelEntries.Add(new backgroundModelPartEntry(cursor, i, ref data));
                    break;
                }

                VUMemory.InitializeMarkers();

                foreach (objectModelPartEntry entry in objectModelParts)
                {
                    Part mesh = new Part();
                    mesh.TextureIndex = entry.textureIndex;
                    sourceChainDmaTag transferTag = new sourceChainDmaTag(); 
                    for (int i=0;i<entry.totalQwc;i++)
                    {
                       sourceChainDmaTag currTag = new sourceChainDmaTag(ref data, i, origin + entry.offDmaSrcChain);
                        if (currTag.command2.Command == VifCode.CMD.UNPACK)
                        {
                            if (currTag.isSprAddr) /* Skip the readings from scratchpad */
                                continue;
                            int addr =       (currTag.command2.imm & 0b0000001111111111) * 0x10;
                            bool usn =       (currTag.command2.imm & 0b0100000000000000) > 0;
                            bool flg =       (currTag.command2.imm & 0b1000000000000000) > 0;
                            bool interrupt = (currTag.command2.cmd & 0b10000000) > 0;
                            bool masked =    (currTag.command2.cmd & 0b00010000) > 0;
                            int vn =         (currTag.command2.cmd & 0b00001100);
                            int vl =         (currTag.command2.cmd & 0b00000011);
                            int dimension = 1 + (vn >> 2);
                            int lengthbytes  = (int)(4 / (vl == 3 ? 2 : Math.Pow(2, vl)));
                            SKLHRC sklHrc = this.Skeleton[(int)currTag.addr];
                            Array.Copy(sklHrc.data,sklHrc.offset, VUMemory.Data, addr, currTag.command2.num * dimension * lengthbytes);
                        }
                        else if (currTag.qwc > 0)
                        {
                            transferTag = currTag;
                        }
                        else
                        {
                            Unpack(origin, transferTag);
                            VUMemory.RemoveMarkers();
                            mesh.AppendFromVUMem(this);
                            VUMemory.InitializeMarkers();
                        }
                    }
                    this.Meshes.Add(mesh.Finalize());
                }
                foreach (backgroundModelPartEntry entry in backgroundModelEntries)
                {
                    Part mesh = new Part();
                    mesh.TextureIndex = entry.textureIndex;
                    sourceChainDmaTag currTag = new sourceChainDmaTag(ref data, 0, origin + entry.firstDmaTagOffset);
                    sourceChainDmaTag nextTag = currTag;
                    do
                    {
                        nextTag = Unpack(origin, nextTag);
                        VUMemory.RemoveMarkers();
                        mesh.AppendFromVUMem(this);
                        VUMemory.InitializeMarkers();
                    }
                    while (nextTag.data != null);

                    this.Meshes.Add(mesh.Finalize());
                }
                VUMemory.RemoveMarkers();

                if (nextOffset > 0 && origin + nextOffset < data.Length)
                {
                    KH2Model model = new KH2Model();
                    model.Skeleton = this.Skeleton;
                    model.ReadModel(ref data, nextOffset, length - nextOffset);
                    this.Models.Add(model);
                }
            }

            public sourceChainDmaTag Unpack(int originAddress, sourceChainDmaTag dmaTag)
            {
                int start = 0;
                int size = sourceChainDmaTag.SIZE;
                switch (dmaTag.id)
                {
                    case sourceChainDmaTag.ID.REF:
                        start =  originAddress + (int)dmaTag.addr;
                        size +=  (int)dmaTag.qwc * 0x10;
                    break;
                    case sourceChainDmaTag.ID.CNT:
                        start =  dmaTag.offset + 8;
                        size +=  (int)dmaTag.qwc * 0x10;
                    break;
                }
                int cursor = start;
                while (cursor<start+size)
                {
                    VifCode command0 = new VifCode(ref dmaTag.data, (cursor+=4)-4);

                    if (command0.Command == VifCode.CMD.MSCNT)/* EE_Users_Manual.pdf#page=99 */
                    {
                        bool interrupt_ = (command0.cmd & 0b10000000) > 0;
                        cursor += (~((cursor-1) & 15) & 15);
                        return new sourceChainDmaTag(ref dmaTag.data, 0, cursor);
                    } else
                    if (command0.Command == VifCode.CMD.STCOL)/* EE_Users_Manual.pdf#page=103 */
                    {
                        bool interrupt_ = (command0.cmd & 0b10000000) > 0;
                        for (int i=0;i<4;i++)
                        {
#if keepVifDataPosition 
ColRegister_ptr.Add(cursor);
#endif
                            ColRegister.Add(BC.ToInt32(dmaTag.data, (cursor+=4)-4));
                        }
                    } else
                    if (command0.Command == VifCode.CMD.STMASK)/* EE_Users_Manual.pdf#page=80 */
                    {
                        bool masked = (command0.cmd & 0b00010000) > 0;
                        int word = BC.ToInt32(dmaTag.data, (cursor+=4)-4);
                        for (int i=0;i<MaskRegister.Length;i++)
                            MaskRegister[i] = (byte)((word>>(i*2)) & 0b00000000000000000000000000000011);
                    } else
                    if (command0.Command == VifCode.CMD.STCYCL)/* EE_Users_Manual.pdf#page=123 */
                    {
                        VifCode command1 = new VifCode(ref dmaTag.data, (cursor+=4)-4);
                        int addr =       (command1.imm & 0b0000001111111111) * 0x10;
                        bool usn =       (command1.imm & 0b0100000000000000) > 0;
                        bool flg =       (command1.imm & 0b1000000000000000) > 0;


                        bool interrupt_ = (command1.cmd & 0b10000000) > 0;
                        bool masked =    (command1.cmd & 0b00010000) > 0;
                        int vn =         (command1.cmd & 0b00001100);
                        int vl =         (command1.cmd & 0b00000011);
                        int dimension = 1 + (vn >> 2);
                        int lengthbytes  = (int)(4 / (vl == 3 ? 2 : Math.Pow(2, vl)));
                        
                        for (int i=0;i<command1.num;i++)
                        {
                            int vu_cursor = addr + i*16;
                            int k=0;
                            for (;BC.ToInt32(VUMemory.Data, vu_cursor) != 0x676B6264&&k<sizeof(int);k++)
                                vu_cursor+=sizeof(int);

                            for (int j=0;k<sizeof(int) && j<ColRegister.Count;j++)
                            {
#if keepVifDataPosition 
Array.Copy(BC.GetBytes(ColRegister_ptr[j]), 0, VUMemory.Data_ptr, vu_cursor+j*sizeof(int), sizeof(int));
#endif
                                Array.Copy(BC.GetBytes(ColRegister[j]), 0, VUMemory.Data, vu_cursor+j*sizeof(int), sizeof(int));
                            }

                            for (int j=0;k<4 && j<dimension;j++)
                            {
                                Array.Copy(new byte[sizeof(int)], 0, VUMemory.Data, vu_cursor, sizeof(int));
                                Array.Copy(dmaTag.data, cursor, VUMemory.Data, vu_cursor,lengthbytes);
#if keepVifDataPosition 
Array.Copy(BC.GetBytes(cursor), 0, VUMemory.Data_ptr, vu_cursor,sizeof(int));
#endif
                                vu_cursor+=4;
                                cursor+=lengthbytes;
                            }
                        }
                        ColRegister.Clear();
                    }
                    while (cursor%4>0) cursor++;
                }
                return new sourceChainDmaTag();
            }
        }


        public MDLX(string filename) : this(new FileStream(filename, FileMode.Open)) { }

        public MDLX(Stream stream) : base(stream)
        {
            this.Models = new List<KH2Model>(0);
            foreach (BAR kh2modelEntry in this.Files.GetAllOfType(4))
            {
                this.Models.Add(new KH2Model(kh2modelEntry));
            }
        }
    }
}
