using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task DeveGerarTokenComUsuarioValido()
    {
        var user = new User("teste", "123");
        var repo = new Mock<IUserRepository>();
        repo.Setup(x => x.GetByUsernameAsync("teste")).ReturnsAsync(user);

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
                { "Jwt:Key", "chave-testes-super-secreta" }
            })
            .Build();

        var service = new AuthService(repo.Object, config);
        var token = await service.Authenticate("teste", "123");

        Assert.NotNull(token);
    }
}