// Sample xUnit test
using Microsoft.AspNetCore.Mvc;
using Xampp_Test2.Controllers;

public class VisualisationControllerTests
{
    [Fact]
    public void SuccessControllerTest()
    {
        // Arrange
        var controller = new VisualisationController();

        // Act
        var result = controller.Histogram("2023", "12", "06") as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Model);
        // Add more assertions based on your expectations
    }

    [Fact]
    public void ErrorControllerTest()
    {
        // Arrange
        var controller = new VisualisationController();

        // Act
        var result = controller.Histogram("invalid", "12", "06") as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Error", result.ViewName);
        // Add more assertions based on your expectations
    }
}
