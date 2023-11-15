namespace blndrer.Writable.Resource;

public class MaskResource : Resource
{
    public uint mFormatToken;
    public uint mVersion;
    public ushort mFlags;
    public ushort mNumElement;
    public uint mUniqueID;
    public float mWeight;
    public JointHash mJointHash;
    public class JointHash : Writable
    {
        public int mWeightID;
        public uint mJointHash;
        public JointHash(BinaryReader br)
        {
            mWeightID = br.ReadInt32();
            mJointHash = br.ReadUInt32();
        }
        public override void Write(BinaryWriter bw)
        {
            bw.Write(mWeightID);
            bw.Write(mJointHash);
        }
    }
    public JointNdx mJointNdx;
    public class JointNdx : Writable
    {
        public int mWeightID;
        public int mJointNdx;
        public JointNdx(BinaryReader br)
        {
            mWeightID = br.ReadInt32();
            mJointNdx = br.ReadInt32();
        }
        public override void Write(BinaryWriter bw)
        {
            bw.Write(mWeightID);
            bw.Write(mJointNdx);
        }
    }
    public string mName;
    private uint[] mExtBuffer;
    public MaskResource(BinaryReader br): base(br)
    {
        mFormatToken = br.ReadUInt32();
        mVersion = br.ReadUInt32();
        mFlags = br.ReadUInt16();
        mNumElement = br.ReadUInt16();
        mUniqueID = br.ReadUInt32();
        long mWeightOffset = br.ReadAddr(baseAddr);
        long mJointHashOffset = br.ReadAddr(baseAddr);
        long mJointNdxOffset = br.ReadAddr(baseAddr);
        mName = br.ReadCString(32);
        mExtBuffer = new uint[]
        {
            br.ReadUInt32(),
            br.ReadUInt32(),
        };
        //ReadExtraBytes();

        long prevPosition = br.BaseStream.Position;
        br.BaseStream.Position = mWeightOffset;
        if(mWeightOffset != 0) mWeight = br.ReadSingle();
        br.BaseStream.Position = mJointHashOffset;
        if(mJointHashOffset != 0) mJointHash = br.Read<JointHash>();
        br.BaseStream.Position = mJointNdxOffset;
        if(mJointNdxOffset != 0) mJointNdx = br.Read<JointNdx>();
        br.BaseStream.Position = prevPosition;
    }

    public override void Write(BinaryWriter bw)
    {
        int baseAddr = (int)bw.BaseStream.Position;
        bw.Write(Memory.SizeOf(this));
        bw.Write(mFormatToken);
        bw.Write(mVersion);
        bw.Write(mFlags);
        bw.Write(mNumElement);
        bw.Write(mUniqueID);
        bw.Write(Memory.Allocate(baseAddr, mWeight, bw => bw.Write(mWeight)));
        bw.Write(Memory.Allocate(baseAddr, mJointHash));
        bw.Write(Memory.Allocate(baseAddr, mJointNdx));
        bw.WriteCString(mName, 32);
        foreach(uint ui in mExtBuffer)
            bw.Write(ui);
    }
};