using Application.Common.Interfaces;
using Application.SkillLists.Commands.CreateSkillList;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.SkillLists.Commands;

public class CreateSkillListCommandTests
{
    private readonly Mock<ISkillListRepository> _skillListRepositoryMock;

    public CreateSkillListCommandTests()
    {
        _skillListRepositoryMock = new();   
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenTitleIsNotUnique()
    {
        //Arrange
        var command = new CreateSkillListCommand{Title = "Tech skills"};

        _skillListRepositoryMock.Setup(
                x => x.BeUniqueTitle(
                    It.IsAny<String>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        var handler = new CreateSkillListCommandHandler(_skillListRepositoryMock.Object);
        
        //Act
        var (result, _) = await handler.Handle(command, default);
        
        //Assert
        result.Succeeded.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[] { "There is already a list with this title" });
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenEmailIsUnique()
    {
        //Arrange
        var command = new CreateSkillListCommand{Title = "Tech skills"};

        _skillListRepositoryMock.Setup(
                x => x.BeUniqueTitle(
                    It.IsAny<String>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        var handler = new CreateSkillListCommandHandler(_skillListRepositoryMock.Object);
        
        //Act
        var (_, listId) = await handler.Handle(command, default);
        
        //Assert
        _skillListRepositoryMock.Verify(
            x => x.Add(It.Is<SkillList>(sl => sl.Id == listId)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmailIsNotUnique()
    {
        //Arrange
        var command = new CreateSkillListCommand{Title = "Tech skills"};

        _skillListRepositoryMock.Setup(
                x => x.BeUniqueTitle(
                    It.IsAny<String>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        var handler = new CreateSkillListCommandHandler(_skillListRepositoryMock.Object);
        
        //Act
        await handler.Handle(command, default);
        
        //Assert
        _skillListRepositoryMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}