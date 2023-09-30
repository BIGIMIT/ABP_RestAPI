using ABP_RestAPI.Models;
using ABP_RestAPI.Services.Experiments;
using Microsoft.AspNetCore.Mvc;

namespace ABP_RestAPI.Controllers;

[Route("api/experiment/statistics")]
[ApiController]
public class ExperimentStatisticsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Dictionary<string, IExperiment> _experiments;

    public ExperimentStatisticsController(ApplicationDbContext dbContext, Dictionary<string, IExperiment> experiments)
    {
        _dbContext = dbContext;
        _experiments = experiments;
    }

    [HttpGet]
    public IActionResult GetStatistics()
    {
        var statistics = new List<ExperimentStatistics>();

        foreach (var experiment in _experiments.Values)
        {
            // Отримуємо ім'я експерименту з ключа словника (де ключ - це ім'я експерименту).
            var experimentName = _experiments.FirstOrDefault(x => x.Value == experiment).Key;

            // Отримуємо можливі значення для даного експерименту.
            var possibleValues = experiment.GetPossibleValues();

            // Розраховуємо загальну кількість пристроїв, що брали участь в експерименті.
            var totalDevices = _dbContext.ExperimentResults
                .Where(er => er.ExperimentName == experimentName)
                .Count();

            // Створюємо статистику для кожного можливого значення експерименту.
            var experimentStats = possibleValues.Select(value =>
            { 
                // Розраховуємо кількість пристроїв з певним значенням.
                var valueCount = _dbContext.ExperimentResults
                    .Where(er => er.ExperimentName == experimentName && er.Result == value)
                    .Count();

                // Розраховуємо відсоток пристроїв з даною відповіддю відносно загальної кількості пристроїв.
                var percentage = totalDevices == 0 ? 0 : (double)valueCount / totalDevices * 100;

                return new ExperimentValueStatistics
                {
                    Value = value,
                    Count = valueCount,
                    Percentage = percentage
                };
            }).ToList();

            // Додаємо статистику для поточного експерименту до загального списку статистики.
            statistics.Add(new ExperimentStatistics { ExperimentName = experimentName, count = totalDevices, Values = experimentStats });

        }
        // Повертаємо статистику у форматі JSON.
        return Ok(statistics);
    }
    public class ExperimentValueStatistics
    {
        public string? Value { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
    public class ExperimentStatistics
    {
        public string? ExperimentName { get; set; }
        public int count { get; set; }
        public List<ExperimentValueStatistics>? Values { get; set; }
    }

}