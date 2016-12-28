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
    public partial class Form1 : Form
    {
        Calc calc = new Calc();
        Cadastro cadastro = new Cadastro();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listaBolasGrid();
        }

        private void sorteioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadSorteio cadSorteio = new FormCadSorteio();
            cadSorteio.Show();
        }

        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            bool apto = false;

            toolStripProgressBar1.Value = 0;

            if (radioButtonTodos.Checked == true || radioButtonIntervalo.Checked == true)
            {
                if(textBoxSorteio1.Text !="" && Convert.ToInt32(textBoxSorteio1.Text) > 0 && textBoxSorteio2.Text !="" && 
                    Convert.ToInt32(textBoxSorteio2.Text) <= Convert.ToInt32(textBoxSorteio.Text))
                {
                    apto = true;
                }
            }
            else if(radioButtonUltimos.Checked==true)
            {
                if(textBox1.Text !="" && Convert.ToInt32(textBox1.Text) > 0 && 
                    Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(textBoxSorteio.Text))
                {
                    apto = true;
                }
            }

            if (apto==true)
            {
                for(int bola = 1; bola <=6; bola++)
                {
                    string cmdSelectTable = "";

                    int valorFinal = Convert.ToInt32(textBoxSorteio2.Text) - Convert.ToInt32(textBoxCalcular.Text);

                    if (radioButtonTodos.Checked==true || radioButtonIntervalo.Checked==true)
                    {
                        cmdSelectTable = @"SELECT BOLA" + bola.ToString() + ", COUNT(BOLA" + bola.ToString() + ") AS 'QNT', ((COUNT(BOLA" +
                        bola.ToString() + ")) * 100)/" + valorFinal.ToString() + ".0 AS '%' FROM MEGASENA WHERE SORTEIO BETWEEN " +
                        textBoxSorteio1.Text + " AND " + valorFinal.ToString() + " GROUP BY BOLA" + bola.ToString() + " ORDER BY 'QNT' DESC";
                    }
                    else if(radioButtonUltimos.Checked==true)
                    {
                        int bola1 = Convert.ToInt32(textBoxSorteio.Text) - Convert.ToInt32(textBox1.Text);

                        cmdSelectTable = @"SELECT BOLA" + bola.ToString() + ", COUNT(BOLA" + bola.ToString() + ") AS 'QNT', ((COUNT(BOLA" +
                        bola.ToString() + ")) * 100)/" + textBox1.Text + ".0 AS '%' FROM MEGASENA WHERE SORTEIO BETWEEN " +
                        bola1.ToString() + " AND " + valorFinal.ToString() + " GROUP BY BOLA" + bola.ToString() + " ORDER BY 'QNT' DESC";
                    }

                    string cmdSelect = @"SELECT BOLA" + bola.ToString() + " FROM MEGASENA WHERE SORTEIO BETWEEN " + 
                        Convert.ToInt32(textBoxSorteio1.Text) + " AND " + valorFinal.ToString();

                    int[] valores = cadastro.returArray(cmdSelect, "BOLA" + bola.ToString());

                    float mediaAritmetica = calc.mediaAritimetica(valores);
                    double variancia = calc.variancia(valores, mediaAritmetica);
                    double desvioPadrao = calc.desvioPadrao(variancia);
                    double coefVariacao = calc.coefVariacao(desvioPadrao, mediaAritmetica);

                    if (bola == 1)
                    {
                        TBmediaAritmetica1.Text = mediaAritmetica.ToString();
                        TBdesvioPadrao1.Text = desvioPadrao.ToString();
                        TBcoefVariacao1.Text = coefVariacao.ToString();

                        cadastro.listdatagrid(cmdSelectTable, dataGridViewBola1);
                    }
                    else if(bola ==2)
                    {
                        TBmediaAritmetica2.Text = mediaAritmetica.ToString();
                        TBdesvioPadrao2.Text = desvioPadrao.ToString();
                        TBcoefVariacao2.Text = coefVariacao.ToString();

                        cadastro.listdatagrid(cmdSelectTable, dataGridViewBola2);
                    }
                    else if(bola ==3)
                    {
                        TBmediaAritmetica3.Text = mediaAritmetica.ToString();
                        TBdesvioPadrao3.Text = desvioPadrao.ToString();
                        TBcoefVariacao3.Text = coefVariacao.ToString();

                        cadastro.listdatagrid(cmdSelectTable, dataGridViewBola3);
                    }
                    else if(bola ==4)
                    {
                        TBmediaAritmetica4.Text = mediaAritmetica.ToString();
                        TBdesvioPadrao4.Text = desvioPadrao.ToString();
                        TBcoefVariacao4.Text = coefVariacao.ToString();

                        cadastro.listdatagrid(cmdSelectTable, dataGridViewBola4);
                    }
                    else if(bola==5)
                    {
                        TBmediaAritmetica5.Text = mediaAritmetica.ToString();
                        TBdesvioPadrao5.Text = desvioPadrao.ToString();
                        TBcoefVariacao5.Text = coefVariacao.ToString();

                        cadastro.listdatagrid(cmdSelectTable, dataGridViewBola5);
                    }
                    else
                    {
                        TBmediaAritmetica6.Text = mediaAritmetica.ToString();
                        TBdesvioPadrao6.Text = desvioPadrao.ToString();
                        TBcoefVariacao6.Text = coefVariacao.ToString();

                        cadastro.listdatagrid(cmdSelectTable, dataGridViewBola6);
                    }
                }

                if(textBoxCalcular.Text !="" && Convert.ToInt32(textBoxCalcular.Text) > 0)
                {
                    getBolas();
                }
            }
        }

        private void getBolas()
        {
            int[] bola1 = { };
            int[] bola2 = { };
            int[] bola3 = { };
            int[] bola4 = { };
            int[] bola5 = { };
            int[] bola6 = { };

            int minimo = Convert.ToInt32(textBoxSorteio.Text) - Convert.ToInt32(textBoxCalcular.Text);

            for(int i=1; i<=6; i++)
            {
                string cmdSelect = @"SELECT BOLA" + i + " FROM MEGASENA WHERE SORTEIO BETWEEN " + minimo.ToString() + " AND " + 
                    textBoxSorteio.Text;

                if (i==1)
                {
                    bola1 = cadastro.returArray(cmdSelect, "BOLA" + i.ToString());
                }
                else if(i==2)
                {
                    bola2 = cadastro.returArray(cmdSelect, "BOLA" + i.ToString());
                }
                else if(i==3)
                {
                    bola3 = cadastro.returArray(cmdSelect, "BOLA" + i.ToString());
                }
                else if(i==4)
                {
                    bola4 = cadastro.returArray(cmdSelect, "BOLA" + i.ToString());
                }
                else if(i==5)
                {
                    bola5 = cadastro.returArray(cmdSelect, "BOLA" + i.ToString());
                }
                else if(i==6)
                {
                    bola6 = cadastro.returArray(cmdSelect, "BOLA" + i.ToString());
                }
            }

            List<int> list = new List<int>();
            list.AddRange(bola1);
            list.AddRange(bola2);
            list.AddRange(bola3);
            list.AddRange(bola4);
            list.AddRange(bola5);
            list.AddRange(bola6);

            int[] geral = list.ToArray();

            textBoxMA1.Text = mediaAritmeticaporCento(minimo, bola1, "BOLA1", dataGridViewBola1).ToString();
            textBoxMA2.Text = mediaAritmeticaporCento(minimo, bola2, "BOLA2", dataGridViewBola2).ToString();
            textBoxMA3.Text = mediaAritmeticaporCento(minimo, bola3, "BOLA3", dataGridViewBola3).ToString();
            textBoxMA4.Text = mediaAritmeticaporCento(minimo, bola4, "BOLA4", dataGridViewBola4).ToString();
            textBoxMA5.Text = mediaAritmeticaporCento(minimo, bola5, "BOLA5", dataGridViewBola5).ToString();
            textBoxMA6.Text = mediaAritmeticaporCento(minimo, bola6, "BOLA6", dataGridViewBola6).ToString();

            textBoxMAGeral.Text = mediaAritmeticaGeral(geral, dataGridViewGeral).ToString();

            textBoxDP1.Text = Math.Sqrt(calc.varianciaDouble(arrayPorcento(bola1, dataGridViewBola1), 
                Convert.ToDouble(textBoxMA1.Text))).ToString();
            textBoxDP2.Text = Math.Sqrt(calc.varianciaDouble(arrayPorcento(bola2, dataGridViewBola2), 
                Convert.ToDouble(textBoxMA2.Text))).ToString();
            textBoxDP3.Text = Math.Sqrt(calc.varianciaDouble(arrayPorcento(bola3, dataGridViewBola3), 
                Convert.ToDouble(textBoxMA3.Text))).ToString();
            textBoxDP4.Text = Math.Sqrt(calc.varianciaDouble(arrayPorcento(bola4, dataGridViewBola4), 
                Convert.ToDouble(textBoxMA4.Text))).ToString();
            textBoxDP5.Text = Math.Sqrt(calc.varianciaDouble(arrayPorcento(bola5, dataGridViewBola5), 
                Convert.ToDouble(textBoxMA5.Text))).ToString();
            textBoxDP6.Text = Math.Sqrt(calc.varianciaDouble(arrayPorcento(bola6, dataGridViewBola6), 
                Convert.ToDouble(textBoxMA6.Text))).ToString();

            textBoxDPGeral.Text = Math.Sqrt(calc.varianciaDouble(arrayPorcento(geral, dataGridViewGeral), 
                Convert.ToDouble(textBoxMAGeral.Text))).ToString();

            textBoxB1.Text = bolasFuturas(dataGridViewBola1, textBoxMA1, textBoxDP1);
            textBoxB2.Text = bolasFuturas(dataGridViewBola2, textBoxMA2, textBoxDP2);
            textBoxB3.Text = bolasFuturas(dataGridViewBola3, textBoxMA3, textBoxDP3);
            textBoxB4.Text = bolasFuturas(dataGridViewBola4, textBoxMA4, textBoxDP4);
            textBoxB5.Text = bolasFuturas(dataGridViewBola5, textBoxMA5, textBoxDP5);
            textBoxB6.Text = bolasFuturas(dataGridViewBola6, textBoxMA6, textBoxDP6);

            textBoxBG.Text = bolasFuturas(dataGridViewGeral, textBoxMAGeral, textBoxDPGeral);


            toolStripProgressBar1.Value = 100;

            timer1.Enabled = true;
            timer1.Start();
        }

        private double mediaAritmeticaGeral(int[] bolasGeral, DataGridView datagrid)
        {
            double mediaAritmetica = 0;

            for(int i = 0; i < bolasGeral.Length; i++)
            {
                int j = 0;

                while(Convert.ToInt32(datagrid.Rows[j].Cells[0].Value.ToString()) != bolasGeral[i])
                {
                    j++;
                }

                if (Convert.ToInt32(datagrid.Rows[j].Cells[0].Value.ToString()) == bolasGeral[i])
                {
                    mediaAritmetica += Convert.ToDouble(datagrid.Rows[j].Cells[2].Value.ToString());
                }
            }

            mediaAritmetica = mediaAritmetica / bolasGeral.Length;

            return mediaAritmetica;
        }

        private string bolasFuturas(DataGridView datagrid, TextBox mAritmetica, TextBox dPadrao)
        {
            double vMinimo = Convert.ToDouble(mAritmetica.Text) - Convert.ToDouble(dPadrao.Text);
            double vMaximo = Convert.ToDouble(mAritmetica.Text) + Convert.ToDouble(dPadrao.Text);

            string bola = "";

            for(int i = 0; i < datagrid.RowCount - 1; i++)
            {
                if(Convert.ToDouble(datagrid.Rows[i].Cells[2].Value.ToString()) >= vMinimo && 
                    Convert.ToDouble(datagrid.Rows[i].Cells[2].Value.ToString()) <= vMaximo)
                {
                    bola += datagrid.Rows[i].Cells[0].Value.ToString();
                    bola += " ";
                }
            }

            return bola;
        }

        private double[] arrayPorcento(int[] bola, DataGridView datagrid)
        {
            double[] porcento = { };

            List<double> list = new List<double>();

            for(int i = 0; i < bola.Length; i++)
            {
                int j = 0;

                while (Convert.ToInt32(datagrid.Rows[j].Cells[0].Value.ToString()) != bola[i])
                {
                    j++;
                }

                if (Convert.ToInt32(datagrid.Rows[j].Cells[0].Value.ToString()) == bola[i])
                {
                    list.Add(Convert.ToDouble(datagrid.Rows[j].Cells[2].Value.ToString()));
                }
            }

            porcento = list.ToArray();

            return porcento;
        }

        public double mediaAritmeticaporCento(int minimo, int[] bola, string tabela, DataGridView datagrid)
        {
            double mediaAritmetica = 0;
            double barraUnit = 16.6666667 / bola.Length;
            double barra = 0;
            int cont = 1;

            for (int i = 0; i < bola.Length; i++)
            {
                int j = 0;
                
                while (Convert.ToInt32(datagrid.Rows[j].Cells[0].Value.ToString()) != bola[i])
                {
                    j++;
                }

                if (Convert.ToInt32(datagrid.Rows[j].Cells[0].Value.ToString()) == bola[i])
                {
                    mediaAritmetica += Convert.ToDouble(datagrid.Rows[j].Cells[2].Value.ToString());
                }

                minimo++;

                string cmdSelectTable = @"SELECT " + tabela + ", COUNT(" + tabela + ") AS 'QNT', ((COUNT(" + tabela + ")) * 100)/" + 
                    minimo.ToString() + ".0 AS '%' FROM MEGASENA WHERE SORTEIO BETWEEN " + textBoxSorteio1.Text + " AND " + 
                    minimo.ToString() + " GROUP BY " + tabela + " ORDER BY 'QNT' DESC";

                cadastro.listdatagrid(cmdSelectTable, datagrid);
                barra += barraUnit;

                if(barra > cont)
                {
                    toolStripProgressBar1.Value += 1;
                    cont++;
                }
            }

            mediaAritmetica = mediaAritmetica / bola.Length - 1;

            return mediaAritmetica;
        }

        private void listaBolasGrid()
        {
            string sorteios = cadastro.returnString(@"SELECT COUNT(SORTEIO) AS QNT FROM MEGASENA");
            textBoxSorteio.Text = sorteios;

            string cmdSelect = @"SELECT TEM.BOLA, COUNT(BOLA) AS QNT, ((COUNT(BOLA) * 100) / " + sorteios + 
                ".) AS '%' FROM (SELECT BOLA1 AS BOLA FROM MEGASENA UNION ALL SELECT BOLA2 AS BOLA FROM MEGASENA UNION ALL SELECT BOLA3 AS BOLA FROM MEGASENA UNION ALL SELECT BOLA4 AS BOLA FROM MEGASENA UNION ALL SELECT BOLA5 AS BOLA FROM MEGASENA UNION ALL SELECT BOLA6 AS BOLA FROM MEGASENA) AS TEM GROUP BY BOLA ORDER BY QNT DESC";

            cadastro.listdatagrid(cmdSelect, dataGridViewGeral);

            textBoxSorteio1.Text = "1";
            textBoxSorteio2.Text = textBoxSorteio.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox10.Enabled = false;
            groupBox9.Enabled = true;

            label1.Text = "Primeira";
            label26.Text = "Última";

            textBoxSorteio1.Text = "1";
            textBoxSorteio2.Text = textBoxSorteio.Text;
        }

        private void radioButtonIntervalo_CheckedChanged(object sender, EventArgs e)
        {
            groupBox10.Enabled = false;
            groupBox9.Enabled = true;

            label1.Text = "Sorteio 1";
            label26.Text = "Sorteio 2";
        }

        private void radioButtonUltimos_CheckedChanged(object sender, EventArgs e)
        {
            groupBox10.Enabled = true;
            groupBox9.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            timer1.Stop();
            timer1.Enabled = false;
        }
    }
}
