using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template.Contracts;
using Template.Contracts.Responses;
using Xunit;

namespace Template.WebAPI.IntegrationTests
{
    public class UserControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.User.Get);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // Returns one user that just registered
            (await response.Content.ReadAsAsync<PagedResponse<IdentityUser>>()).Data.Should().HaveCount(1);
        }
    }
}
