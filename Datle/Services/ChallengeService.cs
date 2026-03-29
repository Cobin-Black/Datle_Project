using System.Net.Http.Json;
using System.Net.Http;
using Datle.Models;

namespace Datle.Services;

public class ChallengeService
{
    private readonly HttpClient _http;
    private List<Challenge>? _cache;

    public ChallengeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Challenge?> GetChallengeByDate(DateTime date)
    {
        // Only load the file once to save memory
        if (_cache == null)
        {
            _cache = await _http.GetFromJsonAsync<List<Challenge>>("data/challenges.json");
        }

        // Return the challenge for the specific date
        return _cache?.FirstOrDefault(c => c.Date.Date == date.Date);
    }

    public List<Challenge> GetPastChallenges()
    {
        if (_cache == null) return new List<Challenge>();

        // Return all challenges from the JSON where the date is today or earlier
        // We sort them by date descending so the newest ones are at the top of the sidebar
        return _cache
            .Where(c => c.Date.Date <= DateTime.Today)
            .OrderByDescending(c => c.Date)
            .ToList();
    }
}