using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var job1 = new Job
        {
            JobTitle = "Software Engineer",
            Company = "Microsoft",
            StartYear = 2019,
            EndYear = 2022
        };

        var job2 = new Job
        {
            JobTitle = "Manager",
            Company = "Apple",
            StartYear = 2022,
            EndYear = 2023
        };

        var myResume = new Resume
        {
            Name = "Allison Rose",
            Jobs = { job1, job2 }
        };

        myResume.Display();
    }
}

class Job
{
    public string JobTitle;
    public string Company;
    public int StartYear;
    public int EndYear;
}

class Resume
{
    public string Name;
    public List<Job> Jobs = new List<Job>();

    public void Display()
    {
        Console.WriteLine($"Name: {Name}\nJobs:");
        foreach (var job in Jobs)
        {
            Console.WriteLine($"{job.JobTitle} ({job.Company}) {job.StartYear}-{job.EndYear}");
        }
    }
}
