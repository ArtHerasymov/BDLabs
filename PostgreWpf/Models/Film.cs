using System;
using System.Linq;

public class Film
{

    private int fid;
    private string title;
    private int budget;
    private string script;
    private int did;

    private static readonly Random random = new Random();

    public Film()
    {
        this.title = GenerateRandomText(1, 10);
        this.budget = random.Next();
        this.script = GenerateRandomText(20, 5);
        this.did = random.Next(1,50);
    }

    public Film(int fid, string title, int budget, string script, int did)
    {
        this.Fid = fid;
        this.Title = title;
        this.Budget = budget;
        this.Script = script;
        this.Did = did;
    }

    public Film(string title, int budget, string script, int did)
    {
        this.Title = title;
        this.budget = budget;
        this.script = script;
        this.did = did;
    }

    private String GenerateRandomText(int numberOfWords, int lengthOfWords)
    {
        String result = "";
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < numberOfWords; i++)
        {
            result += new String(Enumerable.Repeat(chars, lengthOfWords)
          .Select(s => s[random.Next(s.Length)]).ToArray()) + " ";
        }

        return result;
       
    }

    public int Fid { get => fid; set => fid = value; }
    public string Title { get => title; set => title = value; }
    public int Budget { get => budget; set => budget = value; }
    public string Script { get => script; set => script = value; }
    public int Did { get => did; set => did = value; }
}
