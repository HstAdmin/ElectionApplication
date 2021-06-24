using Hst.Persistance.IRepository;
using Hst.Persistance.Reposiotry;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Business.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IOrgRepository OrgRepository { get; }
        IElectionRepository ElectionRepository { get; }
        ICandidateRepository CandidateRepository { get; }
        IPostRepository PostRepository { get; }
        IElectionPostCandiRepository ElecPostCandiRepository { get; }
    }
}
