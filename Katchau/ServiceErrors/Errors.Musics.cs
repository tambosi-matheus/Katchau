using ErrorOr;

public static class Errors
{
    public static class Music
    {
        public static Error NotFound => Error.NotFound(
            code: "Music.NotFound",
            description: "Music not found"
        );
    }
}