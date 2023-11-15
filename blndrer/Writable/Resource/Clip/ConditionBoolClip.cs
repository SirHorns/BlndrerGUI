namespace blndrer.Writable.Resource.Clip;

class ConditionBoolClip : ClipData
{
    public uint mTrackIndex;
    public uint mNumPairs;
    public uint mUpdaterType;
    public bool mChangeAnimationMidPlay;
    public ConditionBoolClip(BinaryReader br) : base(br)
    {
        mTrackIndex = br.ReadUInt32();
        mNumPairs = br.ReadUInt32();
        mUpdaterType = br.ReadUInt32();
        mChangeAnimationMidPlay = br.ReadUInt32() != 0; //TODO: Verify
    }
    public override void Write(BinaryWriter bw)
    {
        base.Write(bw);
        bw.Write(mTrackIndex);
        bw.Write(mNumPairs);
        bw.Write(mUpdaterType);
        bw.Write(Convert.ToUInt32(mChangeAnimationMidPlay));
    }
}