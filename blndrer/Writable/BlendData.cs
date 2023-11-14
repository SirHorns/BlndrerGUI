namespace blndrer;

public class BlendData : Writable
{
    public uint mFromAnimId { get; set; }
    public uint mToAnimId { get; set; }
    public uint mBlendFlags { get; set; }
    public float mBlendTime { get; set; }

    public BlendData() { }

    public BlendData(uint mFromAnimId, uint mToAnimId, uint mBlendFlags, float mBlendTime)
    {
        this.mFromAnimId = mFromAnimId;
        this.mToAnimId = mToAnimId;
        this.mBlendFlags = mBlendFlags;
        this.mBlendTime = mBlendTime;
    }

    public BlendData(BinaryReader br)
    {
        mFromAnimId = br.ReadUInt32();
        mToAnimId = br.ReadUInt32();
        mBlendFlags = br.ReadUInt32();
        mBlendTime = br.ReadSingle();
    }
    public override void Write(BinaryWriter bw)
    {
        bw.Write(mFromAnimId);
        bw.Write(mToAnimId);
        bw.Write(mBlendFlags);
        bw.Write(mBlendTime);
    }
};