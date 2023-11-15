namespace blndrer;

internal class ParallelClip : ClipData
{
    public uint mClipFlag;
    public uint mNumClips;
    public ParallelClip(BinaryReader br) : base(br)
    {
        long mClipFlagPtr = br.ReadAddr();
        mNumClips = br.ReadUInt32();
    }
    public override void Write(BinaryWriter bw)
    {
        base.Write(bw);
        bw.Write(mClipFlag);
        bw.Write(mNumClips);
    }
}