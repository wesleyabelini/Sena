using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sena
{
    public partial class FormCadUser : Form
    {
        public FormCadUser()
        {
            InitializeComponent();
        }

        Crypto crypto = new Crypto();
        Cadastro cadastro = new Cadastro();

        private void buttonCadastro_Click(object sender, EventArgs e)
        {
            if(textBoxLogin.Text != "" && textBoxSenha.Text !="")
            {
                string verifUnique = @"SELECT USUARIO FROM USUARIO WHERE USUARIO ='" + textBoxLogin.Text + "';";

                if(cadastro.returnString(verifUnique, "USUARIO")=="")
                {
                    string hash = crypto.newHash(textBoxSenha);
                    bool admin = false;

                    if (checkBoxAdmin.Checked == true)
                    {
                        admin = true;
                    }

                    string id = cadastro.returnString("SELECT COUNT(USUARIO) AS 'CONT' FROM USUARIO;", "CONT");
                    id = (Convert.ToInt16(id) + 1).ToString();

                    string cadUser = @"INSERT INTO USUARIO VALUES(" + id + " , " + textBoxLogin.Text + " , " + hash + " , " + admin + ");";

                    cadastro.cadastro(cadUser);

                    clean();
                }
                else
                {
                    clean();
                    MessageBox.Show("Usuário já cadastrado no Sistema.", "Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void clean()
        {
            textBoxLogin.Clear();
            textBoxSenha.Clear();
        }
    }
}
