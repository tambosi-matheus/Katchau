using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Models;

public class MusicsController : ApiController
{
    private readonly IMusicService _musicService;

    public MusicsController(IMusicService musicService)
    {
        _musicService = musicService;
    }

    [HttpPost]
    public IActionResult CreateMusic(CreateMusicRequest request)
    {
        ErrorOr<Music> requestToMusicResult = Music.From(request);

        if(requestToMusicResult.IsError)
        {
            return Problem(requestToMusicResult.Errors);
        }

        var music = requestToMusicResult.Value;
        ErrorOr<Created> createMusicResult = _musicService.CreateMusic(music);

        return createMusicResult.Match(
            created => CreatedAtGetMusic(music),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetMusic(Guid id)
    {
        ErrorOr<Music> getMusicResult = _musicService.GetMusic(id);

        return getMusicResult.Match(
            music => Ok(MapMusicResponse(music)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertMusic(Guid id, UpsertMusicRequest request)
    {
        ErrorOr<Music> requestToMusicResult = Music.From(id, request);


        if(requestToMusicResult.IsError)
        {
            return Problem(requestToMusicResult.Errors);
        }

        var music = requestToMusicResult.Value;
        ErrorOr<UpsertMusic> upsertedMusicResult = _musicService.UpsertMusic(music);


        return upsertedMusicResult.Match(
            upserted => upserted.isNewlyCreated ? CreatedAtGetMusic(music) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMusic(Guid id)
    {
        ErrorOr<Deleted> deleteMusicResult = _musicService.DeleteMusic(id);
        
        return deleteMusicResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private static MusicResponse MapMusicResponse(Music music)
    {
        return new MusicResponse(
            music.Id,
            music.Name,
            music.Author,
            music.Genres,
            music.CreatedDateTime,
            music.LastModifiedDateTime
        );
    }
    private IActionResult CreatedAtGetMusic(Music music)
    {
        return CreatedAtAction(
            actionName: nameof(GetMusic),
            routeValues: new { id = music.Id },
            value: MapMusicResponse(music));
    }
}