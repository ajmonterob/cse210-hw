using System;

class Fraction
{
    private int numerator;
    private int denominator;

    public Fraction() : this(0, 1) { }

    public Fraction(int wholeNumber) : this(wholeNumber, 1) { }

    public Fraction(int numerator, int denominator)
    {
    
        this.numerator = numerator;
        this.denominator = denominator;
    }

    public string GetFractionString()
    {
        if (denominator == 1)
        {
            return $"{numerator}";
        }
        else
        {
            return $"{numerator}/{denominator}";
        }
    }

    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction();
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        Fraction f2 = new Fraction(5);
        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        Fraction f3 = new Fraction(3, 4);
        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        Fraction f4 = new Fraction(1, 3);
        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());
    }
}
