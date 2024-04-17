using ErrorOr;
using Models;

public class MusicService : IMusicService
{
    private static readonly Dictionary<Guid, Music> _musics = new(); 
    public ErrorOr<Created> CreateMusic(Music music)
    {
        _musics.Add(music.Id, music);
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteMusic(Guid id)
    {
        _musics.Remove(id);
        return Result.Deleted;
    }

    public ErrorOr<Music> GetMusic(Guid id)
    {
        if(_musics.TryGetValue(id, out var music))
        {

            return music;
        }

        return Errors.Music.NotFound;
    }

    public ErrorOr<UpsertMusic> UpsertMusic(Music music)
    {
        var isNewlyCreated = !_musics.ContainsKey(music.Id);
        _musics[music.Id] = music;

        return new UpsertMusic(isNewlyCreated);
    }
}