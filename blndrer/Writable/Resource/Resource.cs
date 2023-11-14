namespace blndrer;

public abstract class Resource : Writable
{
    protected long baseAddr;

    public uint mResourceSize { get; set; }

    protected Resource()
    {
    }

    protected Resource(uint mResourceSize)
    {
        this.mResourceSize = mResourceSize;
    }

    //public byte[] ExtraBytes;
    private BinaryReader br;
    public Resource(BinaryReader br)
    {
        this.br = br;
        baseAddr = br.BaseStream.Position;
        mResourceSize = br.ReadUInt32();
    }
    /*
    protected void ReadExtraBytes()
    {
        if(mResourceSize == 0) return;
        long readed = br.BaseStream.Position - 1 - baseAddr;
        ExtraBytes = br.ReadBytes((int)(mResourceSize - readed));
    }
    */
}