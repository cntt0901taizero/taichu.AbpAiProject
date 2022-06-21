using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BaseApplication.Factory
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection Connection => _connection ?? (_connection = new SqlConnection(_connectionString));
        public IDbTransaction DbTransaction
        {
            get
            {
                if (Connection.State != ConnectionState.Open && Connection.State != ConnectionState.Connecting)
                {
                    Connection.Open();
                }
                return _transaction ?? (_transaction = Connection.BeginTransaction());
            }

        }

        public void Commit()
        {
            _transaction.Commit();
            DisposeDbTransaction();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            DisposeDbTransaction();
        }

        private void DisposeDbTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (_transaction != null)
            {
                _transaction?.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
