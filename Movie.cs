using Microsoft.Extensions.VectorData;

public record Movie(ulong Key, string Title, string Description)
{
    public static IReadOnlyCollection<Movie> List()
    {
        return
        [
            new(1, "Lion King",
                "The Lion King is a classic Disney animated film that tells the story of a young lion " +
                "named Simba who embarks on a journey to reclaim his throne as the king of the Pride " +
                "Lands after the tragic death of his father."),
            new(2, "Inception",
                "Inception is a science fiction film directed by Christopher Nolan that follows " +
                "a group of thieves who enter the dreams of their targets to steal information."),
            new(3, "The Matrix",
                "The Matrix is a science fiction film directed by the Wachowskis that follows " +
                "a computer hacker named Neo who discovers that the world he lives in is " +
                "a simulated reality created by machines."),
            new(4, "Shrek",
                "Shrek is an animated film that tells the story of an ogre named Shrek " +
                "who embarks on a quest to rescue Princess Fiona from a dragon " +
                "and bring her back to the kingdom of Duloc.")
        ];
    }
}

public class MovieVector
{
    [VectorStoreRecordKey]
    public required ulong Key { get; set; }

    [VectorStoreRecordData]
    public required string Title { get; set; }

    [VectorStoreRecordData]
    public required string Description { get; set; }

    [VectorStoreRecordVector(384, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; set; }

    public static MovieVector FromMovie(Movie movie)
    {
        return new MovieVector
        {
            Key = movie.Key,
            Title = movie.Title,
            Description = movie.Description
        };
    }
}
