namespace OICTCaseStudy.App.Dto;

public class CardValidityDto(DateTime validTo, string cardState)
{
    public DateTime ValidTo { get; set; } = validTo;
    public string State { get; set; } = cardState;
}