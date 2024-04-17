

public record UpsertMusicRequest(
    string Name,
    string Author,
    List<string> Genres,
    DateTime CreatedDateTime
);