using System.Net;
using System.Net.Http.Headers;
using System.Text;
using App.Public.DTO;
using App.Public.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using NuGet.Protocol;
using Tests.WebApp.Helpers;
using Xunit.Abstractions;

namespace Tests.WebApp.Tests.Integration;

public class ApiHotelControllerTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public ApiHotelControllerTests(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }
        );
    }

    [Fact]
    public async Task Test_API_Hotel_Controller()
    {
        // ARRANGE
        var uri = "/api/v1/hotel";

        // ACT
        var getTestResponse = await _client.GetAsync(uri);

        // ASSERT
        Assert.Equal(HttpStatusCode.Unauthorized, getTestResponse.StatusCode);
        await Register_User();
    }
    
    public async Task Register_User()
    {
        // ARRANGE
        var uri = "/api/v1/identity/account/register";

        Register data = new Register()
        {
            Email = "123@123.123",
            Password = "123123",
            FirstName = "123",
            LastName = "Man"
        };
        
        // ACT
        var res = await _client.PostAsync(uri,
            new StringContent(data.ToJson(), Encoding.UTF8, "application/json"));

        // ASSERT
        res.EnsureSuccessStatusCode();
        await Login();
    }
    
    public async Task Login()
    {
        // ARRANGE
        var uri = "/api/v1/identity/account/login";

        Login data = new Login()
        {
            Email = "123@123.123",
            Password = "123123"
        };

        // ACT
        var res = await _client.PostAsync(uri,
            new StringContent(data.ToJson(), Encoding.UTF8, "application/json"));

        // ASSERT
        res.EnsureSuccessStatusCode();

        var body = await res.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(body);

        var resData = JsonHelper.DeserializeWithWebDefaults<App.Public.DTO.Identity.JwtResponse>(body);

        Assert.NotNull(resData);
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", resData!.Jwt);
        await Create_Hotel();
    }

    public async Task Create_Hotel()
    {
        // ARRANGE
        var uri = "/api/v1/hotel";

        Hotel data = new Hotel()
        {
            Name = "Testing_Hotel",
            Description = "Testing_Hotel_Description",
            Id = default!
        };
        
        // ACT
        var res = await _client.PostAsync(uri,
            new StringContent(data.ToJson(), Encoding.UTF8, "application/json"));

        // ASSERT
        res.EnsureSuccessStatusCode();
        
        var body = await res.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(body);

        var resData = JsonHelper.DeserializeWithWebDefaults<App.Public.DTO.Hotel>(body);
        
        await Get_Hotel_1(resData!.Id);
    }

    public async Task Get_Hotel_1(Guid hotelId)
    {
        // ARRANGE
        var uri = "/api/v1/hotel/" + hotelId;

        // ACT
        var res = await _client.GetAsync(uri);

        // ASSERT
        res.EnsureSuccessStatusCode();
        
        var body = await res.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(body);

        var resData = JsonHelper.DeserializeWithWebDefaults<App.Public.DTO.Hotel>(body);
        
        Assert.Equal(resData!.Id, hotelId);
        Assert.Equal("Testing_Hotel", resData!.Name);
        Assert.Equal("Testing_Hotel_Description", resData!.Description);

        await Edit_Hotel(hotelId);
    }

    public async Task Edit_Hotel(Guid hotelId)
    {
        // ARRANGE
        var uri = "/api/v1/hotel/" + hotelId;

        Hotel data = new Hotel()
        {
            Name = "Edited_Testing_Hotel",
            Description = "Edited_Testing_Hotel_Description",
            Id = default!
        };

        // ACT
        var res = await _client.PutAsync(uri,
            new StringContent(data.ToJson(), Encoding.UTF8, "application/json"));

        // ASSERT
        res.EnsureSuccessStatusCode();
        
        await Get_Hotels_2(hotelId);
    }

    public async Task Get_Hotels_2(Guid hotelId)
    {
        // ARRANGE
        var uri = "/api/v1/hotel/" + hotelId;

        // ACT
        var res = await _client.GetAsync(uri);

        // ASSERT
        res.EnsureSuccessStatusCode();
        
        var body = await res.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(body);

        var resData = JsonHelper.DeserializeWithWebDefaults<App.Public.DTO.Hotel>(body);
        
        Assert.Equal(resData!.Id, hotelId);
        Assert.Equal("Edited_Testing_Hotel", resData!.Name);
        Assert.Equal("Edited_Testing_Hotel_Description", resData!.Description);

        await Delete_Hotel(hotelId);
    }
    
    public async Task Delete_Hotel(Guid hotelId)
    {
        // ARRANGE
        var uri = "/api/v1/hotel/" + hotelId;

        // ACT
        var res = await _client.DeleteAsync(uri);

        // ASSERT
        res.EnsureSuccessStatusCode();
        await Get_Hotels_3(hotelId);
    }

    public async Task Get_Hotels_3(Guid hotelId)
    {
        // ARRANGE
        var uri = "/api/v1/hotel/" + hotelId;

        // ACT
        var getTestResponse = await _client.GetAsync(uri);

        // ASSERT
        Assert.Equal(HttpStatusCode.NotFound, getTestResponse.StatusCode);
    }

}
