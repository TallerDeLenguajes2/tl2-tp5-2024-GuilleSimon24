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
            SqliteCommand command = new SqliteCommand(query, sqlitecon);
            sqlitecon.Open();
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = convert.Toint32(reader["idProducto"]);
                    string descrip = convert.Tostring(reader["Descripcion"]);
                    int precio = convert.Toint32(reader["Precio"]);
                    Productos prod1 = new Productos(id, descrip, precio);
                    prods.Add(prod1);   
                }
            }
            sqlitecon.Close();
        }
        return prods;
    }

    public Productos GetProductoID(int id)
    {
        string query = @"SELECT idProducto, Descripcion, Precio FROM Productos WHERE idProducto = @idQuery";
        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query, sqlitecon);
            sqlitecon.Open();

            command.Parameters.Add(new SQLiteParameter("@idQuery", id));
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Productos buscado = new Productos(convert.Toint32(reader[0]), convert.Tostring(reader[1]), convert.Toint32(reader[2]));
                }                
            }

            sqlitecon.Close();
        }

        return buscado;
    }

    public void DeleteProducto(int id)
    {
        string query = @"DELETE FROM Productos WHERE idProducto = @idquery";
        using (SqliteConnection sqlitecon = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(query, sqlitecon);
            sqlitecon.Open();
            
            command.Parameters.Add(new SQLiteParameter("@idQuery", id));

            command.ExecuteNonQuery();

            sqlitecon.Close();
        }
    }
}