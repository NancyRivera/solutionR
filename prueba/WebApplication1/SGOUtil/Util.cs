using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Net.Mail;
using System.Web.Script.Serialization;
using Microsoft.Office.Interop.Outlook;
using System.Web;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;

namespace SGOUtil
{
    public class Util
    {
        public enum TipoMotivoOtorgado
        {
            AnticipoPreCampania = 1,
            AnticipoFinanciamientoDeAcopio = 2,
            Cancelación = 3,
            Devolución = 4,
            Detracción = 6,
            OtrosCargosDiversos = 7,
            SaldosIniciales = 8,
            PagoTransportista = 10,
            HabilitacionCajaChica = 15,
            HabilitaciónCuentaZonaParaAcopio = 16,
            Comision = 17,
            Fertilizante = 18,
            Prima = 19,
            PagoAProveedores = 24,
            ProvisionDetracción = 25,
            CobranzaDudosa = 26,
            AjusteDeCierreDeCosecha = 28,
            NotaDebitoPrpFactura = 29,
            NotaDebitoProveedorFactura = 30,
            NotaCreditoPrpFactura = 31,
            NotaCreditoProveedorFactura = 32,
            CarpaSolar = 33,
            CobroDeIntereses = 34,
            AjustePorRedondeo = 35,
            Anticipo = 36,
            VentaSacoDeSegundoUso = 38,
            NotaDeCreditoPorAnticipo = 39,
            SolicitudDeServicio = 40,
            PremioStarbucksCp = 41,
            PremioStarbucksCpPorPagar = 43,
            AjusteMermaFtFto = 45,
            DiferenciaTipoCambioFtFto = 46,
            OtrosAjustesDeCosecha = 47,
            VentaDePajilla = 48,
            CobroCombustible = 50,
            VentaDeCafe = 51
        }
        public enum CajaChicaAprobacion
        {
            Pendiente = -1,
            Aprobado = 1,
            Cerrado = 2
        }
        public enum CajaChicaTipoOperacion
        {
            Sumar = 1,
            Restar = 2
        }
        public enum BancoPagoMasivoTXT
        {
            BCP = 2,
            BBVA = 3,
            SANTANDER = 16,
            BANBIF = 18,
            PICHINCHA = 19,
            INTERBANK = 9
        }
        public enum TablaBlackList
        {
            TRASLADO_EMPRESA,
            TRASLADO_CHOFER
        }
        public enum HttMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        //20180222
        public enum CnnType
        {
            CnnSGU = 7,
            CnnSGO = 24,
            CnnRRHH = 25,
            CnnSCG = 26,
            CnnPRPE = 27
        }

        public enum TipoApplicacion
        {
            Web,
            Console
        }

        public enum TipoOperacion
        {
            Insertar = 1,
            Modificar = 2,
            Select = 3,
            Seguridad = 4
        }

        public enum EstadoVisita
        {
            Espera = 1,
            Cancelado = 2,
            Ingreso = 3,
            Retiro = 4
        }

        public enum Estado
        {
            Activo = 1,
            Anulado = 0
        }

        public enum EstadoCajaChica
        {
            AprobacionPendiente =-1,
            Aprobado = 1,
            Cerrado= 2
        }
        public enum Vigencia
        {
            Vigente = 1,
            Anulado = 0
        }

        public enum EstadoTicketPesada
        {
            No_requerido = 0,
            Pendiente = 1,
            En_proceso = 2,
            Terminado = 3
        }

        public enum EstadoServicioPRP
        {
            Anulado = 0,
            En_proceso = 1,
            Terminado = 2
        }

        public enum CodigoLocal
        {
            Pichanaqui = 24,
            VillaRica = 31,
            Pangoa = 32,
            MoyobambaA = 29,
            SanIgnacio = 30,
            Mazamari = 25,
            JaenAgromar = 26,
            JaenComcafe = 33,
            Lima = 1,
            MoyobambaB = 36,
            JaenShumba = 38
        }
        public enum OCuentaContable
        {
            Facturacion = 1,
            CajaChica = 2,
            HCajaChica = 3
        }
        public enum StatusInvoice
        {
            Annulled,
            Registered,
            Rejected,
            Approved,
            SentSUNAT,
            AcceptedSUNAT,
            AcceptedWithObservations,
            RejectedSUNAT,
            WrittenOff
        }

        public enum Series
        {
            Exportacion_FF12 = 12
        }

        public enum Gerente
        {
            ROrtiz = 1416,
            AVildoso = 421
        }

        public static IEnumerable<EnumDescriptionAndValue> GetAllEnumsWithChilds()
        {
            var enums = new List<EnumDescriptionAndValue>();
            var order = 0;

            foreach (var type in typeof(Util).GetNestedTypes())
            {
                var parent = new EnumDescriptionAndValue
                {
                    Name = type.Name,
                    Order = order
                };

                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    var i = 0;
                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    parent.Childs.Add(new EnumDescriptionAndValue
                    {
                        Name = field.Name,
                        Value = field.GetRawConstantValue().ToString(),
                        Description = attribute == null ? field.Name : attribute.Description,
                        Order = i
                    });

                    i++;
                }

                enums.Add(parent);
                order++;
            }

            return enums;
        }
        public static string ToEstadoString(int idEstado)
        {
            string response = string.Empty;
            if (idEstado == Convert.ToInt32(Estado.Activo))
            {
                response = Estado.Activo.ToString();
            }
            else
            {
                response = Estado.Anulado.ToString();
            }
            return response;
        }
        public static string GetInvoiceStatus(StatusInvoice status)
        {
            var response = string.Empty;
            if (status == StatusInvoice.Annulled)
            {
                response = "ANULADO";
            }
            else if (status == StatusInvoice.Registered)
            {
                response = "REGISTRADO";
            }
            else if (status == StatusInvoice.Approved)
            {
                response = "APROBADO";
            }
            else if (status == StatusInvoice.Rejected)
            {
                response = "RECHAZADO";
            }
            else if (status == StatusInvoice.SentSUNAT)
            {
                response = "ENVIADO A SUNAT";
            }
            else if (status == StatusInvoice.AcceptedSUNAT)
            {
                response = "ACEPTADO POR SUNAT";
            }
            else if (status == StatusInvoice.AcceptedWithObservations)
            {
                response = "ACEPTADO CON OBSERVACIONES";
            }
            else if (status == StatusInvoice.RejectedSUNAT)
            {
                response = "RECHAZADO POR SUNAT";
            }
            else if (status == StatusInvoice.WrittenOff)
            {
                response = "DADO DE BAJA";
            }
            else
            {
                response = null;
            }
            return response;
        }
        public string[] arrStrJefesZona = { "1416", "1455", "1398", "1402", "1403", "1415", "567", "1487", "1385", "1431", "1477" };
        public static string GetIdEmpresa()
        {
            return ConfigurationManager.AppSettings["IdEmpresa"];
        }

        public static string GetStringConnection(CnnType typeConnection)
        {
            string name = Enum.GetName(typeof(CnnType), typeConnection);
            string conexionWebConfig = name != null ? ConfigurationManager.ConnectionStrings[name].ConnectionString : string.Empty;
            var userSession = HttpContext.Current.Session["TipoConexionDB"];
            if (userSession == null)
            {
                return conexionWebConfig;
            }
            if (name== "CnnSGU")
            {
                return conexionWebConfig;
            }
            if (userSession.Equals("0"))
            {
                return conexionWebConfig;
            }
            string conexion = DecodeFrom64(userSession.ToString());
            return conexion;
        }

        public static string GetDbConexionStringConnection()
        {
            string name = $"{CnnType.CnnSGO}";
            string conexion = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return conexion;
        }

        public static string GetStringConnection(CnnType typeConnection, TipoApplicacion tipoApplicacion)
        {
            string name = Enum.GetName(typeof(CnnType), typeConnection);
            string conexionWebConfig = name != null ? ConfigurationManager.ConnectionStrings[name].ConnectionString : string.Empty;
            return conexionWebConfig;
        }
        public static string GetAppSetings(string key)
        {
            var Confi = string.Empty;
            try
            {
                Confi = ConfigurationManager.AppSettings[key];
            }
            catch (System.Exception)
            {
                Confi = null;
            }
            return Confi;
        }
        public static object Validar_Parametros(SqlDataReader oReader, String campo, String tipo)
        {
            object result = DBNull.Value;
            if (tipo == "string")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) ? oReader.GetString(oReader.GetOrdinal(campo)).Trim() : "";
            }
            else if (tipo == "int")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) ? oReader.GetInt32(oReader.GetOrdinal(campo)) : 0;
            }
            else if (tipo == "date")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) ? oReader.GetDateTime(oReader.GetOrdinal(campo)) : Convert.ToDateTime("01/01/1900");
            }
            else if (tipo == "bool")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) && oReader.GetBoolean(oReader.GetOrdinal(campo));
            }
            else if (tipo == "decimal")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetDecimal(oReader.GetOrdinal(campo));
                else
                    result = (decimal)0;
            }
            else if (tipo == "float")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetFloat(oReader.GetOrdinal(campo));
                else
                    result = (double)0;
            }
            else if (tipo == "long")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetInt64(oReader.GetOrdinal(campo));
                else
                    result = (long)0;
            }
            return result;
        }
        public static object Validar_Parametros(DbDataReader oReader, String campo, String tipo)
        {
            object result = DBNull.Value;
            if (tipo == "string")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) ? oReader.GetString(oReader.GetOrdinal(campo)).Trim() : "";
            }
            else if (tipo == "int")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) ? oReader.GetInt32(oReader.GetOrdinal(campo)) : 0;
            }
            else if (tipo == "date")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) ? oReader.GetDateTime(oReader.GetOrdinal(campo)) : Convert.ToDateTime("01/01/1900");
            }
            else if (tipo == "bool")
            {
                result = !oReader.IsDBNull(oReader.GetOrdinal(campo)) && oReader.GetBoolean(oReader.GetOrdinal(campo));
            }
            else if (tipo == "decimal")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetDecimal(oReader.GetOrdinal(campo));
                else
                    result = (decimal)0;
            }
            else if (tipo == "float")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetFloat(oReader.GetOrdinal(campo));
                else
                    result = (double)0;
            }
            else if (tipo == "long")
            {
                if (!oReader.IsDBNull(oReader.GetOrdinal(campo)))
                    result = oReader.GetInt64(oReader.GetOrdinal(campo));
                else
                    result = (long)0;
            }
            return result;
        }

        public static object Asignar_Parametros(object valor, string tipo, bool nulo)
        {
            object result = DBNull.Value;
            if (tipo == "string")
            {
                var temporal = (string)valor;
                if (!string.IsNullOrEmpty(temporal)) result = temporal;
                else if (!nulo) result = "";
            }
            else if (tipo == "date")
            {
                var temporal = ((DateTime)valor).ToString("dd/MM/yyyy");
                if (temporal == "01/01/0001")
                {
                    if (!nulo) result = "";
                }
                else
                {
                    result = (DateTime)valor;
                }
            }
            if (tipo == "int")
            {
                var Temporal = (int)valor;
                if (Temporal != 0) result = Temporal;
            }
            return result;
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            var values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static string Left(string str, int length)
        {
            return str.Substring(0, Math.Min(str.Length, length));
        }

        public static string Right(string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }

        //************************************************************************************************

        public static Boolean ToEnviarCorreo_Outlook(string strCorreo, string strAsunto, string strCuerpo)
        {
            var resultado = false;

            try
            {
                var oApp = new Microsoft.Office.Interop.Outlook.Application();

                Microsoft.Office.Interop.Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
                //var mAPIFolder = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

                System.Threading.Thread.Sleep(1000);

                var mailItem = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                mailItem.Subject = strAsunto;
                mailItem.HTMLBody = strCuerpo;
                mailItem.To = strCorreo;
                mailItem.Send();
                resultado = true;

            }
            catch (System.Exception ex)
            {
                resultado = false;
            }

            //try
            //{
            //    var oApp = new Microsoft.Office.Interop.Outlook.Application();
            //    MailItem oMsg = default(MailItem);
            //    oMsg = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
            //    oMsg.Subject = strAsunto;
            //    oMsg.HTMLBody = strCuerpo;
            //    oMsg.To = strCorreo.Trim();

            //    oMsg.Send();
            //    oApp = null;
            //    oMsg = null;
            //    resultado = true;
            //    //*****************************************************************************
            //}
            //catch (Exception ex)
            //{
            //    resultado = false;

            //}

            return resultado;
        }

        //************************************************************************************************

        public static Respuesta toSendEmail(string strCorreo, string strAsunto, string strCuerpo, string path)
        {
            var resultado = new Respuesta();
            try
            {
                var oApp = new Microsoft.Office.Interop.Outlook.Application();
                MailItem oMsg = default(MailItem);
                oMsg = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
                oMsg.Subject = strAsunto;
                oMsg.HTMLBody = strCuerpo;
                oMsg.To = strCorreo.Trim();
                if (!string.IsNullOrEmpty(path)) oMsg.Attachments.Add(path);
                //oMsg.CC = Trim(StrCorreoOp)

                oMsg.Send();
                oApp = null;
                oMsg = null;
                resultado.IsSuccess = true;
                resultado.Message = "Se ha enviado correctamente el correo.";
                //*****************************************************************************
            }
            catch (System.Exception ex)
            {
                resultado.IsSuccess = false;
                resultado.Message = ex.Message;
            }
            return resultado;
        }

        //**********************************

        public static Boolean ToEnviarCorreo_SMTP(string strCorreo, string strAsunto, string strCuerpo)
        {
            Boolean resultado = false;
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(strCorreo));
            email.From = new MailAddress("SGO@prodelsur.com");
            email.Subject = strAsunto;
            email.Body = strCuerpo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "10.125.0.15";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential("email_from@example.com", "contraseña");

            string output = null;
            try
            {
                smtp.Send(email);
                email.Dispose();
                resultado = true;
            }
            catch (System.Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }

        //************************************************************************************************

        #region EXCEL

        public static string NumeroALetras(string moneda, string num)
        {
            string dec = "";
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            var entero = Convert.ToInt64(Math.Truncate(nro));
            var decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            var decimale = decimales.ToString();
            decimale = decimale.Length > 1 ? decimale : string.Format("0{0}", decimale);
            string mon = "";

            switch (moneda)
            {
                case "S":
                    mon = "SOLES";
                    break;
                case "D":
                    mon = "DOLARES AMERICANOS";
                    break;
            }
            dec = $" CON {decimale}/100 {mon}";

            var res = "SON ";
            if (entero > 0)
            {
                res += toText(Convert.ToDouble(entero)) + dec;
            }
            else
            {
                res = string.Empty;
            }
            return res;
        }

        //********************************************************************************************

        private static string toText(double value)
        {
            string num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UNO";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) num2Text = "TREINTA";
            else if (value == 40) num2Text = "CUARENTA";
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            else if (value < 100) num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) num2Text = "CIEN";
            else if (value < 200) num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) num2Text = "QUINIENTOS";
            else if (value == 700) num2Text = "SETECIENTOS";
            else if (value == 900) num2Text = "NOVECIENTOS";
            else if (value < 1000) num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) num2Text = "MIL";
            else if (value < 2000) num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) num2Text = num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) num2Text = "UN MILLON";
            else if (value < 2000000) num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) num2Text = num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) num2Text = "UN BILLON";
            else if (value < 2000000000000) num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) num2Text = num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return num2Text;
        }

        //********************************************************************************************

        public static object DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = (Convert.ToString(row[col]));
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(list);
        }

        #endregion

        /// <summary>
        /// Agregado por: Slater Abanto
        /// Fecha: 12/09/2016
        /// </summary>
        public const decimal IGV = 0.18m;
        public const decimal ISC = 0.10m;
        public const decimal Detraccion = 0.10m;
        public enum TipoDoc
        {
            Factura = 01,
            BoleteVenta = 03,
            NotaCredito = 07,
            NotaDebito = 08,
            ComprobanteRetencion = 20
        }
        public static string EncodeTo64(string contents)
        {
            byte[] toEncodeAsBytes = System.Text.Encoding.ASCII.GetBytes(contents);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        public static string DecodeFrom64(string contents)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(contents);
            string returnValue = System.Text.Encoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static bool ValidateIdentificationDocumentPeru(string identificationDocument)
        {
            if (!string.IsNullOrEmpty(identificationDocument))
            {
                int addition = 0;
                int[] hashUbi = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int[] hash = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                int identificationDocumentLength = identificationDocument.Length;

                string identificationComponent;
                if (identificationDocumentLength == 11)
                {
                    identificationComponent = identificationDocument.Substring(0, identificationDocumentLength - 1);
                }
                else if (identificationDocumentLength == 8)
                {
                    identificationComponent = identificationDocument.Substring(0, identificationDocumentLength);
                }
                else
                {
                    throw new NotImplementedException();
                }
                int identificationComponentLength = identificationComponent.Length;

                int diff = hash.Length - identificationComponentLength;

                for (int i = identificationComponentLength - 1; i >= 0; i--)
                {
                    addition += (identificationComponent[i] - '0') * hash[i + diff];
                }

                addition = 11 - (addition % 11);

                if (addition == 11)
                {
                    addition = 0;
                }
                var identificationDocChar = identificationDocument[identificationDocumentLength - 1];
                char last = char.ToUpperInvariant(identificationDocChar);
                if (identificationDocumentLength == 11)
                {
                    // The identification document corresponds to a RUC.
                    return addition.Equals(last - '0');
                }
                else if (char.IsDigit(last))
                {
                    // The identification document corresponds to a DNI with a number as verification digit.
                    char[] hashNumbers = { '6', '7', '8', '9', '0', '1', '1', '2', '3', '4', '5' };
                    var ubiContains = hashUbi.Contains(Convert.ToChar(addition));
                    var hashContition = hashNumbers[addition];
                    var hashContains = hashNumbers.Contains(hashContition);
                    // return last.Equals(hashContition)
                    return ubiContains.Equals(hashContains);
                }
                else if (char.IsLetter(last))
                {
                    // The identification document corresponds to a DNI with a letter as verification digit.
                    char[] hashLetters = { 'K', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
                    var ubiContains = hashUbi.Contains(Convert.ToChar(addition));
                    var hashContition = hashLetters[addition];
                    var hashContains = hashLetters.Contains(hashContition);
                    // return last.Equals(hashLetters[addition])
                    return ubiContains.Equals(hashContains);
                }
            }

            return false;
        }
        public static bool ValidaDNI(string identificationDocument)
        {
            bool flag = false;

            if (!string.IsNullOrEmpty(identificationDocument))
            {
                int addition = 0;
                int[] hashUbi = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int[] hash = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                int identificationDocumentLength = identificationDocument.Length;
                string identificationComponent = identificationDocument.Substring(0, identificationDocumentLength);
                int identificationComponentLength = identificationComponent.Length;
                int diff = hash.Length - identificationComponentLength;
                for (int i = identificationComponentLength - 1; i >= 0; i += -1)
                    addition += System.Convert.ToInt32(identificationComponent[i].ToString()) * hash[i + diff];

                addition = 11 - (addition % 11);
                if (addition == 11)
                    addition = 0;
                var identificationDocChar = identificationDocument[identificationDocumentLength - 1];
                char last = char.ToUpperInvariant(identificationDocChar);
                if (identificationDocumentLength == 11)
                    flag = addition.Equals(System.Convert.ToInt32(last.ToString()));
                else if (char.IsDigit(last))
                {
                    char[] hashNumbers =  { '6', '7', '8', '9', '0', '1', '1', '2', '3', '4', '5' };                    
                    var ubiContains = hashUbi.Contains(Convert.ToChar(addition));
                    var hashContition = hashNumbers[addition];
                    var hashContains = hashNumbers.Contains(hashContition);
                    // flag = last.Equals(hashContition)
                    return ubiContains.Equals(hashContains);
                }
                else if (char.IsLetter(last))
                {
                    var vacio = "";
                    char[] hashLetters = { 'K', 'A', 'B', Convert.ToChar(vacio), 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
                    var ubiContains = hashUbi.Contains(Convert.ToChar(addition));
                    var hashContition = hashLetters[addition];
                    var hashContains = hashLetters.Contains(hashContition);
                    //flag = last.Equals(hashLetters[addition])
                    flag = ubiContains.Equals(hashContains);
                    
                }
            }

            return flag;
        }

    } //FIN PUBLIC CLASS

    public static class FileVersion
    {
        public static string Css(HttpContext context, string filename)
        {
            string version = GetFileVersion(context, filename);
            return filename + version;
        }
        public static string JavaScript(HttpContext context, string filename)
        {
            string version = GetFileVersion(context, filename);
            return filename + version;
        }

        public static string VersionedContent(HttpContext context, string filename)
        {
            string version = GetFileVersion(context, filename);
            return filename + version;
        }

        private static string GetFileVersion(HttpContext context, string filename)
        {
            if (context.Cache[filename] == null)
            {
                string filePhysicalPath = context.Server.MapPath(filename);
                var sha256 = ComputeHash(filePhysicalPath);
                //string version = $"?v={GetFileLastModifiedDateTime(context, filePhysicalPath, "yyyyMMddhhmmss")}";
                string version = $"?v={sha256}";

                return version;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetFileLastModifiedDateTime(HttpContext context, string filePath, string dateFormat)
        {
            return new System.IO.FileInfo(filePath).LastWriteTime.ToString(dateFormat);
        }
        static string ComputeHash(string filename)
        {
            using (var hA256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = hA256.ComputeHash(stream);
                    return Convert.ToBase64String(hash);
                }
            }
        }
    }

    public class EnumDescriptionAndValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public List<EnumDescriptionAndValue> Childs { get; set; }

        public EnumDescriptionAndValue()
        {
            Childs = new List<EnumDescriptionAndValue>();
        }
    }
} // FIN NAMESPACE
