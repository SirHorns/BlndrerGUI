using System.Text;
using Newtonsoft.Json;

namespace blndrer;

public static class BlndTools
{
    public static BlendFile ReadBLND(string path)
    {
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (var br = new BinaryReader(fs, Encoding.Default))
            return br.Read<BlendFile>();
    }
    
    public static void WriteJSON(BlendFile file, string path)
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(file, Formatting.Indented));
    }
    
    public static void WriteBLND(BlendFile file, string path)
    {
        File.WriteAllBytes(path, Memory.Process(bw => Memory.Allocate(0, file)));
    }
}