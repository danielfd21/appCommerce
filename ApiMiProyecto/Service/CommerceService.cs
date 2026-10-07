using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiMiProyecto.Service
{
    public class CommerceService
    {
        private readonly IConfiguration _configuration;

        public CommerceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ProcesarArchivo(IFormFile archivo)
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")!;

            using SqlConnection connection =
                new SqlConnection(connectionString);

            await connection.OpenAsync();

 
            DataTable tabla = new DataTable();

            tabla.Columns.Add("pc_processdate", typeof(DateTime));
            tabla.Columns.Add("pc_nomcomred", typeof(string));
            tabla.Columns.Add("pc_numdoc", typeof(string));
            tabla.Columns.Add("telefono", typeof(string));
            tabla.Columns.Add("correo", typeof(string));

            using StreamReader reader =
                new StreamReader(archivo.OpenReadStream());

        
            string? encabezado = await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                string? linea = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                string[] columnas = linea.Split(',');

                DataRow fila = tabla.NewRow();

                fila["pc_processdate"] =
                    DateTime.Parse(columnas[0]);

                fila["pc_nomcomred"] =
                    columnas[1];

                fila["pc_numdoc"] =
                    columnas[2];

                fila["telefono"] =
                    columnas[3];

                fila["correo"] =
                    columnas[4];

                tabla.Rows.Add(fila);
            }

       
            using SqlCommand command =
                new SqlCommand("sp_create_commerce", connection);

            command.CommandType =
                CommandType.StoredProcedure;

 
            SqlParameter parametro =
                command.Parameters.AddWithValue("@Commerce", tabla);

            parametro.SqlDbType =
                SqlDbType.Structured;

            parametro.TypeName =
                "dbo.CommerceType";

      
            await command.ExecuteNonQueryAsync();
        }

        public async Task<int> ProcesarFecha(DateOnly fecha)
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")!;

            using SqlConnection connection =
                new SqlConnection(connectionString);

            await connection.OpenAsync();

            using SqlCommand command =
                new SqlCommand("sp_process_commerce", connection);

            command.CommandType =
                System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@pc_processdate",
                fecha.ToDateTime(TimeOnly.MinValue)
            );

            object? resultado =
                await command.ExecuteScalarAsync();

            return Convert.ToInt32(resultado);
        }
    }
}
