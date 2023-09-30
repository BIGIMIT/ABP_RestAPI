using ABP_RestAPI.Models;

namespace ABP_RestAPI.Services.Experiments;

public class ButtonExperiment : IExperiment
{
    public string GetExperimentValueAsync(string deviceToken)
    {
        double randomNumber = new Random().NextDouble();

        if (randomNumber < 1.0/3) return "#FF0000";
        else if (randomNumber < 2.00/3) return "#00FF00";
        else return "#0000FF";
    }

    List<string> IExperiment.GetPossibleValues()
    {
        return new List<string> { "#FF0000", "#00FF00", "#0000FF" };
    }
}
