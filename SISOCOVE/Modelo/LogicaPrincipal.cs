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

        internal void InstanciarArchivos()
        {
            String path = @"C:\Users\di_eg\Desktop\Datos UOCT\Mediciones de Flujo por Periodo Centro LS.xlsx";
            SLDocument sl = new SLDocument(path);
        }

        internal void imprimirDatos(List<List<double>> listaDoble, String título)
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
  
            for(int j = 0; j < 20;j++)
            {
                listaRandom.Add(r.NextDouble());
            }
            return listaRandom[r.Next(1,listaRandom.Count-1)];
        }

        internal List<List<double>> SeleccionarPadre(List<List<List<double>>> listaPoblación, List<List<double>> probabilidadesAcumuladas, double numAleatorio, List<List<List<double>>> listaPadres)
        {
            
            foreach (List<double> probAcum in probabilidadesAcumuladas)
            {
                //Console.WriteLine("Numero aleatorio: " + numAleatorio);
          
                if(numAleatorio < probAcum[1])
                {
                   double posición = probAcum[0];
                    double cont = 0;
                   
                   foreach(List<List<double>> ind in listaPoblación)
                    {
                       
                        if(cont == posición)
                        {
                            
                            //verificarPadre(listaPadres, padre);
                            return ind;
                        }
                        cont++;
                    }
                    
                }
                
            }
            return null;
        }

 

        internal bool ValidadPadres(List<List<double>> listaPadres)
        {
            List<double> nodos = new List<double>();
            foreach (List<double> padre in listaPadres)
            {
                nodos.Add(padre[0]);
            }
            if(nodos[1] == nodos[0])
            {
                return false;
            }
            else
            {
                return true;
            }
          
        }
    }

      
  


    
}
