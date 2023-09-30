namespace ABP_RestAPI.Services.Experiments;

public class PriceExperiment : IExperiment
{
    public string GetExperimentValueAsync(string deviceToken)
    {
        double randomNumber = new Random().NextDouble();

        if (randomNumber < 0.05) return "50";
        else if (randomNumber < 0.15) return "20";
        else if (randomNumber < 0.25) return "5";
        else return "10";
    }
    List<string> IExperiment.GetPossibleValues()
    {
        return new List<string> { "50", "20", "5", "10" };
    }
}
