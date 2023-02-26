using MassTransit;
using System.Threading.Tasks;
using Sample.Contracts;
using Microsoft.Extensions.Logging;

namespace Sample.Components.Consumers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        private readonly ILogger<SubmitOrderConsumer> _logger;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            _logger.Log(LogLevel.Debug, "SubmitOrderConsumer: {CustomerNumber}", context.Message.CustomerNumber);
            if (context.Message.CustomerNumber.Contains("TEST"))
            {
                await context.RespondAsync<OrderSubmissionRejected>(new
                {
                    context.Message.OrderId,
                    TimeStamp = InVar.Timestamp,
                    context.Message.CustomerNumber,
                    Reason = $"Test Customer cannot submit orders: {context.Message.CustomerNumber}"
                });

                return;
            }

            await context.RespondAsync<OrderSubmissionAccepted>(new
            {
                context.Message.OrderId,
                TimeStamp = InVar.Timestamp,
                context.Message.CustomerNumber
            });
        }
    }
}
