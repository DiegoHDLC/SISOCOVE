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

        internal List<double> CrowdingPrueba(List<List<double>> individuoGanador, List<List<List<double>>> poblaciónPrueba, List<double> listaDistancias)
        {
            foreach (List<List<double>> ind in poblaciónPrueba)
            {
                double distancia = 0;
                for (int i = 0; i < individuoGanador.Count; i++)
                {
                    List<double> nodosGanador = individuoGanador[i];
                    List<double> nodosOriginal = ind[i];
                    distancia = Math.Sqrt(Math.Pow(nodosGanador[3] - nodosOriginal[3], 2)) + distancia;

                }
                //Console.WriteLine(distancia);
                listaDistancias.Add(distancia);
            }
         
            return listaDistancias;
        }

        internal List<List<List<double>>> InsertarIndividuoGanador(List<List<List<double>>> población, List<List<double>> indGanador, double posición)
        {
            List<List<List<double>>> listaPoblaciónFinal = new List<List<List<double>>>();
            for (int i = 0; i < población.Count; i++)
            {
                if (posición != i)
                {
                    listaPoblaciónFinal.Add(población[i]);
                }
                else
                {
                    listaPoblaciónFinal.Add(indGanador);
                }

            }
            return listaPoblaciónFinal;
        }

        internal double PosiciónIndSemejante(List<double> listaDistancias, double posición, double min)
        {
            for (int i = 0; i < listaDistancias.Count; i++)
            {
                if (listaDistancias[i] < min)
                {
                    min = listaDistancias[i];
                    posición = i;
                }
            }
            return posición;
        }

        internal List<List<double>> SeleccionarGanador(double ganador, List<List<double>> individuoGanador, List<List<List<double>>> listaIndACompetir, List<double> IRIND1)
        {
            if (ganador == IRIND1[0])
            {
                individuoGanador = listaIndACompetir[0];
            }
            else
            {
                individuoGanador = listaIndACompetir[1];
            }

            return individuoGanador;
        }

        internal double Ganador(List<List<double>> IRCompetidores)
        {
            List<double> IRIND1 = IRCompetidores[0];
            List<double> IRIND2 = IRCompetidores[1];
            double ganador = Math.Min(IRIND1[0], IRIND2[0]);
         

            return ganador;
        }

        internal List<List<List<double>>> SeleccionarCompetidores(List<List<List<double>>> poblaciónPrueba, List<List<List<double>>> individuosMutados, List<List<List<double>>> listaIndACompetir)
        {
            List<List<double>> ind2 = new List<List<double>>();
            List<List<double>> indAux = new List<List<double>>();
            for (int i = 0; i < 2; i++)
            {
                List<List<double>> ind1 = new List<List<double>>();
                ind1 = SeleccionarIndividuoAleatorio(poblaciónPrueba);
                while (ind1 == indAux)
                {
                    ind1 = SeleccionarIndividuoAleatorio(individuosMutados);
                }
                indAux = ind1;
                listaIndACompetir.Add(ind1);
            }
            return listaIndACompetir;
        }

        internal List<List<List<double>>> SeleccionarPadres(List<double> listaRandom, List<List<List<double>>> poblaciónPrueba, List<List<double>> probabilidadesAcumuladasPrueba, List<List<List<double>>> listaPadresPrueba)
        {
            LogicaPrincipal logicaPrincipal = new LogicaPrincipal();
            List<List<double>> padreAux = new List<List<double>>();
            for (int i = 0; i < 2; i++)
            {
                List<List<double>> padre = new List<List<double>>();
                double numAleatorioPrueba = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                //Console.WriteLine("número aleatorio: " + numAleatorio);
                padre = logicaPrincipal.SeleccionarPadre(poblaciónPrueba, probabilidadesAcumuladasPrueba, numAleatorioPrueba, listaPadresPrueba);
                while (padre == padreAux)
                {
                    numAleatorioPrueba = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                    padre = logicaPrincipal.SeleccionarPadre(poblaciónPrueba, probabilidadesAcumuladasPrueba, numAleatorioPrueba, listaPadresPrueba);
                }
                padreAux = padre;
                listaPadresPrueba.Add(padre);

            }
            return listaPadresPrueba;
        }
    }
}
