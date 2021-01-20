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

        public List<List<double>> GenerarPoblación(int nodo, List<List<double>> datosCicloNodo, int cantIntersecciones, List<List<double>> población)
        {
            if(datosCicloNodo == null)
            {
                Console.WriteLine("lista de datos, referentes al ciclo del nodo, tiene valor nulo");
            }
            

            List<double> individuo = new List<double>();
                individuo = GenerarIndividuo(nodo, datosCicloNodo, individuo);
                población.Add(individuo);
                /*for(int j = 0; j < individuo.Count; j++)
                {
                    Console.WriteLine(individuo[j]);
                }
                Console.WriteLine();
                */
            
           

            return población;
        }

        private List<double> GenerarIndividuo(int nodo, List<List<double>> datosCicloNodos, List<double> individuo)
        {

            

            List<double> verdeEfectivo = new List<double>();
           
            foreach (List<double> datoCiclo in datosCicloNodos)
            {
                if (datoCiclo[0] == nodo)
                {
                    individuo.Add(nodo);
                    individuo.Add(0);
                    individuo.Add(18);
                    individuo.Add(datoCiclo[7]);
                    individuo.Add(datoCiclo[8]);
                }
                
            }


            return individuo;
        }
    }
}
