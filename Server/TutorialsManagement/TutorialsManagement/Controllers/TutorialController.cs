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

        [HttpGet]
        public async Task<Response<List<Tutorial>>> GetTutorialsList()
        {
            var tutorialsList = await _manageService.GetTutorialsList();

            return new Response<List<Tutorial>>(tutorialsList);
        }

        [HttpGet("{id}")]
        public async Task<Response<Tutorial>> GetTutorialById(int id)
        {
            var tutorialDetail = await _manageService.GetTutorialById(id);

            return new Response<Tutorial>(tutorialDetail);
        }

        [HttpGet("find/{keyword}")]
        public async Task<Response<List<Tutorial>>> FindTutorials(string keyword)
        {
            var tutorialsList = await _manageService.FindTutorials(keyword);

            return new Response<List<Tutorial>>(tutorialsList);
        }

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete("{id}")]
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

        [HttpDelete("all")]
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
