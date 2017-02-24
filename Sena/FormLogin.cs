using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Sena
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        Crypto criptografia = new Crypto();
        Cadastro cadastro = new Cadastro();

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string hash = criptografia.newHash(textBoxsenha);

            string selectUser = @"SELECT ISADMIN FROM USUARIO WHERE USUARIO ='" + textBoxlogin.Text + "' AND SENHA ='" + hash + "';";
            string preLogin = cadastro.returnString(selectUser, "ISADMIN");

            if(preLogin =="")
            {
                clean();
                MessageBox.Show("Usuario não existente", "Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(preLogin == "False")
            {
                clean();
                this.DialogResult = DialogResult.OK;
            }
            else if(preLogin=="True")
            {
                clean();
                FormCadUser cadUser = new FormCadUser();
                cadUser.Show();
            }
        }

        private void clean()
        {
            textBoxlogin.Clear();
            textBoxsenha.Clear();
        }
    }
}
