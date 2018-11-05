using System;

public class Director
{
    private int did;
    private string firstName;
    private string secondName;



	public Director(int did, string firstName, string secondName)
	{
        this.Did = did;
        this.FirstName = firstName;
        this.SecondName = secondName;
	}

    public int Did { get => did; set => did = value; }
    public string FirstName { get => firstName; set => firstName = value; }
    public string SecondName { get => secondName; set => secondName = value; }
}
