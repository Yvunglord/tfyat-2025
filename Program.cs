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
            Dictionary<string, HashSet<int>> res = ProcessInputFile(path);
            WriteResult(res, output, path);
        }

        static Dictionary<string, HashSet<int>> ProcessInputFile(string path)
        {
            Dictionary<string, HashSet<int>> wordsToLines = new Dictionary<string, HashSet<int>>();
            int counter = 0;

            using (var reader = new StreamReader(path))
            { 
                string current = string.Empty;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        line = line.ToLower();
                        counter++;

                        foreach (char c in line)
                        {
                            if (c == ' ' && current.Length > 0)
                            {
                                AddWordOrCounter(wordsToLines, current, counter);

                                current = string.Empty;
                            }

                            else
                            {
                                if (char.IsLetterOrDigit(c))
                                    current += c;
                            }
                        }

                        if (!string.IsNullOrEmpty(current))
                        {
                            AddWordOrCounter(wordsToLines, current, counter);
                        }
                        current = string.Empty;
                    }
                    
                }
            }

            return wordsToLines;
        }

        static void AddWordOrCounter(Dictionary<string, HashSet<int>> wordsToLines, string word, int counter)
        {
            if (!wordsToLines.ContainsKey(word))
            {
                wordsToLines[word] = new HashSet<int>() { counter };
                return;
            }

            wordsToLines[word].Add(counter);
        }

        static void WriteResult(Dictionary<string, HashSet<int>> input, string outputPath, string inputPath)
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