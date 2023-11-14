using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public override string ToString()
    {
        return $"{Date}: {Prompt}\n   {Response}\n";
    }
}

class Journal
{
    private List<JournalEntry> entries;

    public Journal()
    {
        entries = new List<JournalEntry>();
    }

    public void AddEntry(string prompt, string response, string date)
    {
        JournalEntry entry = new JournalEntry
        {
            Prompt = prompt,
            Response = response,
            Date = date
        };
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                outputFile.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        string[] lines = File.ReadAllLines(filename);

        foreach (var line in lines)
        {
            string[] parts = line.Split(",");
            string date = parts[0];
            string prompt = parts[1];
            string response = parts[2];

            AddEntry(prompt, response, date);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal);
                    break;
                case "2":
                    DisplayJournal(journal);
                    break;
                case "3":
                    SaveJournalToFile(journal);
                    break;
                case "4":
                    LoadJournalFromFile(journal);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }

    static void WriteNewEntry(Journal journal)
    {
        string[] prompts =
        {
            "Who was the most interesting person in your day?",
            "What was the best hour of the day?",
            "How was your feeling overall of the day?",
            "What was the strongest emotion I felt today?",
            "Do you have something you would like your kids to remeber?"
        };

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Enter your response: ");
        string response = Console.ReadLine();

        DateTime currentDate = DateTime.Now;
        string date = currentDate.ToShortDateString();

        journal.AddEntry(prompt, response, date);
    }

    static void DisplayJournal(Journal journal)
    {
        Console.WriteLine("\n--- Journal Entries ---");
        journal.DisplayEntries();
        Console.WriteLine("-----------------------\n");
    }

    static void SaveJournalToFile(Journal journal)
    {
        Console.Write("Enter the filename to save the journal: ");
        string saveFilename = Path.Combine(Environment.CurrentDirectory, Console.ReadLine());
        journal.SaveToFile(saveFilename);
        Console.WriteLine($"Journal saved successfully. Full path: {Path.GetFullPath(saveFilename)}\n");
    }

    static void LoadJournalFromFile(Journal journal)
    {
        Console.Write("Enter the filename to load the journal: ");
        string loadFilename = Path.Combine(Environment.CurrentDirectory, Console.ReadLine());
        journal.LoadFromFile(loadFilename);
        Console.WriteLine($"Journal loaded successfully from: {Path.GetFullPath(loadFilename)}\n");
    }
}
