using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Theses.Api.Mappings.Create;
using Theses.Api.Mappings.Person;
using Theses.Api.Mappings.Thesis;
using Theses.Api.Mappings.Update;
using Theses.Application.Theses.Queries.Get;

namespace Theses.IntegrationTests.Controllers;

public class ThesesControllerTests : WebApplicationFactory<Program>
{
    private readonly Faker<PersonDto> _fakePersonsDtoGenerator = new Faker<PersonDto>()
        .CustomInstantiator(f =>
            new PersonDto(f.Person.FirstName,
                string.Empty,
                f.Person.LastName,
                f.Company.CompanyName()));

    private static string FakeEmail => new Faker().Person.Email;
    private static string FakeTopic => new Faker().Lorem.Sentence();
    private static string FakeContent => new Faker().Lorem.Paragraph();

    [Fact]
    private async Task GetAll_ShouldReturnOk()
    {
        var client = CreateClient();
        var response = await client.GetAsync("api/theses/all");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    private async Task Create_WithValidDto_ShouldReturnValidThesis()
    {
        var client = CreateClient();
        var dto = new CreateThesisDto(_fakePersonsDtoGenerator.Generate(1).First(),
            FakeEmail,
            _fakePersonsDtoGenerator.GenerateBetween(0, 3),
            FakeTopic,
            FakeContent);

        var response = await client.PostAsJsonAsync("api/theses", dto);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseDto =
            await JsonSerializer.DeserializeAsync<ThesisDto>(await response.Content.ReadAsStreamAsync(),
                JsonWebSerializerOptions.Instance);
        responseDto.Should().NotBeNull()
            .And
            .BeEquivalentTo(responseDto, config => config.Excluding(x => x.Id));
    }

    [Fact]
    private async Task Create_WithInvalidDto_ShouldReturn400()
    {
        var client = CreateClient();
        var dto = new CreateThesisDto(null,
            string.Empty,
            null,
            string.Empty,
            string.Empty);

        var response = await client.PostAsJsonAsync("api/theses", dto);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    private async Task CreateAndGet_WithValidDto_ShouldReturnValidThesis()
    {
        var client = CreateClient();
        var createDto = new CreateThesisDto(_fakePersonsDtoGenerator.Generate(1).First(),
            FakeEmail,
            _fakePersonsDtoGenerator.GenerateBetween(0, 3),
            FakeTopic,
            FakeContent);

        var createResponse = await client.PostAsJsonAsync("api/theses", createDto);

        var createResponseDto =
            await JsonSerializer.DeserializeAsync<ThesisDto>(await createResponse.Content.ReadAsStreamAsync(),
                JsonWebSerializerOptions.Instance);

        if (createResponseDto == null) Assert.Fail("Can't deserialize response");

        var getResponse = await client.GetAsync($"api/Theses/{createResponseDto.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getResponseDto = await JsonSerializer.DeserializeAsync<ThesisDto>(await getResponse.Content.ReadAsStreamAsync(),
            JsonWebSerializerOptions.Instance);
        getResponseDto.Should().BeEquivalentTo(createDto);
    }

    [Fact]
    private async Task Get_WithInvalidId_ShouldReturn404()
    {
        var client = CreateClient();
        var response = await client.GetAsync($"api/Theses/{-1}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    private async Task CreateAndUpdate_WithValidDto_ShouldReturnValidThesis()
    {
        var client = CreateClient();
        var createDto = new CreateThesisDto(_fakePersonsDtoGenerator.Generate(1).First(),
            FakeEmail,
            _fakePersonsDtoGenerator.GenerateBetween(0, 3),
            FakeTopic,
            FakeContent);

        var createResponse = await client.PostAsJsonAsync("api/theses", createDto);

        var createResponseDto =
            await JsonSerializer.DeserializeAsync<ThesisDto>(await createResponse.Content.ReadAsStreamAsync(),
                JsonWebSerializerOptions.Instance);

        if (createResponseDto == null) Assert.Fail("Can't deserialize response");

        var updateDto = new UpdateThesisDto(_fakePersonsDtoGenerator.Generate(1).First(),
            FakeEmail, _fakePersonsDtoGenerator.GenerateBetween(0, 3), FakeTopic, FakeContent);

        var updateResponse = await client.PutAsJsonAsync($"api/theses/{createResponseDto.Id}", updateDto);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var updateResponseDto =
            await JsonSerializer.DeserializeAsync<ThesisDto>(await updateResponse.Content.ReadAsStreamAsync(),
                JsonWebSerializerOptions.Instance);
        updateResponseDto.Should().BeEquivalentTo(updateDto);
    }

    [Fact]
    private async Task CreateAndUpdate_WithInvalidDto_ShouldReturn400()
    {
        var client = CreateClient();
        var createDto = new CreateThesisDto(_fakePersonsDtoGenerator.Generate(1).First(),
            FakeEmail,
            _fakePersonsDtoGenerator.GenerateBetween(0, 3),
            FakeTopic,
            FakeContent);

        var createResponse = await client.PostAsJsonAsync("api/theses", createDto);

        var createResponseDto =
            await JsonSerializer.DeserializeAsync<ThesisDto>(await createResponse.Content.ReadAsStreamAsync(),
                JsonWebSerializerOptions.Instance);

        if (createResponseDto == null) Assert.Fail("Can't deserialize response");

        var updateDto = new UpdateThesisDto(null, string.Empty, null, string.Empty, string.Empty);

        var updateResponse = await client.PutAsJsonAsync($"api/theses/{createResponseDto.Id}", updateDto);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    private async Task CreateAndUpdate_WithInvalidId_ShouldReturn400()
    {
        var client = CreateClient();

        var updateDto = new UpdateThesisDto(null, string.Empty, null, string.Empty, string.Empty);

        var updateResponse = await client.PutAsJsonAsync("api/theses/-1", updateDto);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    private async Task CreateAndDelete_WithValidDto_ShouldReturnOk()
    {
        var client = CreateClient();
        var createDto = new CreateThesisDto(_fakePersonsDtoGenerator.Generate(1).First(),
            FakeEmail,
            _fakePersonsDtoGenerator.GenerateBetween(0, 3),
            FakeTopic,
            FakeContent);

        var createResponse = await client.PostAsJsonAsync("api/theses", createDto);

        var createResponseDto =
            await JsonSerializer.DeserializeAsync<ThesisDto>(await createResponse.Content.ReadAsStreamAsync(),
                JsonWebSerializerOptions.Instance);

        if (createResponseDto == null) Assert.Fail("Can't deserialize response");

        var deleteResponse = await client.DeleteAsync($"api/Theses/{createResponseDto.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    private async Task Delete_WithInvalidId_ShouldReturn400()
    {
        var client = CreateClient();
        var deleteResponse = await client.DeleteAsync("api/Theses/-1");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
