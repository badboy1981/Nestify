using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SaveSystem
{
    public static class JsonFileRW
    {
        public static string Read(string FilePath)
        {
            using var Reader = new StreamReader(FilePath);
            return Reader.ReadToEnd();
        }
        public static void Write(string FilePath, string Value)
        {
            using var Writer = new StreamWriter(FilePath);
            Writer.Write(Value);
        }
    }
}