using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using SGOUtil;

namespace Datos
{
    public class DA_Partida
    {
        
        public ICollection<BE_VWPartidas> Mostrar()
        {

            var response = new List<BE_VWPartidas>();
            try
            {
                
                using (var conn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))

                {
                    conn.Open();
                    using (var cmd = new SqlCommand("usp_verPartidaVW", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    var responseBe = new BE_VWPartidas
                                    {

                                        IdPartida = Convert.ToInt32(Util.Validar_Parametros(reader, "Idpartida", "int")),
                                        vcDescripcion = Util.Validar_Parametros(reader, "vcDescripcion", "string").ToString(),
                                    };
                                    response.Add(responseBe);
                                }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        SqlCommand cmd = new SqlCommand();

        public int InsertarPartida(string vcDescripcion)
        {
            int count = 0; 
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("usp_NuevaPartidaVW", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@vcdescr", SqlDbType.NVarChar, 200).Value = vcDescripcion;

                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();

            }

            return count;      
        }
        
        public int EditarPartida(int IdPartida, string vcDescripcion)
        {
            
            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("usp_ActualizarPartidaVW", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idpartida", SqlDbType.Int)).Value = IdPartida;
                    cmd.Parameters.Add("@vcdescr", SqlDbType.NVarChar, 200).Value = vcDescripcion;
                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();

            }
            return count;
        }
        
        public int DesactivarPartida(int IdPartida, int InEstado)
        {
            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("usp_DesactivarPartidaVW", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdPartida", SqlDbType.Int).Value = IdPartida;
                    cmd.Parameters.Add("@InEstado", SqlDbType.Int).Value = InEstado;

                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();

            }

            return count;
        }

        public ICollection<BE_VWSubpartida> MostrarSP(int IdPartida)
        {

            var response = new List<BE_VWSubpartida>();
            try
            {
                
                using (var conn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))

                {
                    conn.Open();
                    using (var cmd = new SqlCommand("usp_verSubPartidaVW", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@InPartida", SqlDbType.Int)).Value = IdPartida;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    var responseBe = new BE_VWSubpartida
                                    {
                                        IdSubpartida = Convert.ToInt32(Util.Validar_Parametros(reader, "IdSubpartida", "int")),
                                        vcSubpartida = Util.Validar_Parametros(reader, "vcSubpartida", "string").ToString(),
                                    };
                                    response.Add(responseBe);
                                }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }


        public int InsertarSubPartida(string vcSubpartida, int InPartida)
        {
            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("usp_NuevaSubPartidaVW", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@vcsubpartida", SqlDbType.NVarChar, 200).Value = vcSubpartida;

                    cmd.Parameters.Add("@inpartida", SqlDbType.Int).Value = InPartida;
                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();

            }

            return count;
        }

        public int EditarSubPartida(int IdSubpartida, string vcSubpartida)
        {

            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("usp_ActualizarSubPartidaVW", cnn))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idsubpartida", SqlDbType.Int)).Value = IdSubpartida;
                    cmd.Parameters.Add("@vcsubpartida", SqlDbType.NVarChar, 200).Value = vcSubpartida;
                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();

            }

            return count;
        }

        public int DesactivarSubPartida(int InPartida, int IdSubpartida, int InEstado)
        {
            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("usp_DesactivarPartidaVW", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InPartida", SqlDbType.Int).Value = InPartida;
                    cmd.Parameters.Add("@IdSubpartida", SqlDbType.Int).Value = IdSubpartida;
                    cmd.Parameters.Add("@InEstado", SqlDbType.Int).Value = InEstado;

                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();

            }

            return count;
        }

        public ICollection<BE_GASTO_CENTRO_COSTO> MostrarCC()
        {

            var response = new List<BE_GASTO_CENTRO_COSTO>();
            try
            {
                
                using (var conn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))

                {
                    conn.Open();
                    using (var cmd = new SqlCommand("usp_vergcc", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    var responseBe = new BE_GASTO_CENTRO_COSTO
                                    {

                                        IdGastoCentroCosto = Convert.ToInt32(Util.Validar_Parametros(reader, "IdGastoCentroCosto", "int")),
                                        Descripcion = Util.Validar_Parametros(reader, "Descripcion", "string").ToString(),
                                    };
                                    response.Add(responseBe);
                                }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        
        public int InsertarCeCo(string Codigo, int IdGastoCentroCosto, int IdPartida)
        {
            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("spu_InsertarVWPARTIDA_CENTRO_COSTO", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Codigo", SqlDbType.NVarChar, 10).Value = Codigo;

                    cmd.Parameters.Add("@IdGastoCentroCosto", SqlDbType.Int).Value = IdGastoCentroCosto;
                    cmd.Parameters.Add("@IdPartida", SqlDbType.Int).Value = IdPartida;

                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();

            }

            return count;
        }

        
    }
}
