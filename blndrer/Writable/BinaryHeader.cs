namespace blndrer;

public class BinaryHeader : Writable
{
    public uint mEngineType { get; set; }
    public uint mBinaryBlockType{ get; set; }
    public uint mBinaryBlockVersion{ get; set; }

    public BinaryHeader()
    {
    }

    public BinaryHeader(uint mEngineType, uint mBinaryBlockType, uint mBinaryBlockVersion)
    {
        this.mEngineType = mEngineType;
        this.mBinaryBlockType = mBinaryBlockType;
        this.mBinaryBlockVersion = mBinaryBlockVersion;
    }

    public BinaryHeader(BinaryReader br)
    {
        mEngineType = br.ReadUInt32();
        mBinaryBlockType = br.ReadUInt32();
        mBinaryBlockVersion = br.ReadUInt32();
    }
    
    public override void Write(BinaryWriter bw)
    {
        bw.Write(mEngineType);
        bw.Write(mBinaryBlockType);
        bw.Write(mBinaryBlockVersion);
    }
}