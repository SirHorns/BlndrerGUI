namespace blndrer;

public class Program
{
    public static void Main(string[] args)
    {
        var f1 = BlndTools.ReadBLND("Jinx.blnd");
        //f1.Pool.mBlendDataAry = null;
        //f1.Pool.mTransitionData = null;
        //f1.Pool.mBlendTrackAry = null;
        //f1.Pool.mClassAry = null;
        //f1.Pool.mMaskDataAry = null;
        //f1.Pool.mEventDataAry = null;
        //f1.Pool.mAnimDataAry = null;
        //f1.Pool.mAnimNames = null;

        BlndTools.WriteJSON(f1, "Jinx.blnd.json");
        BlndTools.WriteBLND(f1, "Jinx.2.blnd");
        var f2 = BlndTools.ReadBLND("Jinx.2.blnd");
        BlndTools.WriteJSON(f2, "Jinx.2.blnd.json");
    }
}