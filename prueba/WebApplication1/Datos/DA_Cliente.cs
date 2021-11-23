using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using SGOUtil;
using Entidades.StoreProcedure;

namespace Datos
{
    public class DA_Cliente
    {
        public IEnumerable<UspLisCliente> Usp_LisClientes(string filtro, int estado)
        {
            var response = new List<UspLisCliente>();
            try
            {

                using (var conn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("Usp_LisClientes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = filtro;
                        cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = estado;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    response.Add(new UspLisCliente
                                    {
                                        IdCliente = Convert.ToInt32(Util.Validar_Parametros(reader, "IdCliente", "int")),
                                        DocIdentidad = Convert.ToString(Util.Validar_Parametros(reader, "DocIdentidad", "string")),
                                        NombreComercial = Convert.ToString(Util.Validar_Parametros(reader, "NombreComercial", "string")),
                                        RazonSocial = Convert.ToString(Util.Validar_Parametros(reader, "RazonSocial", "string")),
                                        Estado = Convert.ToInt32(Util.Validar_Parametros(reader, "Estado", "string")),
                                    });
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

        public IEnumerable<UspLisDocumento> UspLisDocumentos (int id)
        {
            var response = new List<UspLisDocumento>();
            try
            {

                using (var conn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("Usp_LisDocumento", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TipoDocumento", SqlDbType.Int).Value = id;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    response.Add(new UspLisDocumento
                                    {
                                        IdTipoDocumento = Convert.ToInt32(Util.Validar_Parametros(reader, "IdTipoDocumento", "int")),
                                        Descripcion = Convert.ToString(Util.Validar_Parametros(reader, "Descripcion", "string")),
                                    });
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
        public IEnumerable<UspLisTipoCliente> UspLisTipoClientes(int id)
        {
            var response = new List<UspLisTipoCliente>();
            try
            {

                using (var conn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("Usp_LisTipoClientes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TipoCliente", SqlDbType.Int).Value = id;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    response.Add(new UspLisTipoCliente
                                    {
                                        IdTipoCliente = Convert.ToInt32(Util.Validar_Parametros(reader, "IdTipoCliente", "int")),
                                        Descripcion = Convert.ToString(Util.Validar_Parametros(reader, "Descripcion", "string")),
                                    });
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

        public int UspInsCliente(int idCliente, string nombreComercial, int idTc, string razonSocial, int idTd, string docId, string direc, int est)
        {
            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("Usp_InsCliente", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                    cmd.Parameters.Add("@NombreComercial", SqlDbType.VarChar, 200).Value = nombreComercial;
                    cmd.Parameters.Add("@IdTipoCliente", SqlDbType.Int).Value = idTc;
                    cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 200).Value = razonSocial;
                    cmd.Parameters.Add("@IdTipoDocumento", SqlDbType.Int).Value = idTd;
                    cmd.Parameters.Add("@DocIdentidad", SqlDbType.VarChar, 100).Value = docId;
                    cmd.Parameters.Add("@DireccionFactura", SqlDbType.VarChar, 200).Value = direc;
                    cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = est;
                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return count;
        }

        public int UspUpdCliente(int idCliente,string nombreComercial, string razonSocial, int idTd, string docId, string direc, int est)
        {
            int count = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("Usp_UpdCliente", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                    cmd.Parameters.Add("@NombreComercial", SqlDbType.VarChar, 200).Value = nombreComercial;
                    cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar,200).Value = razonSocial;
                    cmd.Parameters.Add("@IdTipoDocumento", SqlDbType.Int).Value = idTd;
                    cmd.Parameters.Add("@DocIdentidad", SqlDbType.VarChar, 100).Value = docId;
                    cmd.Parameters.Add("@DireccionFactura", SqlDbType.VarChar, 200).Value = direc;
                    cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = est;
                    count = cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return count;
        }
    }
}
