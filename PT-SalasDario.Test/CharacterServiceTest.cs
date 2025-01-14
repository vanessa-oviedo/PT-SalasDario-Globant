using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using PT_SalasDario.Services.Response;
using PT_SalasDario.Services;

[TestClass]
public class CharacterServiceTests
{
    private Mock<ICharacterRepository> _characterRepositoryMock;
    private IMapper _mapper;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private HttpClient _httpClient;
    private CharacterService _service;

    [TestInitialize]
    public void SetUp()
    {
        _characterRepositoryMock = new Mock<ICharacterRepository>();
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ServiceProfile());
        });

        _mapper = new Mapper(configuration);
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        _service = new CharacterService(_httpClient, _characterRepositoryMock.Object, _mapper);
    }

    [TestMethod]
    public async Task GetAllCharactersAsync_ShouldReturnAllCharacters_WhenPaginationIsNull()
    {
        // Arrange
        var characters = new List<Character>
        {
            new Character { Id = 1, Name = "Rick" },
            new Character { Id = 2, Name = "Morty" }
        };

        var characterDTOs = new List<CharaterResponseDTO>
        {
            new CharaterResponseDTO { Id = 1, Name = "Rick" },
            new CharaterResponseDTO { Id = 2, Name = "Morty" }
        };

        _characterRepositoryMock
            .Setup(r => r.GetAllCharacters())
            .ReturnsAsync(characters);

        // Act
        var result = await _service.GetAllCharactersAsync(null, null);

        // Assert
        result.Should().BeEquivalentTo(characterDTOs);
    }

    [TestMethod]
    public async Task GetAllCharactersAsync_ShouldReturnPaginatedCharacters_WhenPaginationIsProvided()
    {
        // Arrange
        var characters = new List<Character>
        {
            new Character { Id = 1, Name = "Rick" },
            new Character { Id = 2, Name = "Morty" }
        };

        var characterDTOs = new List<CharaterResponseDTO>
        {
            new CharaterResponseDTO { Id = 1, Name = "Rick" },
            new CharaterResponseDTO { Id = 2, Name = "Morty" }
        };

        _characterRepositoryMock
            .Setup(r => r.GetAllCharactersPaginated(1, 2))
            .ReturnsAsync(characters);

        // Act
        var result = await _service.GetAllCharactersAsync(1, 2);

        // Assert
        result.Should().BeEquivalentTo(characterDTOs);
    }
}
