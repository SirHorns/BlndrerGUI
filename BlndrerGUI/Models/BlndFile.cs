using blndrer;

namespace BlndrerGUI.Models;

public class BlndFile
{
    public Header Header { get; init; }
    public Pool Pool { get; init; }
    
    public BlndFile(){}
    
    public BlndFile(BlendFile blendFile)
    {
        Header = new Header(blendFile.Header);
        Pool = new Pool(blendFile.Pool);
    }
}

public class Header
{
    public long EngineType { get; set; }
    public long BinaryBlockType { get; set; }
    public uint BinaryBlockVersion { get; set; }

    public Header(){}

    public Header(BinaryHeader binaryHeader)
    {
        EngineType = binaryHeader.mEngineType; 
        BinaryBlockType = binaryHeader.mBinaryBlockType;
        BinaryBlockVersion = binaryHeader.mBinaryBlockVersion;
    }
}

public class Pool
{
    public long FormatToken{ get; set; }
    public uint Version{ get; set; }
    public bool UseCascadeBlend{ get; set; }
    public float CascadeBlendValue { get; set; }
    public BlendData[] BlendDataAry { get; set; }
    public BlendTrack[] BlendTrackAry { get; set; }
    
    
    public EventDataAry[] EventDataAry { get; set; }
    public string[] AnimDataAry { get; set; }
    
    public PathRecord[] AnimNames{ get; set; }
    public string Skeleton{ get; set; }
    public uint ResourceSize{ get; set; }
    
    public Pool(){}

    public Pool(PoolData poolData)
    {
        FormatToken = poolData.mFormatToken;
        Version = poolData.mVersion;
        UseCascadeBlend = poolData.mUseCascadeBlend;
        CascadeBlendValue = poolData.mCascadeBlendValue;

        var blendDataAry = new BlendData[poolData.mBlendDataAry.Length];
        for (int i = 0; i < poolData.mBlendDataAry.Length; i++)
        {
            var blend = poolData.mBlendDataAry[i];
            blendDataAry[i] = new BlendData()
            {
                FromAnimId = blend.mFromAnimId,
                ToAnimId = blend.mFromAnimId,
                BlendFlags = blend.mBlendFlags,
                BlendTime = blend.mBlendTime
            };
        }
        BlendDataAry = blendDataAry;
        
        var blendTracks = new BlendTrack[poolData.mBlendTrackAry.Length];
        for (int i = 0; i < poolData.mBlendTrackAry.Length; i++)
        {
            var track = poolData.mBlendTrackAry[i];
            blendTracks[i] = new BlendTrack()
            {
                BlendWeight = track.mBlendWeight,
                BlendMode = track.mBlendMode,
                Index = track.mIndex,
                Name = track.mName,
                ResourceSize = track.mResourceSize
            };
        }
        BlendTrackAry = blendTracks;
        
        //mTransitionData
        //mBlendTrackAry
        //mClassAry
        //mMaskDataAry
        //mEventDataAry
        //mAnimDataAry
        
        var pathRecords = new PathRecord[poolData.mAnimNames.Length];
        for (int i = 0; i < poolData.mAnimNames.Length; i++)
        {
            pathRecords[i] = new PathRecord()
            {
                Path = poolData.mAnimNames[i].path
            };
        }
        AnimNames = pathRecords;
        
        Skeleton = poolData.mSkeleton.path;
        ResourceSize = poolData.mResourceSize;
    }
}

public class BlendData
{
    public uint FromAnimId { get; set; }
    public uint ToAnimId { get; set; }
    public uint BlendFlags { get; set; }
    public float BlendTime { get; set; }

    public BlendData(){}
    
    public BlendData(uint fromAnimId, uint toAnimId, uint blendFlags, float blendTime)
    {
        FromAnimId = fromAnimId;
        ToAnimId = toAnimId;
        BlendFlags = blendFlags;
        BlendTime = blendTime;
    }
}

public class BlendTrack
{
    public float BlendWeight { get; set; }
    public uint BlendMode { get; set; }
    public uint Index { get; set; }
    public string Name { get; set; }
    public uint ResourceSize { get; set; }
}

public class EventDataAry
{
    public uint FormatToken { get; set; }
    public uint Version { get; set; }
    public ushort Flags { get; set; }
    public uint UniqueID { get; set; }
    public EventData[] EventArray { get; set; }
    public EventData EventData { get; set; }
    public EventNameHash EventNameHash { get; set; }
    public EventFrame EventFrame { get; set; }
    public string Name { get; set; }
    public uint ResourceSize { get; set; }
}

public class EventData
{
    public string EffectName { get; set; }
    public string BoneName { get; set; }
    public string TargetBoneName { get; set; }
    public string EndFrame { get; set; }
    public string EventTypeId { get; set; }
    public string Flags { get; set; }
    public string Frame { get; set; }
    public string Name { get; set; }
    public string ResourceSize { get; set; }
}

public class EventNameHash
{
    public uint DataId { get; set; }
    public long NameHash { get; set; }
}

public class EventFrame
{
    public uint DataId { get; set; }
    public float Frame { get; set; }
}

public class PathRecord
{
    public string Path { get; set; }
}