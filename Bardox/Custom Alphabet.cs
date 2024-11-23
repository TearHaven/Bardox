using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardox
{
    class CusAl : BaseAl
    {
        public CusAl(string up, string low) : base(up, low) { }
        public static CusAl Load(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string up = reader.ReadLine();
                    string low = reader.ReadLine();

                    if (!string.IsNullOrEmpty(up) && !string.IsNullOrEmpty(low))
                    {
                        return new CusAl(up, low);
                    }
                    else
                    {
                        throw new InvalidOperationException("File does not contain valid alphabet data.");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: File not found.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public void Save(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(this.Up);
                    writer.WriteLine(this.Low);
                }
                Console.WriteLine($"saved file at {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        public static void Del(string filepath)
        {
            try
            {
                File.Delete(filepath);
                Console.WriteLine("File deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }


        public override string Convert(BaseAl targetAl, string input)
        {
            Console.WriteLine("Conversion from Custom Alphabet to English Alphabet:");
            return ConvertAlphabet(targetAl, input);
        }


        private string ConvertAlphabet(BaseAl targetAl, string input)
        {
            char[] converted = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                if (char.IsUpper(currentChar))
                {
                    int index = up.IndexOf(currentChar);
                    if (index != -1)
                    {
                        converted[i] = targetAl.Up[index];
                    }
                }
                else if (char.IsLower(currentChar))
                {
                    int index = low.IndexOf(currentChar);
                    if (index != -1)
                    {
                        converted[i] = targetAl.Low[index];
                    }
                }
                else
                {
                    converted[i] = currentChar;
                }
            }

            return new string(converted);
        }
    }
}
