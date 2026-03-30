using Microsoft.JSInterop;

namespace Datle.Services;

public class StorageService
{
    private readonly IJSRuntime _js;

    public StorageService(IJSRuntime js) => _js = js;

    public async Task SaveProgress(string date, bool isCorrect)
    {
        // Changed from localStorage to sessionStorage
        await _js.InvokeVoidAsync("sessionStorage.setItem", date, isCorrect.ToString().ToLower());
    }

    public async Task<bool?> GetProgress(string date)
    {
        // Changed from localStorage to sessionStorage
        var result = await _js.InvokeAsync<string>("sessionStorage.getItem", date);
        if (result == null) return null;
        return result == "true";
    }

    public async Task<int> CalculateStreak(List<DateTime> allChallengeDates)
    {
        int streak = 0;
        // Get today's date string
        string today = DateTime.Today.ToString("yyyy-MM-dd");
    
        // Check if today is correct in session storage
        var todayCorrect = await GetProgress(today);
    
        if (todayCorrect == true)
        {
            streak = 1; // For your demo, if today is right, streak is 1
        
            // Optional: Check yesterday too if you have it in your JSON
            string yesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            var yesterdayCorrect = await GetProgress(yesterday);
            if (yesterdayCorrect == true) streak = 2;
        }

        return streak;
    }
}