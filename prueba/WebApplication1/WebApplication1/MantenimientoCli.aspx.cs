using System;
using System.Collections.Generic;
using System.Web.Services;
using Entidades;
using Negocios;
using Entidades.StoreProcedure;

namespace WebApplication1
{
    public partial class MantenimientoCli : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var _blCliente = new BL_Cliente();
        }

        [WebMethod()]
        public static IEnumerable<UspLisCliente> Usp_LisClientes(string filtro, int estado)
        {
            var _blCliente = new BL_Cliente();
            return _blCliente.Usp_LisClientes(filtro, estado);
        }

        [WebMethod()]
        public static IEnumerable<UspLisDocumento> UspLisDocumentos(int id)
        {
            var _blCliente = new BL_Cliente();
            return _blCliente.UspLisDocumentos(id);
        }
        [WebMethod()]
        public static IEnumerable<UspLisTipoCliente> UspLisTipoCliente(int id)
        {
            var _blCliente = new BL_Cliente();
            return _blCliente.UspLisTipoClientes(id);
        }
        [WebMethod()]
        public static int UspInsCliente(int idCliente, string nombreComercial, int idTc, string razonSocial, int idTd, string docId, string direc, int est)
        {
            var _blCliente = new BL_Cliente();
            return _blCliente.UspInsCliente(idCliente, nombreComercial, idTc, razonSocial, idTd, docId, direc, est);
        }

        [WebMethod()]
        public static int UspUpdCliente(int idCliente, string nombreComercial, int idTc,  string razonSocial, int idTd, string docId, string direc, int est)
        {
            var _blCliente = new BL_Cliente();
            return _blCliente.UspUpdCliente(idCliente, nombreComercial, idTc, razonSocial, idTd, docId, direc, est);
        }
    }
}