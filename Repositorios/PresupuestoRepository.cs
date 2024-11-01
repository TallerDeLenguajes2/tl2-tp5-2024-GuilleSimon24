using Microsoft.Data.Sqlite;
class PresupuestoRepository
{
    string cadenaConexion = "Data Source=db/Tiendo.db";
    public void CrearPresupuesto(Presupuestos presupuesto)
    {
        string query1 = @"INSERT INTO Presupuestos (idPresupuesto, NombreDestinatario, FechaCreacion) VALUES (@idPre, @nombrePre, @fechaPre)";
        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query1, sqlitecon);
            DateTime fecha = DateTime.Today;
            sqlitecon.Open();
            
            command.Parameters.Add(new SqliteParameter("@idPre", presupuesto.IdPresupuesto));
            command.Parameters.Add(new SqliteParameter("@nombrePre", presupuesto.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@fechaPre", fecha));

            command.ExecuteNonQuery();

            sqlitecon.Close();
            
        }

        foreach (var item in presupuesto.Detalle)
        {
            string query2 = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPre, @idProdu, @canti)";
            using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(query2, sqlitecon);
                Productos prod1 = item.Producto;
                sqlitecon.Open();

                command.Parameters.Add(new SqliteParameter("@idPre", presupuesto.IdPresupuesto));
                command.Parameters.Add(new SqliteParameter("@idProdu", prod1.IdProducto));
                command.Parameters.Add(new SqliteParameter("@canti", item.Cantidad));

                command.ExecuteNonQuery();

                sqlitecon.Close();
            }
        }
    }

    public List<Presupuestos> GetPresupuestos()
    {
        string queryDetalle = @"SELECT idPresupuesto, NombreDestinatario, FROM Presupuestos";
        List<Presupuestos> presupuestos = new List<Presupuestos>();
        using(SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(queryDetalle, sqlitecon);
            sqlitecon.Open();


            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idpres = Convert.ToInt32(reader["idPresupuesto"]);
                    string nombre = Convert.ToString(reader["NombreDestinatario"]);
                    DateTime fecha = Convert.ToDateTime(reader["FechaCreacion"]);
                    Presupuestos presu1 = new Presupuestos(idpres, nombre, new List<PresupuestoDetalle>());
                    presupuestos.Add(presu1);
                }
            }
            sqlitecon.Close();
        }
        return presupuestos;
    }

    public Productos getDetalle(int id)
    {
        string query = @"SELECT pr.idProd, prod.Descripcion, prod.Precio FROM PresupuestosDetaller as pr INNER JOIN Productos as prod WHERE idPresupuesto = @idquery";
        Productos buscado = new Productos();
        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query, sqlitecon);
            sqlitecon.Open();
            
            command.Parameters.Add(new SqliteParameter("@idquery", id));
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idProd = Convert.ToInt32(reader["idProd"]);
                    string descrip = Convert.ToString(reader["Descripcion"]);
                    int precio = Convert.ToInt32(reader["Precio"]);
                    
                    buscado.IdProducto = idProd;
                    buscado.Descripcion = descrip;
                    buscado.Precio = precio;
                    
                }
            }
            sqlitecon.Close();
        }

        return buscado;
    }


    public void DeletePresupuesto(int id)
    {
        string query1 = @"DELETE FROM Presupuestos WHERE idPresupuesto = @idQuery";
        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query1, sqlitecon);
            sqlitecon.Open();

            command.Parameters.Add(new SqliteParameter("@idquery", id));

            command.ExecuteNonQuery();

            sqlitecon.Close();
        }

        string query2 = @"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @idquery";
        
        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query2, sqlitecon);
            sqlitecon.Open();

            command.Parameters.Add(new SqliteParameter("@idquery", id));

            command.ExecuteNonQuery();

            sqlitecon.Close();
        }
    }
}