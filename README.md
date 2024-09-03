# Kafka Streams PoC with .NET 8

## Overview

- This Proof of Concept (PoC) demonstrates the use of Kafka Streams with .NET 8 for real-time data synchronization in the Redcloud B2B marketplace. The project focuses on validating, processing, and synchronizing product, price, and stock data from various sources, ensuring the marketplace always has up-to-date information.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Apache Kafka (running locally or in Docker)

### Setup Kafka

1. **Start Kafka using Docker (optional):**

    ```bash
    docker-compose up -d
    ```

    Or, follow [Kafka installation instructions](https://kafka.apache.org/quickstart) to set up Kafka locally.

2. **Create Kafka Topics:**

    - `ProductDataIngestion`
    - `ValidatedProductData`
    - `SyncEngineUpdates`

### Build and Run the Application

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/HenriqueRedCloud/KafkaStreamsPoC.git
    cd KafkaStreamsPoC
    ```

2. **Build the Solution:**

    ```bash
    dotnet build
    ```

3. **Run the Application:**

    ```bash
    dotnet run --project src/KafkaStreamsPoC
    ```

4. **Simulate Data Ingestion:**

   Use the `KafkaProducerService` to send product data to the `ProductDataIngestion` topic for processing.

### Testing

1. **Run Unit Tests:**

    ```bash
    dotnet test
    ```

    The tests are located in the `src/KafkaStreamsPoC.Tests` project and cover basic validation logic.

## Project Components

- **Models/ProductData.cs**: Defines the structure of the product data model.
  
- **Services/DataValidationService.cs**: Handles validation and mapping of incoming product data.
  
- **Services/KafkaProducerService.cs**: Sends data to Kafka topics.
  
- **Services/KafkaConsumerService.cs**: Consumes data from Kafka topics and processes it.
  
- **Services/SyncEngineService.cs**: Pushes validated data to the marketplace backend.

- **Program.cs**: The entry point of the application, setting up services and starting the Kafka consumer loop.

## Expanding the PoC

This PoC can be expanded by:

- **Adding more complex validation rules.**
- **Integrating with a real backend system for data synchronization.**
- **Implementing error handling and retry mechanisms for failed messages.**
- **Scaling the solution to handle larger data volumes.**
