using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MathGameArvid
{
    public class Program
    {
        // Medlemsvariabler
        public static bool gameOver = false;
        public static int tal1;
        public static int tal2;
        public static int points;
        public static int antalRatt;
        public static int antalRattHistory;
        public static int antalFragor;
        public static int difficulty = 45; // easy as standard
        public static List<string> GameHistory = new List<string>();

        public static bool isTimeUp = false;

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Svårghetsgrad är inställd på Easy (45 sekunder) som standard.");
            while (!gameOver)
            {
                int val = UserChoice();
                MenuSelection(val);
            }
        }

        public static int UserChoice()
        {
            while (true)
            {
                Console.WriteLine("Välj operation" +
                        "\n1. Addition" +
                        "\n2. Subtraktion" +
                        "\n3. Multiplikation" +
                        "\n4. Division" +
                        "\n5. Random" +
                        "\n6. Visa historik" +
                        "\n7. Välj svårighetsgrad" +
                        "\n8. Avsluta");

                if (int.TryParse(Console.ReadLine(), out int val) && val >= 1 && val <= 8)
                {
                    return val;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning, vänligen välj alternativ 1-8");
                }
            }
        }

        public static void GameStartMessage(int difficulty)
        {
            Console.WriteLine($"Spelet börjar nu och du har {difficulty} sekunder på dig att få så många rätt som möjigt!");
        }
        public static void MenuSelection(int userInput)
        {
            switch (userInput)
            {
                case 1: // Addition
                    GameStartMessage(difficulty);
                    StartGameWithTimer(difficulty, userInput);
                    break;
                case 2: // Subtraktion
                    GameStartMessage(difficulty);
                    StartGameWithTimer(difficulty, userInput);
                    break;
                case 3: // Multiplikation
                    GameStartMessage(difficulty);
                    StartGameWithTimer(difficulty, userInput);
                    break;
                case 4: // Division
                    GameStartMessage(difficulty);
                    StartGameWithTimer(difficulty, userInput);
                    break;
                case 5: // Random
                    Random rnd = new Random();
                    int randomChoice = rnd.Next(1, 5);

                    switch (randomChoice)
                    {
                        case 1:
                            Console.WriteLine("Addition!");
                            userInput = 1;
                            GameStartMessage(difficulty);
                            StartGameWithTimer(difficulty, userInput);
                            break;

                        case 2: // Subtraktion
                            Console.WriteLine("Subtraktion!");
                            userInput = 2;
                            GameStartMessage(difficulty);
                            StartGameWithTimer(difficulty, userInput);
                            break;

                        case 3: // Multiplikation
                            Console.WriteLine("Multiplikation!");
                            userInput = 3;
                            GameStartMessage(difficulty);
                            StartGameWithTimer(difficulty, userInput);
                            break;

                        case 4: // Division
                            Console.WriteLine("Division!");
                            userInput = 4;
                            GameStartMessage(difficulty);
                            StartGameWithTimer(difficulty, userInput);
                            break;
                    }
                    break;
                case 6:
                    VisaHistorik();
                    break;
                case 7:
                    ChangeDifficulty();
                    break;
                case 8: // Exit
                    gameOver = true;
                    break;
                default:
                    break;
            }
        }

        public static void ChangeDifficulty()
        {

            while (true)
            {
                Console.WriteLine("Välj svårighetsgrad:" +
                            "\n1. Easy, 45 sekunder" +
                            "\n2. Medium, 30 sekunder" +
                            "\n3. Hard, 15 sekunder");

                if (int.TryParse(Console.ReadLine(), out int svarighet) && svarighet >= 1 && svarighet <= 3)
                {
                    switch(svarighet)
                    {
                        case 1:
                            difficulty = 45;
                            Console.WriteLine("Svårighetsgrad är satt på Easy!");
                            break;
                        case 2:
                            difficulty = 30;
                            Console.WriteLine("Svårighetsgrad är satt på Medium!");
                            break;
                        case 3:
                            difficulty = 15;
                            Console.WriteLine("Svårighetsgrad är satt på Hard!");
                            break;

                    }
                    return;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning, vänligen välj alternativ 1-3");
                }
            }
        }
        public static void VisaHistorik()
        {
            Console.WriteLine(string.Join("\n", GameHistory));
            Console.WriteLine($"Du har fått {antalRattHistory} frågor rätt av {antalFragor} och du har {points} poäng.");
        }

        public static void StartGameWithTimer(int seconds, int val)
        {
            // Initialize a cancellation token source to stop tasks when time is up
            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Run(() => StartTimer(seconds, cts));

            // Game loop to solve math problems
            while (!cts.Token.IsCancellationRequested)
            {
                switch (val)
                {
                    case 1:
                        Addition();
                        break;

                    case 2: // Subtraktion
                        Subtraktion();
                        break;

                    case 3: // Multiplikation
                        Multiplikation();
                        break;

                    case 4: // Division
                        Division();
                        break;
              }

            }

            Console.WriteLine($"Time's up! Du fick {antalRatt} svar korrekt och fick {points} poäng.");

            antalRatt = 0;
        }

        public static void StartTimer(int seconds, CancellationTokenSource cts)
        {
            Thread.Sleep(seconds * 1000);
            cts.Cancel();  // Signal the cancellation of tasks
        }

        public static void Addition()
        {
            Random rnd = new Random();
            tal1 = rnd.Next(1, 101);
            tal2 = rnd.Next(1, 101);

            antalFragor++;

            Console.WriteLine($"{tal1} + {tal2} = ?");
            if (int.TryParse(Console.ReadLine(), out int svar))
            {
                int korrektSvar = tal1 + tal2;
                if (svar == korrektSvar)
                {
                    Console.WriteLine($"{svar} var rätt!");
                    GameHistory.Add($"{tal1} + {tal2} = {korrektSvar}");
                    points += 5;
                    antalRatt++;
                    antalRattHistory++;
                }
                else
                {
                    Console.WriteLine($"Fel. {korrektSvar} är korrekt.");
                }
            }
            else
            {
                Console.WriteLine("Felaktig inmatning.");
            }
        }

        public static void Subtraktion()
        {
            int temp = 0;
            Random rnd = new Random();
            tal1 = rnd.Next(1, 101);
            tal2 = rnd.Next(1, 101);

            if (tal1 < tal2)
            {
                temp = tal1;
                tal1 = tal2;
                tal2 = temp;
            }

            antalFragor++;

            Console.WriteLine($"{tal1} - {tal2} = ?");
            if (int.TryParse(Console.ReadLine(), out int svar))
            {
                int korrektSvar = tal1 - tal2;
                if (svar == korrektSvar)
                {
                    Console.WriteLine($"{svar} var rätt!");
                    GameHistory.Add($"{tal1} + {tal2} = {korrektSvar}");
                    points += 5;
                    antalRatt++;
                    antalRattHistory++;
                }
                else
                {
                    Console.WriteLine($"Fel. {korrektSvar} är korrekt.");
                }
            }
            else
            {
                Console.WriteLine("Felaktig inmatning.");
            }
        }

        public static void Multiplikation()
        {
            int temp = 0;
            Random rnd = new Random();
            tal1 = rnd.Next(1, 101);
            tal2 = rnd.Next(1, 101);

            if (tal1 < tal2)
            {
                temp = tal1;
                tal1 = tal2;
                tal2 = temp;
            }

            antalFragor++;

            Console.WriteLine($"{tal1} * {tal2} = ?");
            if (int.TryParse(Console.ReadLine(), out int svar))
            {
                int korrektSvar = tal1 * tal2;
                if (svar == korrektSvar)
                {
                    Console.WriteLine($"{svar} var rätt!");
                    GameHistory.Add($"{tal1} + {tal2} = {korrektSvar}");
                    points += 5;
                    antalRatt++;
                    antalRattHistory++;
                }
                else
                {
                    Console.WriteLine($"Fel. {korrektSvar} är korrekt.");
                }
            }
            else
            {
                Console.WriteLine("Felaktig inmatning.");
            }
        }

        public static void Division()
        {
            int temp = 0;
            Random rnd = new Random();
            tal1 = rnd.Next(1, 101);
            tal2 = rnd.Next(1, 101);

            if (tal1 < tal2)
            {
                temp = tal1;
                tal1 = tal2;
                tal2 = temp;
            }
            
            int korrektSvar = tal1 / tal2;
            

            if (tal1 % tal2 == 0)
            {
                antalFragor++;
                Console.WriteLine($"{tal1} / {tal2} = ?");

                if (int.TryParse(Console.ReadLine(), out int svar))
                {
                    

                    if (svar == korrektSvar)
                    {
                        Console.WriteLine($"{svar} var rätt!");
                        GameHistory.Add($"{tal1} + {tal2} = {korrektSvar}");
                        points += 5;
                        antalRatt++;
                        antalRattHistory++;
                    }
                    else
                    {
                        Console.WriteLine($"Fel. {korrektSvar} är korrekt.");
                    }
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning.");
                }
            }
            else
            {
                return;
            }
        }
    }
}

