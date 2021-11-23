using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Datos;
using Entidades.StoreProcedure;

namespace Negocios
{
    public class BL_Cliente
    {
        DA_Cliente _daCliente = new DA_Cliente();

        public IEnumerable<UspLisCliente> Usp_LisClientes(string filtro, int estado)
        {
            return _daCliente.Usp_LisClientes(filtro, estado);
        }

        public IEnumerable<UspLisDocumento> UspLisDocumentos(int id)
        {
            return _daCliente.UspLisDocumentos(id);
        }
        public IEnumerable<UspLisTipoCliente> UspLisTipoClientes(int id)
        {
            return _daCliente.UspLisTipoClientes(id);
        }

        public int UspInsCliente(int idCliente, string nombreComercial,int idTc, string razonSocial, int idTd, string docId, string direc, int est)
        {
            return _daCliente.UspInsCliente(idCliente, nombreComercial, idTc, razonSocial, idTd, docId, direc, est);
        }
        public int UspUpdCliente(int idCliente, string nombreComercial,int idTc, string razonSocial, int idTd, string docId, string direc, int est)
        {
            return _daCliente.UspInsCliente(idCliente, nombreComercial, idTc, razonSocial, idTd, docId, direc, est);
        }
    }
}
