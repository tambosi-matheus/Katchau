

public record UpsertMusicRequest(
    string Name,
    string Description,
    DateTime CreatedDateTime,
    List<string> Genres
);