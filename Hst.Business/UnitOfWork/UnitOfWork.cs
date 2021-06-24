using Hst.Persistance.IRepository;
using Hst.Persistance.Reposiotry;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Business.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IUserRepository userRepository,
            IOrgRepository orgRepository,
            IElectionRepository electionRepository,
            ICandidateRepository candidateRepository,
            IPostRepository postRepository,
            IElectionPostCandiRepository electionPostCandiRepository
            )
        {
            UserRepository = userRepository;
            OrgRepository = orgRepository;
            ElectionRepository = electionRepository;
            CandidateRepository = candidateRepository;
            PostRepository = postRepository;
            ElecPostCandiRepository = electionPostCandiRepository;
        }
        public IUserRepository UserRepository { get; }

        public IOrgRepository OrgRepository { get; }

        public IElectionRepository ElectionRepository { get; }

        public ICandidateRepository CandidateRepository { get; }

        public IPostRepository PostRepository { get; }

        public IElectionPostCandiRepository ElecPostCandiRepository { get; }
    }
}
