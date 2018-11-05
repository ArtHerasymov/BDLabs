using System;

public class Actor
{
    private int aid;
    private string firstName;
    private string secondName;
    private int experience;

	public Actor(int aid, string firstName, string secondName, int experience)
	{
        this.Aid = aid;
        this.FirstName = firstName;
        this.SecondName = secondName;
        this.Experience = experience;
	}

    public int Aid { get => aid; set => aid = value; }
    public string FirstName { get => firstName; set => firstName = value; }
    public string SecondName { get => secondName; set => secondName = value; }
    public int Experience { get => experience; set => experience = value; }
}
