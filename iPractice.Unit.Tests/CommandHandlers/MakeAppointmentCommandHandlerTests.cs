using iPractice.Application.Commands;
using iPractice.Application.Commands.Handlers;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;
using iPractice.Unit.Tests.Builders;
using Moq;

namespace iPractice.Unit.Tests.CommandHandlers
{
    public class MakeAppointmentCommandHandlerTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IAvailabilityRepository> _availabilityRepositoryMock;

        public MakeAppointmentCommandHandlerTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _availabilityRepositoryMock = new Mock<IAvailabilityRepository>();
        }
        
        [Fact]
        public async Task Client_MakingAppointment_ShouldRespondSuccessfully()
        {
            // Arrange
            var psychologist = new PsychologistBuilder().Build();
            
            var availability = new AvailabilityBuilder()
                .WithPsychologist(psychologist)
                .Build();
            
            var client = new ClientBuilder()
                .AddPsychologists(psychologist)
                .Build();
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync(client);
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availability.Id))
                .ReturnsAsync(availability);
            
            // Act
            var makeAppointmentCommand = new MakeAppointmentCommand(client.Id, availability.Id);
            var sut = new MakeAppointmentCommandHandler(_availabilityRepositoryMock.Object, _clientRepositoryMock.Object);
            await sut.HandleAsync(makeAppointmentCommand);
            
            // Assert
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availability.Id), Times.Once);
        }
        
        [Fact]
        public async Task Client_MakingAppointmentWithInvalidClient_ShouldFail()
        {
            // Arrange
            const int clientId = 1;
            const int availabilityId = 1;
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(clientId))
                .ReturnsAsync((Client)null);
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availabilityId))
                .ReturnsAsync(It.IsAny<Availability>());
            
            // Act
            var makeAppointmentCommand = new MakeAppointmentCommand(clientId, availabilityId);
            var sut = new MakeAppointmentCommandHandler(_availabilityRepositoryMock.Object, _clientRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<ClientNotFoundException>(() => sut.HandleAsync(makeAppointmentCommand));
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(clientId), Times.Once);
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availabilityId), Times.Never);
        }
        
        [Fact]
        public async Task Client_MakingInvalidAppointment_ShouldFail()
        {
            // Arrange
            var client = new ClientBuilder().Build();
            
            const int availabilityId = 1;
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync(client);
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availabilityId))
                .ReturnsAsync((Availability)null);
            
            // Act
            var makeAppointmentCommand = new MakeAppointmentCommand(client.Id, availabilityId);
            var sut = new MakeAppointmentCommandHandler(_availabilityRepositoryMock.Object, _clientRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<TimeSlotNotFoundException>(() => sut.HandleAsync(makeAppointmentCommand));
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availabilityId), Times.Once);
        }
        
        [Fact]
        public async Task Client_MakingAppointmentAlreadyAssigned_ShouldFail()
        {
            // Arrange
            var psychologist = new PsychologistBuilder().Build();
            
            var client = new ClientBuilder()
                .AddPsychologists(psychologist)
                .Build();
            
            var availability = new AvailabilityBuilder()
                .WithClient(client)
                .WithPsychologist(psychologist)
                .Build();
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync(client);
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availability.Id))
                .ReturnsAsync(availability);
            
            // Act
            var makeAppointmentCommand = new MakeAppointmentCommand(client.Id, availability.Id);
            var sut = new MakeAppointmentCommandHandler(_availabilityRepositoryMock.Object, _clientRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<ClientAlreadyAssignedException>(() => sut.HandleAsync(makeAppointmentCommand));
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availability.Id), Times.Once);
        }
        
        [Fact]
        public async Task Client_MakingAppointmentWithInvalidPsychologist_ShouldFail()
        {
            // Arrange
            var psychologist = new PsychologistBuilder().Build();
            
            var invalidPsychologist = new PsychologistBuilder()
                .WithId(2)
                .Build();
            
            var client = new ClientBuilder()
                .AddPsychologists(psychologist)
                .Build();
            
            var availability = new AvailabilityBuilder()
                .WithPsychologist(invalidPsychologist)
                .Build();
            
            _clientRepositoryMock.Setup(x => x.GetClientAsync(client.Id))
                .ReturnsAsync(client);
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availability.Id))
                .ReturnsAsync(availability);
            
            // Act
            var makeAppointmentCommand = new MakeAppointmentCommand(client.Id, availability.Id);
            var sut = new MakeAppointmentCommandHandler(_availabilityRepositoryMock.Object, _clientRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<PsychologistMismatchException>(() => sut.HandleAsync(makeAppointmentCommand));
            
            _clientRepositoryMock.Verify(x => x.GetClientAsync(client.Id), Times.Once);
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availability.Id), Times.Once);
        }
    }
};