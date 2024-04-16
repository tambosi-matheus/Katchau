

public record MusicResponse(
    Guid Id,
    string Name,
    string Author,
    List<string> Genres,
    DateTime CreatedDateTime,
    DateTime LastModifiedDateTime
);