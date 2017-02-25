using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Sena
{
    public partial class FormCadSorteio : Form
    {
        Cadastro cadastro = new Cadastro();
        public FormCadSorteio()
        {
            InitializeComponent();
            horaAtual(maskedTextBox1);

            MessageBox.Show("Realize o cadastramento na ordem de saíde de bolas no sorteio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            if(verificaApto() == true)
            {
                string returnUltimoDado = cadastro.returnString(@"SELECT COUNT(SORTEIO) AS 'SORTEIO' FROM MEGASENA;", "SORTEIO");

                DateTime data = DateTime.Parse(maskedTextBox1.Text, new CultureInfo("en-GB"));
                int soma = Convert.ToInt32(returnUltimoDado) + 1;

                cadastroMegasena(soma, data.ToString());
                cadastroMegasena2(soma, data.ToString());

                clean();
            }
        }

        private void cadastroMegasena(int soma, string data)
        {
            /*Para realizar este cadastro é realizado um ordenamento de bolas do menor para o maior
             * */

            ordenarBolas();

            string insertCadastro = @"INSERT INTO MEGASENA VALUES(" + soma + ", '" + data + "', " + Convert.ToInt32(textBox1.Text) +
                    ", " + Convert.ToInt32(textBox2.Text) + ", " + Convert.ToInt32(textBox3.Text) + ", " + Convert.ToInt32(textBox4.Text) +
                    ", " + Convert.ToInt32(textBox5.Text) + ", " + Convert.ToInt32(textBox6.Text) + ");";

            cadastro.cadastro(insertCadastro);
        }

        private void cadastroMegasena2(int soma, string data)
        {
            string insertCadastro = @"INSERT INTO MEGASENA2 VALUES(" + soma + ", '" + data + "', " + Convert.ToInt32(textBox1.Text) +
                    ", " + Convert.ToInt32(textBox2.Text) + ", " + Convert.ToInt32(textBox3.Text) + ", " + Convert.ToInt32(textBox4.Text) +
                    ", " + Convert.ToInt32(textBox5.Text) + ", " + Convert.ToInt32(textBox6.Text) + ");";

            cadastro.cadastro(insertCadastro);
        }

        private void ordenarBolas()
        {
            List<int> bolas = new List<int>();

            bolas.Add(Convert.ToInt16(textBox1.Text));
            bolas.Add(Convert.ToInt16(textBox2.Text));
            bolas.Add(Convert.ToInt16(textBox3.Text));
            bolas.Add(Convert.ToInt16(textBox4.Text));
            bolas.Add(Convert.ToInt16(textBox5.Text));
            bolas.Add(Convert.ToInt16(textBox6.Text));

            int[] bolasOrdem = { 0, 60, 60, 60, 60, 60, 60 };

            for (int i = 1; i < 7; i++)
            {
                int j = 0;

                while(j < 6)
                {
                    int a = bolasOrdem[i];
                    int b = bolasOrdem[i - 1];
                    int c = bolas[j];

                    if (bolasOrdem[i] > bolas[j] && bolasOrdem[i -1] < bolas[j])
                    {
                        bolasOrdem[i] = bolas[j];
                    }
                    j++;
                }
            }

            textBox1.Text = bolasOrdem[1].ToString();
            textBox2.Text = bolasOrdem[2].ToString();
            textBox3.Text = bolasOrdem[3].ToString();
            textBox4.Text = bolasOrdem[4].ToString();
            textBox5.Text = bolasOrdem[5].ToString();
            textBox6.Text = bolasOrdem[6].ToString();
        }

        private void horaAtual(MaskedTextBox mask)
        {
            mask.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private bool verificaApto()
        {
            /*Verifica aptidão do cadastro
             * Todas as bolas dentro dos valores esperados
             * Todas as bolas com valores diferentes entre si
             * */

            bool verificaGeral = false;

            bool verificaBall1 = verificaApoio(textBox1);
            bool verificaBall2 = verificaApoio(textBox2);
            bool verificaBall3 = verificaApoio(textBox3);
            bool verificaBall4 = verificaApoio(textBox4);
            bool verificaBall5 = verificaApoio(textBox5);
            bool verificaBall6 = verificaApoio(textBox6);

            bool verificaIgual = verificaNIgual();

            if(verificaBall1 == true && verificaBall2==true && verificaBall3 == true && verificaBall4==true && verificaBall5==true &&
                verificaBall6==true && verificaIgual==true)
            {
                verificaGeral = true;
            } 

            return verificaGeral;
        }

        private bool verificaApoio(TextBox text)
        {
            //Verifica se as bolas estão dentro os valores esprados

            bool retorno = false;

            if (text.Text != "" && Convert.ToInt32(text.Text) > 0 && Convert.ToInt32(text.Text) < 61)
            {
                retorno = true;
            }

            return retorno;
        }

        private bool verificaNIgual()
        {
            //Verifica se todas as bolas são diferentes

            bool igual = true;

            List<int> lista = new List<int>();

            lista.Add(Convert.ToInt16(textBox1.Text));
            lista.Add(Convert.ToInt16(textBox2.Text));
            lista.Add(Convert.ToInt16(textBox3.Text));
            lista.Add(Convert.ToInt16(textBox4.Text));
            lista.Add(Convert.ToInt16(textBox5.Text));
            lista.Add(Convert.ToInt16(textBox6.Text));

            for(int i = 0; i < lista.Count; i++)
            {
                for(int j= i + 1; j < 6; j++)
                {
                    if(lista[i]==lista[j])
                    {
                        igual = false;
                    }
                }
            }

            return igual;
        }

        private void clean()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
    }
}
