using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardox
{
    class EngAl : BaseAl
    {
        public EngAl() : base("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz") { }

        public override string Convert(BaseAl targetAl, string input)
        {
            Console.WriteLine("Converting from English Alphabet to Custom Alphabet:");
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
