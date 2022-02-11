using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HitCalculation
{
    class Output
    {
        public static void OutputText(List<string> debugText)
        {
            string path = @"C:\temp\debugOutput.txt";
            using (StreamWriter sw = File.AppendText(path))

                foreach (string line in debugText)
                {
                    sw.WriteLineAsync(line);
                }
        }
    }
}
