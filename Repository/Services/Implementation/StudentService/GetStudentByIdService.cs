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
    public class GetStudentByIdService
    {
        private IUnitOfWork _unitOfWork;
        private int _studentId;
        private IMapper _mapper;        
        private Student _student;
        public GetStudentByIdService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public StudentResponse Get(int id)
        {
            ResetStudentId(id);
            GetStudentById();
            if (_student == null) return StudentNotFoundResponse();            
            return GetStudentSuccessfulResponse();
        }
        private void ResetStudentId(int id)
        {
            _studentId = id;
        }
        private void GetStudentById()
        {
            _student = _unitOfWork.StudentRepository.Get(student => student.Id == _studentId).FirstOrDefault();
        }       
        private string GetGroupName()
        {
            return _unitOfWork.StudentGroupRepository
                .Get(stuGroup => stuGroup.Id == _student.GroupId)
                .FirstOrDefault()
                .GroupName;
        }
        private StudentResponse GetStudentSuccessfulResponse()
        {
            return new StudentResponse()
            {
                Result = "Success",
                StatusCode = StudentServiceStatusCode.GET_STUDENT_BY_ID_SUCCESSFUL,
                Message = "Successfully retrieved student data",
                ReturnData = new
                {
                    Id = _student.Id,
                    Email = _student.Email,
                    FullName = _student.FullName,
                    DateOfBirth = _student.DateOfBirth,
                    GroupName = GetGroupName()
                }
            };
        }
        
        private StudentResponse StudentNotFoundResponse()
        {
            return new StudentResponse()
            {
                Result = "Failure",
                StatusCode = StudentServiceStatusCode.STUDENT_NOT_FOUND,
                Message = $"The student with the Id {_studentId} is not found. Please try again with another StudentId."
            };
        }
    }
}
