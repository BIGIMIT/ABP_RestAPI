namespace ABP_RestAPI_xUnitTest;

public class PriceExperimentTests
{
    [Fact]
    public void GetExperimentValueAsync_ReturnsValidValue()
    {
        // Arrange
        IExperiment experiment = new PriceExperiment();

        // Act
        var result = experiment.GetExperimentValueAsync("someDeviceToken");

        // Assert
        Assert.Contains(result, experiment.GetPossibleValues());
    }
}
