namespace OICTCaseStudy.App.Dto;

public class CardValidityDto(DateOnly validTo, string cardState)
{
    public DateOnly ValidTo { get; set; } = validTo;
    public string State { get; set; } = cardState;
}