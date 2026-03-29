using Datle.Models;

namespace Datle.Services;

public class ChallengeService
{
    public Challenge GetDailyChallenge()
    {
        // For now, we hardcode one. Later, this will fetch from your Azure Database!
        return new Challenge
        {
            Title = "Array Capacity",
            Question = "An Array has a capacity of 4. If we add a 5th element, what usually happens in a Dynamic Array (List)?",
            Options = new[] { "It Crashes", "It doubles to 8", "It stays at 4", "It becomes 5" },
            CorrectOptionIndex = 1,
            Explanation = "Dynamic arrays typically double their capacity (O(n) operation) to make room for future growth."
        };
    }
}