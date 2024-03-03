using iPractice.Application.Queries;
using iPractice.Application.Queries.Handlers;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;
using iPractice.Unit.Tests.Builders;
using Moq;

namespace iPractice.Unit.Tests.QueryHandlers
{
    public class GetAvailablePsychologistsQueryHandlerTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;

        public GetAvailablePsychologistsQueryHandlerTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
        }
        
        [Fact]
        public async Task Client_WithPsychologistsAvailable_ShouldReturnTimeSlots()
        {
            // Arrange
            var psychologist = new PsychologistBuilder().Build();
            
            var availabilities = new AvailabilityBuilder()
                .WithPsychologist(psychologist)
                .Build();
            
            psychologist.AddAvailability(new List<Availability>{ availabilities });
            
            var client = new ClientBuilder()
                .AddPsychologists(psychologist)
                .Build();
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync(client);
            
            // Act
            var getAvailablePsychologistsQuery = new GetAvailablePsychologistsQuery(client.Id);
            var sut = new GetAvailablePsychologistsQueryHandler(_clientRepositoryMock.Object);
            var psychologistsTimeSlots = await sut.HandleAsync(getAvailablePsychologistsQuery);
            
            // Assert
            Assert.NotNull(psychologistsTimeSlots);
            
            psychologistsTimeSlots = psychologistsTimeSlots.ToList();
            
            Assert.Single(psychologistsTimeSlots);
            Assert.Equal(psychologist, psychologistsTimeSlots.FirstOrDefault());
            Assert.Equal(psychologist, psychologistsTimeSlots.LastOrDefault());
            Assert.Equal(psychologist.Availabilities, psychologistsTimeSlots.FirstOrDefault()?.Availabilities);
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
        }
        
        [Fact]
        public async Task Client_WithoutPsychologistsAvailable_ShouldReturnEmptyResult()
        {
            // Arrange
            var client = new ClientBuilder().Build();
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync(client);
            
            // Act
            var getAvailablePsychologistsQuery = new GetAvailablePsychologistsQuery(client.Id);
            var sut = new GetAvailablePsychologistsQueryHandler(_clientRepositoryMock.Object);
            var psychologistsTimeSlots = await sut.HandleAsync(getAvailablePsychologistsQuery);
            
            // Assert
            Assert.NotNull(psychologistsTimeSlots);
            Assert.Empty(psychologistsTimeSlots);
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
        }
        
        [Fact]
        public async Task Client_WithPsychologistsUnavailable_ShouldReturnEmptyAvailabilities()
        {
            // Arrange
            var client = new ClientBuilder()
                .AddPsychologists(new PsychologistBuilder().Build())
                .Build();
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync(client);
            
            // Act
            var getAvailablePsychologistsQuery = new GetAvailablePsychologistsQuery(client.Id);
            var sut = new GetAvailablePsychologistsQueryHandler(_clientRepositoryMock.Object);
            var psychologistsTimeSlots = await sut.HandleAsync(getAvailablePsychologistsQuery);
            
            // Assert
            Assert.NotNull(psychologistsTimeSlots);
            var psychologist = psychologistsTimeSlots.ToList().FirstOrDefault();
            
            Assert.NotNull(psychologist);
            Assert.NotNull(psychologist.Availabilities);
            Assert.Empty(psychologist.Availabilities);
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
        }
        
        [Fact]
        public async Task Client_NotFound_ShouldFail()
        {
            // Arrange
            var client = new ClientBuilder().Build();
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync((Client)null);
            
            // Act
            var getAvailablePsychologistsQuery = new GetAvailablePsychologistsQuery(client.Id);
            var sut = new GetAvailablePsychologistsQueryHandler(_clientRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<ClientNotFoundException>(() => sut.HandleAsync(getAvailablePsychologistsQuery));
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
        }
    }
};