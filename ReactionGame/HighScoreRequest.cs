using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ReactionGame;
class HighScoreRequest
{
    public async Task PostRequest(Highscore highScore)
    {
        HttpClient httpClient = new HttpClient();

        string json = JsonConvert.SerializeObject(highScore);
        var highscore = new StringContent(json, Encoding.UTF8, "application/json");

        // The data to send in the request body

        var response = await httpClient.PostAsync("http://localhost:5235/api/Highscores", highscore);
    }

     public async Task<List<Highscore>> GetTopTen()
    {
         HttpClient httpClient = new HttpClient();

        var highScoreList = await httpClient.GetFromJsonAsync<List<Highscore>>("http://localhost:5235/api/Highscores/top10");

        if(highScoreList == null)
        {
            return highScoreList = new List<Highscore>();
        }

        return highScoreList;
    }

    public async Task<List<Highscore>> GetHighScore()
    {
         HttpClient httpClient = new HttpClient();

        var highScoreList = await httpClient.GetFromJsonAsync<List<Highscore>>("http://localhost:5235/api/Highscores");

        return highScoreList;
    }

}