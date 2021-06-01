using Hst.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Business.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IUserRepository userRepository
            )
        {
            UserRepository = userRepository;
        }
        public IUserRepository UserRepository { get; }
    }
}
