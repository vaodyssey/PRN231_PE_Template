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
    public class CreateStudentService
    {
        private IUnitOfWork _unitOfWork;
        private NewStudentRequest _newStudentRequest;
        private IMapper _mapper;
        private Student _student;
        public CreateStudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public NewStudentResponse Create(NewStudentRequest newStudentRequest)
        {
            ResetNewStudentRequest(newStudentRequest);            
            InsertStudentToDb();
            return CreateStudentSuccessfulResponse();
        }
        private void ResetNewStudentRequest(NewStudentRequest newStudentRequest)
        {
            _newStudentRequest = null!;
            _newStudentRequest = newStudentRequest;
        }     
        private void InsertStudentToDb()
        {
            _student = _mapper.Map<Student>(_newStudentRequest);
            //_student.Id = GetLatestId();
            _unitOfWork.StudentRepository.Insert(_student);
            _unitOfWork.Save();
        }
        //private int GetLatestId()
        //{
        //    return _unitOfWork.StudentRepository.Get().OrderByDescending(e => e.Id).FirstOrDefault().Id + 1;
        //}
        private NewStudentResponse CreateStudentSuccessfulResponse()
        {
            return new NewStudentResponse
            {
                Result = "Success",
                StatusCode = StudentServiceStatusCode.CREATE_STUDENT_SUCCESSFUL,
                Message = "Successfully created a new Student.",
                ReturnData = new { 
                   Id = _student.Id 
                }
            };
        }
    }
}
