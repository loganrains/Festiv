namespace Festiv.Models;

public class Gift
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int PartyId { get; set; }
    public Party Party { get; set; }

    public Gift(string name, Guid userId)
    {
        Name = name;
        UserId = userId;
    }
}