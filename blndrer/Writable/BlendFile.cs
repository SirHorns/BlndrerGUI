using System.Diagnostics;
using blndrer.Writable;
using blndrer.Writable.Resource;

namespace blndrer;

public class BlendFile: Writable.Writable
{
    public BinaryHeader Header { get; set; }
    public PoolData Pool { get; set; }

    public BlendFile()
    {
    }

    public BlendFile(BinaryHeader header, PoolData pool)
    {
        Header = header;
        Pool = pool;
    }

    public BlendFile(BinaryReader br)
    {
        Header = br.Read<BinaryHeader>();
        Debug.Assert(
            Header.mEngineType == 845427570u &&
            Header.mBinaryBlockType == 1684958306u &&
            Header.mBinaryBlockVersion == 1u
        );
        Pool = br.Read<PoolData>();
    }
    
    public override void Write(BinaryWriter bw)
    {
        bw.Write(Header);
        bw.Write(Pool);
    }
}