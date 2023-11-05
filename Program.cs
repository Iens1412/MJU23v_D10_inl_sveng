namespace MJU23v_D10_inl_sveng
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
                else if (command == "new") //TODO: else if argument == 2
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
                else if (command == "delete") //FIXME: System.ArgumentOutOfRangeException + else if argument == 2
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
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
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
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
                }else if(command == "help")
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
                using (StreamReader reader = new StreamReader(argument[1]))
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
        }
    }
}