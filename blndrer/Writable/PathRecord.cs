namespace blndrer.Writable;

public class PathRecord : Writable
{
    public string Path { get; set; }

    public PathRecord(string path)
    {
        this.Path = path;
    }

    public PathRecord(BinaryReader br)
    {
        uint pathHash = br.ReadUInt32();
        long pathOffset = br.ReadAddr(); //TODO: Verify

        long prevPosition = br.BaseStream.Position;
        br.BaseStream.Position = pathOffset;
        if(pathOffset != 0) Path = br.ReadCString();
        br.BaseStream.Position = prevPosition;
    }

    public override void Write(BinaryWriter bw)
    {
        int c() => (int)bw.BaseStream.Position;
        bw.Write(HashFunctions.HashStringFNV1a(Path));
        bw.Write(Memory.Allocate(c(), Path));
    }
}