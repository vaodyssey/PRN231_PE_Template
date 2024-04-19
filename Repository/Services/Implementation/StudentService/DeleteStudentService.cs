using AutoMapper;
using Repository.Constants;
using Repository.Entities;
using Repository.Payload.Request.StudentService;
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
        private Student _student;
        public DeleteStudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public DeleteStudentResponse Delete(int id)
        {
            ResetDeleteStudentId(id);
            GetStudentById();
            if (_student == null) return StudentNotFoundResponse();
            DeleteStudentById();
            return DeleteStudentSuccessfulResponse();
        }
        private void ResetDeleteStudentId(int id)
        {
            _studentId = id;
        }
        private void GetStudentById()
        {
            _student = _unitOfWork.StudentRepository.Get(student => student.Id == _studentId).FirstOrDefault();
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
        private DeleteStudentResponse StudentNotFoundResponse()
        {
            return new DeleteStudentResponse()
            {
                Result = "Failure",
                StatusCode = StudentServiceStatusCode.STUDENT_NOT_FOUND,
                Message = $"The student with the Id {_studentId} is not found. Please try again with another StudentId."
            };
        }
    }
}
