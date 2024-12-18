using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;

var store = new QdrantVectorStore(new QdrantClient("localhost"));

var collection = store.GetCollection<ulong, MovieVector>("movies");
await collection.CreateCollectionIfNotExistsAsync();

var generator = new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "all-minilm");
foreach (var vector in Movie.List().Select(MovieVector.FromMovie))
{
    vector.Vector = await generator.GenerateEmbeddingVectorAsync(vector.Description);
    await collection.UpsertAsync(vector);
}

var query = "A family friendly movie that includes ogres and dragons";
var queryEmbedding = await generator.GenerateEmbeddingVectorAsync(query);

var searchOptions = new VectorSearchOptions
{
    Top = 2,
    VectorPropertyName = "Vector"
};

var results = await collection.VectorizedSearchAsync(queryEmbedding, searchOptions);
await foreach (var result in results.Results)
{
    Console.WriteLine($"Title: {result.Record.Title}");
    Console.WriteLine($"Description: {result.Record.Description}");
    Console.WriteLine($"Score: {result.Score}");
    Console.WriteLine();
}
