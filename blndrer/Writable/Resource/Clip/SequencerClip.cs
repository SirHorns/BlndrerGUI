﻿namespace blndrer.Writable.Resource.Clip;

class SequencerClip : ClipData
{
    public uint mTrackIndex;
    public uint mNumPairs;
    public SequencerClip(BinaryReader br) : base(br)
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