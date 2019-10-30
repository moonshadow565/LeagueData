using System;
using System.Text;
using System.Numerics;
using System.IO;

namespace LeagueData
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var d in Directory.EnumerateDirectories("C:/lol_420/Game/DATA/Characters"))
            {
                var name = d.Substring(d.LastIndexOf("\\") + 1);
                var iniPath = $"{d}/{name}.inibin";
                if(File.Exists(iniPath))
                {
                    using var file = new BinaryReader(File.OpenRead(iniPath));
                    var ini = new IniBin(file);
                    var chardata = new CharData(name, ini);
                    Console.WriteLine($"Read: {name}");
                }
            }
        }
    }
}
