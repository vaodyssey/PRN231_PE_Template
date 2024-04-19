using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Constants
{
    public class StudentServiceStatusCode
    {

        public static int INTERNAL_SERVER_ERROR = 500;
        public static int STUDENTS_NOT_FOUND = 200;
        public static int STUDENT_NOT_FOUND = 404;
        public static int GET_STUDENTS_SUCCESSFUL = 200;
        public static int GET_STUDENT_BY_ID_SUCCESSFUL = 200;
        public static int CREATE_STUDENT_SUCCESSFUL = 200;
        public static int DELETE_STUDENT_SUCCESSFUL = 200;
        public static int UPDATE_STUDENT_SUCCESSFUL = 200;
    }
}
