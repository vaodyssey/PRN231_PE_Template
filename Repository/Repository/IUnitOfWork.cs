using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> StudentRepository { get; }
        IGenericRepository<StudentGroup> StudentGroupRepository { get; }
        IGenericRepository<UserRole> UserRoleRepository{ get; }
        void Save();
    }

}
