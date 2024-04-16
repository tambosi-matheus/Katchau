public class Music
{

    public Guid Id { get; }
    public string Name { get; }
    public string Author { get; }
    public List<string> Genres { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime LastModifiedDateTime { get;}

    public Music(
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
}