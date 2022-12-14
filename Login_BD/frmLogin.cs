using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login_BD
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btn_Ingresar_Click(object sender, EventArgs e)
        {
            //validamos que haya ingresado un usuario
            if (txt_Usuario.Text == "")
            {
                MessageBox.Show("Se debe ingresar un usuario.");
                return;
            }
            // validamos que haya ingresado una contrasena
            if (txt_Password.Text == "")
            {
                MessageBox.Show("Se debe ingresar una contrasena");
                return ;
            }

            if (ValidarCredenciales(txt_Usuario.Text,txt_Password.Text))
            {
                MessageBox.Show("Usted ha ingresado al sistema");
                this.Close();
            }
            else 
            {
                MessageBox.Show("Debe ingresar usuario y password valido");
            }
        }

        //public bool ValidarCredenciales(String pUsuario, String pPassword)
        //{
        //    bool usuarioValido = false;
        //    SqlConnection conexion = new SqlConnection();
        //    conexion.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=BugTracker93584;Integrated Security=True";

        //    try
        //    {
        //        //Abrimos la conexion a la base de datos.
        //        conexion.Open();

        //        //Construimos la consulta sql para buscar el usuario en la base de datos.
        //        String consultaSql = string.Concat(" SELECT * ",
        //                                           "   FROM Usuarios ",
        //                                           "  WHERE usuario =  '", pUsuario, "'");

        //        //Creamos un objeto command para luego ejecutar la consulta sobre la base de datos
        //        SqlCommand command = new SqlCommand(consultaSql, conexion);

        //        // El metodo ExecuteReader retorna un objeto SqlDataReader con la respuesta de la base de datos. 
        //        // Con SqlDataReader los datos se leen fila por fila, cambiando de fila cada vez que se ejecuta el metodo Read()
        //        SqlDataReader reader = command.ExecuteReader();

        //        // El metodo Read() lee la primera fila disponible, si NO existe una fila retorna false (la consulta no devolvio resultados).
        //        if (reader.Read())
        //        {
        //            //En caso de que exista el usuario, validamos que password corresponda al usuario
        //            if (reader["password"].ToString() == pPassword)
        //            {
        //                usuarioValido = true;
        //            }
        //        }

        //    }
        //    catch (SqlException ex)
        //    {
        //        //Mostramos un mensaje de error indicando que hubo un error en la base de datos.
        //        MessageBox.Show(string.Concat("Error de base de datos: ", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        //Preguntamos si el estado de la conexion es abierto antes de cerrar la conexion.
        //        if (conexion.State == ConnectionState.Open)
        //        {
        //            //Cerramos la conexion
        //            conexion.Close();
        //        }
        //    }

        //    // Retornamos el valor de usuarioValido. 
        //    return usuarioValido;
        //}

        public bool ValidarCredenciales(string pUsuario, string pPassword)
        {
            //Inicializamos la variable usuarioValido en false, para que solo si el usuario es valido retorne true
            bool usuarioValido = false;

            //La doble barra o */ nos permite escribir comentarios sobre nuestro codigo sin afectar su funcionamiento.

            //La sentencia try...catch nos permite "atrapar" excepciones (Errores) y dar al usuario un mensaje más amigable.
            try
            {

                //Construimos la consulta sql para buscar el usuario en la base de datos.
                String consultaSql = string.Concat(" SELECT * ",
                                                   "   FROM Usuarios ",
                                                   "  WHERE usuario =  '", pUsuario, "'");

                //Usando el método GetDataManager obtenemos la instancia unica de DataManager (Patrón Singleton) y ejecutamos el método ConsultaSQL()
                DataTable resultado = DataManager.GetInstance().ConsultaSQL(consultaSql);

                //Validamos que el resultado tenga al menos una fila.
                if (resultado.Rows.Count >= 1)
                {
                    //En caso de que exista el usuario, validamos que password corresponda al usuario
                    if (resultado.Rows[0]["password"].ToString() == pPassword)
                    {
                        usuarioValido = true;
                    }
                }

            }
            catch (SqlException ex)
            {
                //Mostramos un mensaje de error indicando que hubo un error en la base de datos.
                MessageBox.Show(string.Concat("Error de base de datos: ", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Retornamos el valor de usuarioValido. 
            return usuarioValido;
        }
    }
}
