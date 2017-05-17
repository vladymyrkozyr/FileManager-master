using System;
using FileManager.DAL.Contexts;
using FileManager.DAL.Entities;
using FileManager.DAL.Repositories;

namespace FileManager.DAL.UnitsOfWork
{
    public class UnitOfWork : IDisposable
    {
        private FileManagerDbContext _context = new FileManagerDbContext();
        private GenericRepository<User> _userRepository;
        private GenericRepository<File> _fileRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public GenericRepository<File> FileRepository
        {
            get
            {

                if (this._fileRepository == null)
                {
                    this._fileRepository = new GenericRepository<File>(_context);
                }
                return _fileRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
