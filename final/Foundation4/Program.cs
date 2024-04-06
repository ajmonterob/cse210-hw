using System;

public abstract class Activity
{
    protected string Date;
    protected int DurationInMinutes;

    public Activity(string date, int durationInMinutes)
    {
        Date = date;
        DurationInMinutes = durationInMinutes;
    }

    
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    
    public virtual string GetSummary()
    {
        return $"{Date}: {this.GetType().Name} ({DurationInMinutes} min)- Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

//

public class Running : Activity
{
    private double Distance; 
    public Running(string date, int durationInMinutes, double distance) : base(date, durationInMinutes)
    {
        Distance = distance;
    }

    public override double GetDistance() => Distance;
    public override double GetSpeed() => (Distance / DurationInMinutes) * 60; 
    public override double GetPace() => DurationInMinutes / Distance; 
}


//
public class Cycling : Activity
{
    private double Speed; 

    public Cycling(string date, int durationInMinutes, double speed) : base(date, durationInMinutes)
    {
        Speed = speed;
    }

    public override double GetDistance() => (Speed * DurationInMinutes) / 60; 
    public override double GetSpeed() => Speed;
    public override double GetPace() => 60 / Speed; 
}

//
public class Swimming : Activity
{
    private readonly int NumberOfLaps;

    public Swimming(string date, int durationInMinutes, int numberOfLaps) : base(date, durationInMinutes)
    {
        NumberOfLaps = numberOfLaps;
    }

    public override double GetDistance() => (NumberOfLaps * 50) / 1000 * 0.62; 
    public override double GetSpeed() => GetDistance() / (DurationInMinutes / 60.0); 
    public override double GetPace() => DurationInMinutes / GetDistance(); 
}
//
public class Program
{
    public static void Main(string[] args)
    {
        
        var activities = new List<Activity>
        {
            new Running("03 April 2024", 30, 3.0),
            new Cycling("04 April 2024", 45, 15.0),
            new Swimming("05 April 2024", 30, 20)
        };

        
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}


