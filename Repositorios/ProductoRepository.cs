using Microsoft.Data.Sqlite;
class ProductoRepository
{
    string cadenaConexion = "Data Source=db/Tiendo.db";
    public void CrearProducto(Productos producto){
        string query = $"INSERT INTO Productos (Descripcion, Precio) VALUES (@descrip, @precio);";

        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query, sqlitecon);
            sqlitecon.Open();
            
            command.Parameters.Add(new SqliteParameter("@descrip", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));

            command.ExecuteNonQuery();

            sqlitecon.Close();
            
        }
    }

    public void ModificarProducto(int id, Productos producto)
    {
        string query = @"UPDATE Productos SET Descripcion = '@descrip', Precio = '@precio' 
        WHERE idPresupuesto = @idQuery;";

        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query, sqlitecon);
            sqlitecon.Open();
            
            command.Parameters.Add(new SQLiteParameter("@idQuery", id));
            command.Parameters.Add(new SqliteParameter("@descrip", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));

            command.ExecuteNonQuery();

            sqlitecon.Close();
        }
    }

    public List<Productos> getProductos()
    {
        List<Productos> prods = new List<Productos>();
        string query = @"SELECT P.idProducto, P.Descripcion, P.Precio FROM Productos P";

        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Productos prod1 = new Productos(reader[0], reader[1], reader[2]);
                    prods.Add(prod1);
                }
            }
        }
    }
}