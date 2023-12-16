using Xunit;
using Xampp_Test2.Models;

public class TemperatureTests
{
    [Fact]
    public void TemperaturePropertiesSetCorrectly()
    {
        // Arrange
        int expectedId = 1;
        int expectedValue = 25;

        // Act
        Temperature temperature = new Temperature
        {
            temp_id = expectedId,
            temp_value = expectedValue
        };

        // Assert
        Assert.Equal(expectedId, temperature.temp_id);
        Assert.Equal(expectedValue, temperature.temp_value);
    }
}
