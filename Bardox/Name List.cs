using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardox
{
    class NameList
    {
        public static void Namesave(string name, string filepath = "C:\\Users\\User\\source\\repos\\Bardox\\NameList.txt")
        {
            try
            {
                if (!File.ReadAllLines(filepath).Contains(name))
                {
                    using (StreamWriter writer = File.AppendText(filepath))
                    {
                        writer.WriteLine(name);
                    }
                }
                else
                {
                    Console.WriteLine("The name is already in the file. Proceding to updating the file");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        public static List<string> Nameload(string filepath = "C:\\Users\\User\\source\\repos\\Bardox\\NameList.txt")
        {
            List<string> names = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        names.Add(line);
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
            return names;
        }
        public static List<string> Nameremove(string name, string filepath = "C:\\Users\\User\\source\\repos\\Bardox\\NameList.txt")
        {
            List<string> names = new List<string>();
            try
            {
                names = File.ReadAllLines(filepath).ToList();
                if (names.Contains(name))
                {
                    names.Remove(name);
                    File.WriteAllLines(filepath, names);
                }
                else
                {
                    Console.WriteLine("Name not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
            return names;
        }
    }
}
