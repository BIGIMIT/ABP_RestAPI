namespace ABP_RestAPI.Models;

public class ExperimentResult
{
    public long ID { get; set; }
    public Guid DeviceToken { get; set; }
    public string? ExperimentName { get; set; }
    public string? Result { get; set; }
}
