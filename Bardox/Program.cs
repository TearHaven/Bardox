using System;
using System.IO;
using System.Linq;
using System.Text;
using Bardox;
using Spectre.Console;

namespace Bardox
{
    class Program
    {
        static void Main()
        {
            EngAl engAl = new EngAl();
            string input;
            List<string> name = NameList.Nameload();
            CusAl[] cusal = new CusAl[name.Count + 10];
            int choice = 0;
            int cnt = 0;
            bool loop = true;
            char[] temp = new char[26];
            for (int q = 0; q < name.Count; q++)
            {
                var customAlphabet = CusAl.Load($"{name[q]}.txt");
                if (customAlphabet == null)
                {
                    Console.WriteLine($"Failed to load {name[q]}. Skipping...");
                    continue;
                }
                cusal[q] = customAlphabet;
            }

            try
            {
                do
                {
                Mainmenu:
                    Console.Clear();
                    Console.WriteLine("   ===================");
                    Console.WriteLine("        Main Menu");
                    Console.WriteLine("   ===================\n\n");
                    
                    var strings = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .AddChoices(new[] {"Encode","Create/Edit Alphabet", "Decode", "Remove Alphabet",    "Exit" })
                    );
                    Dictionary<string, int> mainmenu= new Dictionary<string, int>            
                    {
                        { "Encode",1},{"Create/Edit Alphabet", 2},{"Decode", 3},{"Remove Alphabet", 4},{"Exit", 5 }
                    };
                    Dictionary<string, int> alph = new Dictionary<string, int>
                    {
                        {"English Alphabet", 1 },{"Custom Alphabet", 2}, {"Back", 3}
                    };
                    choice=mainmenu.GetValueOrDefault(strings);

                    switch (choice)
                    {
                        case 1:
                        Encode:
                            Console.Clear();
                            Console.WriteLine("==========");
                            Console.WriteLine("  Encode");
                            Console.WriteLine("==========\n\n");
                            choice = 0;
                                Console.WriteLine("Input the words to be encoded:\n");
                                input = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"Inputted word: {input}");
                                do
                                {
                                    strings = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .PageSize(10)
                                        .AddChoices(new[] {"English Alphabet", "Custom Alphabet", "Back"})
                                    );
                                    choice = alph.GetValueOrDefault(strings);
                                    switch (choice)
                                    {
                                    case 1:
                                            input = encode(input);
                                        Console.WriteLine("\nPress Y to decode");
                                        if (Console.ReadKey().Key == ConsoleKey.Y)
                                        {
                                            Console.ReadLine();
                                            input=decode(input);
                                            Console.WriteLine(input);
                                        }
                                        else
                                        {
                                            Console.ReadLine();
                                            break;
                                        }
                                           
                                        Console.WriteLine("Press Y to encode another message");
                                        if (Console.ReadKey().Key == ConsoleKey.Y)
                                        {
                                            Console.ReadLine();
                                            goto Encode;
                                        }
                                        else
                                        {
                                            Console.ReadLine();
                                            Console.WriteLine("Returning to main menu...");
                                            Console.Beep(37, 1000);
                                            break;
                                        }

                                    case 2:
                                            if (name != null && name.Count > 0)
                                            {

                                                Console.WriteLine("Please choose from the following Alphabets.");
                                                var prompt = new SelectionPrompt<string>()
                                                    .PageSize(10);

                                                for (int i = 0; i < name.Count; i++)
                                                {
                                                    prompt.AddChoice(name[i]);
                                                }
                                                prompt.AddChoice("Back");
                                                strings = AnsiConsole.Prompt(prompt);
                                                Dictionary<string, int> avail = new Dictionary<string, int>{};
                                                for (int i = 0; i < name.Count; i++)
                                                {
                                                    if (avail.ContainsKey(name[i]))
                                                        continue;
                                                    else
                                                        avail.Add(name[i], i+1);
                                                }
                                                choice = avail.GetValueOrDefault(strings);
                                                if (strings=="Back")
                                                    continue;
                                                cusal[choice - 1] = CusAl.Load($"{name[choice - 1]}.txt");
                                                if (cusal[choice - 1] == null)
                                                    Console.WriteLine("this alphabet is unavailable.");
                                                else
                                                {
                                                    input = engAl.Convert(cusal[choice - 1], input);
                                                    input = encode(input);
                                                    Console.WriteLine("\nPress Y to decode");
                                                    if (Console.ReadKey().Key == ConsoleKey.Y)
                                                    {
                                                        Console.ReadLine();
                                                        input = decode(input);
                                                        input = cusal[choice - 1].Convert(engAl, input);
                                                        Console.WriteLine(input);
                                                    }
                                                }
                                            Console.WriteLine("Press Y to encode another message");
                                            if (Console.ReadKey().Key == ConsoleKey.Y)
                                            {
                                                Console.ReadLine();
                                                goto Encode;
                                            }
                                            else
                                            {
                                                Console.ReadLine();
                                                Console.WriteLine("Returning to main menu...");
                                                Console.Beep(37, 1000);
                                                break;
                                            }
                                            }
                                            else
                                            {
                                                choice = 0;
                                                Console.WriteLine("You dont have any Custom Alphabets");
                                                continue;
                                            }
                                        case 3:
                                        goto Mainmenu;
                                        default:
                                            choice = 0;
                                            Console.WriteLine("Invalid input please try again!");
                                            continue;

                                    }
                                } while (choice == 0);
                            break;
                        case 2:
                            choice = 0;
                            Console.Clear();
                            if (name != null && name.Count > 0)
                            {
                                Console.WriteLine("========================");
                                Console.WriteLine("  Available alphabets.");
                                Console.WriteLine("========================\n\n");
                                var prompt =new SelectionPrompt<string>()
                                        .PageSize(10);

                                for(int i = 0; i < name.Count; i++)
                                {
                                    prompt.AddChoice(name[i]);
                                }

                                prompt.AddChoice("Create Alphabet");
                                prompt.AddChoice("Back");
                                strings = AnsiConsole.Prompt(prompt);
                                Dictionary<string, int> avail= new Dictionary<string, int> 
                                {
                                 
                                };
                                
                                for (int i = 0; i < name.Count; i++)
                                {
                                    if (i==name.Count-1)
                                    avail.Add("Back", i + 1);
                                    if (avail.ContainsKey(name[i]))
                                        continue;
                                    else
                                        avail.Add(name[i], i+1);
                                    
                                }
                                choice = avail.GetValueOrDefault(strings);

                            }
                            else
                            {
                                Console.WriteLine("You don't have an alphabet");
                            }
                            if (strings == "Back")
                                goto Mainmenu;
                            else if (choice == 0 || choice > name.Count)
                            {

                                Console.Clear();
                                Console.WriteLine("What would be the name of your alphabet?\n\n");
                                name.Add(Console.ReadLine());
                                NameList.Namesave(name[name.Count-1]);

                                temp = new char[engAl.Up.Length];

                                for (int j = 0; j < engAl.Up.Length; j++)
                                {
                                    Console.Clear();
                                    Console.Write($"What would you like to replace {engAl.Up[j]} with?: ");
                                    temp[j] = Console.ReadKey().KeyChar;
                                    Console.ReadLine();
                                    int index = engAl.Up.IndexOf(char.ToUpper(temp[j]));
                                    if (index == -1)
                                    {
                                        Console.WriteLine("Invalid input, please try again!");
                                        j--;
                                        Console.Beep(37, 1000);
                                    }
                                    for (int i = 0; i < j; i++)
                                        if (temp[i] == temp[j])
                                        {
                                            Console.WriteLine("Invalid input, please try again!");
                                            j--;
                                            Console.Beep(37, 1000);
                                            break;
                                        }
                                }

                                //alphabet display
                                if (name != null)
                                {
                                    cusal[name.Count-1] = new CusAl(new string(temp).ToUpper(), new string(temp).ToLower());
                                    cusal[name.Count - 1].Save($"{name[name.Count-1]}.txt");
                                    position(display(name[name.Count-1], cusal[ name.Count-1]));
                                }
                                else
                                {
                                    cusal[cnt] = new CusAl(new string(temp).ToUpper(), new string(temp).ToLower());
                                    cusal[cnt].Save($"{name[name.Count-1]}.txt");
                                    position(display(name[cnt], cusal[cnt]));
                                }
                                Console.WriteLine("Press Enter to return to main menu");
                                if (Console.ReadKey().Key==ConsoleKey.Enter) 
                                {
                                    Console.WriteLine("Returning to main menu...");
                                    Console.Beep(37, 1000);
                                    cnt++;
                                    break;
                                }
                            }
                            else if (choice <= name.Count && name.Count > 0)
                            {
                                int index = 0, index2 = 0;
                                char l, k, h;
                                do
                                {
                                    position(display(name[choice - 1], cusal[choice - 1]));
                                    temp = cusal[choice - 1].Up.ToCharArray();
                                    Console.WriteLine("Which letter would you like to replace?");
                                    l = Console.ReadKey().KeyChar;
                                    Console.ReadLine();
                                    index = cusal[choice - 1].Up.IndexOf(char.ToUpper(l));
                                    if (index == -1)
                                    {
                                        Console.WriteLine("Letter not found, please try again!");
                                        continue;
                                    }
                                    Console.WriteLine($"Letter {l} is currently at slot {index + 1}, note that whatever you change letter {l} with will be replaced by that letter {l}");
                                    Console.Write($"What would you like to replace {l} with?: ");
                                    k = Console.ReadKey().KeyChar;
                                    Console.ReadLine();
                                    index2 = cusal[choice - 1].Up.IndexOf(char.ToUpper(k));
                                    if (index2 == -1)
                                    {
                                        Console.WriteLine("Replacement letter not found, please try again!");
                                        continue;
                                    }
                                    h = temp[index];
                                    temp[index] = temp[index2];
                                    temp[index2] = h;
                                    cusal[choice - 1] = new CusAl(new string(temp).ToUpper(), new string(temp).ToLower());
                                    cusal[choice - 1].Save($"{name[choice - 1]}.txt");
                                    //alphabet display
                                    position(display(name[choice - 1], cusal[choice - 1]));
                                    Console.WriteLine("would you like to replace another letter?[Y/N]");
                                    if (char.ToLower(Console.ReadKey().KeyChar) == 'y')
                                    {
                                        Console.ReadLine();
                                        continue;
                                    }
                                    else
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("Returning to main menu...");
                                        Console.Beep(37, 1000);
                                        break;
                                    }
                                } while (true);
                            }
                            break;
                        case 3:
                            Decode:
                            Console.Clear();
                            Console.WriteLine("==========");
                            Console.WriteLine("  Decode");
                            Console.WriteLine("==========\n\n");
                            choice = 0;
                            Console.WriteLine("Input the Morse code to be decoded (use '.' for dots, '-' for dashes, and spaces to separate letters):\n");
                            input = Console.ReadLine();
                            do
                            {
                                strings = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .PageSize(10)
                                        .AddChoices(new[] { "English Alphabet", "Custom Alphabet", "Back" })
                                    );
                                choice = alph.GetValueOrDefault(strings);
                                switch (choice)
                                {
                                    case 1:
                                        input = decode(input);
                                        Console.WriteLine(input);
                                        Console.WriteLine("\nPress Y to Decode another message");
                                        if (Console.ReadKey().Key == ConsoleKey.Y)
                                        {
                                            Console.ReadLine();
                                            goto Decode;
                                        }
                                        else
                                        {
                                            Console.ReadLine();
                                            Console.WriteLine("Returning to main menu...");
                                            Console.Beep(37, 1000);
                                            break;
                                        }
                                    case 2:
                                        if (name != null && name.Count > 0)
                                        {

                                            var prompt = new SelectionPrompt<string>()
                                                    .PageSize(10);

                                            for (int i = 0; i < name.Count; i++)
                                            {
                                                prompt.AddChoice(name[i]);
                                            }
                                            prompt.AddChoice("Back");
                                            strings = AnsiConsole.Prompt(prompt);
                                            Dictionary<string, int> avail = new Dictionary<string, int>
                                            {

                                            };
                                            for (int i = 0; i < name.Count; i++)
                                            {
                                                if (avail.ContainsKey(name[i]))
                                                    continue;
                                                else
                                                    avail.Add(name[i], i+1);
                                            }
                                            if (strings == "Back")
                                            {
                                                choice = 0;
                                                continue;
                                            }
                                            choice = avail.GetValueOrDefault(strings);
                                            cusal[choice - 1] = CusAl.Load($"{name[choice - 1]}.txt");
                                            if (cusal[choice - 1] == null)
                                            {
                                                Console.WriteLine("This alphabet is unavailable.");
                                            }
                                            else
                                            {
                                                input = decode(input);
                                                input = cusal[choice - 1].Convert(engAl, input);
                                                Console.WriteLine(input);
                                            }
                                            Console.WriteLine("\nPress Y to Decode another message");
                                            if (Console.ReadKey().Key == ConsoleKey.Y)
                                            {
                                                Console.ReadLine();
                                                goto Decode;
                                            }
                                            else
                                            {
                                                Console.ReadLine();
                                                Console.WriteLine("Returning to main menu...");
                                                Console.Beep(37, 1000);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            choice = 0;
                                            Console.WriteLine("You don't have any Custom Alphabets.");
                                            continue;
                                        }

                                    case 3:
                                        goto Mainmenu;

                                    default:
                                        choice = 0;
                                        Console.WriteLine("Invalid input, please try again!");
                                        continue;
                                }
                            } while (choice == 0);

                            Console.WriteLine("Returning to main menu...");
                            Console.Beep(37, 1000);
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("==========");
                            Console.WriteLine("  Remove");
                            Console.WriteLine("==========\n\n");
                            choice = 0;
                            if (name != null && name.Count > 0)
                            {
                                Console.WriteLine("Wich alphabet would you like to remove?.");
                                var prompt = new SelectionPrompt<string>()
                                    .PageSize(10);

                                for (int i = 0; i < name.Count; i++)
                                {
                                    prompt.AddChoice(name[i]);
                                }
                                prompt.AddChoice("Back");
                                strings = AnsiConsole.Prompt(prompt);
                                if (strings == "Back")
                                    goto Mainmenu;
                                Dictionary<string, int> avail = new Dictionary<string, int> { };
                                for (int i = 0; i < name.Count; i++)
                                {
                                    if (avail.ContainsKey(name[i]))
                                        continue;
                                    else
                                        avail.Add(name[i], i+1);
                                }
                                choice = avail.GetValueOrDefault(strings);
                                remove(name[choice - 1]);
                                name.Remove(name[choice - 1]);
                            }
                            else
                            {
                                Console.WriteLine("You dont have any Custom Alphabets");

                                Console.WriteLine("Returning to main menu...");
                                Console.Beep(37, 1000);
                                break;
                            }

                            Console.WriteLine("Returning to main menu...");
                            Console.Beep(37, 1000);
                            break;
                        case 5:
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input please try again!");
                            continue;
                    }
                } while (loop == true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }





        }
        static void remove(string name)
        {
            NameList.Nameremove(name);
            CusAl.Del($"{name}.txt");
        }
        static string dot()
        {
            Console.Beep(500, 750);
            Console.Write(".");
            return ".";
        }
        static string dash()
        {
            Console.Beep(500, 1500);
            Console.Write("-");
            return "-";
        }
        static string space()
        {
            Console.Beep(37, 1000);
            Console.Write(" ");
            return " ";
        }
        static string divider()
        {
            Console.Write("/");
            return "/";
        }
        static string encode(string input)
        {
            Dictionary<char, Func<string>> charToMorse = new Dictionary<char, Func<string>>
{
    { 'a', () => dot() + dash() + space() },
    { 'b', () => dash() + dot() + dot() + dot() + space() },
    { 'c', () => dash() + dot() + dash() + dot() + space() },
    { 'd', () => dash() + dot() + dot() + space() },
    { 'e', () => dot() + space() },
    { 'f', () => dot() + dot() + dash() + dot() + space() },
    { 'g', () => dash() + dash() + dot() + space() },
    { 'h', () => dot() + dot() + dot() + dot() + space() },
    { 'i', () => dot() + dot() + space() },
    { 'j', () => dot() + dash() + dash() + dash() + space() },
    { 'k', () => dash() + dot() + dash() + space() },
    { 'l', () => dot() + dash() + dot() + dot() + space() },
    { 'm', () => dash() + dash() + space() },
    { 'n', () => dash() + dot() + space() },
    { 'o', () => dash() + dash() + dash() + space() },
    { 'p', () => dot() + dash() + dash() + dot() + space() },
    { 'q', () => dash() + dash() + dot() + dash() + space() },
    { 'r', () => dot() + dash() + dot() + space() },
    { 's', () => dot() + dot() + dot() + space() },
    { 't', () => dash() + space() },
    { 'u', () => dot() + dot() + dash() + space() },
    { 'v', () => dot() + dot() + dot() + dash() + space() },
    { 'w', () => dot() + dash() + dash() + space() },
    { 'x', () => dash() + dot() + dot() + dash() + space() },
    { 'y', () => dash() + dot() + dash() + dash() + space() },
    { 'z', () => dash() + dash() + dot() + dot() + space() },
    { '1', () => dot() + dash() + dash() + dash() + dash() + space() },
    { '2', () => dot() + dot() + dash() + dash() + dash() + space() },
    { '3', () => dot() + dot() + dot() + dash() + dash() + space() },
    { '4', () => dot() + dot() + dot() + dot() + dash() + space() },
    { '5', () => dot() + dot() + dot() + dot() + dot() + space() },
    { '6', () => dash() + dot() + dot() + dot() + dot() + space() },
    { '7', () => dash() + dash() + dot() + dot() + dot() + space() },
    { '8', () => dash() + dash() + dash() + dot() + dot() + space() },
    { '9', () => dash() + dash() + dash() + dash() + dot() + space() },
    { '0', () => dash() + dash() + dash() + dash() + dash() + space() },
    { '.', () => dot() + dash() + dot() + dash() + dot() + dash() + space() },
    { ',', () => dash() + dash() + dot() + dot() + dash() + dash() + space() },
    { '?', () => dot() + dot() + dash() + dash() + dot() + dot() + space() },
    { '\'', () => dot() + dash() + dash() + dash() + dash() + dot() + space() },
    { '!', () => dash() + dot() + dash() + dot() + dash() + dash() + space() },
    { '/', () => dash() + dot() + dot() + dash() + dot() + space() },
    { '(', () => dash() + dot() + dash() + dash() + dot() + space() },
    { ')', () => dash() + dot() + dash() + dash() + dot() + dash() + space() },
    { '&', () => dot() + dash() + dot() + dot() + dot() + space() },
    { ':', () => dash() + dash() + dash() + dot() + dot() + dot() + space() },
    { ';', () => dash() + dot() + dash() + dot() + dash() + dot() + space() },
    { '=', () => dash() + dot() + dot() + dot() + dash() + space() },
    { '+', () => dot() + dash() + dot() + dash() + dot() + space() },
    { '-', () => dash() + dot() + dot() + dot() + dot() + dash() + space() },
    { '_', () => dot() + dot() + dash() + dash() + dot() + dash() + space() },
    { '"', () => dot() + dash() + dot() + dot() + dash() + dot() + space() },
    { '$', () => dot() + dot() + dot() + dash() + dot() + dot() + dash() + space() },
    { '@', () => dot() + dash() + dash() + dot() + dash() + dot() + space() },
    { ' ', () => space() + divider() + space()}  // Space handling
};

            StringBuilder encodedText = new StringBuilder();
            string[] words = input.ToLower().Split(' ');

            foreach (var word in words)
            {
                foreach (var character in word)
                {
                    if (charToMorse.TryGetValue(character, out Func<string> morseCode))
                    {
                        encodedText.Append(morseCode() + " ");
                    }
                    else
                    {
                        encodedText.Append("? ");
                    }
                }
                Console.Write(" / ");
                encodedText.Append("/ ");
            }
            return encodedText.ToString().TrimEnd(' ', '/');
        }
        static string decode(string input)
        {
            Dictionary<string, char> morseToChar = new Dictionary<string, char>
    {
        { ".-", 'a' }, { "-...", 'b' }, { "-.-.", 'c' }, { "-..", 'd' },
        { ".", 'e' }, { "..-.", 'f' }, { "--.", 'g' }, { "....", 'h' },
        { "..", 'i' }, { ".---", 'j' }, { "-.-", 'k' }, { ".-..", 'l' },
        { "--", 'm' }, { "-.", 'n' }, { "---", 'o' }, { ".--.", 'p' },
        { "--.-", 'q' }, { ".-.", 'r' }, { "...", 's' }, { "-", 't' },
        { "..-", 'u' }, { "...-", 'v' }, { ".--", 'w' }, { "-..-", 'x' },
        { "-.--", 'y' }, { "--..", 'z' }, { ".----", '1' }, { "..---", '2' },
        { "...--", '3' }, { "....-", '4' }, { ".....", '5' }, { "-....", '6' },
        { "--...", '7' }, { "---..", '8' }, { "----.", '9' }, { "-----", '0' },
        { ".-.-.-", '.' }, { "--..--", ',' }, { "..--..", '?' }, { ".----.", '\'' },
        { "-.-.--", '!' }, { "-..-.", '/' }, { ".--.-.", '@' }, { ".-...", '&' },
        { "---...", ':' }, { "-.-.-.", ';' }, { "..--.-", '_' }, { "-...-", '=' },
        { ".-.-.", '+' }, { "-....-", '-' }, { ".-..-.", '\"' }, { "-.--.", '(' },
        { "-.--.-", ')' },{" / ",' '}
    };

            StringBuilder decodedText = new StringBuilder();

            string[] morseWords = input.Split(new[] { " / " }, StringSplitOptions.None);
            foreach (var word in morseWords)
            {
                string[] morseChars = word.Split(' ');

                foreach (var morseChar in morseChars)
                {
                    if (morseToChar.TryGetValue(morseChar, out char decodedChar))
                    {
                        decodedText.Append(decodedChar);
                    }
                }

                decodedText.Append(' ');
            }

            return decodedText.ToString().Trim();
        }
        static string display(string name, CusAl cusal)
        {

            Console.Clear();
            for (int e = 0; e < name.Length + 4; e++)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n");
            Console.WriteLine(name);
            for (int e = 0; e < name.Length + 4; e++)
            {
                Console.Write("=");
            }
            //alphabet
            return               "\nAlphabet slots:"+
                                $"\nslot 1: {cusal.Up[0]}      slot 6: {cusal.Up[5]}       slot 11: {cusal.Up[10]}     slot 16: {cusal.Up[15]}     slot 21: {cusal.Up[20]}     slot 26: {cusal.Up[25]}" +
                                $"\nslot 2: {cusal.Up[1]}      slot 7: {cusal.Up[6]}       slot 12: {cusal.Up[11]}     slot 17: {cusal.Up[16]}     slot 22: {cusal.Up[21]}" +
                                $"\nslot 3: {cusal.Up[2]}      slot 8: {cusal.Up[7]}       slot 13: {cusal.Up[12]}     slot 18: {cusal.Up[17]}     slot 23: {cusal.Up[22]}" +
                                $"\nslot 4: {cusal.Up[3]}      slot 9: {cusal.Up[8]}       slot 14: {cusal.Up[13]}     slot 19: {cusal.Up[18]}     slot 24: {cusal.Up[23]}" +
                                $"\nslot 5: {cusal.Up[4]}      slot 10: {cusal.Up[9]}      slot 15: {cusal.Up[14]}     slot 20: {cusal.Up[19]}     slot 25: {cusal.Up[24]}"
                                ;
        }
        static void position( string msg)
        {

                Console.SetCursorPosition(25, 0);
                Console.Write(msg);
                Console.WriteLine("\n");
            
        }
    }
}