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
        private Student _Student;
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
            if (!IsGroupIdValid()) return NoStudentGroupFound();
            GetExistingStudentById(_updateStudentRequest.Id);
            if (_Student == null) return StudentNotFoundResponse();
            UpdateStudentDetails();
            return UpdateStudentSuccessfulResponse();
        }
        private bool IsGroupIdValid()
        {
            IEnumerable<StudentGroup> groups = _unitOfWork.StudentGroupRepository.Get();
            foreach (var group in groups)
            {
                if (group.Id == _updateStudentRequest.GroupId) return true;
            }
            return false;
        }
        private void ResetUpdateStudentRequest(UpdateStudentRequest updateStudentRequest)
        {
            _updateStudentRequest = null!;
            _updateStudentRequest = updateStudentRequest;
        }
        private void GetExistingStudentById(int id)
        {
            _Student = _unitOfWork.StudentRepository
                .Get(Student => Student.Id == _updateStudentRequest.Id)
                .FirstOrDefault();
        }
        private void UpdateStudentDetails()
        {
            _mapper.Map(_updateStudentRequest, _Student);
            _unitOfWork.StudentRepository.Update(_Student);
            _unitOfWork.Save();
        }

        private UpdateStudentResponse UpdateStudentSuccessfulResponse()
        {
            return new UpdateStudentResponse()
            {
                Result = "Success",
                StatusCode = StudentServiceStatusCode.UPDATE_STUDENT_SUCCESSFUL,
                Message = $"Successfully updated the details of Student with Id = {_updateStudentRequest.Id}."
            };
        }
        private UpdateStudentResponse StudentNotFoundResponse()
        {
            return new UpdateStudentResponse()
            {
                Result = "Failure",
                StatusCode = StudentServiceStatusCode.STUDENT_NOT_FOUND,
                Message = $"The Student with the Id {_updateStudentRequest.Id} is not found. Please try again with another StudentId."
            };
        }
        private UpdateStudentResponse NoStudentGroupFound()
        {
            return new UpdateStudentResponse
            {
                Result = "Failure",
                StatusCode = 400,
                Message = "The input StudentGroupId is invalid. Try another one.",
                ReturnData = null
            };
        }
    }
    }
