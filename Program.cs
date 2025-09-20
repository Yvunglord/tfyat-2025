using System;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Text;

namespace Tfyat
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\k_yak\source\repos\tfyat\in.txt";
            string output = @"C:\Users\k_yak\source\repos\tfyat\output.txt";
            Dictionary<string, List<int>> res = Foo(path);
            WriteResult(res, output, path);
        }

        static Dictionary<string, List<int>> Foo(string path)
        {
            Dictionary<string, List<int>> pairs = new Dictionary<string, List<int>>();
            int counter = 0;

            using (var reader = new StreamReader(path))
            { 
                string current = string.Empty;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        counter++;
                        foreach (char c in line)
                        {
                            if (c == ' ')
                            {
                                if (pairs.ContainsKey(current))
                                {
                                    pairs[current].Add(counter);
                                }
                                else
                                {
                                    pairs[current] = new List<int>() { counter };
                                }

                                current = string.Empty;
                            }

                            else
                            {
                                if (char.IsLetterOrDigit(c))
                                    current += c;
                            }
                        }

                        current = string.Empty;
                    }
                    
                }
            }

            return pairs;
        }

        static void WriteResult(Dictionary<string, List<int>> input, string outputPath, string inputPath)
        {
            var sorted = input.Keys.OrderBy(k => k);
            using (var writer = new StreamWriter(outputPath))
            {
                foreach (var word in sorted)
                {
                    string line = $"{word} {string.Join(" ", input[word])}";
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine("Done!");
        }
    }
}