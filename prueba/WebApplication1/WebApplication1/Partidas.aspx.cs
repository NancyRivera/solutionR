using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Newtonsoft.Json;
using Entidades;

namespace WebApplication1
{
    public partial class Partidas : System.Web.UI.Page
    {
        private static BL_Partida objBLpar;
        BE_VWPartidas objPar = new BE_VWPartidas();

        protected void Page_Load(object sender, EventArgs e)
        {
            objBLpar = new BL_Partida();
        }

        [WebMethod()]
        public static string verPartidaVW()
        {

            objBLpar = new BL_Partida();
            var response = objBLpar.Mostrar().ToList();
            var respuesta = JsonConvert.SerializeObject(response);
            return respuesta;
        }


        [WebMethod()]
        public static int InsertarPartida(string vcDescripcion) 
        {
            var response = objBLpar.InsertarPartida(vcDescripcion);
            return response;
        }
        
        [WebMethod()]
        public static int EditarPartida(int IdPartida, string vcDescripcion)
        {
           return objBLpar.EditarPartida(IdPartida, vcDescripcion);
        }

        [WebMethod()]
        public static int DesactivarPartida(int IdPartida, int InEstado)
        {
            var response = objBLpar.DesactivarPartida(IdPartida, InEstado);
            return response;
        }

        ///para subpartida

        [WebMethod()]
        public static string verSubpartidaVW(int Inpartida)
        {

            objBLpar = new BL_Partida();
            var response = objBLpar.MostrarSP(Inpartida).ToList();
            var respuesta = JsonConvert.SerializeObject(response);
            return respuesta;
        }

        [WebMethod()]
        public static int InsertarSubPartida(string vcSubpartida, string InPartida) 
        {
            var response = objBLpar.InsertarSubPartida(vcSubpartida, Convert.ToInt32(InPartida));
            return response;
        }

        [WebMethod()]
        public static int EditarSubPartida(int IdSubpartida, string vcSubpartida)
        {
            return objBLpar.EditarSubPartida(IdSubpartida, vcSubpartida);
        }

        [WebMethod()]
        public static int DesactivarSubPartida(int InPartida, int IdSubpartida, int InEstado )
        {
            return objBLpar.DesactivarSubPartida(InPartida, IdSubpartida, InEstado);
        }

        //centro costo
        [WebMethod()]
        public static string MostrarCC()
        {

            objBLpar = new BL_Partida();
            var response = objBLpar.MostrarCC().ToList();
            var respuesta = JsonConvert.SerializeObject(response);
            return respuesta;
        }
       //nuevo centro costo

        [WebMethod()]
        public static int InsertarCeCo(string Codigo, string IdGastoCentroCosto, int IdPartida) 
        {
            var response = objBLpar.InsertarCeCo(Codigo, Convert.ToInt32(IdGastoCentroCosto), IdPartida);
            return response;
        }
        
    }
}