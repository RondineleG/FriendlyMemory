using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FriendlyMemory.Core.Data
{
    internal class AcessoBancoDeDados
    {
        private string StringDesqlConnection
        {
            get
            {
                var connectionStringSettings = ConfigurationManager.ConnectionStrings["BancoDeDados"];

                return connectionStringSettings != null ? connectionStringSettings.ConnectionString : string.Empty;
            }
        }

        internal void Executar(string NomeProcedure, List<SqlParameter> parametros)
        {
            var sqlCommand = new SqlCommand();
            var sqlConnection = new SqlConnection(StringDesqlConnection);

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = NomeProcedure;
            foreach (var item in parametros)
                sqlCommand.Parameters.Add(item);

            sqlConnection.Open();
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal DataSet Consultar(string NomeProcedure, List<SqlParameter> parametros)
        {
            var sqlCommand = new SqlCommand();
            var sqlConnection = new SqlConnection(StringDesqlConnection);

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = NomeProcedure;
            foreach (var item in parametros)
            {
                sqlCommand.Parameters.Add(item);
            }

            var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            var dataSet = new DataSet();

            sqlConnection.Open();

            try
            {
                sqlDataAdapter.Fill(dataSet);
            }
            finally
            {
                sqlConnection.Close();
            }

            return dataSet;
        }

    }
}
