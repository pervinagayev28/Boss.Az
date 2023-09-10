namespace Boss.Az
{
    internal class Program
    {
        static bool ColorTemp = true;
        static void Main(string[] args)
        {
            StartUp();
        }
        static string ColorSettings()
        {
            if (ColorTemp)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                ColorTemp = false;
                return "";
            }
            Console.BackgroundColor = default;
            ColorTemp = true;
            return "";
        }
        static void StartUp()
        {
            int start = 1;
            while (true)
            {
                if (start == 1)
                {
                    ColorSettings();
                    Console.WriteLine("Guest");
                    ColorSettings();
                    Console.WriteLine("Worker");
                    Console.WriteLine("Employer");
                }
                else if (start == 2)
                {
                    Console.WriteLine("Guest");
                    ColorSettings();
                    Console.WriteLine("Worker");
                    ColorSettings();
                    Console.WriteLine("Employer");
                }
                else if (start == 3)
                {
                    Console.WriteLine("Guest");
                    Console.WriteLine("Worker");
                    ColorSettings();
                    Console.WriteLine("Employer");
                    ColorSettings();
                }


                var key = Console.ReadKey();
                switch (key.Key)
                {

                    case ConsoleKey.DownArrow:
                        if (start < 3)
                            start++;
                        else
                            start = 1;
                        break;
                    case ConsoleKey.UpArrow:
                        if (start > 1)
                            start--;
                        else
                            start = 3;
                        break;
                    case ConsoleKey.Enter:
                        if(start ==1)

                        break;
                }
                Console.Clear();
            }
        }
    }
}
