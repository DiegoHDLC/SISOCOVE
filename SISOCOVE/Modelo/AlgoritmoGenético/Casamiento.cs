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

        internal List<List<List<double>>> CruzarIndividuos(List<List<List<double>>> listaPadresPrueba)
        {
            double alpha = 0.5;
            List<List<double>> Padre1 = new List<List<double>>();
            List<List<double>> Padre2 = new List<List<double>>();
            List<List<List<double>>> listaHijos = new List<List<List<double>>>();

            List<double> nodo1 = new List<double>();
            List<double> nodo2 = new List<double>();
            double verdeEfectivo1 = 0;
            double verdeEfectivo2 = 0;
            Padre1 = listaPadresPrueba[0];
            Padre2 = listaPadresPrueba[1];
            nodo1 = Padre1[0];
            nodo2 = Padre2[0];
            verdeEfectivo1 = nodo1[3];
            verdeEfectivo2 = nodo2[3];
            double Cmax = Math.Max(verdeEfectivo1, verdeEfectivo2);
            double Cmin = Math.Min(verdeEfectivo1, verdeEfectivo2);
            double I = Cmax - Cmin;
            double limite1 = Cmin - I * alpha;
            double limite2 = Cmax + I * alpha;
            int i = 0;
            Random r = new Random();
            listaHijos = new List<List<List<double>>>();
            foreach (List<List<double>> padre in listaPadresPrueba)
            {
                List<List<double>> hijo1 = new List<List<double>>();
                List<List<double>> hijo2 = new List<List<double>>();
                double num = Convert.ToDouble(r.Next(Convert.ToInt32(limite1), Convert.ToInt32(limite2)));
                foreach (List<double> nodo in padre)
                {                
                    List<double> nodoHijo1 = new List<double>();
                    List<double> nodoHijo2 = new List<double>();
                    if (i == 0)
                    {
                        nodoHijo1.Add(nodo[0]);
                        nodoHijo1.Add(nodo[1]);
                        nodoHijo1.Add(nodo[2]);
                        nodoHijo1.Add(Math.Abs(num));
                        nodoHijo1.Add(nodo[4]);
                        hijo1.Add(nodoHijo1);                   
                    }
                    else
                    {
                        nodoHijo2.Add(nodo[0]);
                        nodoHijo2.Add(nodo[1]);
                        nodoHijo2.Add(nodo[2]);
                        nodoHijo2.Add(Math.Abs(num));
                        nodoHijo2.Add(nodo[4]);
                        hijo2.Add(nodoHijo2);
                        
                        //Console.WriteLine("Agrega hijo2");
                    }

                }
                if(i == 0)
                {
                    listaHijos.Add(hijo1);
                    //Console.WriteLine("Agrega hijo1");
                }
                else
                {
                    listaHijos.Add(hijo2);
                    //Console.WriteLine("Agrega hijo2");
                }
                i++;
            }
            LogicaPrincipal logicaPrincipal = new LogicaPrincipal();
            //logicaPrincipal.imprimirDatos(hijo1, "Hijo1");
            //logicaPrincipal.imprimirDatos(hijo2, "Hijo2");
            return listaHijos;
        }

        /*internal List<List<List<double>>> CruceAritmético(List<List<List<double>>> listaPadresPrueba)
        {
            List<List<double>> padre1 = new List<List<double>>();
            List<List<double>> padre2 = new List<List<double>>();

            padre1 = listaPadresPrueba[0];
            padre2 = listaPadresPrueba[1];


        }*/
    }
}
