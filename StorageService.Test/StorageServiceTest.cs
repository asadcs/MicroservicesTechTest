using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Pixel.Application.Contract.Interfaces;
using Pixel.Application.Events;
using Pixel.Infrastructure.Messaging;
using Serilog;
using Serilog.Sinks.TestCorrelator;

namespace StorageService.Test
{
    public class StorageServiceTest
    {
        public StorageServiceTest()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.TestCorrelator()
                .CreateLogger();
        }

        [Fact]
        public async Task Consume_ValidEvent_CallsRegistrationService()
        {
            // Setup Serilog to use with Microsoft.Extensions.Logging
            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
            var logger = loggerFactory.CreateLogger<VisitRegisteredEventConsumer>();

            var mockRegistrationService = new Mock<IVisitRegistrationService>();
            var consumer = new VisitRegisteredEventConsumer(logger, mockRegistrationService.Object);
            var visitEvent = new VisitRegisteredEvent
            {
                Referrer = "https://example.com",
                UserAgent = "Mozilla/5.0",
                IpAddress = "127.0.0.1",
                Timestamp = DateTime.UtcNow
            };

            var consumeContext = Mock.Of<ConsumeContext<VisitRegisteredEvent>>(ctx => ctx.Message == visitEvent);

            await consumer.Consume(consumeContext);

            mockRegistrationService.Verify(s => s.RegisterVisitAsync(visitEvent), Times.Once);
        }

        [Fact]
        public async Task Consume_NullEvent_DoesNotCallRegistrationService()
        {
            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
            var logger = loggerFactory.CreateLogger<VisitRegisteredEventConsumer>();

            var mockRegistrationService = new Mock<IVisitRegistrationService>();
            var consumer = new VisitRegisteredEventConsumer(logger, mockRegistrationService.Object);
            var consumeContext = Mock.Of<ConsumeContext<VisitRegisteredEvent>>(ctx => ctx.Message == null);

            await consumer.Consume(consumeContext);

            mockRegistrationService.Verify(s => s.RegisterVisitAsync(It.IsAny<VisitRegisteredEvent>()), Times.Never);
        }


        [Fact]
        public void LogConcurrently_LogsCorrectly()
        {
            using (var logContext = TestCorrelator.CreateContext())
            {
                var tasks = new List<Task>();
                for (int i = 0; i < 10; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            Log.Information($"Log message {j} from task {Task.CurrentId}");
                        }
                    }));
                }

                Task.WaitAll(tasks.ToArray());

                // Assert with Serilog's testing library to verify the log output
                TestCorrelator.GetLogEventsFromContextGuid(logContext.Guid).Should().HaveCount(1000);
            }
        }
    }

}