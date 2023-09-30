using ABP_RestAPI.Models;

namespace ABP_RestAPI.Services.Experiments
{
    public interface IExperiment
    {
        string GetExperimentValueAsync(string deviceToken);
        List<string> GetPossibleValues();
    }
}
