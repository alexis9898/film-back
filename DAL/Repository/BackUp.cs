using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BackUp
    {
        public async Task BackUpDatabase(string connectionString, string filePath)
        {
            using (var sqlConnection = new SqlConnection(connectionString)) {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "select * from sys.tables";
                    using (var readerTables = await command.ExecuteReaderAsync())
                    {
                        while (await readerTables.NextResultAsync())
                        {
                            command.CommandText = "select * from " + readerTables[0];
                            var dbReader = await command.ExecuteReaderAsync();
                            while (await dbReader.NextResultAsync())
                            {   
                                for(int i = 0; i < dbReader.FieldCount; i++)
                                {
                                    var value = dbReader[i];
                                    var header = dbReader.GetName(i);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
