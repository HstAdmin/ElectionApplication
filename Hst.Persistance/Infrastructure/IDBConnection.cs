using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hst.Persistance.Infrastructure
{
    public interface IConnectionfactory : IDisposable 
    {
        IDbConnection GetConnection();
    }
}
