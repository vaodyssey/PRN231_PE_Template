using AutoMapper;
using Repository.Constants;
using Repository.Entities;
using Repository.Payload.Request.StudentService;
using Repository.Payload.Response.StudentService;
using Repository.Repository;


namespace Repository.Services.Implementation.StudentService
{
    public class GetStudentListService
    {
        private IUnitOfWork _unitOfWork;           
        private List<Student> _students;
        private GetStudentListRequest _request;
        private List<object> _responseStudentList;
        public GetStudentListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public StudentListResponse Get(GetStudentListRequest request)
        {
            ResetStudentListRequest(request);
            GetStudentsByRequest();
            if (_students.Count==0) return StudentsNotFoundResponse();
            MapStudentsToResponseStudentList();            
            return GetStudentListSuccessfulResponse();
        }
        private void ResetStudentListRequest(GetStudentListRequest request)
        {
            _request = null!;
            _request = request;
        }
        private void GetStudentsByRequest()
        {
            _students = _unitOfWork.StudentRepository.Get()
                .Where(student => student.GroupId == _request.GroupId
                        && student.DateOfBirth >= _request.MinBirthYear 
                        && student.DateOfBirth <= _request.MaxBirthYear)
                .Skip((_request.PageIndex - 1) * _request.PageSize)
                .Take(_request.PageSize)
                .ToList();
        }
        private void MapStudentsToResponseStudentList() {
            _responseStudentList = new List<object>();
            foreach (var student in _students) {
                _responseStudentList.Add(new
                {
                    Id = student.Id,
                    Email = student.Email,
                    FullName = student.FullName,
                    DateOfBirth = student.DateOfBirth,
                    GroupName = GetGroupNameByStudent(student)
                });
            }
        }
        private string GetGroupNameByStudent(Student student)
        {
            return _unitOfWork.StudentGroupRepository
                .Get(stuGroup => stuGroup.Id == student.GroupId)
                .FirstOrDefault()
                .GroupName;
        }
        private StudentListResponse GetStudentListSuccessfulResponse()
        {
            return new StudentListResponse()
            {
                Result = "Success",
                StatusCode = StudentServiceStatusCode.GET_STUDENTS_SUCCESSFUL,
                Message = "Successfully retrieved student data",
                ReturnData = _responseStudentList
            };
        }

        private StudentListResponse StudentsNotFoundResponse()
        {
            return new StudentListResponse()
            {
                Result = "Success",
                StatusCode = StudentServiceStatusCode.STUDENTS_NOT_FOUND,
                Message = $"No students are found in the database. Please add some Students, then try again."
            };
        }
    }
}
