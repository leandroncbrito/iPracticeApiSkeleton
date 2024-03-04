using iPractice.Application.Commands;
using iPractice.Application.Commands.Handlers;
using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;
using iPractice.Unit.Tests.Builders;
using Moq;

namespace iPractice.Unit.Tests.CommandHandlers
{
    public class CreateAvailabilityCommandHandlerTests
    {
        private readonly Mock<IAvailabilityService> _availabilityServiceMock;
        private readonly Mock<IPsychologistRepository> _psychologistRepositoryMock;

        public CreateAvailabilityCommandHandlerTests()
        {
            _availabilityServiceMock = new Mock<IAvailabilityService>();
            _psychologistRepositoryMock = new Mock<IPsychologistRepository>();
        }
        
        [Fact]
        public async Task Client_MakingAppointment_ShouldRespondSuccessfully()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;
            
            var psychologist = new PsychologistBuilder()
                .AddAvailability()
                .Build();

            var availabilities = psychologist.Availabilities;
            
            var createAvailabilityCommand = new CreateAvailabilityCommand(psychologist.Id, dateTimeNow, dateTimeNow.AddMinutes(30));
           
            _availabilityServiceMock.Setup(x => x.GenerateAvailabilitiesFromBatch(psychologist, createAvailabilityCommand))
                .Returns(availabilities);
            
            _psychologistRepositoryMock.Setup(x => x.GetPsychologistAsync(psychologist.Id))
                .ReturnsAsync(psychologist);
            
            // Act
            var sut = new CreateAvailabilityCommandHandler(_availabilityServiceMock.Object, _psychologistRepositoryMock.Object);
            await sut.HandleAsync(createAvailabilityCommand);
            
            // Assert
            _psychologistRepositoryMock.Verify(x => x.GetPsychologistAsync(psychologist.Id), Times.Once);
            _availabilityServiceMock.Verify(x => x.GenerateAvailabilitiesFromBatch(psychologist, createAvailabilityCommand), Times.Once);
        }
        
        [Fact]
        public async Task Psychologist_CreatingInvalidAvailabilities_ShouldFail()
        {
            // Arrange
            var psychologist = new PsychologistBuilder().Build();
            
            var createAvailabilityCommand = new CreateAvailabilityCommand(psychologist.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>());
           
            _psychologistRepositoryMock.Setup(x => x.GetPsychologistAsync(psychologist.Id))
                .ReturnsAsync((Psychologist)null);
            
            // Act
            var sut = new CreateAvailabilityCommandHandler(_availabilityServiceMock.Object, _psychologistRepositoryMock.Object);
            
            // Assert
            await Assert.ThrowsAsync<PsychologistNotFoundException>(() => sut.HandleAsync(createAvailabilityCommand));
            
            _psychologistRepositoryMock.Verify(x => x.GetPsychologistAsync(psychologist.Id), Times.Once);
            _availabilityServiceMock.Verify(x => x.GenerateAvailabilitiesFromBatch(It.IsAny<Psychologist>(), createAvailabilityCommand), Times.Never);
        }
    }
};