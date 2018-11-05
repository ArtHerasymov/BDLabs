using System;
using System.Linq;

public class Actor
{
    private int aid;
    private string firstName;
    private string secondName;
    private int experience;
    private static readonly Random random = new Random();


    public Actor(int aid, string firstName, string secondName, int experience)
    {
        this.aid = aid;
        this.firstName = firstName;
        this.secondName = secondName;
        this.experience = experience;

    }
	public Actor(string firstName, string secondName, int experience)
	{
        this.FirstName = firstName;
        this.SecondName = secondName;
        this.Experience = experience;
	}

    public Actor()
    {
        this.FirstName = GenerateRandomText(10);
        this.SecondName = GenerateRandomText(10);
        this.Experience = random.Next(1, 30);
    }

    private String GenerateRandomText(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string FirstName { get => firstName; set => firstName = value; }
    public string SecondName { get => secondName; set => secondName = value; }
    public int Experience { get => experience; set => experience = value; }
    public int Aid { get => aid; set => aid = value; }
}
