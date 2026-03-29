namespace Datle.DataStructures;

public class SelectionSortState
{
    public int[] Array { get; set; } = { 29, 10, 14, 37, 13 };
    public int CurrentIndex { get; set; } = 0;
    public int MinIndex { get; set; } = 0;
    public int ComparingIndex { get; set; } = 0;

    public void Step()
    {
        // Logic to move through the array one 'comparison' at a time
        // This allows the UI to update colors as the algorithm works
    }
}