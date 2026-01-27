using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace MicroApi.Helper
{
    public static class ConnectionString
    {
        // 🔽 Method to Read Connection String from XML
        public static string ReadConnectionString()
        {
            try
            {
                // Register Encoding Provider
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string filePath;

                // Check if the application is running in Development mode
                if (IsDevelopment())
                {
                    string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                    filePath = Path.Combine(solutionPath, "dbconnection.xml");
                }
                else
                {
                    string exeDirectory = AppContext.BaseDirectory;
                    filePath = Path.Combine(exeDirectory, "dbconnection.xml");
                }

                // Check if file exists
                if (!File.Exists(filePath))
                {
                    string errorMessage = $"[{DateTime.Now}] ERROR: Could not find file '{filePath}'.";
                    LogError(errorMessage);
                    throw new Exception(errorMessage);
                }

                XmlDocument objDoc = new XmlDocument();
                objDoc.Load(filePath);

                XmlNodeList objNodeList = objDoc.SelectNodes("settings");
                foreach (XmlNode objNode in objNodeList)
                {
                    string server = objNode.ChildNodes.Item(0)?.InnerText;
                    string database = objNode.ChildNodes.Item(1)?.InnerText;
                    string user = DecryptString(objNode.ChildNodes.Item(2)?.InnerText);
                    string password = DecryptString(objNode.ChildNodes.Item(3)?.InnerText);

                    return $"Data Source={server};Initial Catalog={database};User ID={user};Password={password};Encrypt=True;TrustServerCertificate=True;";
                }

                throw new Exception("No valid database settings found in XML.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading connection string: {ex.Message}");
                return string.Empty;
            }
        }

        // 🔽 Helper method to determine Development mode
        static bool IsDevelopment()
        {
            string? environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                                          Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase);
        }

        // 🔽 Log Errors
        static void LogError(string message)
        {
            File.AppendAllText("error.log", message + Environment.NewLine);
        }

        static string DecryptString(string Value)
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
        static SymmetricAlgorithm SetEnc()
        {
            return new TripleDESCryptoServiceProvider();
        }
        static string SetLengthString(string str, int length)
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
    }
}