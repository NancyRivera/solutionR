using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Datos;
using Entidades;

namespace Negocios
{
    public class BL_Partida
    {
        DA_Partida objData = new DA_Partida();


        public ICollection<BE_VWPartidas> Mostrar()
        {
            return objData.Mostrar();
        }

        public int InsertarPartida(string vcDescripcion)
        {
            return objData.InsertarPartida(vcDescripcion);

        }
        public int EditarPartida(int IdPartida, string vcDescripcion)
        {
            return objData.EditarPartida(Convert.ToInt32(IdPartida),vcDescripcion);

        }
        public int DesactivarPartida(int IdPartida,int InEstado)
        {
            return objData.DesactivarPartida(IdPartida, InEstado);
        }


        //PARA SUBPARTIDA

        public ICollection<BE_VWSubpartida> MostrarSP(int IdPartida)
        {
            return objData.MostrarSP(IdPartida);
        }

        public int InsertarSubPartida(string vcSubpartida, int InPartida)
        {
            return objData.InsertarSubPartida(vcSubpartida, InPartida);

        }

        public int EditarSubPartida(int IdSubpartida, string vcSubpartida)
        {
            return objData.EditarSubPartida(Convert.ToInt32(IdSubpartida), vcSubpartida);
        }

        public int DesactivarSubPartida(int InPartida, int IdSubpartida, int InEstado)
        {
            return objData.DesactivarSubPartida(InPartida, IdSubpartida, InEstado);
        }
        //para centro de costo
        public ICollection<BE_GASTO_CENTRO_COSTO> MostrarCC()
        {
            return objData.MostrarCC();
        }
        //pcc
        public int InsertarCeCo(string Codigo, int IdGastoCentroCosto, int IdPartida)
        {
            
            return objData.InsertarCeCo(Codigo, IdGastoCentroCosto, IdPartida);

        }
    }
}
