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
    public partial class FrmMDI : Form
    {
        public FrmMDI()
        {
            InitializeComponent();
        }

        private void FrmMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }

        private void gestionDeUsuaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!Globales.MiFormGestionUsuarios.Visible) 
            {
                Globales.MiFormGestionUsuarios = new FrmUsuariosGestion();
                Globales.MiFormGestionUsuarios.Show();
            }

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void FrmMDI_Load(object sender, EventArgs e)
        {
            //Mostrar el usuario logueado

            string InfoUsuario = string.Format("{0}-{1}({1})",
                Globales.MiUsuarioGlobal.UsuarioNombre,
                Globales.MiUsuarioGlobal.UsuarioCorreo,
                Globales.MiUsuarioGlobal.MiRolTipo.UsuarioRolDescripcion);
        LblUsuario.Text = InfoUsuario;

            switch (Globales.MiUsuarioGlobal.MiRolTipo.UsuarioRolID)
            {
                case 1:
                    //seri admin, no se oculta nada
                    break;
                case 2:
                    //seria usuario normal, se deben ocultar algunas opciones de menu
                    gestionDeUsuaToolStripMenuItem.Visible= false;  
                    rolesDeUsuarioToolStripMenuItem.Visible= false; 
                    tiposDeProveedorToolStripMenuItem.Visible= false;   
                    tiposDeCompraToolStripMenuItem.Visible= false;
                    break;
            }


        }

        private void registroDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormRegistroCompra.Visible)
            {
                Globales.MiFormRegistroCompra = new FrmRegistroCompra();
                Globales.MiFormRegistroCompra.Show();
            }
        }
    }
}
