using BaseDeDatosSQLite.ClasesDB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BaseDeDatosSQLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            verClientes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente objCliente = new Cliente()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
            };
            
            bool reapuesta = ClienteLogic.Instancia.Guardar(objCliente);
            if (reapuesta)
            {
                limpiar();
                verClientes();
            }

        }

        //Metodo reutilizable para visualizar clientes en DGV
        public void verClientes()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = ClienteLogic.Instancia.ListarClientes();
        }


        //Metodo limpiar textboxs
        public void limpiar()
        {
            txtNCliente.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "" ;
            txtEmail.Text = "";

            txtNombre.Focus(); //centrar en txtnombre
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Cliente objCliente = new Cliente()
            {
                NCliente = int.Parse(txtNCliente.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
            };

            bool reapuesta = ClienteLogic.Instancia.Editar(objCliente);
            if (reapuesta)
            {
                limpiar();
                verClientes();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Cliente objCliente = new Cliente()
            {
                NCliente = int.Parse(txtNCliente.Text),
                
            };

            bool reapuesta = ClienteLogic.Instancia.Eliminar(objCliente);
            if (reapuesta)
            {
                limpiar();
                verClientes();
            }
        }
    }
}