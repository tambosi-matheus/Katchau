using ErrorOr;

namespace Models;
public class Music
{
    public const int MinNameLength = 1;
    public const int MaxNameLength = 50;
    public const int MinAuthorLength = 1;
    public const int MaxAuthorLength = 20;

    public Guid Id { get; }
    public string Name { get; }
    public string Author { get; }
    public List<string> Genres { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime LastModifiedDateTime { get;}

    private Music(
        Guid id, 
        string name, 
        string author, 
        List<string> genres, 
        DateTime createdDateTime,
        DateTime lastModifiedDateTime)
    {
        Id = id;
        Name = name;
        Author = author;
        Genres = genres;
        CreatedDateTime = createdDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
    }     
    public static ErrorOr<Music> Create(
        string name,
        string author,
        List<string> genres,
        DateTime createdDateTime,
        Guid? id = null
    )
    {

        List<Error> errors = new();
        
        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Music.InvalidName);
        }
        if (author.Length is < MinAuthorLength or > MaxAuthorLength)
        {
            errors.Add(Errors.Music.InvalidAuthor);
        }
        
        if(errors.Count > 0) 
        {
            return errors;
        }

        return new Music(
            id ?? Guid.NewGuid(),
            name,
            author,
            genres,
            createdDateTime,
            DateTime.UtcNow);
    }

    internal static ErrorOr<Music> From(CreateMusicRequest request)
    {
        return Create(
            request.Name,
            request.Author,
            request.Genres,
            request.CreatedDateTime
        );
    }

    internal static ErrorOr<Music> From(Guid id, UpsertMusicRequest request)
    {
        return Create(
            request.Name,
            request.Author,
            request.Genres,
            request.CreatedDateTime,
            id
        );
    }
}