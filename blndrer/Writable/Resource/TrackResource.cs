using blndrer.Writable.Resource;

namespace blndrer;

public class TrackResource: Resource
{
    public float mBlendWeight { get; set; }
    public uint mBlendMode { get; set; }
    public uint mIndex { get; set; }
    public string mName { get; set; }

    public TrackResource()
    {
        
    }

    public TrackResource(float mBlendWeight, uint mBlendMode, uint mIndex, string mName)
    {
        this.mBlendWeight = mBlendWeight;
        this.mBlendMode = mBlendMode;
        this.mIndex = mIndex;
        this.mName = mName;
    }

    public TrackResource(BinaryReader br): base(br)
    {
        mBlendWeight = br.ReadSingle();
        mBlendMode = br.ReadUInt32();
        mIndex = br.ReadUInt32();
        mName = br.ReadCString(32);
        //ReadExtraBytes();
    }
    public override void Write(BinaryWriter bw)
    {
        bw.Write(Memory.SizeOf(this));
        bw.Write(mBlendWeight);
        bw.Write(mBlendMode);
        bw.Write(mIndex);
        bw.WriteCString(mName, 32);
    }
}