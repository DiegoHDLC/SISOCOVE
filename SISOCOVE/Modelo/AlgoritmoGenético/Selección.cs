using SISOCOVE.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Modelo.AlgoritmoGenético
{
    class Selección
    {
        Coordinador miCoordinador;
        internal void SetCoordinador(Coordinador miCoordinador)
        {
            this.miCoordinador = miCoordinador;
        }

        internal List<double> SelecciónPorTorneo(List<double> ind1, List<double> ind2, double flujo, double ciclo, double flujoSaturación)
        {
            FunciónFitness funciónfitness = new FunciónFitness();
            double IR1 = funciónfitness.FFitness(flujo,ind1[3], flujoSaturación, ciclo );
            double IR2 = funciónfitness.FFitness(flujo, ind2[3], flujoSaturación, ciclo);
            //Console.WriteLine("IR1: "+IR1);
            //Console.WriteLine("IR2: " + IR2);
            if (IR1 < IR2)
            {
                return ind1;
            }
            else
            {
                return ind2;
            }
        }

        internal List<List<double>> SeleccionarIndividuoAleatorio(List<List<List<double>>> lista)
        {
            List<double> individuo = new List<double>();
            
            Random r = new Random();
            int numAleatorio = r.Next(0, lista.Count);
            int i = 0;
            foreach(List<List<double>> ind in lista)
            {
                if(i == numAleatorio)
                {
                    return ind;
                }
              
                i++;
            }
            return lista[numAleatorio];
        }

        internal List<List<double>> Crowding(List<List<double>> listaPoblación, List<double> indGanador)
        {
            
            List<double> listaDistanciaAux = new List<double>();
            List<List<double>> listaDistanciaNodos = new List<List<double>>();
            foreach(List<double> ind in listaPoblación)
            {
                List<double> listaDistancia = new List<double>();
                double distancia = Math.Sqrt(Math.Pow(ind[1] - indGanador[1], 2) + Math.Pow(ind[2] - indGanador[2], 2) + Math.Pow(ind[3] - indGanador[3], 2) + Math.Pow(ind[4] - indGanador[4], 2));
                listaDistancia.Add(ind[0]);
                listaDistanciaAux.Add(distancia);
                listaDistancia.Add(distancia);
                listaDistanciaNodos.Add(listaDistancia);
            }
            double min = listaDistanciaAux[0];
            for(int i = 0; i < listaDistanciaAux.Count; i++)
            {
                if(listaDistanciaAux[i] < min)
                {
                    min = listaDistanciaAux[i];
                }
            }
            LogicaPrincipal logicaPrincipal = new LogicaPrincipal();
            //logicaPrincipal.imprimirDatos(listaDistanciaNodos, "Lista distancia nodos");
            List<double> nuevoInd = new List<double>();
            foreach(List<double> ind in listaDistanciaNodos)
            {
                if (ind[1] == min)
                {

                    nuevoInd = ind;
                }
            }
            for(int i = 0; i< nuevoInd.Count; i++)
            {
                //Console.WriteLine(nuevoInd[i]);
            }
            
            List<List<double>> listaNuevaPoblación = new List<List<double>>();
            
            foreach(List<double> lista in listaPoblación)
            {
                if (nuevoInd[0] != lista[0])
                {
                    List<double> nuevoIndividuo = new List<double>();
                    nuevoIndividuo.Add(lista[0]);
                    nuevoIndividuo.Add(lista[1]);
                    nuevoIndividuo.Add(lista[2]);
                    nuevoIndividuo.Add(lista[3]);
                    nuevoIndividuo.Add(lista[4]);
                    listaNuevaPoblación.Add(nuevoIndividuo);
                }
                else
                {
                    List<double> nuevoIndividuo = new List<double>();
                    nuevoIndividuo.Add(nuevoInd[0]);
                    nuevoIndividuo.Add(indGanador[1]);
                    nuevoIndividuo.Add(indGanador[2]);
                    nuevoIndividuo.Add(indGanador[3]);
                    nuevoIndividuo.Add(indGanador[4]);
                    listaNuevaPoblación.Add(nuevoIndividuo);
                }
            }
            return listaNuevaPoblación;
        }

        internal List<List<List<double>>> CrowdingPrueba(List<List<double>> individuoGanador, List<List<List<double>>> poblaciónPrueba)
        {
            foreach (List<double> nodo in individuoGanador)
            {
                double VEGanador = nodo[3];
            }

            foreach (List<List<double>> ind in poblaciónPrueba)
            {
                
                foreach (List<double> nodo in ind)
                {
                   
                }
            }


            return poblaciónPrueba;
        }
    }
}
