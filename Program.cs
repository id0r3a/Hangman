using Figgle;
using Spectre.Console;
using System.Text;

namespace Hangman
{
    class Hangman
    {
        static void Main(string[] args)
        {
            // Skapa en logga
            var welcomeText = FiggleFonts.Small.Render("Welcome to");
            var gameText = FiggleFonts.Small.Render("Hangman Game");

            // Visa logotypen 
            AnsiConsole.Clear();  // Rensar konsolen innan vi skriver ut logotypen
            AnsiConsole.MarkupLine($"[bold yellow]{welcomeText}[/]");   // Visa "Welcome to" i gul
            AnsiConsole.MarkupLine($"[bold red]{gameText}[/]");          // Visa "Hangman Game" i röd

            // Vänta lite så användaren hinner se logotypen
            AnsiConsole.MarkupLine("[green]Press any key to start the game...[/]");
            Console.ReadKey(true);  // Vänta på att användaren trycker på en tangent för att starta spelet

            // Ordlistan med möjliga ord att gissa
            string[] words = { "coding", "csharp", "hangman", "game", "application" };

            // Välj ett slumpmässigt ord från listan
            Random random = new Random();
            string secretWord = words[random.Next(words.Length)];

            // Skapa en array för att hålla koll på de gissade bokstäverna
            char[] guessedWord = new string('_', secretWord.Length).ToCharArray();

            // Variabler för försök och max antal försök
            int maxTries = 6;
            int incorrectGuesses = 0;

            // Lista med tidigare gissade bokstäver
            StringBuilder guessedLetters = new StringBuilder();

            // Spelloop
            while (incorrectGuesses < maxTries)
            {
                Console.Clear();
                // Visa ordet med de gissade bokstäverna
                AnsiConsole.MarkupLine($"[bold blue]The word:[/] {new string(guessedWord)}");
                // Visa försök kvar
                AnsiConsole.MarkupLine($"[bold yellow]Left attempts:[/] {maxTries - incorrectGuesses}");
                // Visa gissade bokstäver
                AnsiConsole.MarkupLine($"[bold cyan]Guessed letters:[/] {guessedLetters}");
                // Visa hangman-bilden
                DisplayHangman(incorrectGuesses);

                // Läs in spelarens gissning
                AnsiConsole.MarkupLine("[bold green]Enter a letter:[/]");
                char guess = Char.ToLower(Console.ReadKey(true).KeyChar);

                // Kolla om gissningen är en giltig bokstav
                if (!char.IsLetter(guess))
                {
                    AnsiConsole.MarkupLine("[red]Invalid input. Please enter a letter.[/]");
                    continue;
                }

                // Kolla om bokstaven redan är gissad
                if (guessedLetters.ToString().Contains(guess))
                {
                    AnsiConsole.MarkupLine("[red]You've already guessed this letter.[/]");
                    continue;
                }

                // Lägg till bokstaven till listan med gissade bokstäver
                guessedLetters.Append(guess);

                // Kontrollera om gissningen finns i ordet
                bool correctGuess = false;
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWord[i] == guess)
                    {
                        guessedWord[i] = guess;
                        correctGuess = true;
                    }
                }

                // Om gissningen var felaktig, öka antalet misslyckade gissningar
                if (!correctGuess)
                {
                    incorrectGuesses++;
                }

                // Kontrollera om spelaren har vunnit
                if (!guessedWord.Contains('_'))
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[bold green]Congratulations! You guessed the word:[/] " + secretWord);
                    break;
                }
            }

            // Om spelaren förlorade
            if (incorrectGuesses == maxTries)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[bold red]Sorry, you've lost. The word was:[/] " + secretWord);
            }

            AnsiConsole.MarkupLine("[bold blue]The game is over. Press any key to exit.[/]");
            Console.ReadKey();
        }

        // Metod för att visa hangman-figur beroende på antalet misslyckade gissningar
        static void DisplayHangman(int incorrectGuesses)
        {
            var hangmanArt = new string[]
            {
            @" 
             ______
            |      |
            |      
            |     
            |     
            |      
            |______
            ",
            @" 
             ______
            |      |
            |      O
            |     
            |     
            |      
            |______
            ",
            @" 
             ______
            |      |
            |      O
            |      |
            |     
            |      
            |______
            ",
            @" 
             ______
            |      |
            |      O
            |     /|
            |     
            |      
            |______
            ",
            @" 
             ______
            |      |
            |      O
            |     /|\
            |     
            |      
            |______
            ",
            @" 
             ______
            |      |
            |      O
            |     /|\
            |     / 
            |      
            |______
            ",
            @" 
             ______
            |      |
            |      O
            |     /|\
            |     / \
            |      
            |______
            "
            };

            // Visa den aktuella hangman-bilden baserat på misslyckade gissningar
            Console.WriteLine(hangmanArt[incorrectGuesses]);
        }
    }
}