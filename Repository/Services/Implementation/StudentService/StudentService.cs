using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
    public class StudentService : IStudentService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private GetStudentByIdService _getStudentByIdService;   
        private CreateStudentService _createStudentService;
        private UpdateStudentService _updateStudentService;
        private DeleteStudentService _deleteStudentService;
        private GetStudentListService _getStudentListService;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            InitializeServices();
        }
        public NewStudentResponse Create(NewStudentRequest newStudentRequest)
        {
            return _createStudentService.Create(newStudentRequest);
        }
        public UpdateStudentResponse Update(UpdateStudentRequest updateStudentRequest)
        {
            return _updateStudentService.Update(updateStudentRequest);
        }
        public DeleteStudentResponse Delete(int id)
        {
            return _deleteStudentService.Delete(id);
        }
        public StudentResponse GetById(int id)
        {
            return _getStudentByIdService.Get(id);
        }
        public StudentListResponse Get(GetStudentListRequest request)
        {
            return _getStudentListService.Get(request);
        }
        private void InitializeServices()
        {
            _createStudentService = new CreateStudentService(_unitOfWork, _mapper);            
            _updateStudentService = new UpdateStudentService(_unitOfWork, _mapper);
            _getStudentByIdService = new GetStudentByIdService(_unitOfWork,_mapper);
            _getStudentListService = new GetStudentListService(_unitOfWork);
            _deleteStudentService = new DeleteStudentService(_unitOfWork);
        }

       
    }
}
