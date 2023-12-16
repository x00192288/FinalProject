using Xunit;
using Xampp_Test2.Models;

public class AirApiTests
{
    [Fact]
    public void AirApiPropertiesSetCorrectly()
    {
        // Arrange
        int expectedApiId = 1;
        int expectedResponse = 200;

        // Act
        AirApi airApi = new AirApi
        {
            api_id = expectedApiId,
            response = expectedResponse
        };

        // Assert
        Assert.Equal(expectedApiId, airApi.api_id);
        Assert.Equal(expectedResponse, airApi.response);
    }
}
