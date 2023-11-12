using System.Diagnostics;

namespace blndrer;

public class BlendFile: Writable
{
    public BinaryHeader Header;
    public PoolData Pool;
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