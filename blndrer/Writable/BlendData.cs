namespace blndrer;

public class BlendData : Writable
{
    public uint mFromAnimId;
    public uint mToAnimId;
    public uint mBlendFlags;
    public float mBlendTime;
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