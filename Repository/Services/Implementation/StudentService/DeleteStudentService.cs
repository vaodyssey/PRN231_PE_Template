using AutoMapper;
using Repository.Constants;
using Repository.Entities;
using Repository.Payload.Response.StudentService;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services.Implementation.StudentService
{
    public class DeleteStudentService
    {
        private int _studentId;
        private IUnitOfWork _unitOfWork;
        public DeleteStudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public DeleteStudentResponse Delete(int id)
        {
            ResetDeleteStudentId(id);
            DeleteStudentById();
            return DeleteStudentSuccessfulResponse();
        }
        private void ResetDeleteStudentId(int id)
        {
            _studentId = id;
        }
        private void DeleteStudentById()
        {
            _unitOfWork.StudentRepository.Delete(_studentId);
            _unitOfWork.Save();
        }
        private DeleteStudentResponse DeleteStudentSuccessfulResponse()
        {
            return new DeleteStudentResponse()
            {
                Result = "Success",
                StatusCode = StudentServiceStatusCode.DELETE_STUDENT_SUCCESSFUL,
                Message = $"Successfully deleted the Student with Id = {_studentId}."
            };
        }
    }
}
