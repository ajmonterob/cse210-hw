using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user for their grade percentage
        Console.Write("What is your grade percentage? ");
        string gradePercentage = Console.ReadLine();
        int percent = int.Parse(gradePercentage);

        // Determine the letter grade using if-elif-else statements
        char letter = '\0';  // Initialize with a default value

        if (percent >= 90)
        {
            letter = 'A';
        }
        else if (percent >= 80)
        {
            letter = 'B';
        }
        else if (percent >= 70)
        {
            letter = 'C';
        }
        else if (percent >= 60)
        {
            letter = 'D';
        }
        else
        {
            letter = 'F';
        }

        // Display the letter grade
        Console.WriteLine($"Your letter grade is: {letter}");

        // Determine if the user passed the course and display a message
        if (percent >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
}
