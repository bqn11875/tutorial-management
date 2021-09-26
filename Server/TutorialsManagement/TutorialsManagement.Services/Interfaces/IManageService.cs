using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialsManagement.Models;

namespace TutorialsManagement.Services.Interfaces
{
    public interface IManageService
    {
        Task<List<Tutorial>> GetTutorialsList();
        Task<Tutorial> GetTutorialById(int id);
        Task<List<Tutorial>> FindTutorials(string keyword);
        Task<TutorialResponseModel> CreateNewTutorial(Tutorial tutorial);
        Task<TutorialResponseModel> UpdateTutorial(Tutorial tutorial);
        Task<TutorialResponseModel> DeleteTutorial(int id);
        Task<TutorialResponseModel> DeleteAllTutorials();
    }
}
