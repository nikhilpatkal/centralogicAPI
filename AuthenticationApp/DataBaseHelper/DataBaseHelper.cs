using System.Data;
using Microsoft.Data.SqlClient;

namespace AuthenticationApp.Helpers
{
  public class DataBaseHelper
  {
    private readonly string _connectionString;
    public DataBaseHelper(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    //this is the calling to the store procedure so we need to give type procedure name an  parameter then this retun inlist Dictonary
    public List<T> ExecuteStoredProcedureDynamic<T>(string procedureName, SqlParameter[] parameters) where T : new()
    {
      var results = new List<T>();

      using (SqlConnection conn = new SqlConnection(_connectionString))//and this represent the connection between the sql server
      {
        using (SqlCommand cmd = new SqlCommand(procedureName, conn))//this represent the connection betwenn the database 
        {
          cmd.CommandType = CommandType.StoredProcedure; //give the command type bydefault support the raw query if we want to execute the store procedure then give that type like hear

          if (parameters != null) //this check that can this has parameter if yes then add it in the command parameter
          {
            cmd.Parameters.AddRange(parameters);
          }

          conn.Open(); //Establishes a live connection to the database.
          //After this line, SQL commands can be executed on this connection.

          using (SqlDataReader reader = cmd.ExecuteReader())//Executes the stored procedure and returns a forward-only stream of rows from the database.
          {
            var properties = typeof(T).GetProperties();

            while (reader.Read())//reader helps you loop through the result set row by row.
            {
              T item = new T();//T is a generic type parameter (e.g., User, Product, etc.)


              foreach (var property in properties)
              {
                // Check if the column exists
                bool hasColumn = false;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                  if (reader.GetName(i).Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                  {
                    hasColumn = true;
                    break;
                  }
                }

                // Map the property if the column exists and is not null
                if (hasColumn && !reader.IsDBNull(reader.GetOrdinal(property.Name)))
                {
                  property.SetValue(item, reader[property.Name]);
                }
              }

              results.Add(item);
            }
          }
        }
      }

      return results;
    }

  }
}
