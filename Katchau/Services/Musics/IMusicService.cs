
using ErrorOr;
using Models;

public interface IMusicService
{
    ErrorOr<Created> CreateMusic(Music music);
    ErrorOr<Music> GetMusic(Guid id);
    ErrorOr<UpsertMusic> UpsertMusic(Music music);
    ErrorOr<Deleted> DeleteMusic(Guid id);
}