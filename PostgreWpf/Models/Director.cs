using System;
using System.Linq;

public class Director
{
    private int did;
    private string firstName;
    private string secondName;
    private string isCertified;

    private static readonly Random random = new Random();

    public Director(int did, string firstName, string secondName, string isCertified)
	{
        this.Did = did;
        this.FirstName = firstName;
        this.SecondName = secondName;
        this.isCertified = isCertified;
	}

    public Director(string firstName, string secondName, string isCertified)
    {
        this.firstName = firstName;
        this.secondName = secondName;
        this.isCertified = isCertified;
    }

    public Director()
    {
        this.FirstName = GenerateRandomText(10);
        this.SecondName = GenerateRandomText(10);
        if (random.NextDouble() >= 0.5)
            this.isCertified = "TRUE";
        else
            this.IsCertified = "FALSE";
    }

    private String GenerateRandomText(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string FirstName { get => firstName; set => firstName = value; }
    public string SecondName { get => secondName; set => secondName = value; }
    public string IsCertified { get => isCertified; set => isCertified = value; }
    public int Did { get => did; set => did = value; }
}
