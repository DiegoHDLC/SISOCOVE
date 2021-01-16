using SISOCOVE.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Modelo.AlgoritmoGenético
{
    class Casamiento
    {
        Coordinador miCoordinador;
        internal void SetCoordiador(Coordinador miCoordinador)
        {
            this.miCoordinador = miCoordinador;
        }

        internal List<List<double>> CruzarIndividuos(List<List<double>> listaPadres)
        {
            List<double> padre1 = new List<double>();
            List<double> padre2 = new List<double>();
            List<List<double>> listaHijos = new List<List<double>>();
            List<double> hijo1 = new List<double>();
            List<double> hijo2 = new List<double>();
            List<double> hijo = new List<double>();
            double Cmax = 0;
            double Cmin = 0;
            double I = 0;
            double limite1 = 0;
            double limite2 = 0;
  
            padre1 = listaPadres[0];
            padre2 = listaPadres[1];
            Random r = new Random();
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine("Hijo"+j);
                Console.WriteLine();
                double alpha = 0.5;
                hijo = new List<double>();
                hijo.Add(0); //nodo ficticio
                for (int i = 1; i < padre1.Count; i++)
                {
                    //Console.WriteLine("Gen" + i);
                    Cmax = Math.Max(padre1[i], padre2[i]);
                    //Console.WriteLine("Cmax: "+Cmax);
                    Cmin = Math.Min(padre1[i], padre2[i]);
                    //Console.WriteLine("Cmin: " + Cmin);
                    I = Cmax - Cmin;
                    limite1 = Cmin - I * alpha;
                    limite2 = Cmax + I * alpha;
                    hijo.Add(Convert.ToDouble(r.Next(Convert.ToInt32(limite1), Convert.ToInt32(limite2))));

                }
                listaHijos.Add(hijo);
                
            }
            LogicaPrincipal logicaPrincipal = new LogicaPrincipal();
            logicaPrincipal.imprimirDatos(listaHijos, "hijos: ");
            return listaHijos;
            


        }
    }
}
