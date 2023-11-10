using Application.SkillLists.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebUI.Controllers;

namespace WebUI.UnitTests
{
    public class ListsControllerTest
    {
        ListsController _controller;

        public ListsControllerTest()
        {
            _controller = new ListsController();
        }
        [Fact]
        public async void GetAllListsTest()
        {
            //arrange
            //act
            var result = await _controller.GetAllLists();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<SkillListDto>>(list.Value);

            var lists = list.Value as List<SkillListDto>;
            Assert.Single(lists);
        }
    }
}