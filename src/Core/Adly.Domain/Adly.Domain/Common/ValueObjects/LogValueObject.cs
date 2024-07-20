namespace Adly.Domain.Common.ValueObjects;

public record LogValueObject(DateTime EntryDate,string Message,string? AdditionalDescription=null);