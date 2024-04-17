using ErrorOr;
using Models;

public static class Errors
{
    public static class Music
    {
        public static Error InvalidName => Error.Validation(
            code: "Music.InvalidName",
            description: $"Music name must be between {Models.Music.MinNameLength} and {Models.Music.MaxNameLength} characters"
        );
        public static Error InvalidAuthor => Error.Validation(
            code: "Music.InvalidAuthor",
            description: $"Music author must be between {Models.Music.MinNameLength} and {Models.Music.MaxNameLength} characters"
        );
        public static Error NotFound => Error.NotFound(
            code: "Music.NotFound",
            description: "Music not found"
        );
    }
}