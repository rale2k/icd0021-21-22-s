namespace Base.Domain;

public class LangStr : Dictionary<string, string>
{
    private const string DefaultCulture = "en";
    
    public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
    }
    
    public LangStr()
    {
    }
    
    public LangStr(string value, string culture)
    {
        this[culture] = value;
    }
    
    private static string GetCultureName(string culture)
    {
        return culture.Split("-")[0];
    }
    
    public void SetTranslation(string value)
    {
        this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    }

    public string? Translate(string? culture = null)
    {
        if (this.Count == 0) return null;

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        // do we have exact match - en-GB == en-GB
        if (ContainsKey(culture))
        {
            return this[culture];
        }

        // do we have match without the region en-US.StartsWith(en)
        var key = Keys.FirstOrDefault(t => culture.StartsWith(t));
        if (key != null)
        {
            return this[key];
        }

        // try to find the default culture
        key = Keys.FirstOrDefault(t => culture.StartsWith(DefaultCulture));
        if (key != null)
        {
            return this[key];
        }

        // return whatever we have
        return this.First().Value;
    }

    public override string ToString()
    {
        return Translate() ?? "????";
    }
    
    public static implicit operator string(LangStr? l) => l?.ToString() ?? "null";
    public static implicit operator LangStr(string s) => new LangStr(s);
}
