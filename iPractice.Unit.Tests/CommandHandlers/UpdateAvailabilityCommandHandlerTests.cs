using iPractice.Application.Commands;
using iPractice.Application.Commands.Handlers;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;
using iPractice.Unit.Tests.Builders;
using Moq;

namespace iPractice.Unit.Tests.CommandHandlers
{
    public class UpdateAvailabilityCommandHandlerTests
    {
        private readonly Mock<IAvailabilityRepository> _availabilityRepositoryMock;

        public UpdateAvailabilityCommandHandlerTests()
        {
            _availabilityRepositoryMock = new Mock<IAvailabilityRepository>();
        }
        
        [Fact]
        public async Task Psychologist_UpdatingAvailability_ShouldRespondSuccessfully()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;

            var psychologist = new PsychologistBuilder().Build();

            var availability = new AvailabilityBuilder()
                .WithPsychologist(psychologist)
                .Build();
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availability.Id))
                .ReturnsAsync(availability);
            
            // Act
            var updateAvailabilityCommand = new UpdateAvailabilityCommand(psychologist.Id, availability.Id, dateTimeNow.AddHours(1), dateTimeNow.AddHours(1).AddMinutes(30));
            var sut = new UpdateAvailabilityCommandHandler(_availabilityRepositoryMock.Object);
            
            await sut.HandleAsync(updateAvailabilityCommand);
            
            // Assert
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availability.Id), Times.Once);
        }
        
        [Fact]
        public async Task Psychologist_UpdatingIncorrectAvailability_ShouldFail()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;

            var psychologist = new PsychologistBuilder().Build();
            
            var availability = new AvailabilityBuilder()
                .WithPsychologist(psychologist)
                .Build();
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availability.Id))
                .ReturnsAsync((Availability)null);
            
            // Act
            var updateAvailabilityCommand = new UpdateAvailabilityCommand(psychologist.Id, availability.Id, dateTimeNow.AddHours(1), dateTimeNow.AddHours(1).AddMinutes(30));
            var sut = new UpdateAvailabilityCommandHandler(_availabilityRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<AvailabilityNotFoundException>(() => sut.HandleAsync(updateAvailabilityCommand));
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availability.Id), Times.Once);
        }
        
        [Fact]
        public async Task Psychologist_UpdatingInvalidAvailability_ShouldFail()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;

            var availability = new AvailabilityBuilder()
                .WithPsychologist(new PsychologistBuilder().Build())
                .Build();
            
            _availabilityRepositoryMock.Setup(x => x.GetAvailabilityAsync(availability.Id))
                .ReturnsAsync(availability);
            
            // Act
            var updateAvailabilityCommand = new UpdateAvailabilityCommand(100, availability.Id, dateTimeNow.AddHours(1), dateTimeNow.AddHours(1).AddMinutes(30));
            var sut = new UpdateAvailabilityCommandHandler(_availabilityRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<PsychologistMismatchException>(() => sut.HandleAsync(updateAvailabilityCommand));
            _availabilityRepositoryMock.Verify(x => x.GetAvailabilityAsync(availability.Id), Times.Once);
        }
    }
};