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

 

        internal List<List<double>> SeleccionarIndividuoAleatorio(List<List<List<double>>> lista)
        {
            List<double> individuo = new List<double>();
            Random r = new Random();
            int numAleatorio = r.Next(0, lista.Count);
            int i = 0;
            foreach(List<List<double>> ind in lista)
            {
                if (lista.Count == 0)
                {
                    Console.WriteLine("La lista no preseneta individuos");
                }
                if(i == numAleatorio)
                {
                    return ind;
                }
              
                i++;
            }

            return lista[numAleatorio];
        }

      

        internal List<double> Crowding(List<List<double>> individuoGanador, List<List<List<double>>> poblaciónPrueba, List<double> listaDistancias)
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
                Console.WriteLine("Distancia: "+distancia);
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

        internal List<List<double>> SelecciónPorTorneo(double ganador, List<List<double>> individuoGanador, List<List<List<double>>> listaIndACompetir, List<double> IRIND1)
        {
            if(listaIndACompetir.Count == 0)
            {
                Console.WriteLine("No existen competidores");
            }
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
            if(IRCompetidores.Count == 0)
            {
                Console.WriteLine("No se presentan datos de competidores");
            }
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
                padre = SeleccionarPadre(poblaciónPrueba, probabilidadesAcumuladasPrueba, numAleatorioPrueba, listaPadresPrueba);
                while (padre == padreAux)
                {
                    numAleatorioPrueba = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                    padre = SeleccionarPadre(poblaciónPrueba, probabilidadesAcumuladasPrueba, numAleatorioPrueba, listaPadresPrueba);
                }
                padreAux = padre;
                listaPadresPrueba.Add(padre);

            }
            return listaPadresPrueba;
        }

        private List<List<double>> SeleccionarPadre(List<List<List<double>>> listaPoblación, List<List<double>> probabilidadesAcumuladas, double numAleatorio, List<List<List<double>>> listaPadres)
        {
            foreach (List<double> probAcum in probabilidadesAcumuladas)
            {
                //Console.WriteLine("Numero aleatorio: " + numAleatorio);
                if(listaPoblación.Count == 0)
                {
                    Console.WriteLine("No existen indidivuos dentro de la población");
                }
                if (numAleatorio < probAcum[1])
                {
                    double posición = probAcum[0];
                    double cont = 0;

                    foreach (List<List<double>> ind in listaPoblación)
                    {
                        
                        if (cont == posición)
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
    }
}
