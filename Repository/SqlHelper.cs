using System.Diagnostics;
using System.Reflection;

namespace AssetManager
{
    public class SqlHelper<T>
    {
        public string GetSqlFromEmbeddedResource(string name)
        {
            try
            {
                using var resourceStream = typeof(T).Assembly.GetManifestResourceStream(typeof(T).Namespace + ".Sql." + name + ".sql");

                if (resourceStream == null)
                    throw new Exception("ERROR_Embedded resource not found.");

                using var reader = new StreamReader(resourceStream!);
                string SQLStatement = reader.ReadToEnd();
                return SQLStatement;
            } 
            catch
            {
                throw;
            }
        }
    }
}
