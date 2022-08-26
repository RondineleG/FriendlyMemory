using FriendlyMemory.Core.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FriendlyMemory.Core.Data
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class ClienteAcessoDados : AcessoBancoDeDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Cliente cliente)
        {
            var sqlParameter = new List<SqlParameter>
            {
                new SqlParameter("Nome", cliente.Nome),
                new SqlParameter("Sobrenome", cliente.Sobrenome),
                new SqlParameter("Nacionalidade", cliente.Nacionalidade),
                new SqlParameter("CEP", cliente.CEP),
                new SqlParameter("Estado", cliente.Estado),
                new SqlParameter("Cidade", cliente.Cidade),
                new SqlParameter("Logradouro", cliente.Logradouro),
                new SqlParameter("Email", cliente.Email),
                new SqlParameter("Telefone", cliente.Telefone)
            };

            var dataSet = Consultar("SP_IncClienteV2", sqlParameter);
            long retorno = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                long.TryParse(dataSet.Tables[0].Rows[0][0].ToString(), out retorno);
            }

            return retorno;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal Cliente Consultar(long Id)
        {
            var sqlParameter = new List<SqlParameter>
            {
                new SqlParameter("Id", Id)
            };

            DataSet dataSet = Consultar("SP_ConsCliente", sqlParameter);
            var cliente = Converter(dataSet);

            return cliente.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            var sqlParameter = new List<SqlParameter>
            {
                new SqlParameter("CPF", CPF)
            };

            var dataSet = Consultar("SP_VerificaCliente", sqlParameter);

            return dataSet.Tables[0].Rows.Count > 0;
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var sqlParameter = new List<SqlParameter>
            {
                new SqlParameter("iniciarEm", iniciarEm),
                new SqlParameter("quantidade", quantidade),
                new SqlParameter("campoOrdenacao", campoOrdenacao),
                new SqlParameter("crescente", crescente)
            };

            var dataSet = Consultar("SP_PesqCliente", sqlParameter);
            var cliente = Converter(dataSet);

            int iQtd = 0;

            if (dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
            {
                int.TryParse(dataSet.Tables[1].Rows[0][0].ToString(), out iQtd);
            }

            qtd = iQtd;

            return cliente;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Cliente> Listar()
        {
            var sqlParameter = new List<SqlParameter>
            {
                new SqlParameter("Id", 0)
            };

            var dataSet = Consultar("SP_ConsCliente", sqlParameter);
            var cliente = Converter(dataSet);

            return cliente;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(Cliente cliente)
        {
            var sqlParameter = new List<SqlParameter>
            {
                new SqlParameter("Nome", cliente.Nome),
                new SqlParameter("Sobrenome", cliente.Sobrenome),
                new SqlParameter("Nacionalidade", cliente.Nacionalidade),
                new SqlParameter("CEP", cliente.CEP),
                new SqlParameter("Estado", cliente.Estado),
                new SqlParameter("Cidade", cliente.Cidade),
                new SqlParameter("Logradouro", cliente.Logradouro),
                new SqlParameter("Email", cliente.Email),
                new SqlParameter("Telefone", cliente.Telefone),
                new SqlParameter("ID", cliente.Id)
            };

            Executar("SP_AltCliente", sqlParameter);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            var sqlParameter = new List<SqlParameter>
            {
                new SqlParameter("Id", Id)
            };

            Executar("SP_DelCliente", sqlParameter);
        }

        private List<Cliente> Converter(DataSet dataSet)
        {
            var listaClientes = new List<Cliente>();
            if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var cliente = new Cliente
                    {
                        Id = row.Field<long>("Id"),
                        CEP = row.Field<string>("CEP"),
                        Cidade = row.Field<string>("Cidade"),
                        Email = row.Field<string>("Email"),
                        Estado = row.Field<string>("Estado"),
                        Logradouro = row.Field<string>("Logradouro"),
                        Nacionalidade = row.Field<string>("Nacionalidade"),
                        Nome = row.Field<string>("Nome"),
                        Sobrenome = row.Field<string>("Sobrenome"),
                        Telefone = row.Field<string>("Telefone")
                    };
                    listaClientes.Add(cliente);
                }
            }

            return listaClientes;
        }
    }
}
