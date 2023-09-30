using ABP_RestAPI.Models;
using ABP_RestAPI.Services.Experiments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABP_RestAPI.Controllers;

[Route("api/experiment")]
[ApiController]
public class ExperimentController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Dictionary<string, IExperiment> _experiments;

    public ExperimentController(ApplicationDbContext dbContext, Dictionary<string, IExperiment> experiments)
    {
        _dbContext = dbContext;
        _experiments = experiments;
    }

    [HttpGet("{experimentName}")]
    public async Task<IActionResult> GetExperiment([FromRoute] string experimentName, [FromQuery] string? deviceToken)
    {
        try
        {
            // Перевірка, чи існує експеримент з вказаною назвою
            if (!_experiments.TryGetValue(experimentName, out var experiment))
        {
            return NotFound(); // Експеримент з таким ім'ям не знайдено
        }

            // Перевірка, чи передано параметр deviceToken, і чи він може бути перетворений у Guid
            if (string.IsNullOrEmpty(deviceToken) || !Guid.TryParse(deviceToken, out Guid parsedDeviceToken))
            {
                parsedDeviceToken = Guid.NewGuid();
                var newUrl = $"/api/experiment/{experimentName}?deviceToken={parsedDeviceToken}";
                return Redirect(newUrl);
            }
        
            // Пошук результату експерименту в базі даних
            var experimentResult = await _dbContext.ExperimentResults
                .Where(er => er.DeviceToken == parsedDeviceToken && er.ExperimentName == experimentName)
                .FirstOrDefaultAsync();

            // Якщо результат знайдено, повертаємо його 
            // Якщо результат не знайдено, виконуємо експеримент та зберігаємо результат в базі даних
            if (experimentResult != null)
            {
                return Ok(new { key = experimentName, value = experimentResult.Result });
            }
            else
            {
                var experimentValue = experiment.GetExperimentValueAsync(deviceToken);
                await _dbContext.ExperimentResults.AddAsync(new ExperimentResult
                {
                    DeviceToken = parsedDeviceToken,
                    ExperimentName = experimentName,
                    Result = experimentValue
                });
                await _dbContext.SaveChangesAsync();

                return Ok(new { key = experimentName, value = experimentValue });
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync("Помилка в методі GetExperiment:\n" + ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

}