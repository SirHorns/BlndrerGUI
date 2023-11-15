namespace blndrer;

class SelectorClip : ClipData
{
    public uint mTrackIndex;
    public uint mNumPairs;
    public SelectorClip(BinaryReader br) : base(br)
    {
        mTrackIndex = br.ReadUInt32();
        mNumPairs = br.ReadUInt32();
    }
    public override void Write(BinaryWriter bw)
    {
        base.Write(bw);
        bw.Write(mTrackIndex);
        bw.Write(mNumPairs);
    }
}