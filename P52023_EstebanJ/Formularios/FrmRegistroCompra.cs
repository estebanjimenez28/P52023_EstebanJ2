using Logica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P52023_EstebanJ.Formularios
{
    public partial class FrmRegistroCompra : Form
    {
        

        public Compra MiCompraLocal { get; set;}  
        
         public DataTable ListaProductos { get; set; }  

        public FrmRegistroCompra()
        {
            InitializeComponent();
            MiCompraLocal = new Compra();
            ListaProductos= new DataTable();    
        }

        private void FrmRegistroCompra_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;

        }

        private void BtnProveedorBucar_Click(object sender, EventArgs e)
        {
            //Se abre un nuevo formulario de busqueda de proveedor
            //este formulario no es necesario definirlo en los globales (aunque es posible)
        Form FormBusquedaProveedor = new FrmProveedorBuscar();  
            DialogResult  respuesta = FormBusquedaProveedor.ShowDialog();

            if(respuesta == DialogResult.OK) 
            {
              //usamos las composiciones a proveedor para extraer el valor del nombre
              //del proveedor
                TxtNombreProveedor.Text = MiCompraLocal.MiProveedor.ProveedorNombre;

            }


        }

        private void BtnProductoAgregar_Click(object sender, EventArgs e)
        {
            Form MiFormBusquedaItem = new FrmCompraAgregarProducto();

            DialogResult respuesta = MiFormBusquedaItem.ShowDialog();

            if(respuesta == DialogResult.OK )
            {
                DgvLista.DataSource = ListaProductos;
            }
        }
    }
}
