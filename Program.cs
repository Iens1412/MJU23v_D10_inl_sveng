﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new List<SweEngGloss>();
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)
                    {
                        load_file(argument);
                    }
                    else if(argument.Length == 1)
                    {
                        load_dafault(defaultFile);
                    }
                }
                else if (command == "list")
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new") 
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1)
                    {
                        NewWord();
                    }
                }
                else if (command == "delete") 
                {
                    if (argument.Length == 3)
                    {
                        delete_wrod(argument);
                    }else if(argument.Length == 2)
                    {
                        delete_word_two_argument(argument);
                    }
                    else if (argument.Length == 1)
                    {
                        delete();
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        translate_word(argument);
                    }
                    else if (argument.Length == 1)
                    {
                        translate();
                    }
                }
                else if(command == "help")
                {
                    //NYI: add help commnd and content
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);

            static void load_file(string[] argument)
            {
                try
                {
                    string path = $"..\\..\\..\\dict\\{argument[1]}";

                    using (StreamReader reader = new StreamReader(path))
                    {
                        dictionary = new List<SweEngGloss>(); // Empty it!
                        string line = reader.ReadLine();
                        while (line != null)
                        {
                            SweEngGloss gloss = new SweEngGloss(line);
                            dictionary.Add(gloss);
                            line = reader.ReadLine();
                        }
                    }
                } catch (FileNotFoundException e) { Console.WriteLine("File Not Found??"); }
                
            }

            static void load_dafault(string defaultFile)
            {
                using (StreamReader reader = new StreamReader(defaultFile)) //FIXME: System.IO.FileNotFoundException
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = reader.ReadLine();
                    }
                }
            }

            static void NewWord()
            {
                Console.Write("Write word in Swedish: ");
                string swedish_word = Console.ReadLine();
                Console.Write("Write word in English: ");
                string english_word = Console.ReadLine();
                dictionary.Add(new SweEngGloss(swedish_word, english_word));
            }

            static void delete_wrod(string[] argument)
            {

                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                        index = i;
                }
                if (index != -1)
                {
                    dictionary.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("this words does not exsist!!!");
                }

            }

            static void delete()
            {
                Console.Write("Write word in Swedish: ");
                string swedish_word = Console.ReadLine();
                Console.Write("Write word in English: ");
                string english_word = Console.ReadLine();
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == swedish_word && gloss.word_eng == english_word)
                        index = i;
                }
                if (index != -1)
                {
                    dictionary.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("this words does not exsist!!!");
                }
            }

            static void translate_word(string[] argument)
            {
                foreach (SweEngGloss gloss in dictionary)
                {
                    if (gloss.word_swe == argument[1])
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == argument[1])
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }

            static void translate()
            {
                Console.Write("Write word to be translated: ");
                string word_to_trnslate = Console.ReadLine();
                foreach (SweEngGloss gloss in dictionary)
                {
                    if (gloss.word_swe == word_to_trnslate)
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == word_to_trnslate)
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }

            static void delete_word_two_argument(string[] argument)
            {
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == argument[1])
                        index = i;
                }
                if (index != -1)
                {
                    dictionary.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("this words does not exsist!!!");
                }
            }
        }
    }
}