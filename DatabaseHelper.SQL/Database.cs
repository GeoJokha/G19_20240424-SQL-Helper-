using System.Data;
using Microsoft.Data.SqlClient;

namespace DatabaseHelper.SQL
{
    // TODO: Add transaction support to this class.
    // TODO: Test execute reader.
    public sealed class Database
    {
        
        private readonly string _connectionString;

        
        public Database(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new(_connectionString);
            return connection;
        }

        public SqlCommand GetCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlCommand command = GetConnection().CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Parameters.AddRange(parameters);
            return command;
        }

        public SqlCommand GetCommand(string commandText, params SqlParameter[] parameters) =>
            GetCommand(commandText, CommandType.Text, parameters);

        public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {

            using SqlCommand command = GetCommand(commandText, commandType, parameters);
            command.Connection.Open();
            return command.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(string commandText, params SqlParameter[] parameters) =>
            ExecuteNonQuery(commandText, CommandType.Text, parameters);

        public object? ExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand command = GetCommand(commandText, commandType, parameters);
            command.Connection.Open();
            return command.ExecuteScalar();
        }

        public object? ExecuteScalar(string commandText, params SqlParameter[] parameters) =>
            ExecuteScalar(commandText, CommandType.Text, parameters);

        public SqlDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlCommand command = GetCommand(commandText, commandType, parameters);
            command.Connection.Open();
            return command.ExecuteReader();
        }

        public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters) =>
            ExecuteReader(commandText, CommandType.Text, parameters);

        SqlTransaction BeginTransaction()
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            return connection.BeginTransaction();
        }

        public int ExecuteNonQuery(SqlTransaction transaction, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand command = GetCommand(commandText, commandType, parameters);
            command.Connection = transaction.Connection;
            command.Transaction = transaction;
            return command.ExecuteNonQuery();
        }
        
        public int ExecuteNonQuery(SqlTransaction transaction, string commandText, params SqlParameter[] parameters) =>
            ExecuteNonQuery(transaction, commandText, CommandType.Text, parameters);

        public object? ExecuteScalar(SqlTransaction transaction, string commandText, CommandType commandType, params SqlParameter[] parameters)

        {
            using SqlCommand command = GetCommand(commandText, commandType, parameters);
            command.Connection = transaction.Connection;
            command.Transaction = transaction;
            return command.ExecuteScalar();
        }

        public object? ExecuteScalar(SqlTransaction transaction, string commandText, params SqlParameter[] parameters) =>
            ExecuteScalar(transaction, commandText, CommandType.Text, parameters);

        public SqlDataReader ExecuteReader(SqlTransaction transaction, string commandText, CommandType commandType, params SqlParameter[] parameters)

        {
            SqlCommand command = GetCommand(commandText, commandType, parameters);
            command.Connection = transaction.Connection;
            command.Transaction = transaction;
            return command.ExecuteReader();
        }

        public SqlDataReader ExecuteReader(SqlTransaction transaction, string commandText, params SqlParameter[] parameters) =>
            ExecuteReader(transaction, commandText, CommandType.Text, parameters);

        public void CommitTransaction(SqlTransaction transaction)

        {
            transaction.Commit();
            transaction.Connection.Close();
        }

        public void RollbackTransaction(SqlTransaction transaction)

        {
            transaction.Rollback();
            transaction.Connection.Close();
        }       
    }
}
