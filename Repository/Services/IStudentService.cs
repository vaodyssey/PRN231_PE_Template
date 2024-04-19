using Repository.Payload.Request.StudentService;
using Repository.Payload.Response.StudentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IStudentService
    {
        public StudentListResponse Get(GetStudentListRequest request);
        public StudentResponse GetById(int id);
        public NewStudentResponse Create(NewStudentRequest newStudentRequest);
        public DeleteStudentResponse Delete(int id);
        public UpdateStudentResponse Update(UpdateStudentRequest updateStudentRequest);
    }
}
