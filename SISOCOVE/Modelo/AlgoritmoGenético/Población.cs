using SISOCOVE.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Modelo
{
    class Población
    {
        Coordinador miCoodinador;

        internal void SetCoordinador(Coordinador miCoordinador)
        {
            this.miCoodinador = miCoordinador;
        }

        public List<List<double>> GenerarPoblación(List<int> listaNodos, List<List<double>> datosCicloNodo, int cantIntersecciones)
        {
            List<List<double>> población = new List<List<double>>();

            for (int i = 0; i < cantIntersecciones; i++)
            {
                List<double> individuo = new List<double>();
                individuo = GenerarIndividuo(listaNodos[i], datosCicloNodo);
                población.Add(individuo);
            }

            return población;
        }

        private List<double> GenerarIndividuo(int nodo, List<List<double>> datosCicloNodos)
        {

            List<double> individuo = new List<double>();

            List<double> verdeEfectivo = new List<double>();
            int i = 0;
            foreach (List<double> datoCiclo in datosCicloNodos)
            {
                if (datoCiclo[i] == nodo)
                {
                    individuo.Add(0);
                    individuo.Add(18);
                    individuo.Add(datoCiclo[3]);
                    individuo.Add(datoCiclo[4]);
                }
                i++;
            }


            return individuo;
        }
    }
}
