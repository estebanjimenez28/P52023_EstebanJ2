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
    }
}
