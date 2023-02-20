using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Model;
using System.Linq;

namespace webapi.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class HighscoresController : ControllerBase
{
    
    //Authorize (bara admin kan t.ex använda delete)
    private readonly MyDbContext _dbContext;

    public HighscoresController(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Lägger till score
    [HttpPost] //201 response code
    public async Task<ActionResult<Highscore>> PostHighScore(Highscore highscore)
    {

        if (highscore == null)
        {
            return BadRequest("Highscore object is null.");
        }

        try
        {
            _dbContext.highscores.Add(highscore);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHighScore), new { id = highscore.HighScoreId }, highscore);//returnerar 201 response code
            //return Ok(highscore);//returnerar 200 response code
        }

        catch (DbUpdateException ex)
        {

            Console.WriteLine($"Error saving highscore to the database: {ex.Message}");

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the highscore.");
        }


    }

    //Hämtar alla i scores i listan
    [HttpGet] //200 response code
    public async Task<ActionResult<List<Highscore>>> GetHighScore()
    {
        var highScore = await _dbContext.highscores.ToListAsync();

        if (highScore == null)
        {
            return NotFound();
        }

        return Ok(highScore);
    }

    //Hämtar top 10 i listan (senaste)
    [HttpGet("top10")]
    public async Task<ActionResult<List<Highscore>>> GetTopTen()
    {

        var highScore = await _dbContext.highscores.ToListAsync();
        if (highScore == null)
        {
            return NotFound();
        }

        return Ok(highScore.TakeLast(10));
    }

    //En av responserna måste vara satt till ett värde annars slår dom ihope (500 fel meddelande)
    //Eller om både är satta vill api inte prata alls
    [HttpGet("{HighScoreId:int}")]
    public async Task<ActionResult<Highscore>> GetHighScoreById(int HighScoreId)
    {
        //FindAsync ger mig entitet med ett givet PrimaryKey
        //var highScoreId = await _dbContext.highscores.FindAsync(HighScoreId);
        //Bråkar ej med typer och HttpGet("{Name}") om jag göra på sättet nedan
        var highScoreId = await _dbContext.highscores.Where(x => x.HighScoreId == HighScoreId)
            .ToListAsync();

        if (highScoreId == null)
        {
            return NotFound();
        }

        return Ok(highScoreId);
    }

    [HttpGet("{Name}")]
    public async Task<ActionResult<Highscore>> GetHighScoreByName(string Name)
    {
        //SELECT * highscore FROM highscores WHERE x.Name == @Name 
        //Detta efterfrågar till lista så att alla scores för namnet visas
        var highScoreName = await _dbContext.highscores.Where(x => x.Name == Name)
            .ToListAsync();

        if (highScoreName == null)
        {
            return NotFound();
        }

        return Ok(highScoreName);
    }

    [HttpDelete]//204 response code
    public async Task<IActionResult> DeleteAll(MyDbContext _dbContext) //[FromServices] 
    {
        _dbContext.highscores.RemoveRange(_dbContext.highscores);
        await _dbContext.SaveChangesAsync();

        return NoContent(); //Borde göra en 204 response.

    }
}