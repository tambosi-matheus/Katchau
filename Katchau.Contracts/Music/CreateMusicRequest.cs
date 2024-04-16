

public record CreateMusicRequest(
    string Name,
    string Author,
    List<string> Genres,
    DateTime CreatedDateTime
);