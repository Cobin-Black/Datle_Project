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
}