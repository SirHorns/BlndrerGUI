﻿namespace BlndrerGUI.Models;

public class BlndFile
{
    public Header Header { get; init; }
    public Pool Pool { get; init; }
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
    public string Skeleton{ get; set; }
    public string[] AnimNames{ get; set; }
    public uint ResourceSize{ get; set; }
}