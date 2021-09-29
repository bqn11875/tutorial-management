using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialsManagement.Core.Interfaces;
using TutorialsManagement.Models;
using TutorialsManagement.Services.Interfaces;

namespace TutorialsManagement.Services.Implementations
{
    public class ManageService : IManageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<Tutorial>> GetTutorialsList()
        {
            var tutorialsList = _unitOfWork.ExecuteReader("GetTutorials").AsEnumerable();
            var tutorialsListModel = tutorialsList.Select(item =>
            {
                return new Tutorial()
                {
                    Id = item.Field<int>("Id"),
                    Title = item.Field<string>("Title"),
                    Description = item.Field<string>("Description"),
                    Published = item.Field<bool>("Published")
                };
            }).ToList();

            return Task.FromResult(tutorialsListModel);
        }

        public Task<Tutorial> GetTutorialById(int id)
        {
            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "pId",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.BigInt,
                    Value = id
                }
            };

            var tutorial = _unitOfWork.ExecuteReader("GetTutorialById", sqlParameters).AsEnumerable();
            var tutorialModel = tutorial.Select(item =>
            {
                return new Tutorial()
                {
                    Id = item.Field<int>("Id"),
                    Title = item.Field<string>("Title"),
                    Description = item.Field<string>("Description"),
                    Published = item.Field<bool>("Published")
                };
            }).SingleOrDefault();

            return Task.FromResult(tutorialModel);
        }

        public Task<List<Tutorial>> FindTutorials(string keyword)
        {
            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "pKeyword",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.NVarChar,
                    Value = keyword
                }
            };

            var tutorialsList = _unitOfWork.ExecuteReader("FindTutorials", sqlParameters).AsEnumerable();
            var tutorialsListModel = tutorialsList.Select(item =>
            {
                return new Tutorial()
                {
                    Id = item.Field<int>("Id"),
                    Title = item.Field<string>("Title"),
                    Description = item.Field<string>("Description"),
                    Published = item.Field<bool>("Published")
                };
            }).ToList();

            return Task.FromResult(tutorialsListModel);
        }

        public Task<TutorialResponseModel> CreateNewTutorial(Tutorial tutorial)
        {
            var result = false;

            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "pTitle",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tutorial.Title
                },
                new SqlParameter
                {
                    ParameterName = "pDescription",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tutorial.Description
                }
            };
            var id = (int)_unitOfWork.ExecuteNonQuery("CreateNewTutorial", sqlParameters);

            result = true;

            var response = new TutorialResponseModel()
            {
                Id = id,
                Result = result
            };

            return Task.FromResult(response);
        }

        public Task<TutorialResponseModel> UpdateTutorial(Tutorial tutorial)
        {
            var result = false;

            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "pId",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Int,
                    Value = tutorial.Id
                },
                new SqlParameter
                {
                    ParameterName = "pTitle",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tutorial.Title
                },
                new SqlParameter
                {
                    ParameterName = "pDescription",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.NVarChar,
                    Value = tutorial.Description
                },
                new SqlParameter
                {
                    ParameterName = "pPublished",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Bit,
                    Value = tutorial.Published
                }
            };
            _unitOfWork.ExecuteNonQuery("UpdateTutorial", sqlParameters);

            result = true;

            var response = new TutorialResponseModel()
            {
                Result = result
            };

            return Task.FromResult(response);
        }

        public Task<TutorialResponseModel> DeleteTutorial(int id)
        {
            var result = false;

            var sqlParameters = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "pId",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                Value = id
            }
            };
            _unitOfWork.ExecuteNonQuery("DeleteTutorial", sqlParameters);

            result = true;

            var response = new TutorialResponseModel()
            {
                Result = result
            };

            return Task.FromResult(response);
        }

        public Task<TutorialResponseModel> DeleteAllTutorials()
        {
            var result = false;

            _unitOfWork.ExecuteNonQuery("DeleteAllTutorials");

            result = true;

            var response = new TutorialResponseModel()
            {
                Result = result
            };

            return Task.FromResult(response);
        }
    }
}
