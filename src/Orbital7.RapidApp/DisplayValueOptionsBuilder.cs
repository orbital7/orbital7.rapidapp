namespace Orbital7.Extensions;

// TODO: Move to Orbital7.Extensions.
public class DisplayValueOptionsBuilder
{
    private DisplayValueOptions _options = new DisplayValueOptions();

    public DisplayValueOptions Build()
    {
        return _options;
    }


    // Currency options.
    public DisplayValueOptionsBuilder UseCurrencyForDecimals(
        bool value)
    {
        _options.UseCurrencyForDecimals = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForCurrencyAddSymbol(
        bool value)
    {
        _options.CurrencyAddSymbol = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForCurrencyAddCommas(
        bool value)
    {
        _options.CurrencyAddCommas = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForCurrencyAddPlusIfPositive(
        bool value)
    {
        _options.CurrencyAddPlusIfPositive = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForCurrencyUseDecimalPlaces(
        int value)
    {
        _options.CurrencyDecimalPlaces = value;
        return this;
    }
    public DisplayValueOptionsBuilder ForCurrencyUseRoundingMode(
        MidpointRounding value)
    {
        _options.CurrencyRoundingMode = value;
        return this;
    }


    // Percentage options.
    public DisplayValueOptionsBuilder ForPercentageAddCommas(
        bool value)
    {
        _options.PercentageAddCommas = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForPercentageAddPlusIfPositive(
        bool value)
    {
        _options.PercentageAddPlusIfPositive = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForPercentageUseDecimalPlaces(
        int value)
    {
        _options.PercentageDecimalPlaces = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForPercentageUseRoundingMode(
        MidpointRounding value)
    {
        _options.PercentageRoundingMode = value;
        return this;
    }


    // Number options.
    public DisplayValueOptionsBuilder ForNumbersAddPlusIfPositive(
        bool value)
    {
        _options.ForNumbersAddPlusIfPositive = value;
        return this;
    }


    // Date/time options.
    public DisplayValueOptionsBuilder ForDateTimeUseFormat(
        string value)
    {
        _options.DateTimeFormat = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForDateOnlyUseFormat(
        string value)
    {
        _options.DateOnlyFormat = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForTimeOnlyUseFormat(
        string value)
    {
        _options.TimeOnlyFormat = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForTimeSpanUseFormat(
        string value)
    {
        _options.TimeSpanFormat = value;
        return this;
    }

    public DisplayValueOptionsBuilder ForDateTimeUseTimeZoneId(
        string value)
    {
        _options.TimeZoneId = value;
        return this;
    }
}