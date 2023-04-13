using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace WordsCounter
{
    class Program
    {
        static void Main(string[] args)
        {          
            string inputPath = @"C:\Users\User\Desktop\WaP.txt";
            string outputPath = @"C:\Users\User\Desktop\outputFile.txt";

            WriteFile(CountUniqueWords(ReadFile(inputPath)), outputPath);

        }


        public static string ReadFile(string inputPath)
        {
            var originalText = string.Empty;

            if (File.Exists(inputPath))
            {
                originalText = File.ReadAllText($"{inputPath}");                
            }
            else
                Console.WriteLine("Файл не найден");

            return originalText;
        }


        public static Dictionary<string, int> CountUniqueWords(string originalText)
        {
            Dictionary<string, int> dicUniqueWords = new Dictionary<string, int>();

            if (!string.IsNullOrEmpty(originalText))
            {
                var newText = Regex.Replace(originalText.ToLower(), "[-.?!)(,:]|[][]|(\r\n)", "");

                string[] words = newText.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {
                    if (!Int32.TryParse(word, out int resul) && !string.IsNullOrEmpty(word))
                    {
                        if (dicUniqueWords.ContainsKey(word))
                        {
                            dicUniqueWords[word]++;
                        }
                        else
                            dicUniqueWords.Add(word, 1);
                    }

                }
                var sortedDictionary = dicUniqueWords.OrderBy(x=>x.Value).Reverse().ToDictionary(x=>x.Key,x=>x.Value);

                return sortedDictionary;
            }

            return null;    
           
        }


        public static void WriteFile(Dictionary<string, int> dicWords, string outputPath)
        {
            
            using (StreamWriter streamWriter = new StreamWriter(outputPath))
            {
                foreach (var item in dicWords)
                {
                    streamWriter.WriteLine($"{item.Key} - {item.Value}");
                }
            }

            // File.WriteAllText(path, string.Join("\r\n", dicWords));
        }
    }
}
