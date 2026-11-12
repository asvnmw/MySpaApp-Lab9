namespace MySpaApp.Models;

public class Trainer
{
    public int Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Sport { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Salary { get; set; }
}