using FriendlyMemory.Core.Data;
using FriendlyMemory.Core.Entities;
using System.Collections.Generic;

namespace FriendlyMemory.Core.Bussines
{
    public class ClienteBussines
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(Cliente cliente)
        {
            var clienteAcessoDados = new ClienteAcessoDados();
            return clienteAcessoDados.Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(Cliente cliente)
        {
            var clienteAcessoDados = new ClienteAcessoDados();
            clienteAcessoDados.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public Cliente Consultar(long id)
        {
            var clienteAcessoDados = new ClienteAcessoDados();
            return clienteAcessoDados.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            var clienteAcessoDados = new ClienteAcessoDados();
            clienteAcessoDados.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Listar()
        {
            var clienteAcessoDados = new ClienteAcessoDados();
            return clienteAcessoDados.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var clienteAcessoDados = new ClienteAcessoDados();
            return clienteAcessoDados.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            var clienteAcessoDados = new ClienteAcessoDados();
            return clienteAcessoDados.VerificarExistencia(CPF);
        }
    }
}
