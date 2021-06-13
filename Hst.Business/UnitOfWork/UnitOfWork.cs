using Hst.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Business.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IUserRepository userRepository,
            IOrgRepository orgRepository
            )
        {
            UserRepository = userRepository;
            OrgRepository = orgRepository;
        }
        public IUserRepository UserRepository { get; }

        public IOrgRepository OrgRepository { get; }
    }
}
