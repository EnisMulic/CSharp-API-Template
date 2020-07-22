﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Template.Contracts;
using Template.Contracts.Requests;
using Template.Contracts.Responses;
using Template.Data;

namespace Template.WebAPI.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<TemplateContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<TemplateContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });

                        var sp = services.BuildServiceProvider();
                    });

                });

            _serviceProvider = appFactory.Services;
            _httpClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }


        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.UserAccount.Register, new UserAccountRegistrationRequest
            {
                Username = "Integrator",
                Email = "test2@integration.com",
                Password = "SomePass1234!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<TemplateContext>();
            context.Database.EnsureDeleted();
        }
    }
}