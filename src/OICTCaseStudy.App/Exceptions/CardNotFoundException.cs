namespace OICTCaseStudy.App.Exceptions;

public sealed class CardNotFoundException(ulong cardId)
    : Exception($"Card {cardId} was not found.");
