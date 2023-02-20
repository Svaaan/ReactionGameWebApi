namespace webapi.Model;
public class Highscore
{
    public string? GameName {get; set;}
    public int HighScoreId { get; set; }
    public string? Name { get; set; }
    public long Time { get; set; }
    //public System.Net.CookieCollection Cookies { get; set; } //Ser ut som det måste ha eget table. Vänta med denna
    public DateTime ClickCreated {get; set;} = DateTime.Now;
}
