namespace blndrer.Writable.Resource.Clip;

internal class ParametricClip : ClipData
{
    public uint mNumPairs;
    public uint mUpdaterType;
    public MaskResource mMaskData;
    public TrackResource mTrackData;
    public ParametricClip(BinaryReader br) : base(br)
    {
        mNumPairs = br.ReadUInt32();
        mUpdaterType = br.ReadUInt32();
        
        long mMaskDataAddr = br.ReadAddr();
        long mTrackDataAddr = br.ReadAddr();

        var prevPosition = br.BaseStream.Position;
        br.BaseStream.Position = mMaskDataAddr;
        if(mMaskDataAddr != 0) mMaskData = br.Read<MaskResource>();
        br.BaseStream.Position = mTrackDataAddr;
        if(mTrackDataAddr != 0) mTrackData = br.Read<TrackResource>();
        br.BaseStream.Position = prevPosition;
    }
    public override void Write(BinaryWriter bw)
    {
        base.Write(bw);
        bw.Write(mNumPairs);
        bw.Write(mUpdaterType);

        int c() => (int)bw.BaseStream.Position;
        bw.Write(Memory.Allocate(c(), mMaskData));
        bw.Write(Memory.Allocate(c(), mTrackData));
    }
}