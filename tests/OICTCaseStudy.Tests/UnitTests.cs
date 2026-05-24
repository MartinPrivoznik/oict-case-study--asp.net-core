using System.Net;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using OICTCaseStudy.ApiClient.Litacka.Core.Response;
using OICTCaseStudy.ApiClient.Litacka.Services;
using OICTCaseStudy.App.Exceptions;
using OICTCaseStudy.App.Query.GetCardValidityById;

namespace OICTCaseStudy.Tests;

public class GetCardValidityByIdQueryHandlerTests
{
    private GetCardValidityByIdQueryHandler _handler = null!;
    private ILitackaCardService _service = null!;

    [SetUp]
    public void Setup()
    {
        _service = Substitute.For<ILitackaCardService>();

        _service.RequestCardValidity(12345UL, Arg.Any<CancellationToken>())
            .Returns(new CardValidityResponse { ValidityEnd = new DateTime(2025, 12, 31) });
        _service.RequestCardState(12345UL, Arg.Any<CancellationToken>())
            .Returns(new CardStateResponse { StateDescription = "Active" });
        _service.RequestCardValidity(99999UL, Arg.Any<CancellationToken>())
            .Throws(new HttpRequestException(null, null, HttpStatusCode.NotFound));

        _handler = new GetCardValidityByIdQueryHandler(_service);
    }

    [Test]
    public async Task Handle_CombinesStateAndValidity_IntoDto()
    {
        var result = await _handler.Handle(new GetCardValidityByIdQuery(12345), CancellationToken.None);

        Assert.That(result.State, Is.EqualTo("Active"));
        Assert.That(result.ValidTo, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Handle_WhenCardNotFound_ThrowsCardNotFoundException()
    {
        Assert.ThrowsAsync<CardNotFoundException>(() =>
            _handler.Handle(new GetCardValidityByIdQuery(99999), CancellationToken.None));
    }
}