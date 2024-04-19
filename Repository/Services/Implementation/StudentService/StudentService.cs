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
        private CreateStudentService _createStudentService;
        private UpdateStudentService _updateStudentService;
        private DeleteStudentService _deleteStudentService;        
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
        private void InitializeServices()
        {
            _createStudentService = new CreateStudentService(_unitOfWork, _mapper);
            _deleteStudentService = new DeleteStudentService(_unitOfWork);
            _updateStudentService = new UpdateStudentService(_unitOfWork, _mapper);
        }

    }
}
