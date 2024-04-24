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
    public class UpdateStudentService
    {
        private IUnitOfWork _unitOfWork;
        private Student _student;
        private IMapper _mapper;
        private UpdateStudentRequest _updateStudentRequest;
        public UpdateStudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public UpdateStudentResponse Update(UpdateStudentRequest updateStudentRequest)
        {
            ResetUpdateStudentRequest(updateStudentRequest);
            GetExistingStudentById(_updateStudentRequest.Id);
            if (_student == null) return StudentNotFoundResponse();
            UpdateStudentDetails();
            return UpdateStudentSuccessfulResponse();
        }
        private void ResetUpdateStudentRequest(UpdateStudentRequest updateStudentRequest)
        {
            _updateStudentRequest = null!;
            _updateStudentRequest = updateStudentRequest;
        }
        private void GetExistingStudentById(int id)
        {
            _student = _unitOfWork.StudentRepository
                .Get(student => student.Id == _updateStudentRequest.Id)
                .FirstOrDefault();
        }
        private void UpdateStudentDetails()
        {
            _mapper.Map(_updateStudentRequest,_student);
            _unitOfWork.StudentRepository.Update(_student);
            _unitOfWork.Save();
        }

        private UpdateStudentResponse UpdateStudentSuccessfulResponse()
        {
            return new UpdateStudentResponse()
            {
                Result = "Success",
                StatusCode = StudentServiceStatusCode.UPDATE_STUDENT_SUCCESSFUL,
                Message = $"Successfully updated the details of Student with Id = {_updateStudentRequest.Id}.",
                ReturnData = _updateStudentRequest
            };
        }
        private UpdateStudentResponse StudentNotFoundResponse()
        {
            return new UpdateStudentResponse()
            {
                Result = "Failure",
                StatusCode = StudentServiceStatusCode.STUDENT_NOT_FOUND,
                Message = $"The student with the Id {_updateStudentRequest.Id} is not found. Please try again with another StudentId."
            };
        }

    }
}
