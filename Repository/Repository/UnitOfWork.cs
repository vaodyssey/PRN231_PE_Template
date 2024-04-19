using Repository.Entities;
using Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PRN231_SU23_StudentGroupDBContext? _dbContext;
        private bool disposed = false;
        private IGenericRepository<Student> _studentRepository { get; set; }
        private IGenericRepository<StudentGroup> _studentGroupRepository { get; set; }
        private IGenericRepository<UserRole> _userRoleRepository { get; set; }
        public UnitOfWork(PRN231_SU23_StudentGroupDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<Student> StudentRepository 
        {
            get
            {
                return _studentRepository ??= new GenericRepository<Student>(_dbContext!);
            }
        }
        public IGenericRepository<StudentGroup> StudentGroupRepository
        {
            get
            {
                return _studentGroupRepository ??= new GenericRepository<StudentGroup>(_dbContext!);
            }
        }
        public IGenericRepository<UserRole> UserRoleRepository
        {
            get
            {
                return _userRoleRepository ??= new GenericRepository<UserRole>(_dbContext!);
            }
        }


        public void Save()
        {
            _dbContext!.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext!.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
