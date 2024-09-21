using System.Collections.Generic;

public class Config
{
    public List<string>? InputFiles { get; set; }
    public string? OutputFile { get; set; }
    public MergeSettings? MergeSettings { get; set; }
    public Logging? Logging { get; set; }
}

public class MergeSettings
{
    public string? MergeOrder { get; set; }
    public List<string>? MatchKeys { get; set; }
}   

public class Logging
{
    public string? Level { get; set; }
    public string? LogFile { get; set; }
}