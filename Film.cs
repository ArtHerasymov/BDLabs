using System;

public class Film
{

    private int fid;
    private string title;
    private int budget;
    private string script;
    private int did;

    public Film(string fid, string title, int budget, string script, int did)
    {
        this.Fid = fid;
        this.Title = title;
        this.Budget = budget;
        this.Script = script;
        this.Did = did;
    }

    public int Fid { get => fid; set => fid = value; }
    public string Title { get => title; set => title = value; }
    public int Budget { get => budget; set => budget = value; }
    public string Script { get => script; set => script = value; }
    public int Did { get => did; set => did = value; }
}
