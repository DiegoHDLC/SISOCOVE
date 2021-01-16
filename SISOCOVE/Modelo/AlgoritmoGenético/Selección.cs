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
            Console.WriteLine("IR1: "+IR1);
            Console.WriteLine("IR2: " + IR2);
            if (IR1 < IR2)
            {
                return ind1;
            }
            else
            {
                return ind2;
            }
        }

        internal List<double> SeleccionarIndividuoAleatorio(List<List<double>> lista)
        {
            List<double> individuo = new List<double>();
            Random r = new Random();
            int numAleatorio = r.Next(0, lista.Count);  
            return lista[numAleatorio];
        }

        internal List<List<double>> Crowding(List<List<double>> listaPoblación, List<double> indGanador)
        {
            List<double> listaDistancia = new List<double>();
            List<double> listaDistanciaAux = new List<double>();
            List<List<double>> listaDistanciaNodos = new List<List<double>>();
            foreach(List<double> ind in listaPoblación)
            {
                double distancia = Math.Sqrt(Math.Pow(ind[1] - indGanador[1], 2) + Math.Pow(ind[2] - indGanador[2], 2) + Math.Pow(ind[3] - indGanador[3], 2) + Math.Pow(ind[4] - indGanador[4], 2));
                listaDistancia.Add(ind[0]);
                listaDistanciaAux.Add(distancia);
                listaDistancia.Add(distancia);
            }
            double min = listaDistanciaAux[0];
            for(int i = 0; i < listaDistanciaAux.Count; i++)
            {
                if(listaDistanciaAux[i] < min)
                {
                    min = listaDistanciaAux[i];
                }
            }
            List<double> nuevoInd = new List<double>();
            foreach(List<double> ind in listaDistanciaNodos)
            {
                if (ind[1] == min)
                {

                    nuevoInd = ind;
                }
            }
            
            
            return listaPoblación;
        }
    }
}
