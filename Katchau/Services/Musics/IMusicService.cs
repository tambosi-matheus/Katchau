
using ErrorOr;

public interface IMusicService
{
    void CreateMusic(Music music);
    void DeleteMusic(Guid id);
    ErrorOr<Music> GetMusic(Guid id);
    void UpsertMusic(Music music);
}