using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena
{
    class Calc
    {
        public float mediaAritimetica(int[] valores)
        {
            int media = 0;

            for(int i=0; i < valores.Length; i++)
            {
                media += valores[i];
            }

            float resultado = media / valores.Length;
            return resultado;
        }

        public double variancia(int[] valores, double mediaAritmetica)
        {
            double variancia = 0;

            for (int i = 0; i < valores.Length; i++)
            {
                variancia += Math.Pow(valores[i] - mediaAritmetica, 2);
            }

            variancia = variancia / valores.Length;

            return variancia;
        }

        public double varianciaDouble(double[] valores, double mediaAritmetica)
        {
            double variancia = 0;

            for (int i = 0; i < valores.Length; i++)
            {
                variancia += Math.Pow(valores[i] - mediaAritmetica, 2);
            }

            variancia = variancia / valores.Length;

            return variancia;
        }

        public double desvioPadrao(double variancia)
        {
            double desvioPadrao = Math.Sqrt(Convert.ToDouble(variancia));

            return desvioPadrao;
        }

        public double coefVariacao(double desvioPadrao, float mediaAritmetica)
        {
            double coefVariacao = (desvioPadrao /Convert.ToDouble(mediaAritmetica)) * 100;

            return coefVariacao;
        }
    }
}
