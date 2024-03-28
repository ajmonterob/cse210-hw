using System;
using System.Collections.Generic;
using System.IO;

public class Program {
    public static void Main(string[] args) {
        GoalManager manager = new GoalManager();
        manager.Start(); 
    }
}

// Base Class
public abstract class Goal {
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points) {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public virtual string GetDetailsString() => $"[ ] {_shortName} ({_description})";
    public abstract string GetStringRepresentation();

    // Add Bonus
    public int Points {
        get { return _points; }
    }
}
//SimpleGoal Class
public class SimpleGoal : Goal {
    private bool _isComplete = false;

    public SimpleGoal(string name, string description, int points, bool isComplete) : base(name, description, points) {
    _isComplete = isComplete;
    }

    public override void RecordEvent() {
        _isComplete = true;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetStringRepresentation() => $"SimpleGoal:{_shortName},{_description},{_points},{_isComplete}";

    public override string GetDetailsString() {
    string status = _isComplete ? "[X]" : "[ ]";
    return $"{status} {_shortName} ({_description})";
    }

}

//EternalGoal Class
public class EternalGoal : Goal {
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent() {
        // Eternal goals do not complete
    }

    public override bool IsComplete() => false;

    public override string GetStringRepresentation() => $"EternalGoal:{_shortName},{_description},{_points}";
}
//Checklist Class
public class ChecklistGoal : Goal {
    private int _amountCompleted = 0;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted) : base(name, description, points) {
    _target = target;
    _bonus = bonus;
    _amountCompleted = amountCompleted;
    }

    public override void RecordEvent() {
        _amountCompleted++;
    }

    public override bool IsComplete() => _amountCompleted >= _target;

    public override string GetDetailsString() {
    string status = IsComplete() ? "[X]" : "[ ]";
    return $"{status} {_shortName} ({_description}) -- Currently completed: {_amountCompleted}/{_target}";
    }


    public override string GetStringRepresentation() => $"ChecklistGoal:{_shortName},{_description},{_points},{_bonus},{_target},{_amountCompleted}";

    // Add Bonus
    public int Bonus {
        get { return _bonus; }
    }
}


// Goal Manager
public class GoalManager {
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void Start() {
    bool running = true;
    while (running) {
        Console.WriteLine("You have " + _score + " points.\n");
        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. List Goals");
        Console.WriteLine("3. Save Goals");
        Console.WriteLine("4. Load Goals");
        Console.WriteLine("5. Record Event");
        Console.WriteLine("6. Quit");
        Console.Write("Select a choice from the Menu: ");
        
        int choice = int.Parse(Console.ReadLine() ?? "0");

        switch (choice) {
            case 1:
                CreateGoal();
                break;
            case 2:
                ListGoals();
                break;
            case 3:
                SaveGoals();
                break;
            case 4:
                LoadGoals();
                break;
            case 5:
                RecordEvent();
                break;
            case 6:
                running = false;
                Console.WriteLine("Exiting program.");
                break;
            default:
                Console.WriteLine("Invalid option, please try again.");
                break;
        }
    }
    }

public void CreateGoal() {
    Console.WriteLine("The types of Goals are:");
    Console.WriteLine("1. Simple Goal");
    Console.WriteLine("2. Eternal Goal");
    Console.WriteLine("3. Checklist Goal");
    Console.Write("Which type of goal would you like to create? ");
    
    int goalType = int.Parse(Console.ReadLine() ?? "0");
    Console.Write("What is the name of your goal? ");
    string name = Console.ReadLine();
    Console.Write("What is a short description of it? ");
    string description = Console.ReadLine();
    Console.Write("What is the amount of points associated with this goal? ");
    int points = int.Parse(Console.ReadLine() ?? "0");

    Goal goal = null;
    switch (goalType) {
        case 1: 
            goal = new SimpleGoal(name, description, points, false); 
            break;
        case 2: 
            goal = new EternalGoal(name, description, points); 
            break;
        case 3: 
            Console.Write("How many times does this goal need to be accomplished for a bonus? ");
            int target = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("What is the bonus for accomplishing it that many times? ");
            int bonus = int.Parse(Console.ReadLine() ?? "0");
            goal = new ChecklistGoal(name, description, points, target, bonus, 0); 
            break;
        default:
            Console.WriteLine("Invalid goal type selected.");
            break;
    }

    if (goal != null) {
        _goals.Add(goal);
        Console.WriteLine("Goal created successfully.");
    }
    }

    public void ListGoals() {
    if (_goals.Count == 0) {
        Console.WriteLine("No goals have been created yet.");
        return;
    }

    Console.WriteLine("Current Goals:");
    for (int i = 0; i < _goals.Count; i++) {
        Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
    }
    }

    public void UpdateScore(int points) {
        _score += points;
        Console.WriteLine($"Congratulations! You have earned {points} Points!");
        Console.WriteLine($"You now have {_score} points.");
    }

    public void RecordEvent() {
    Console.WriteLine("The goals are:");
    for (int i = 0; i < _goals.Count; i++) {
        Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
    }
    Console.Write("Which goal did you accomplish? ");
    int choice;
    if (int.TryParse(Console.ReadLine(), out choice)) {
        choice -= 1; 
        if (choice >= 0 && choice < _goals.Count) {
            Goal completedGoal = _goals[choice];
            completedGoal.RecordEvent(); // Marks the event as complete 
            UpdateScore(completedGoal.Points); // Add the base points 

            
            if (completedGoal is ChecklistGoal checklistGoal && checklistGoal.IsComplete()) {
                UpdateScore(checklistGoal.Bonus); 
                Console.WriteLine($"Bonus awarded! {checklistGoal.Bonus} bonus points added.");
            }
        } else {
            Console.WriteLine("Invalid goal selection.");
        }
    } else {
        Console.WriteLine("Please enter a valid number.");
    }
    }

    public void DisplayPlayerInfo() {
        Console.WriteLine($"You have {_score} points.");
    }

    public void SaveGoals() {
    Console.Write("What is the filename for the goal file? ");
    string filename = Console.ReadLine();
    
    using (StreamWriter writer = new StreamWriter(filename)) {
        writer.WriteLine(_score);
        
        foreach (Goal goal in _goals) {
            writer.WriteLine(goal.GetStringRepresentation());
        }
    }
    Console.WriteLine("Goals have been saved.");
    }

    public void LoadGoals() {
    Console.Write("What is the filename for the goal file? ");
    string filename = Console.ReadLine();
    
    if (File.Exists(filename)) {
        string[] lines = File.ReadAllLines(filename);
    
        _score = int.Parse(lines[0]);

        _goals.Clear();
        
        //  each  line as a goal
        for (int i = 1; i < lines.Length; i++) {
            string line = lines[i];
            string[] parts = line.Split(':');
            string goalType = parts[0];
            string[] attributes = parts[1].Split(',');
            
            switch (goalType) {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(attributes[0], attributes[1], int.Parse(attributes[2]), bool.Parse(attributes[3])));
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(attributes[0], attributes[1], int.Parse(attributes[2])));
                    break;
                case "ChecklistGoal":
                    _goals.Add(new ChecklistGoal(attributes[0], attributes[1], int.Parse(attributes[2]), int.Parse(attributes[3]), int.Parse(attributes[4]), int.Parse(attributes[5])));
                    break;
            }
        }
        Console.WriteLine("Goals have been loaded.");
    } else {
        Console.WriteLine("File not found.");
    }
    }

}
