using ErrorOr;

public class MusicService : IMusicService
{
    private static readonly Dictionary<Guid, Music> _musics = new(); 
    public void CreateMusic(Music music)
    {
        _musics.Add(music.Id, music);
    }

    public void DeleteMusic(Guid id)
    {
        _musics.Remove(id);
    }

    public ErrorOr<Music> GetMusic(Guid id)
    {
        if(_musics.TryGetValue(id, out var music))
        {

            return music;
        }

        return Errors.Music.NotFound;
    }

    public void UpsertMusic(Music music)
    {
        _musics[music.Id] = music;
    }
}