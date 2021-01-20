using IronXL;
using SISOCOVE.Controlador;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SISOCOVE.Modelo
{

    class LogicaPrincipal
    {

        Coordinador miCoordinador;
        internal void SetCoordinador(Coordinador miCoordinador)
        {
            this.miCoordinador = miCoordinador;
            throw new NotImplementedException();
        }

        internal List<List<double>> ObtenerProbabilidad(int nodo, List<List<double>> IRPoblación, double sumaIR, List<List<double>> probabilidadNodos)
        {

            foreach (List<double> IRIndividuo in IRPoblación)
            {
                if (IRIndividuo[0] == nodo)
                {
                    List<double> probabiliad = new List<double>();
                    probabiliad.Add(nodo);
                    probabiliad.Add(IRIndividuo[1] / sumaIR);
                    probabilidadNodos.Add(probabiliad);
                }
            }
            return probabilidadNodos;
        }

        internal List<List<double>> ObtenerProbabilidadAcumulada( List<List<double>> probabilidadNodos, double suma, List<List<double>> probabilidadAcumuladaNodos)
        {
            foreach (List<double> probNodo in probabilidadNodos)
            {
                List<double> probabilidadAcumuladaNodo = new List<double>();
                int i = 0;
                    foreach (double prob in probNodo)
                    {  
                    if (i == 1)
                        {
                            suma = prob + suma;
                        }
                        i++;
                    }
                    probabilidadAcumuladaNodo.Add(probNodo[0]);
                    probabilidadAcumuladaNodo.Add(suma);
                    probabilidadAcumuladaNodos.Add(probabilidadAcumuladaNodo);
            }
            return probabilidadAcumuladaNodos;
        }

        internal double GenerarNumAleatorio(double ciclo)
        {
            List<double> listaRand = new List<double>();
            Random r = new Random();
            for(int i = 0; i< 500; i++)
            {
                listaRand.Add(r.Next(1, Convert.ToInt32(ciclo)));
            }
            return listaRand[r.Next(0,listaRand.Count)];
        }

        internal void InstanciarArchivos()
        {
            String path = @"C:\Users\di_eg\Desktop\Datos UOCT\Mediciones de Flujo por Periodo Centro LS.xlsx";
            SLDocument sl = new SLDocument(path);
        }

        internal void ImprimirDatos(List<List<double>> listaDoble, String título)
        {
            Console.WriteLine(título);
            foreach(List<double> lista in listaDoble)
            {
                for(int i = 0; i < lista.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write("nodo["+lista[i]+"]: ");
                    }
                    Console.Write(" " + lista[i]);
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        internal double GenerarNumAleatorio(List<double> listaRandom)
        {
            Random r = new Random();
  
            for(int j = 0; j < 10;j++)
            {
                listaRandom.Add(r.NextDouble());
            }
            return listaRandom[r.Next(1,listaRandom.Count-1)];
        }

        internal void imprimirDatosGrandes(List<List<List<double>>> poblaciónPrueba, string v)
        {
            foreach(List<List<double>> ind in poblaciónPrueba)
            {
                ImprimirDatos(ind, v);
            }
        }
    }

      
  


    
}
