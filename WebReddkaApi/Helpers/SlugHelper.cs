using System.Text.RegularExpressions;

namespace WebAPIDB.Helpers;

public static class SlugHelper
{
    public static string Slugify(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        text = text.ToLowerInvariant();

        text = Transliterate(text);

        text = Regex.Replace(text, @"[^a-z0-9\s-]", "");

        text = Regex.Replace(text, @"\s+", "-").Trim('-');

        text = Regex.Replace(text, @"-+", "-");

        return text;
    }

    private static string Transliterate(string text)
    {
        var translit = new (string Cyrillic, string Latin)[]
        {
            ("а", "a"), ("б", "b"), ("в", "v"), ("г", "h"), ("ґ", "g"), ("д", "d"),
            ("е", "e"), ("є", "ie"), ("ж", "zh"), ("з", "z"), ("и", "y"), ("і", "i"),
            ("ї", "i"), ("й", "i"), ("к", "k"), ("л", "l"), ("м", "m"), ("н", "n"),
            ("о", "o"), ("п", "p"), ("р", "r"), ("с", "s"), ("т", "t"), ("у", "u"),
            ("ф", "f"), ("х", "kh"), ("ц", "ts"), ("ч", "ch"), ("ш", "sh"), ("щ", "shch"),
            ("ь", ""), ("ю", "iu"), ("я", "ia")
        };

        foreach (var (cyr, lat) in translit)
            text = text.Replace(cyr, lat);

        return text;
    }
}