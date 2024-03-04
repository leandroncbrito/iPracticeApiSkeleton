# iPractice Technical Assignment - Skeleton Project
#### We have created this project to reduce the setup time required for our assignment. If and how it is used is entirely at your discretion.

### Project Structure
This is a simple ASP.Net core API project setup to run using SQLite. There is an initial migration to create and seed the database and two controllers with the required endpoints.

It is entirely acceptable to add remove or rename any of the components of the project.

### Architecture and Design
- The `iPractice.Api.Models` folder contains the models used as DTOs for the API.
- `CustomExceptionFilter` is used to handle exceptions and return a consistent error response. This way we avoid the need to handle exceptions in every controller action.
    - For more accurate error handling, we can use try-catch blocks in the command handlers and throw custom exceptions.
    - We can also use a middleware to handle exceptions globally.  


- CQRS pattern is used to separate the read and write concerns.
  - The `iPractice.Application.Commands` folder contains the command handlers and the `iPractice.Application.Queries` folder contains the query handlers.
  - `ICommandHandler` and `IQueryHandler` interfaces are used to define the common methods for the command and query handlers.
  - `CommandHandler` and `QueryHandler` classes are used to implement the common methods for the command and query handlers.


- The `iPractice.DataAccess.Repositories` folder contains the database context and the repositories.
  - `IRepository` interface is used to define the common methods for the repositories.
  - `Repository` class is used to implement the common methods for the repositories.
  
### Unit Tests
With unit tests, we can test the command and query handlers to ensure that the business logic is working as expected.
- `iPractice.Unit.Tests` project contains the unit tests for the API.
- `xUnit` library is used to write the unit tests.
- `Moq` library is used to mock the dependencies of the command and query handlers.
- `Builder` pattern is used to create the test data for the unit tests and to reduce the duplication of the test data creation.

### Integration Tests (TODO)
With integration tests, we can test the API endpoints and the database interactions to ensure that the API is working as expected and the database is being updated correctly.
Some test cases to consider:
- Client API
  - Test if `GET /Clients/{clientId}/timeslots` returns the correct timeslots for the client id.
  - Test if `POST /Clients/{clientId}/appointment` creates the appointment for the client id returning the correct response and updating the database.
  - Test the error cases for the API endpoints.
    - Client not found.
    - Psychologist not found.
    - Availability not found.
    - Invalid input data for creating the appointment.
  

- Psychologist API
  - Test if `POST /Psychologists/{psychologistId}/availability` creates the availability for the psychologist from a batch of timeslots returning the correct response and updating the database.
  - Test if `PUT /Psychologists/{psychologistId}/availability/{availabilityId}` updates the availability for the psychologist returning the correct response and updating the database.
  - Test the error cases for the API endpoints.    
    - Psychologist not found.
    - Availability not found.
    - Invalid input data for creating the availability.
    - Invalid input data for updating the availability.


- `FluentAssertions` library can be used to write the integration tests and to verify the responses.
- `WebApplicationFactory` can be used to create the test server for the integration tests and to configure the test server.
  - `HttpClient` can be used to make the HTTP requests to the test server and to verify the responses.

