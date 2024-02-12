//using FluentAssertions;
using FluentAssertions;
using MassTransit;
using Moq;
using Pixel.Application.Events;
using Pixel.Application.Features.Command;
using Pixel.Application.Features.Handlers;
using Pixel.Application.Features.Validators;
using Pixel.Domain.Exceptions;
using Xunit;

namespace Pixel.Api.Test.Integration
{
    public class CommandTests
    {
        [Fact]
        public async Task AddVisitCommand_WithNullIpAddress_ShouldThrowCommandProcessingException()
        {
            // Arrange
            var command = new AddVisitCommand("referrer", "useragent", null);
            var validator = new AddVisitCommandValidator(); // Using the real validator for a more integrated test.
            var publishEndpointMock = new Mock<IPublishEndpoint>();
            var handler = new AddVisitCommandHandler(publishEndpointMock.Object, validator);

            // Act & Assert
            await Assert.ThrowsAsync<CommandProcessingException>(() => handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task AddVisitCommand_WithValidIpAddress_ShouldPublishEventWithCorrectData()
        {
            // Arrange
            var command = new AddVisitCommand("referrer", "useragent", "127.0.0.1");
            var validatorMock = new Mock<IAddVisitCommandValidator>();
            validatorMock.Setup(v => v.Validate(command)); // Assuming validation passes.
            var publishEndpointMock = new Mock<IPublishEndpoint>();

            var handler = new AddVisitCommandHandler(publishEndpointMock.Object, validatorMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            publishEndpointMock.Verify(m => m.Publish(It.Is<VisitRegisteredEvent>(
                evt => evt.Referrer == "referrer" && evt.UserAgent == "useragent" && evt.IpAddress == "127.0.0.1" && evt.Timestamp <= DateTime.UtcNow),
                It.IsAny<CancellationToken>()), Times.Once);
        }


    }





}
