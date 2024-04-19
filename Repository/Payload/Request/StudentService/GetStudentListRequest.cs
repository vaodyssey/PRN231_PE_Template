using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Payload.Request.StudentService
{
    public class GetStudentListRequest
    {
        public int GroupId { get; set; }
        public DateTime MinBirthYear { get; set; }
        public DateTime MaxBirthYear { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
