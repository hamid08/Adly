namespace Adly.Domain.Common;

public record DomainResult(bool IsSuccess, string ErrorMessage)
{
    public static DomainResult None = new DomainResult(true, string.Empty);
}