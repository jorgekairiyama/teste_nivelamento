using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;

public class Program
{
    static HttpClient client = new HttpClient();
    static readonly string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches";
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        //await getTotalScoredGoals(teamName, year);
        int totalGoals = await getTotalScoredGoals(teamName, year);
        

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);
        

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        int total_goals = await getTotalScoredGoalsByLocalization(team, year, true);
        total_goals += await getTotalScoredGoalsByLocalization(team, year, false);

        return total_goals;
    }
    
    private static async Task<int> getTotalScoredGoalsByLocalization(string team, int year, bool team1)
    {
        int total_goals = 0;
        bool isLastPage;
        int current_page = 1;

        do
        {
            (int pageScore, bool lastPage) = await getScorePerPage(team, year, current_page, team1);
            total_goals += pageScore;
            isLastPage = lastPage;
            current_page++;
        } while (!isLastPage);

        return total_goals;
    }

    private static async Task<(int totalScore, bool isLastPage)> getScorePerPage(string team, int year, int page, bool isTeam1)
    {
        string localia = isTeam1 ? "team1" : "team2";
        HttpResponseMessage response = await client.GetAsync($"{baseUrl}?year={year}&{localia}={team}&page={page}");

        var return_page = await response.Content.ReadFromJsonAsync<Page>();

        if (return_page is null || return_page.data.Count == 0)
        {
            return (0, true);
        }

        if (isTeam1)
        {
            return (return_page.data.Sum(m => int.Parse(m.team1goals)), return_page.total_pages <= page);
        }
        return (return_page.data.Sum(m => int.Parse(m.team2goals)), return_page.total_pages <= page);

    }

}

public class Page
{
    public int total { get; set; }
    public int per_page { get; set; }
    public int page { get; set; }
    public int total_pages { get; set; }
    public List<Match> data { get; set; }
}

public class Match
{
    public string competition { get; set; }
    public int year { get; set; }
    public string round { get; set; }
    public string team1 { get; set; }
    public string team2 { get; set; }
    public string team1goals { get; set; }
    public string team2goals { get; set; }
}