using iPractice.Application.Commands;
using iPractice.Application.Services;
using iPractice.Domain.Exceptions;
using iPractice.Unit.Tests.Builders;

namespace iPractice.Unit.Tests.Services
{
    public class AvailabilityServiceTests
    {
        [Theory]
        [InlineData("2026-01-01 10:00:00", "2026-01-01 12:00:00", 4)]
        [InlineData("2026-01-01 10:00:00", "2026-01-01 14:00:00", 8)]
        [InlineData("2026-01-01 10:00:00", "2026-01-01 10:20:00", 0)]
        public void Availability_WithValidPeriod_ShouldReturnTimeSlots(string from, string to, int expectedCount)
        {
            // Arrange
            var dateTimeFrom = DateTime.Parse(from);
            var dateTimeTo = DateTime.Parse(to);
            
            var psychologist = new PsychologistBuilder().Build();
         
            var createAvailabilityCommand = new CreateAvailabilityCommand(psychologist.Id, dateTimeFrom, dateTimeTo);
           
            // Act
            var sut = new AvailabilityService();
            var availabilities = sut.GenerateAvailabilitiesFromBatch(psychologist, createAvailabilityCommand);

            // Assert
            Assert.NotNull(availabilities);
            
            availabilities = availabilities.ToList();
            
            Assert.Equal(expectedCount, availabilities.Count());
            
            var start = dateTimeFrom;
            var end = start.AddMinutes(30);
            
            foreach (var timeSlot in availabilities)
            {
                Assert.Equal(start, timeSlot.From);
                Assert.Equal(end, timeSlot.To);
                Assert.Equal(psychologist.Id, timeSlot.Psychologist.Id);
                
                start = start.AddMinutes(30);
                end = end.AddMinutes(30);
            }
        }
        
        [Theory]
        [InlineData("2026-01-01 08:00:00", "2025-12-31 10:00:00")]
        [InlineData("2026-01-01 10:00:00", "2026-01-01 08:00:00")]
        [InlineData("2020-01-01 08:00:00", "2026-01-01 10:00:00")]
        [InlineData("2020-01-01 08:00:00", "2020-01-01 10:00:00")]
        public void Availability_WithInvalidPeriod_ShouldFail(string from, string to)
        {
            // Arrange
            var dateTimeFrom = DateTime.Parse(from);
            var dateTimeTo = DateTime.Parse(to);
            
            var psychologist = new PsychologistBuilder().Build();
         
            var createAvailabilityCommand = new CreateAvailabilityCommand(psychologist.Id, dateTimeFrom, dateTimeTo);
           
            // Act
            var sut = new AvailabilityService();

            // Assert
            Assert.Throws<InvalidAvailabilityException>(() => sut.GenerateAvailabilitiesFromBatch(psychologist, createAvailabilityCommand));
        }
    }
};