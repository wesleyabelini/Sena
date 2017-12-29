using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace DBCreateSena
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] data = getValues(@"D:/data.txt");
            string[] bola1 = getValues(@"D:/bola1.txt");
            string[] bola2 = getValues(@"D:/bola2.txt");
            string[] bola3 = getValues(@"D:/bola3.txt");
            string[] bola4 = getValues(@"D:/bola4.txt");
            string[] bola5 = getValues(@"D:/bola5.txt");
            string[] bola6 = getValues(@"D:/bola6.txt");

            DateTime date;
            
            for(int i =0; i < data.Count(); i++)
            {
                date = DateTime.ParseExact(data[i], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                insertData(@"INSERT INTO megasena VALUES('" + date.ToShortDateString() + "', " + bola1[i] + ", " + 
                    bola2[i] + ", " + bola3[i] + ", " + bola4[i] + ", " + bola5[i] + ", " + bola6[i] + ");");
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }

        public static string[] getValues(string local)
        {
            string[] variable = File.ReadAllLines(local);
            return variable;
        }

        public static void insertData(string cmd)
        {
            string conection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\Projetos\Softwares\C#\Meus Projetos\DBCreateSena\sena.mdf';Integrated Security=True;Connect Timeout=30";

            SqlConnection conexao = new SqlConnection(conection);
            SqlCommand comando = new SqlCommand(cmd, conexao);

            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();

                conexao.Close();
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}
