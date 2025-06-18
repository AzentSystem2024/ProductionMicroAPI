using Microsoft.AspNetCore.Hosting.Server;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace MicroApi.Helper
{
    public static class ADO
    {
        private static string _connectionString;
        public static void Initialize(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            //_connectionString = ReadConnectionString();
        }
            public static SqlConnection GetConnection()
            {

                try
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        throw new InvalidOperationException("Database connection string is not initialized.");
                    }

                    SqlConnection objCon = new SqlConnection(_connectionString);
                    objCon.Open();
                    return objCon;
                }
                catch (Exception ex)
                {
                    throw new Exception("Database connection failed: " + ex.Message, ex);
                }
            }

        //private static string? ReadConnectionString()
        //{
        //    try
        //    {
        //        // Register Encoding Provider
        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //        string filePath;

        //        // Check if the application is running in Development mode
        //        if (IsDevelopment())
        //        {
        //            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        //            filePath = Path.Combine(solutionPath, "dbconnection.xml");
        //        }
        //        else
        //        {
        //            string exeDirectory = AppContext.BaseDirectory;
        //            filePath = Path.Combine(exeDirectory, "dbconnection.xml");
        //        }

        //        // Check if file exists

        //        if (!File.Exists(filePath))
        //        {
        //            string errorMessage = $"[{DateTime.Now}] ERROR: Could not find file '{filePath}'.";
        //            LogError(errorMessage);

        //        }

        //        XmlDocument objDoc = new XmlDocument();
        //        objDoc.Load(filePath);

        //        XmlNodeList objNodeList = objDoc.SelectNodes("settings");
        //        foreach (XmlNode objNode in objNodeList)
        //        {
        //            string server = objNode.ChildNodes.Item(0)?.InnerText;
        //            string database = objNode.ChildNodes.Item(1)?.InnerText;
        //            string user = DecryptString(objNode.ChildNodes.Item(2)?.InnerText);
        //            string password = DecryptString(objNode.ChildNodes.Item(3)?.InnerText);

        //            return $"Data Source={server};Initial Catalog={database};User ID={user};Password={password};";
        //        }

        //        throw new Exception("No valid database settings found in XML.");
        //    }
        //    catch(Exception ex)
        //    {
        //        return ex.Message;
        //    }
            
        //}
        // Method to log errors to a file
        private static void LogError(string message)
        {
            string logFilePath = Path.Combine(AppContext.BaseDirectory, "error.log");

            try
            {
                File.AppendAllText(logFilePath, message + Environment.NewLine);
            }
            catch
            {
                // If logging fails, we ignore it to prevent further crashes
            }
        }

        private static bool IsDevelopment()
        {
            string? environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                                  Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase);
        }


        public static DataTable GetDataTable(string strSQL, string TableName = "TBL")
        {
            DataTable tbl = new DataTable();
            tbl.TableName = TableName;
            SqlConnection objCon = new SqlConnection();
            try
            {


                objCon = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(tbl);

                tbl.TableName = TableName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return tbl;
        }
        public static DataTable GetDataTable(string strSQL, SqlConnection objCon, SqlTransaction objTrans, string TableName = "TBL")
        {
            DataTable tbl = new DataTable();
            tbl.TableName = TableName;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.Transaction = objTrans;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(tbl);

                tbl.TableName = TableName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return tbl;
        }
        
        private static string DecryptString(string Value)
        {

            SymmetricAlgorithm mCSP;
            mCSP = SetEnc();
            string iv = "PenS8UCVF7s=";
            mCSP.IV = Convert.FromBase64String(iv);
            string key = SetLengthString("12345678", 32);
            mCSP.Key = Convert.FromBase64String(key);
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            Byte[] byt = new byte[64];
            try
            {
                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

                byt = Convert.FromBase64String(Value);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                //throw (new Exception("An error occurred while decrypting string"));
            }
            return Value;
        }

        //private string SetLengthString(object p, int v)
        //{
        //    throw new NotImplementedException();
        //}

        private static SymmetricAlgorithm SetEnc()
        {
            return new TripleDESCryptoServiceProvider();
        }
        private static string SetLengthString(string str, int length)
        {
            while (length > str.Length)
            {
                str += str;
            }
            if (str.Length > length)
            {
                str = str.Remove(length);
            }
            return str;
        }
        public static string SQLString(object vInput)
        {
            try
            {

                return "N'" + vInput.ToString().Replace("'", "''") + "'";
            }
            catch (Exception ex)
            {
                return "''";
            }
        }
        public static string SQLDate(string vInput)
        {
            try
            {
                if (vInput != "")
                {
                    DateTime dt = DateTime.ParseExact(vInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return "'" + dt.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    return "NULL";
                }
            }
            catch
            { return "NULL"; }

        }

        public static bool ExecuteNonQuery(string strSQL, SqlConnection objCon, SqlTransaction objTrans)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.Transaction = objTrans;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static bool ExecuteNonQuery(string strSQL)
        {
            SqlConnection objCon = new SqlConnection();
            try
            {
                objCon = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

        }
        public static bool ExecuteNonQueryIgnoreException(string strSQL)
        {
            SqlConnection objCon = new SqlConnection();
            try
            {
                objCon = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return false;

        }
        public static bool LogError(Exception ex, string vModule, string vActivity)
        {
            try
            {
                string strSQL = "INSERT INTO TB_ERROR_LOG (MODULE, ACTIVITY, ERROR_MESSAGE, ERROR_TIME)" +
                                "VALUES (" + SQLString(vModule) + "," + SQLString(vActivity) + "," +
                                SQLString(ex.Message) + ", GETDATE())";

                ExecuteNonQuery(strSQL);

            }
            catch (Exception x)
            {
            }
            return true;

        }
        public static Int32 ToInt32(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToInt32(value);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string ToString(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToString(value);
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static decimal ToDecimal(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToDecimal(value);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static bool Toboolean(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToBoolean(value);
                else
                    return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        public static float ToFloat(object vInput)
        {
            try
            {
                if (vInput != null)
                    return Convert.ToSingle(vInput);
                else
                    return 0.0f;
            }
            catch (Exception ex)
            {
                return 0.0f;
            }
        }
        public static string TODateString(object vInput)
        {
            try
            {
                if (vInput != null)
                    return Convert.ToDateTime(vInput).ToString("yyyy-MM-dd");
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static String ExecuteScalar(string strSQL)
        {
            try
            {
                SqlConnection objCon = GetConnection();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = objCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    object value = cmd.ExecuteScalar();

                    try
                    {
                        return Convert.ToString(value);
                    }
                    catch (Exception ex)
                    {
                        return "";
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    objCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object ExecuteScalar(string strSQL, SqlConnection objCon, SqlTransaction objTrans)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.Transaction = objTrans;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
