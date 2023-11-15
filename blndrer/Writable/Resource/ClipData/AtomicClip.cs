namespace blndrer;

class AtomicClip : ClipData
{
    public uint mStartTick;
    public uint mEndTick;
    public float mTickDuration;
    public int mAnimDataIndex;
    //public AnimResourceBase? mAnimData;
    public EventResource? mEventData;
    public MaskResource? mMaskData;
    public TrackResource? mTrackData;
    public UpdaterResource? mUpdaterData;
    public string? mSyncGroupName;
    public uint mSyncGroup;
    public uint[] mExtBuffer;

    public AtomicClip(BinaryReader br): base(br)
    {   
        mStartTick = br.ReadUInt32();
        mEndTick = br.ReadUInt32();
        mTickDuration = br.ReadSingle();
        mAnimDataIndex = br.ReadInt32();
        long mEventDataAddr = br.ReadAddr();
        long mMaskDataAddr = br.ReadAddr();
        long mTrackDataAddr = br.ReadAddr();
        long mUpdaterDataOffset = br.ReadAddr();
        long mSyncGroupNameOffset = br.ReadAddr();
        mSyncGroup = br.ReadUInt32();
        mExtBuffer = new uint[]
        {
            br.ReadUInt32(),
            br.ReadUInt32(),
        };

        var prevPosition = br.BaseStream.Position;
        //br.BaseStream.Position = mAnimDataAddr;
        //if(mAnimDataAddr != 0) mAnimData = AnimResourceBase.Read(br);
        //if(mAnimDataIndex != -1) mAnimData = pool.mAnimDataAry[mAnimDataIndex];
        br.BaseStream.Position = mEventDataAddr;
        if(mEventDataAddr != 0) mEventData = br.Read<EventResource>();
        br.BaseStream.Position = mMaskDataAddr;
        if(mMaskDataAddr != 0) mMaskData = br.Read<MaskResource>();
        br.BaseStream.Position = mTrackDataAddr;
        if(mTrackDataAddr != 0) mTrackData = br.Read<TrackResource>();
        br.BaseStream.Position = mUpdaterDataOffset;
        if(mUpdaterDataOffset != 0) mUpdaterData = br.Read<UpdaterResource>();
        br.BaseStream.Position = mSyncGroupNameOffset;
        if(mSyncGroupNameOffset != 0) mSyncGroupName = br.ReadCString();
        br.BaseStream.Position = prevPosition;
    }

    public override void Write(BinaryWriter bw)
    {
        base.Write(bw);

        //Debug.Assert(mAnimData == null || (pool.mAnimDataAry?.Contains(mAnimData) ?? false));
        // Debug.Assert(mAnimDataIndex >= 0 && mAnimDataIndex < pool.mAnimDataAry.Length);
        // Debug.Assert(mEventData == null || (pool.mEventDataAry?.Contains(mEventData) ?? false));
        // Debug.Assert(mMaskData == null || (pool.mMaskDataAry?.Contains(mMaskData) ?? false));
        // Debug.Assert(mTrackData == null || (pool.mBlendTrackAry?.Contains(mTrackData) ?? false));

        bw.Write(mStartTick);
        bw.Write(mEndTick);
        bw.Write(mTickDuration);

        int c() => (int)bw.BaseStream.Position;

        bw.Write(mAnimDataIndex);
        bw.Write(Memory.Allocate(c(), mEventData));
        bw.Write(Memory.Allocate(c(), mMaskData));
        bw.Write(Memory.Allocate(c(), mTrackData));
        bw.Write(Memory.Allocate(c(), mUpdaterData));
        bw.Write(Memory.Allocate(c(), mSyncGroupName));
        bw.Write(mSyncGroup);

        foreach(uint ui in mExtBuffer)
            bw.Write(ui);
    }
};