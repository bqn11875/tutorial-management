using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorialsManagement.Models;
using TutorialsManagement.Services.Interfaces;

namespace TutorialsManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/tutorial")]
    [ApiController]
    public class TutorialController : ControllerBase
    {
        private readonly IManageService _manageService;

        public TutorialController(IManageService manageService)
        {
            _manageService = manageService;
        }

        [HttpGet] // link: http://localhost:56528/api/v1/tutorial
        public async Task<Response<List<Tutorial>>> GetTutorialsList()
        {
            var tutorialsList = await _manageService.GetTutorialsList();

            return new Response<List<Tutorial>>(tutorialsList);
        }

        [HttpGet("{id}")] // link: http://localhost:56528/api/v1/tutorial/1
        public async Task<Response<Tutorial>> GetTutorialById(int id)
        {
            var tutorialDetail = await _manageService.GetTutorialById(id);

            return new Response<Tutorial>(tutorialDetail);
        }

        [HttpGet("find/{keyword}")] // link: http://localhost:56528/api/v1/tutorial/find/react
        public async Task<Response<List<Tutorial>>> FindTutorials(string keyword)
        {
            var tutorialsList = await _manageService.FindTutorials(keyword);

            return new Response<List<Tutorial>>(tutorialsList);
        }

        [HttpPost] // link: http://localhost:56528/api/v1/tutorial
        //Body > raw > JSON:
        //{
        //    "Title": "Test",
        //    "Description": "Desc"
        //}
        public async Task<Response<TutorialResponseModel>> CreateNewTutorial([FromBody] Tutorial tutorial)
        {
            try
            {
                var response = await _manageService.CreateNewTutorial(tutorial);
                return new Response<TutorialResponseModel>(response);
            }
            catch (Exception e)
            {
                var response = new TutorialResponseModel()
                {
                    Result = false,
                    Message = e.Message
                };
                return new Response<TutorialResponseModel>(response);
            }
        }

        [HttpPut] // link: http://localhost:56528/api/v1/tutorial
        //Body > raw > JSON:
        //{
        //    "Id": 1,
        //    "Title": "Test 2",
        //    "Description": "Desc 2"
        //    "Published": true
        //}
        public async Task<Response<TutorialResponseModel>> EditEmployee([FromBody] Tutorial tutorial)
        {
            try
            {
                var response = await _manageService.UpdateTutorial(tutorial);
                return new Response<TutorialResponseModel>(response);
            }
            catch (Exception e)
            {
                var response = new TutorialResponseModel()
                {
                    Result = false,
                    Message = e.Message
                };
                return new Response<TutorialResponseModel>(response);
            }
        }

        [HttpDelete("{id}")] // link: http://localhost:56528/api/v1/tutorial/1
        public async Task<Response<TutorialResponseModel>> DeleteTutorial(int id)
        {
            try
            {
                var response = await _manageService.DeleteTutorial(id);
                return new Response<TutorialResponseModel>(response);
            }
            catch (Exception e)
            {
                var response = new TutorialResponseModel()
                {
                    Result = false,
                    Message = e.Message
                };
                return new Response<TutorialResponseModel>(response);
            }
        }

        [HttpDelete("all")] // link: http://localhost:56528/api/v1/tutorial/all
        public async Task<Response<TutorialResponseModel>> DeleteAllTutorials()
        {
            try
            {
                var response = await _manageService.DeleteAllTutorials();
                return new Response<TutorialResponseModel>(response);
            }
            catch (Exception e)
            {
                var response = new TutorialResponseModel()
                {
                    Result = false,
                    Message = e.Message
                };
                return new Response<TutorialResponseModel>(response);
            }
        }
    }
}
