using NSubstitute;
using OICTCaseStudy.ApiClient.Litacka.Core.Response;
using OICTCaseStudy.ApiClient.Litacka.Services;
using OICTCaseStudy.App.Query.GetCardValidityById;

namespace OICTCaseStudy.Tests;

public class GetCardValidityByIdQueryHandlerTests
{
    [Test]
    public async Task Handle_CombinesStateAndValidity_IntoDto()
    {
        var service = Substitute.For<ILitackaCardService>();
        service.RequestCardValidity(12345UL, Arg.Any<CancellationToken>())
            .Returns(new CardValidityResponse { ValidityEnd = new DateTime(2025, 12, 31) });
        service.RequestCardState(12345UL, Arg.Any<CancellationToken>())
            .Returns(new CardStateResponse { StateDescription = "Active" });

        var handler = new GetCardValidityByIdQueryHandler(service);
        var result = await handler.Handle(new GetCardValidityByIdQuery(12345), CancellationToken.None);

        Assert.That(result.State, Is.EqualTo("Active"));
        Assert.That(result.ValidTo, Is.Not.Null.And.Not.Empty);
    }
}
