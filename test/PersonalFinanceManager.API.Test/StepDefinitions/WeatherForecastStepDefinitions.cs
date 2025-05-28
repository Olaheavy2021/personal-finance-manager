using NUnit.Framework;
using PersonalFinanceManager.Client;
using System.Net;

namespace PersonalFinanceManager.API.Test.StepDefinitions;

[Binding]
public class WeatherForecastStepDefinitions(IPFMv1Client pfmClient, ScenarioContext scenarioContext, HttpClient client)
{
    [Given("the API is running")]
    public async Task GivenTheAPIIsRunning()
    {
        var res = await client.GetAsync("healthz/startup");
        Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [When("the weather forecast is requested")]
    public async Task WhenTheWeatherForecastIsRequested()
    {
        var res = await pfmClient.GetWeatherForecastsAsync();
        scenarioContext.Add("WeatherForecasts", res);
    }

    [Then("the response should contain a list of weather forecasts")]
    public void ThenTheResponseShouldContainAListOfWeatherForecasts()
    {
        var res = scenarioContext.Get<List<WeatherForecast>>("WeatherForecasts");
        Assert.That(res, Is.Not.Null);
        Assert.That(res, Is.Not.Empty, "Weather forecasts list should not be empty.");
        Assert.That(res, Is.InstanceOf<List<WeatherForecast>>(), "Response should be a list of WeatherForecast objects.");
        Assert.That(res.Count, Is.GreaterThan(0), "Weather forecasts list should contain at least one item.");
    }

    [Then("each weather forecast should have a date, temperature, and summary")]
    public void ThenEachWeatherForecastShouldHaveADateTemperatureAndSummary()
    {
        var res = scenarioContext.Get<List<WeatherForecast>>("WeatherForecasts");

        Assert.That(res.All(wf => wf.Date != default), "All weather forecasts should have a valid date.");
        Assert.That(
   res.Select(wf => wf.TemperatureC),
   Is.All.GreaterThanOrEqualTo(-273),
   "TemperatureC should be ? absolute zero"
 );
        Assert.That(res.All(wf => !string.IsNullOrEmpty(wf.Summary)), "All weather forecasts should have a summary.");

    }
}
