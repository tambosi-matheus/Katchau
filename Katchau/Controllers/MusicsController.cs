using ErrorOr;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MusicsController : ControllerBase
{
    private readonly IMusicService _musicService;

    public MusicsController(IMusicService musicService)
    {
        _musicService = musicService;
    }

    [HttpPost]
    public IActionResult CreateMusic(CreateMusicRequest request)
    {
        var music = new Music(
            Guid.NewGuid(),
            request.Name,
            request.Author,
            request.Genres,
            request.CreatedDateTime,
            DateTime.UtcNow
        );

        _musicService.CreateMusic(music);

        var response = new MusicResponse(
            music.Id,
            music.Name,
            music.Author,
            music.Genres,
            music.CreatedDateTime,
            music.LastModifiedDateTime
        );
        return CreatedAtAction(
            actionName: nameof(GetMusic),
            routeValues: new {id = music.Id},
            value: response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetMusic(Guid id)
    {
        ErrorOr<Music> getMusicResult = _musicService.GetMusic(id);

        if(getMusicResult.IsError &&
            getMusicResult.FirstError == Errors.Music.NotFound)
            return NotFound();

        var music = getMusicResult.Value;

        var response = new MusicResponse(
            music.Id,
            music.Name,
            music.Author,
            music.Genres,
            music.CreatedDateTime,
            music.LastModifiedDateTime
        );
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertMusic(Guid id, CreateMusicRequest request)
    {
        var music = new Music(
            id,
            request.Name,
            request.Author,
            request.Genres,
            request.CreatedDateTime,
            DateTime.UtcNow);

        _musicService.UpsertMusic(music);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMusic(Guid id)
    {
        _musicService.DeleteMusic(id);
        return NoContent();
    }
}