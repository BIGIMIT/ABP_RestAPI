namespace ABP_RestAPI_xUnitTest;

public class ButtonExperimentTests
{
    [Fact]
    public void GetExperimentValueAsync_ReturnsValidColor()
    {
        // Arrange
        var buttonExperiment = new ButtonExperiment();

        // Act
        var result = buttonExperiment.GetExperimentValueAsync("deviceToken");

        // Assert
        // Перевірте, чи результат є дійсним кольором (у форматі "#RRGGBB")
        Assert.Matches("^#(?:[0-9a-fA-F]{6})$", result);
    }
}
