namespace Datle.Models;

public class Challenge
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = "";
    public string Question { get; set; } = "";
    public string[] Options { get; set; } = new string[4];
    public int CorrectOptionIndex { get; set; }
    public string Explanation { get; set; } = "";
    public string DSType { get; set; } = ""; // e.g., "BST", "Array"
    public string DataString { get; set; } = ""; // For storing any relevant data (like an array or tree structure) as a string
}