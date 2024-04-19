using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Constants
{
    internal class StudentServiceStatusCode
    {

        public static int INTERNAL_SERVER_ERROR = 500;
        public static int STUDENT_NOT_FOUND = 404;
        public static int CREATE_STUDENT_SUCCESSFUL, DELETE_STUDENT_SUCCESSFUL, UPDATE_STUDENT_SUCCESSFUL = 200;
    }
}
