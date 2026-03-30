using System.Linq;

namespace Datle.DataStructures;

public class SelectionSortLogic
{
    // This is the actual array the visualizer will look at
    public int[] Data { get; set; } = Array.Empty<int>();
    
    // Tracks the current boundary between sorted and unsorted parts
    public int CurrentOuterIndex { get; set; } = 0;

    /// <summary>
    /// Parses a string like "5,2,8,1" into the Data array.
    /// </summary>
    public void LoadData(string dataString)
    {
        if (string.IsNullOrWhiteSpace(dataString))
        {
            Data = Array.Empty<int>();
            return;
        }

        try 
        {
            Data = dataString.Split(',')
                             .Select(s => int.Parse(s.Trim()))
                             .ToArray();
            CurrentOuterIndex = 0;
        }
        catch
        {
            // Fallback if the JSON string is malformed
            Data = new int[] { 1, 2, 3 }; 
        }
    }

    /// <summary>
    /// Performs one 'pass' of the selection sort.
    /// </summary>
    public void NextStep()
    {
        if (Data.Length == 0 || CurrentOuterIndex >= Data.Length - 1) return;

        int minIdx = CurrentOuterIndex;
        for (int j = CurrentOuterIndex + 1; j < Data.Length; j++)
        {
            if (Data[j] < Data[minIdx])
            {
                minIdx = j;
            }
        }

        // Swap
        int temp = Data[minIdx];
        Data[minIdx] = Data[CurrentOuterIndex];
        Data[CurrentOuterIndex] = temp;

        CurrentOuterIndex++;
    }

    public void Reset()
    {
        CurrentOuterIndex = 0;
        // Note: You might want to store the original string to re-load it here
    }
}