using System;

class Program
{
    static void Main(string[] args)
    {
        // Step 1: Generate a random number from 1 to 100
        Random random = new Random();
        int magicNumber = random.Next(1, 101);

        // Step 2: Add a loop to keep playing until the guess is correct
        bool guessedCorrectly = false;

        while (!guessedCorrectly)
        {
            // Step 3: Ask the user for a guess
            Console.Write("What is your guess? ");
            int userGuess = int.Parse(Console.ReadLine());

            // Step 4: Determine if the user needs to guess higher, lower, or if they guessed it
            if (userGuess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (userGuess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
                guessedCorrectly = true; // Exit the loop
            }
        }
    }
}
