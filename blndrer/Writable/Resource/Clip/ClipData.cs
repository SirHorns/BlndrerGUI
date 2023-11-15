namespace blndrer.Writable.Resource.Clip;

public class ClipData : Writable
{
    public ClipTypes mClipTypeID;
    public enum ClipTypes
    {
        eInvalid = 0x0,
        eAtomic = 0x1,
        eSelector = 0x2,
        eSequencer = 0x3,
        eParallel = 0x4,
        eMultiChildClip = 0x5,
        eParametric = 0x6,
        eConditionBool = 0x7,
        eConditionFloat = 0x8,
    };
    protected long baseAddr;
    protected ClipData(BinaryReader br)
    {
        baseAddr = br.BaseStream.Position;
        mClipTypeID = (ClipTypes)br.ReadUInt32();
    }
    public static ClipData Read(BinaryReader br)
    {
        var prevPosition = br.BaseStream.Position;
        ClipTypes mClipTypeID = (ClipTypes)br.ReadUInt32();
        br.BaseStream.Position = prevPosition;
        if(mClipTypeID == ClipTypes.eAtomic)
        {
            return br.Read<AtomicClip>();
        }
        else if(mClipTypeID == ClipTypes.eSelector)
        {
            return br.Read<SelectorClip>();
        }
        else if(mClipTypeID == ClipTypes.eSequencer)
        {
            return br.Read<SequencerClip>();
        }
        else if(mClipTypeID == ClipTypes.eParallel)
        {
            return br.Read<ParallelClip>();
        }
        /*
        else if(mClipTypeID == ClipTypes.eMultiChildClip)
        {
            return br.Read<MultiChildClip>();
        }
        */
        else if(mClipTypeID == ClipTypes.eParametric)
        {
            return br.Read<ParametricClip>();
        }
        else if(mClipTypeID == ClipTypes.eConditionBool)
        {
            return br.Read<ConditionBoolClip>();
        }
        else if(mClipTypeID == ClipTypes.eConditionFloat)
        {
            return br.Read<ConditionFloatClip>();
        }
        else
            throw new Exception("Invalid clip type");
    }
    public override void Write(BinaryWriter bw)
    {
        bw.Write((uint)mClipTypeID);
    }
}