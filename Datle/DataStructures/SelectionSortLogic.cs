namespace Datle.DataStructures;

public class SelectionSortLogic
{
    public int[] Data { get; set; } = { 5, 2, 8, 1, 9, 3 };
    public int CurrentOuterIndex { get; set; } = 0;

    public void NextStep()
    {
        // If we've reached the end, stop.
        if (CurrentOuterIndex >= Data.Length - 1) return;

        int minIdx = CurrentOuterIndex;
        
        // Find the minimum element in the remaining unsorted array
        for (int j = CurrentOuterIndex + 1; j < Data.Length; j++)
        {
            if (Data[j] < Data[minIdx])
            {
                minIdx = j;
            }
        }

        // Swap the found minimum element with the first element
        int temp = Data[minIdx];
        Data[minIdx] = Data[CurrentOuterIndex];
        Data[CurrentOuterIndex] = temp;

        // Move the boundary of the sorted and unsorted subarrays
        CurrentOuterIndex++;
    }

    public void Reset()
    {
        Data = new int[] { 5, 2, 8, 1, 9, 3 };
        CurrentOuterIndex = 0;
    }
}