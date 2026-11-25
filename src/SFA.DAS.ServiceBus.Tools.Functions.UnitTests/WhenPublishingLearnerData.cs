using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.LearnerData.Events;
using SFA.DAS.ServiceBus.Tools.Functions.Services;

namespace SFA.DAS.ServiceBus.Tools.Functions.UnitTests;

public class WhenPublishingLearnerData
{
    [Test]
    public async Task Then_Endpoint_Publish_Is_Called_With_OnServicePayments()
    {
        var endpoint = new Mock<IFunctionEndpoint>();
        var logger = new Mock<ILogger<MessageProcessor>>();
        var sut = new MessageProcessor(endpoint.Object, logger.Object);
        var context = Mock.Of<FunctionContext>();
        var payload = "{\"ULN\":1234567890,\"OnServicePayments\":true}";
        await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(payload));

        await sut.PublishEvent<LearnerDataEvent>(stream, context);

        endpoint.Verify(
            x => x.Publish(
                It.Is<LearnerDataEvent>(e => e.ULN == 1234567890 && e.OnServicePayments == true),
                context),
            Times.Once);
    }
}
