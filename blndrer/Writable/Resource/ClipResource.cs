
using System.Diagnostics;
using blndrer;

public class ClipResource: Resource
{
    public Flags mFlags;
    public enum Flags
    {
        eNone_0 = 0x0,
        eMain = 0x1,
        eLoop = 0x2,
        eContinue = 0x4,
        ePlayOnce = 0x8,
    }
    public uint mUniqueID;
    public string mName;
    public ClipData mClipData;
    public ClipResource(BinaryReader br): base(br)
    {
        mFlags = (Flags)br.ReadUInt32(); //TODO: Verify (u16)
        mUniqueID = br.ReadUInt32();
        long mNameOffset = br.ReadAddr(baseAddr);
        long mClipDataOffset = br.ReadAddr(baseAddr);
        //ReadExtraBytes();

        long prevPosition = br.BaseStream.Position;
        br.BaseStream.Position = mNameOffset;
        if(mNameOffset != 0) mName = br.ReadCString();
        br.BaseStream.Position = mClipDataOffset;
        if(mClipDataOffset != 0) mClipData = ClipData.Read(br);
        br.BaseStream.Position = prevPosition;
    }

    public override void Write(BinaryWriter bw)
    {
        int baseAddr = (int)bw.BaseStream.Position;
        bw.Write(Memory.SizeOf(this));
        bw.Write((uint)mFlags);
        bw.Write(mUniqueID);
        bw.Write(Memory.Allocate(baseAddr, mName));
        bw.Write(Memory.Allocate(baseAddr, mClipData));
    }
}

class UpdaterResource : Resource
{
    public uint mVersion;
    public ushort mNumUpdaters;
    public UpdaterData mUpdaters;
    public UpdaterResource(BinaryReader br): base(br)
    {
    }

    public override void Write(BinaryWriter bw)
    {
        throw new NotImplementedException();
    }
}
class UpdaterData : Resource
{
    public ushort mInputType;
    public ushort mOutputType;
    public byte mNumTransforms;
    public AnimValueProcessorData[] mProcessors;
    public UpdaterData(BinaryReader br): base(br)
    {
    }

    public override void Write(BinaryWriter bw)
    {
        throw new NotImplementedException();
    }
}

class AnimValueProcessorData : Resource
{
    public ushort mProcessorType;
    public AnimValueProcessorData(BinaryReader br) : base(br)
    {
        mProcessorType = br.ReadUInt16();
        br.ReadUInt16(); //TODO: Verify (alignment)
    }

    public override void Write(BinaryWriter bw)
    {
        throw new NotImplementedException();
    }
}

// Basically SelectorClip

// Basically ConditionBoolClip