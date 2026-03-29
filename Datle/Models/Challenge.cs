namespace Datle.Models;

public class Challenge
{
    public string Title { get; set; } = "";
    public string Question { get; set; } = "";
    public string[] Options { get; set; } = new string[4];
    public int CorrectOptionIndex { get; set; }
    public string Explanation { get; set; } = "";
    public string DSType { get; set; } = ""; // e.g., "BST", "Array"
}