using System;


public class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;  
    }

    protected void DisplayStartingMessage()
    {
        Console.WriteLine($"{_name} - {_description}");
        Console.WriteLine("How long would you like this activity to last?");
        _duration = int.Parse(Console.ReadLine());  
        
        Console.Write("You may begin in: ");
        for (int countdown = 5; countdown > 0; countdown--)
        {
            Console.Write(countdown);
            Thread.Sleep(1000); 
            Console.Write("\b \b");
        }
        Console.WriteLine("Begin!");
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("Well done! You've completed the activity.");
        ShowSpinner(2);  
        Console.WriteLine($"You have completed {_name} for {_duration} seconds.");
    }

    protected void ShowSpinner(int durationInSeconds)
    {
        DateTime endTime = DateTime.Now.AddSeconds(durationInSeconds);
        char[] spinnerChars = new char[] { '|', '/', '-', '\\' };
        int spinnerIndex = 0;

        Console.Write(" ");  
        while (DateTime.Now < endTime)
        {
            Console.Write($"\b{spinnerChars[spinnerIndex++]}");  
            if (spinnerIndex >= spinnerChars.Length) spinnerIndex = 0;  
            Thread.Sleep(100);  
        }
        Console.Write("\b \b"); 
    }

    public virtual void Run()
    {
        // Placeholder 
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
    DisplayStartingMessage();
    
    int elapsedTime = 0;
    while (elapsedTime < _duration)
    {
        Console.Write("Breathe in... ");
        for (int countdown = 5; countdown > 0; countdown--)
        {
            Console.Write(countdown + " ");
            Thread.Sleep(1000);  
            Console.Write("\b\b");  
        }
        Console.WriteLine();  
        
        Console.Write("Breathe out... ");
        
        for (int countdown = 5; countdown > 0; countdown--)
        {
            Console.Write(countdown + " ");
            Thread.Sleep(1000);  
            Console.Write("\b\b");  
        }
        Console.WriteLine();  
        
        elapsedTime += 10;  
    }

    DisplayEndingMessage();
    }
} 


public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
        : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    private string GetRandomPrompt()
    {
        Random rnd = new Random();
        int index = rnd.Next(_prompts.Count);
        return _prompts[index];
    }
    
    public override void Run()  
    {
    DisplayStartingMessage();
    string prompt = GetRandomPrompt();
    Console.WriteLine($"Prompt: {prompt}");

    DateTime endTime = DateTime.Now.AddSeconds(_duration);
    Console.WriteLine("Start listing (type 'end' to finish):");

    int itemCount = 0;
    while (DateTime.Now < endTime)
    {
        if (Console.KeyAvailable)
        {
            string input = Console.ReadLine();
            if (input.ToLower() == "end") break;
            itemCount++;
        }
        ShowSpinner(1); 
    }

    Console.WriteLine($"You listed {itemCount} items.");
    DisplayEndingMessage();
    }
}

public class ReflectingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you overcame a personal challenge.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "What did you learn from this experience?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?"
    };

public ReflectingActivity()
    : base("Reflecting", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
{
}

private string GetRandomPrompt()
{
    Random rnd = new Random();
    int index = rnd.Next(_prompts.Count);
    return _prompts[index];
}

private string GetRandomQuestion(List<string> remainingQuestions)
{
    Random rnd = new Random();
    if (remainingQuestions.Count == 0)
    {
        return null; 
    }
    int index = rnd.Next(remainingQuestions.Count);
    string question = remainingQuestions[index];
    remainingQuestions.RemoveAt(index); 
    return question;
    }
    public override void Run()
    {
    DisplayStartingMessage();
    string prompt = GetRandomPrompt();
    Console.WriteLine($"Reflection prompt: {prompt}");

    Console.WriteLine("Press ENTER to start the activity");
    Console.ReadLine();

    Console.WriteLine("Reflect on this (type 'end' to finish):");
    DateTime endTime = DateTime.Now.AddSeconds(_duration);
    List<string> remainingQuestions = new List<string>(_questions); 

    while (DateTime.Now < endTime && remainingQuestions.Count > 0)
    {
        string question = GetRandomQuestion(remainingQuestions); 
        if (string.IsNullOrEmpty(question)) break; 
        
        Console.WriteLine(question);
        string input = Console.ReadLine();
        if (input?.ToLower() == "end") break;

        ShowSpinner(1);  
    }

    DisplayEndingMessage();
}



}

class Program
{
static void Main(string[] args)
{
    while (true)
    {
        Console.WriteLine("Menu Options: \n1.Start breathing activity\n2.Start listing activity\n3.Start reflecting activity\nType 'quit' to exit.");
        string choice = Console.ReadLine();
        if (choice.ToLower() == "quit")
        {
            break;
        }

        Activity activity = null;

        switch (choice)
        {
            case "1":
                activity = new BreathingActivity();
                break;
            case "2":
                activity = new ListingActivity();
                break;
            case "3":
                activity = new ReflectingActivity();
                break;
            default:
                Console.WriteLine("Invalid selection.");
                continue;
        }

        activity?.Run();
    }
}
}

