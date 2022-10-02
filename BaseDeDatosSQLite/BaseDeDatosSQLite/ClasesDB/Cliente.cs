using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;

namespace BaseDeDatosSQLite.ClasesDB
{
    public class Cliente
    {
        //Representacion de la Tabla creada en SQLite 
        
        public int NCliente { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

    }
    public class ClienteLogic
    {
        //Cadena de conexion 
        
        private static string cadenaDeConexion = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString;

        private static ClienteLogic _instancia = null;

        public ClienteLogic() { 
        
        }

        //patron de diseño singleton.
        //asegurara de que la clase ClienteLogic tenga una única instancia
        public static ClienteLogic Instancia
        {
            get {
                if( _instancia == null) { 
                _instancia = new ClienteLogic();
                }
            return _instancia;
            }
            
        }

        public bool Guardar(Cliente obj) 
        {
            bool respuesta = true;

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaDeConexion))
            {
                conexion.Open();
                string consulta = "insert into Cliente(Nombre,Apellido,Telefono,Email) values (@nombre,@apellido,@telefono,@email)";

                SQLiteCommand cmd = new SQLiteCommand(consulta, conexion);
                cmd.Parameters.Add(new SQLiteParameter("@nombre", obj.Nombre));
                cmd.Parameters.Add(new SQLiteParameter("@apellido", obj.Apellido));
                cmd.Parameters.Add(new SQLiteParameter("@telefono", obj.Telefono));
                cmd.Parameters.Add(new SQLiteParameter("@email", obj.Email));
                cmd.CommandType = System.Data.CommandType.Text;

                //si no hubo ninguna inserción, respuesta = false
                if (cmd.ExecuteNonQuery() < 1)
                {
                    respuesta=false;
                }
            }

                return respuesta;
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SQLiteConnection conexion = new SQLiteConnection(cadenaDeConexion))
            {
                conexion.Open();
                string consulta = "select * from Cliente";

                SQLiteCommand cmd = new SQLiteCommand(consulta, conexion);
                cmd.CommandType = System.Data.CommandType.Text;

                using (SQLiteDataReader dataR = cmd.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        lista.Add(new Cliente()
                        {
                            NCliente = int.Parse(dataR["N° Cliente"].ToString()),
                            Nombre = dataR["Nombre"].ToString(),
                            Apellido = dataR["Apellido"].ToString(),
                            Telefono = dataR["Telefono"].ToString(),
                            Email = dataR["Email"].ToString(),
                        }) ;
                    }


                }
            }

            return lista;
        }

    }

}
