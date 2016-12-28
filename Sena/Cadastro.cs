using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Sena
{
    class Cadastro
    {
        string sqlconect = ConfigurationManager.ConnectionStrings["conexaoSena"].ConnectionString;

        public void cadastro(string comando)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = sqlconect;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public string returnString(string comando)
        {
            string valor = "";

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = sqlconect;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    valor = reader["QNT"].ToString();
                }

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            return valor;
        }

        public int[] returArray(string comando, string tabela)
        {
            int[] valores = { };

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = sqlconect;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader reader = cmd.ExecuteReader();

                List<int> lista = new List<int>();

                while (reader.Read())
                {
                    lista.Add(Convert.ToInt32(reader[tabela]));
                }

                valores = lista.ToArray<int>();

                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            return valores;
       }

        public void listdatagrid(string cmdsql, DataGridView datagrid)
        {
            datagrid.Columns.Clear();

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = sqlconect;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(cmdsql, conexao);

                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                data.Fill(table);

                datagrid.DataSource = table;

                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
    }
}
