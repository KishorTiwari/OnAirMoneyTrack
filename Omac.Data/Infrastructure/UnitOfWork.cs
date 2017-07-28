using Omack.Data.DAL;
using Omack.Data.Infrastructure.Repositories;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private OmackContext _context;
        private GroupRepository groupRepository;
        private GroupUserRepository groupUserRepository;
        private ItemRepository itemRepository;
        private MediaRepository mediaRepository;
        private TransactionRepository transactionRepository;
        public UnitOfWork(OmackContext omackContext)
        {
            _context = omackContext;
        }

        public GroupRepository GroupRepository
        {
            get
            {
                if (this.groupRepository == null)
                {
                    this.groupRepository = new GroupRepository(_context);
                }
                return groupRepository;
            }
        }
        public GroupUserRepository GroupUserRepository
        {
            get
            {
                if (this.groupUserRepository == null)
                {
                    this.groupUserRepository = new GroupUserRepository(_context);
                }
                return groupUserRepository;
            }
        }
        public ItemRepository ItemRepository
        {
            get
            {
                if (this.itemRepository == null)
                {
                    this.itemRepository = new ItemRepository(_context);
                }
                return itemRepository;
            }
        }
        public MediaRepository MediaRepository
        {
            get
            {
                if(mediaRepository == null)
                {
                    mediaRepository = new MediaRepository(_context);
                    return mediaRepository;
                }
                return mediaRepository;
            }
        }
        public TransactionRepository TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                {
                    this.transactionRepository = new TransactionRepository(_context);
                }
                return transactionRepository;
            }
        }


        public void Save()
        {
            _context.SaveChanges(); //save changes to the db.
        }
        private bool disposed = false;  //initially set disposed to false.
        void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;   //changed it to true once it is disposed
        }
        public void Dispose() //gets called everytime the unitofwork is called. Because it is of IDisposable type
        {
            Dispose(true); //call above method Dispose(bool disposing). Remember that method is valid because it has different methods. AKA overload methods.
            GC.SuppressFinalize(this);
        }

        //Return IDbContextTransaction (using Microsoft.EntityFrameworkCore.Storage;)
        public IDatabaseTransaction BeginTransaction()
        {
            return new DatabaseTransaction(_context);
        }
    }
}
