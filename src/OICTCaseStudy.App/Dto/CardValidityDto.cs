using System.Globalization;

namespace OICTCaseStudy.App.Dto;

public class CardValidityDto(DateTime validTo, string cardState)
{
    public string ValidTo { get; set; } = validTo.ToString("d", CultureInfo.CurrentCulture);
    public string State { get; set; } = cardState;
}