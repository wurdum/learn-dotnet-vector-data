# .NET Vector Search Demo

This project demonstrates how to use vector embeddings and similarity search in .NET, leveraging:
- Microsoft.Extensions.VectorData for vector operations
- Ollama with all-minilm model for generating embeddings
- Qdrant as the vector database
- Docker for running the required services

## Prerequisites

- .NET 8.0 SDK
- Docker and Docker Compose

## Getting Started

1. Clone this repository

2. Start the required services using Docker Compose:

```bash
docker compose up
```

This will start:
- Ollama service with all-minilm model (port 11434)
- Qdrant vector database (ports 6333, 6334)


3. Run the application:

```bash
dotnet run --project learn-dotnet-vector-data.csproj
```

## How It Works

The demo shows how to:

1. Create embeddings for movie descriptions using the all-minilm model through Ollama
2. Store these embeddings in Qdrant vector database
3. Perform similarity search to find movies matching a text query
