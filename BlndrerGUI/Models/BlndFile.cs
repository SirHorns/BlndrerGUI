namespace BlndrerGUI.Models;

public class BlndFile
{
    public Header Header { get; init; }
    public Pool Pool { get; init; }
    
    public BlndFile(){}
    
    public BlndFile(Header header, Pool pool)
    {
        Header = header;
        Pool = pool;
    }
}

public class Header
{
    public long EngineType { get; set; }
    public long BinaryBlockType { get; set; }
    public uint BinaryBlockVersion { get; set; }
}

public class Pool
{
    public long FormatToken{ get; set; }
    public uint Version{ get; set; }
    public bool UseCascadeBlend{ get; set; }
    public float CascadeBlendValue{ get; set; }
    public BlendTrack[] BlendTrackAry { get; set; }
    public string Skeleton{ get; set; }
    public string[] AnimNames{ get; set; }
    public uint ResourceSize{ get; set; }
}

public class BlendTrack
{
    public float BlendWeight { get; set; }
    public uint BlendMode { get; set; }
    public uint Index { get; set; }
    public string Name { get; set; }
    public uint ResourceSize { get; set; }
}