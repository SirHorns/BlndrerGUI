namespace blndrer;

public class PoolData : Resource
{
    public uint mFormatToken{ get; set; }
    public uint mVersion { get; set; }
    public bool mUseCascadeBlend { get; set; }
    public float mCascadeBlendValue { get; set; }
    
    public BlendData[]? mBlendDataAry { get; set; }
    public TransitionClipData[]? mTransitionData { get; set; }
    public TrackResource[]? mBlendTrackAry { get; set; }
    public ClipResource[]? mClassAry { get; set; }
    public MaskResource[]? mMaskDataAry { get; set; }
    public EventResource[]? mEventDataAry { get; set; }
    public AnimResourceBase?[]? mAnimDataAry { get; set; }
    public PathRecord[]? mAnimNames { get; set; }
    public PathRecord mSkeleton { get; set; }

    private uint[] mExtBuffer { get; }

    public PoolData() : base(null) { }

    public PoolData(
        uint mFormatToken, 
        uint mVersion, 
        bool mUseCascadeBlend, 
        float mCascadeBlendValue, 
        BlendData[]? mBlendDataAry, 
        TransitionClipData[]? mTransitionData, 
        TrackResource[]? mBlendTrackAry, 
        ClipResource[]? mClassAry, 
        MaskResource[]? mMaskDataAry, 
        EventResource[]? mEventDataAry, 
        AnimResourceBase?[]? mAnimDataAry, 
        PathRecord[]? mAnimNames, 
        PathRecord mSkeleton): base(null)
    {
        this.mFormatToken = mFormatToken;
        this.mVersion = mVersion;
        this.mUseCascadeBlend = mUseCascadeBlend;
        this.mCascadeBlendValue = mCascadeBlendValue;
        this.mBlendDataAry = mBlendDataAry;
        this.mTransitionData = mTransitionData;
        this.mBlendTrackAry = mBlendTrackAry;
        this.mClassAry = mClassAry;
        this.mMaskDataAry = mMaskDataAry;
        this.mEventDataAry = mEventDataAry;
        this.mAnimDataAry = mAnimDataAry;
        this.mAnimNames = mAnimNames;
        this.mSkeleton = mSkeleton;
    }

    public PoolData(BinaryReader br): base(br)
    {
        mFormatToken = br.ReadUInt32();
        mVersion = br.ReadUInt32();
        uint mNumClasses = br.ReadUInt32();
        uint mNumBlends = br.ReadUInt32();
        uint mNumTransitionData = br.ReadUInt32();
        uint mNumTracks = br.ReadUInt32();
        uint mNumAnimData = br.ReadUInt32();
        uint mNumMaskData = br.ReadUInt32();
        uint mNumEventData = br.ReadUInt32();
        mUseCascadeBlend = br.ReadUInt32() != 0; //TODO: Verify
        mCascadeBlendValue = br.ReadSingle();

        long mBlendDataAryAddr = br.ReadAddr();
        long mTransitionDataOffset = br.ReadAddr();
        long mBlendTrackAryAddr = br.ReadAddr();
        long mClassAryAddr = br.ReadAddr();
        long mMaskDataAryAddr = br.ReadAddr();
        long mEventDataAryAddr = br.ReadAddr();
        long mAnimDataAryAddr = br.ReadAddr();
        uint mAnimNameCount = br.ReadUInt32();
        long mAnimNamesOffset = br.ReadAddr();

        mSkeleton = br.Read<PathRecord>();
        mExtBuffer = new uint[]
        {
            br.ReadUInt32(),
        };
        //ReadExtraBytes();

        if(mBlendDataAryAddr != 0) mBlendDataAry = br.ReadArr(mBlendDataAryAddr, mNumBlends, br => br.Read<BlendData>());
        if(mTransitionDataOffset != 0) mTransitionData = br.ReadArr(mTransitionDataOffset, mNumTransitionData, br => br.Read<TransitionClipData>());
        if(mBlendTrackAryAddr != 0) mBlendTrackAry = br.ReadArr(mBlendTrackAryAddr, mNumTracks, br => br.Read<TrackResource>());

        if(mMaskDataAryAddr != 0) mMaskDataAry = br.ReadArr2(mMaskDataAryAddr, mNumMaskData, br => br.Read<MaskResource>());
        if(mEventDataAryAddr != 0) mEventDataAry = br.ReadArr2(mEventDataAryAddr, mNumEventData, br => br.Read<EventResource>());
        if(mAnimDataAryAddr != 0) mAnimDataAry = br.ReadArr2(mAnimDataAryAddr, mNumAnimData, br => AnimResourceBase.Read(br));
        if(mClassAryAddr != 0) mClassAry = br.ReadArr2(mClassAryAddr, mNumClasses, br => br.Read<ClipResource>());

        if(mAnimNamesOffset != 0) mAnimNames = br.ReadArr(mAnimNamesOffset, mAnimNameCount, br => br.Read<PathRecord>());
    }

    public override void Write(BinaryWriter bw)
    {
        int baseAddr = (int)bw.BaseStream.Position;

        bw.Write(Memory.SizeOf(this));
        bw.Write(mFormatToken);
        bw.Write(mVersion);
        bw.Write(mClassAry?.Length ?? 0);
        bw.Write(mBlendDataAry?.Length ?? 0);
        bw.Write(mTransitionData?.Length ?? 0);
        bw.Write(mBlendTrackAry?.Length ?? 0);
        bw.Write(mAnimDataAry?.Length ?? 0);
        bw.Write(mMaskDataAry?.Length ?? 0);
        bw.Write(mEventDataAry?.Length ?? 0);
        bw.Write(Convert.ToUInt32(mUseCascadeBlend));
        bw.Write(mCascadeBlendValue);

        int mBlendDataAryAddr;
        int mTransitionDataOffset;
        int mBlendTrackAryAddr;
        int mClassAryAddr;
        int mMaskDataAryAddr;
        int mEventDataAryAddr;
        int mAnimDataAryAddr;
        int mAnimNameCount;
        int mAnimNamesOffset;

        int c() => (int)bw.BaseStream.Position;
        bw.Write(mBlendDataAryAddr = Memory.AllocateArray(c(), mBlendDataAry));
        bw.Write(mTransitionDataOffset = Memory.AllocateArray(c(), mTransitionData));
        bw.Write(mBlendTrackAryAddr = Memory.AllocateArray(c(), mBlendTrackAry));

        bw.Write(mClassAryAddr = Memory.AllocateArray2(c(), mClassAry));
        bw.Write(mMaskDataAryAddr = Memory.AllocateArray2(c(), mMaskDataAry));
        bw.Write(mEventDataAryAddr = Memory.AllocateArray2(c(), mEventDataAry));
        bw.Write(mAnimDataAryAddr = Memory.AllocateArray2(c(), mAnimDataAry));

        bw.Write(mAnimNameCount = mAnimNames?.Length ?? 0);
        bw.Write(mAnimNamesOffset = Memory.AllocateArray(c(), mAnimNames));
        
        bw.Write(mSkeleton);
        
        foreach(uint ui in mExtBuffer)
            bw.Write(ui);
    }
}