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

        int[] bola1 = { };
        int[] bola2 = { };
        int[] bola3 = { };
        int[] bola4 = { };
        int[] bola5 = { };
        int[] bola6 = { };

        int[] bolaNO2 = { };
        int[] bolaNO3 = { };
        int[] bolaNO4 = { };
        int[] bolaNO5 = { };
        int[] bolaNO6 = { };

        int[] bolasGeral = { };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string tabela = getTabelaDB();

            listaBolasGrid(tabela);
        }

        private void sorteioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadSorteio cadSorteio = new FormCadSorteio();
            cadSorteio.Show();
        }

        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            string tabela = getTabelaDB(); //pega qual tavela sera feita a consulta


            //caso os requisitos de dados seja suficiente, iniciara o if ABAIXO

            if (requisitosOK()==true)
            {
                for(int bola = 1; bola <=6; bola++)
                {
                    string cmdSelectTable = "";

                    int valorFinal = Convert.ToInt32(textBoxSorteio2.Text) - Convert.ToInt32(textBoxCalcular.Text);

                    if (radioButtonTodos.Checked==true || radioButtonIntervalo.Checked==true)
                    {
                        cmdSelectTable = @"SELECT BOLA" + bola.ToString() + ", COUNT(BOLA" + bola.ToString() + ") AS 'QNT', ((COUNT(BOLA" +
                        bola.ToString() + ")) * 100)/" + valorFinal.ToString() + ".0 AS '%' FROM " + tabela + " WHERE SORTEIO BETWEEN " +
                        textBoxSorteio1.Text + " AND " + valorFinal.ToString() + " GROUP BY BOLA" + bola.ToString() + " ORDER BY 'QNT' DESC";
                    }
                    else if(radioButtonUltimos.Checked==true)
                    {
                        int bola1 = Convert.ToInt32(textBoxSorteio.Text) - Convert.ToInt32(textBox1.Text);

                        cmdSelectTable = @"SELECT BOLA" + bola.ToString() + ", COUNT(BOLA" + bola.ToString() + ") AS 'QNT', ((COUNT(BOLA" +
                        bola.ToString() + ")) * 100)/" + textBox1.Text + ".0 AS '%' FROM " + tabela + " WHERE SORTEIO BETWEEN " +
                        bola1.ToString() + " AND " + valorFinal.ToString() + " GROUP BY BOLA" + bola.ToString() + " ORDER BY 'QNT' DESC";
                    }

                    string cmdSelect = @"SELECT BOLA" + bola.ToString() + " FROM " + tabela + " WHERE SORTEIO BETWEEN " + 
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
                    getBolas(tabela);
                }
            }
        }

        private void getBolas(string tabela)
        {
            int minimo = Convert.ToInt32(textBoxSorteio.Text) - Convert.ToInt32(textBoxCalcular.Text);

            for(int i=1; i<=6; i++)
            {
                string cmdSelect = @"SELECT BOLA" + i + " FROM " + tabela + " WHERE SORTEIO BETWEEN " + minimo.ToString() + " AND " + 
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

            textBoxMA1.Text = mediaAritmeticaporCento(minimo, bola1, "BOLA1", dataGridViewBola1, tabela).ToString();
            textBoxMA2.Text = mediaAritmeticaporCento(minimo, bola2, "BOLA2", dataGridViewBola2, tabela).ToString();
            textBoxMA3.Text = mediaAritmeticaporCento(minimo, bola3, "BOLA3", dataGridViewBola3, tabela).ToString();
            textBoxMA4.Text = mediaAritmeticaporCento(minimo, bola4, "BOLA4", dataGridViewBola4, tabela).ToString();
            textBoxMA5.Text = mediaAritmeticaporCento(minimo, bola5, "BOLA5", dataGridViewBola5, tabela).ToString();
            textBoxMA6.Text = mediaAritmeticaporCento(minimo, bola6, "BOLA6", dataGridViewBola6, tabela).ToString();

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

            bola1 = null;
            bola2 = null;
            bola3 = null;
            bola4 = null;
            bola5 = null;
            bola6 = null;

            bola1 = bolasFuturas(dataGridViewBola1, textBoxMA1, textBoxDP1);
            bola2 = bolasFuturas(dataGridViewBola2, textBoxMA2, textBoxDP2);
            bola3 = bolasFuturas(dataGridViewBola3, textBoxMA3, textBoxDP3);
            bola4 = bolasFuturas(dataGridViewBola4, textBoxMA4, textBoxDP4);
            bola5 = bolasFuturas(dataGridViewBola5, textBoxMA5, textBoxDP5);
            bola6 = bolasFuturas(dataGridViewBola6, textBoxMA6, textBoxDP6);

            bolasGeral = bolasFuturas(dataGridViewGeral, textBoxMAGeral, textBoxDPGeral);

            bolaNO2 = removeDuplicado(bola1, bola2);
            bolaNO3 = removeDuplicado(bola1, bola3);
            bolaNO3 = removeDuplicado(bola2, bolaNO3);
            bolaNO4 = removeDuplicado(bola1, bola4);
            bolaNO4 = removeDuplicado(bola2, bolaNO4);
            bolaNO4 = removeDuplicado(bola3, bolaNO4);
            bolaNO5 = removeDuplicado(bola1, bola5);
            bolaNO5 = removeDuplicado(bola2, bolaNO5);
            bolaNO5 = removeDuplicado(bola3, bolaNO5);
            bolaNO5 = removeDuplicado(bola4, bolaNO5);
            bolaNO6 = removeDuplicado(bola1, bola5);
            bolaNO6 = removeDuplicado(bola2, bolaNO6);
            bolaNO6 = removeDuplicado(bola3, bolaNO6);
            bolaNO6 = removeDuplicado(bola4, bolaNO6);
            bolaNO6 = removeDuplicado(bola5, bolaNO6);

            List<int> todasBolasFuturas = new List<int>();
            todasBolasFuturas.AddRange(bola1);
            todasBolasFuturas.AddRange(bola2);
            todasBolasFuturas.AddRange(bola3);
            todasBolasFuturas.AddRange(bola4);
            todasBolasFuturas.AddRange(bola5);
            todasBolasFuturas.AddRange(bola6);

            todasBolasFuturas.AddRange(bolasGeral);

            int[] contarValores = contarValoresA(todasBolasFuturas);

            for(int i=1; i < contarValores.Length; i++)
            {
                listBox1.Items.Add((i).ToString() + " = " + contarValores[i].ToString());
            }

            if(checkBoxRepetidos.Checked==true)
            {
                textBoxB1.Text = listToString(bola1);
                textBoxB2.Text = listToString(bola2);
                textBoxB3.Text = listToString(bola3);
                textBoxB4.Text = listToString(bola4);
                textBoxB5.Text = listToString(bola5);
                textBoxB6.Text = listToString(bola6);

                textBoxBG.Text = listToString(bolasGeral);
            }
            else if(checkBoxRepetidos.Checked==false)
            {
                textBoxB1.Text = listToString(bola1);
                textBoxB2.Text = listToString(bolaNO2);
                textBoxB3.Text = listToString(bolaNO3);
                textBoxB4.Text = listToString(bolaNO4);
                textBoxB5.Text = listToString(bolaNO5);
                textBoxB6.Text = listToString(bolaNO6);

                textBoxBG.Text = listToString(bolasGeral);
            }

            toolStripProgressBar1.Value = 100;

            timer1.Enabled = true;
            timer1.Start();
        }

        private int[] removeDuplicado(int[] bola1, int[] bola2)
        {
            List<int> bolaA1 = new List<int>();
            List<int> bolaA2 = new List<int>();

            bolaA1.AddRange(bola1);
            bolaA2.AddRange(bola2);


            for (int i = 0; i < bola1.Length; i++)
            {
                int j = 0;
                int k = bolaA2.Count - 1;

                if(k < 0)
                {
                    k++;
                }

                if(k > 0)
                {
                    while (bola1[i] != bolaA2[j] && j < k)
                    {
                        j++;
                    }

                    if (bola1[i] == bolaA2[j])
                    {
                        bolaA2.RemoveAt(j);
                    }
                }
            }

            bola2 = bolaA2.ToArray();
            return bola2;
        }

        private string listToString(int[] bolas)
        {
            //CONVERTE A LISTA ARRAYS INTEIROS EM STRING

            string listaBolas = "";

            for(int i=0; i < bolas.Length; i++)
            {
                listaBolas += bolas[i].ToString() + " ";
            }

            return listaBolas;
        }

        private int[] contarValoresA(List<int> list)
        {
            //REALIZA A CONTAGEM DA QUANTIDADE QUE DETERMINADO VALOR PODE SAIR DE ACORDO COM A ESTIMATIVA

            int[] contagem = new int[61];

            for(int i=0; i < list.Count; i++)
            {

                contagem[list[i]] += 1;
            }

            return contagem;
        }

        private double mediaAritmeticaGeral(int[] bolasGeral, DataGridView datagrid)
        {
            /*calculo da media aritmetica dos sorteios geral.
             * 
             * OBS: O SORTEIO GERAL APENAS UTILIZA OS DADOS DE TODOS OS SORTEIOS JA REALIZADOS. 
             * NÃO CORRE OS DADOS EM HISTORICO
             * 
             * */

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

        private int[] bolasFuturas(DataGridView datagrid, TextBox mAritmetica, TextBox dPadrao)
        {
            // preenche um array com as bolas onde os dados de % estejam dentro das estimativas de min e max

            double vMinimo = Convert.ToDouble(mAritmetica.Text) - Convert.ToDouble(dPadrao.Text);
            double vMaximo = Convert.ToDouble(mAritmetica.Text) + Convert.ToDouble(dPadrao.Text);

            List<int> list = new List<int>();

            int[] bola = { };

            for(int i = 0; i < datagrid.RowCount - 1; i++)
            {
                if(Convert.ToDouble(datagrid.Rows[i].Cells[2].Value.ToString()) >= vMinimo && 
                    Convert.ToDouble(datagrid.Rows[i].Cells[2].Value.ToString()) <= vMaximo)
                {
                    list.Add(Convert.ToInt32(datagrid.Rows[i].Cells[0].Value.ToString()));
                }
            }
            bola = list.ToArray();
            return bola;
        }

        private double[] arrayPorcento(int[] bola, DataGridView datagrid)
        {
            //cria um array de porcentagens na saido dos dados

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

        public double mediaAritmeticaporCento(int minimo, int[] bola, string tabela, DataGridView datagrid, string tabelas)
        {
            double mediaAritmetica = 0;


            double barraUnit = 16.6666667 / bola.Length;
            double barra = 0; //barra de progresso
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
                    minimo.ToString() + ".0 AS '%' FROM " + tabelas + " WHERE SORTEIO BETWEEN " + textBoxSorteio1.Text + " AND " + 
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

        private void listaBolasGrid(string tabela)
        {
            string sorteios = cadastro.returnString(@"SELECT COUNT(SORTEIO) AS QNT FROM " + tabela + "", "QNT");
            textBoxSorteio.Text = sorteios;

            string cmdSelect = @"SELECT TEM.BOLA, COUNT(BOLA) AS QNT, ((COUNT(BOLA) * 100) / " + sorteios + 
                ".) AS '%' FROM (SELECT BOLA1 AS BOLA FROM " + tabela + " UNION ALL SELECT BOLA2 AS BOLA FROM " + tabela + 
                " UNION ALL SELECT BOLA3 AS BOLA FROM " + tabela + " UNION ALL SELECT BOLA4 AS BOLA FROM " + tabela + 
                " UNION ALL SELECT BOLA5 AS BOLA FROM " + tabela + " UNION ALL SELECT BOLA6 AS BOLA FROM " + tabela + 
                ") AS TEM GROUP BY BOLA ORDER BY QNT DESC";

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            timer1.Stop();
            timer1.Enabled = false;
        }

        private bool requisitosOK()
        {
            bool apto = false;

            toolStripProgressBar1.Value = 0;

            if (radioButtonTodos.Checked == true || radioButtonIntervalo.Checked == true)
            {
                if (textBoxSorteio1.Text != "" && Convert.ToInt32(textBoxSorteio1.Text) > 0 && textBoxSorteio2.Text != "" &&
                    Convert.ToInt32(textBoxSorteio2.Text) <= Convert.ToInt32(textBoxSorteio.Text))
                {
                    apto = true;
                }
            }
            else if (radioButtonUltimos.Checked == true)
            {
                if (textBox1.Text != "" && Convert.ToInt32(textBox1.Text) > 0 &&
                    Convert.ToInt32(textBox1.Text) <= Convert.ToInt32(textBoxSorteio.Text))
                {
                    apto = true;
                }
            }

            return apto;
        }

        private string getTabelaDB()
        {
            string tabela = "";

            if (radioButtonDB1.Checked == true)
            {
                tabela = "MEGASENA";
            }
            else if (radioButtonDB2.Checked == true)
            {
                tabela = "MEGASENA2";
            }

            return tabela;
        }

        private void checkBoxRepetidos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRepetidos.Checked == true)
            {
                textBoxB1.Text = listToString(bola1);
                textBoxB2.Text = listToString(bola2);
                textBoxB3.Text = listToString(bola3);
                textBoxB4.Text = listToString(bola4);
                textBoxB5.Text = listToString(bola5);
                textBoxB6.Text = listToString(bola6);

                textBoxBG.Text = listToString(bolasGeral);
            }
            else if (checkBoxRepetidos.Checked == false)
            {
                textBoxB1.Text = listToString(bola1);
                textBoxB2.Text = listToString(bolaNO2);
                textBoxB3.Text = listToString(bolaNO3);
                textBoxB4.Text = listToString(bolaNO4);
                textBoxB5.Text = listToString(bolaNO5);
                textBoxB6.Text = listToString(bolaNO6);

                textBoxBG.Text = listToString(bolasGeral);
            }
        }
    }
}
