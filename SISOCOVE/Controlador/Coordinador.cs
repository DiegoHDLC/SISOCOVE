using SISOCOVE.Modelo;
using SISOCOVE.Modelo.AlgoritmoGenético;
using SISOCOVE.Modelo.Intersecciones;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Controlador
{
    class Coordinador
    {
        VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
        LogicaPrincipal logicaPrincipal = new LogicaPrincipal();
        Población población = new Población();
        FunciónFitness funciónFitness = new FunciónFitness();
        Intersección intersección = new Intersección();

        internal static double ObtenerIR()
        {
            FunciónFitness funciónFitness = new FunciónFitness();
            // ObtenerIR();
            return 2;
        }

        Casamiento casamiento = new Casamiento();
        Mutación mutación = new Mutación();
        Selección selección = new Selección();

        public void setVentanaPrincipal(VentanaPrincipal ventanaPrincipal)
        {
            this.ventanaPrincipal = ventanaPrincipal;
            throw new NotImplementedException();
        }

        public void setLogicaPrincipal(LogicaPrincipal logicaPrincipal)
        {
            this.logicaPrincipal = logicaPrincipal;
            throw new NotImplementedException();
        }

        internal void SetFunciónFitness(FunciónFitness funciónFitness)
        {
            this.funciónFitness = funciónFitness;
        }

        internal void setPoblación(Población población)
        {
            this.población = población;
        }

        public void Evaluar(int cantPoblación)
        {

            logicaPrincipal = new LogicaPrincipal();
            funciónFitness = new FunciónFitness();
            población = new Población();
            intersección = new Intersección();
            casamiento = new Casamiento();
            mutación = new Mutación();
            selección = new Selección();
            List<int> listaNodos = new List<int>();
            List<List<double>> listaPoblación = new List<List<double>>();
            List<double> cicloNodo1 = new List<double>();
            int cantGeneraciones = 3;
            logicaPrincipal.InstanciarArchivos();
            listaNodos.Add(8);
            listaNodos.Add(10);
            listaNodos.Add(14);
            double flujoSaturación = 0;
            double flujo = 0;
            double ciclo = 0;
            double sumaIR;



            List<double> individuo = new List<double>();
            List<List<double>> datosFlujoNodos = new List<List<double>>();
            List<List<double>> datosCicloNodos = new List<List<double>>();
            List<List<double>> listaFlujoSaturación = new List<List<double>>();
            List<List<double>> IRIntersecciones = new List<List<double>>();

            List<List<double>> individuoPrueba = new List<List<double>>();




            int cantIntersecciones = 0;


            cantIntersecciones = intersección.CalcularCantIntersecciones(listaNodos);
            for (int i = 0; i < cantIntersecciones; i++)
            {
                datosFlujoNodos = intersección.ObtenerDatosFlujoNodo(listaNodos[i], datosFlujoNodos);
                datosCicloNodos = intersección.ObtenerDatosCicloNodo(listaNodos[i], datosCicloNodos);
                listaFlujoSaturación = intersección.ObtenerFlujoSaturación(listaNodos[i], datosFlujoNodos, listaFlujoSaturación);
                IRIntersecciones = funciónFitness.ObtenerIR(listaNodos[i], datosFlujoNodos, datosCicloNodos, listaFlujoSaturación, IRIntersecciones, 0, 0);
                individuoPrueba = población.GenerarPoblación(listaNodos[i], datosCicloNodos, cantIntersecciones, individuoPrueba);
                listaPoblación = población.GenerarPoblación(listaNodos[i], datosCicloNodos, cantIntersecciones, listaPoblación);
            }

            List<List<List<double>>> poblaciónPrueba = new List<List<List<double>>>();
            logicaPrincipal.imprimirDatos(individuoPrueba, "Individuo prueba: ");
            Random rand = new Random();

            cicloNodo1 = datosCicloNodos[0];
            ciclo = cicloNodo1[1];

            for (int i = 0; i < 3; i++)
            {

                List<List<double>> individuoPruebaVEIncent = new List<List<double>>();
                double nuevoEV = rand.Next(1, Convert.ToInt32(ciclo));
                foreach (List<double> nodo in individuoPrueba)
                {
                    List<double> nodoPrueba = new List<double>();
                    nodoPrueba.Add(nodo[0]);
                    nodoPrueba.Add(nodo[1]);
                    nodoPrueba.Add(nodo[2]);
                    nodoPrueba.Add(nuevoEV);
                    nodoPrueba.Add(nodo[4]);

                    individuoPruebaVEIncent.Add(nodoPrueba);

                }

                poblaciónPrueba.Add(individuoPruebaVEIncent);

            }

            foreach (List<List<double>> indiv in poblaciónPrueba)
            {
                logicaPrincipal.imprimirDatos(indiv, "Individuo prueba nuevo: ");
            }

            for (int generacion = 0; generacion < cantGeneraciones; generacion++)
            {
                int cont = 0;
            List<List<double>> IRPoblaciónPrueba = new List<List<double>>();
            foreach (List<List<double>> ind in poblaciónPrueba)
            {
                IRIntersecciones = new List<List<double>>();
                datosFlujoNodos = new List<List<double>>();
                datosCicloNodos = new List<List<double>>();
                listaFlujoSaturación = new List<List<double>>();
                List<double> IRIndividuoPrueba = new List<double>();
                foreach (List<double> inter in ind)
                {
                    datosFlujoNodos = intersección.ObtenerDatosFlujoNodo(Convert.ToInt32(inter[0]), datosFlujoNodos);
                    datosCicloNodos = intersección.ObtenerDatosCicloNodo(Convert.ToInt32(inter[0]), datosCicloNodos);
                    listaFlujoSaturación = intersección.ObtenerFlujoSaturación(Convert.ToInt32(inter[0]), datosFlujoNodos, listaFlujoSaturación);
                    IRIntersecciones = funciónFitness.ObtenerIR(Convert.ToInt32(inter[0]), datosFlujoNodos, datosCicloNodos, listaFlujoSaturación, IRIntersecciones, inter[3], 1);
                }
                sumaIR = funciónFitness.SumaIR(IRIntersecciones, 0);
                IRIndividuoPrueba.Add(cont);
                IRIndividuoPrueba.Add(sumaIR);
                IRPoblaciónPrueba.Add(IRIndividuoPrueba);

                cont++;
            }
            logicaPrincipal.imprimirDatos(IRPoblaciónPrueba, "IR Población");

            
           
                double sumaIRTotal = funciónFitness.SumaIR(IRPoblaciónPrueba, 0);
                List<List<double>> probabilidadIndividuos = new List<List<double>>();
                foreach (List<double> IRInd in IRPoblaciónPrueba)
                {
                    probabilidadIndividuos = logicaPrincipal.ObtenerProbabilidad(Convert.ToInt32(IRInd[0]), IRPoblaciónPrueba, sumaIRTotal, probabilidadIndividuos);
                }

                logicaPrincipal.imprimirDatos(probabilidadIndividuos, "Probabilidad individuos");
                List<List<double>> probabilidadesAcumuladasPrueba = new List<List<double>>();
                probabilidadesAcumuladasPrueba = logicaPrincipal.ObtenerProbabilidadAcumulada(probabilidadIndividuos, 0, probabilidadesAcumuladasPrueba);
                logicaPrincipal.imprimirDatos(probabilidadesAcumuladasPrueba, "Probabilidades acumuladas");

                List<List<List<double>>> listaPadresPrueba = new List<List<List<double>>>();
                List<double> listaRandom = new List<double>();
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
                        Console.WriteLine("entra");
                        padre = logicaPrincipal.SeleccionarPadre(poblaciónPrueba, probabilidadesAcumuladasPrueba, numAleatorioPrueba, listaPadresPrueba);
                    }
                    padreAux = padre;
                    listaPadresPrueba.Add(padre);

                }
                foreach (List<List<double>> padre in listaPadresPrueba)
                {
                    logicaPrincipal.imprimirDatos(padre, "Padre");
                }

                double numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                List<List<List<double>>> individuosAMutar = new List<List<List<double>>>();
                List<List<List<double>>> individuosMutados = new List<List<List<double>>>();
                if (numAleatorio < 0.6) { individuosAMutar = casamiento.CruzarIndividuosPrueba(listaPadresPrueba); }
                else { individuosAMutar = listaPadresPrueba; }


                List<List<double>> individuoMutado = new List<List<double>>();

                foreach (List<List<double>> ind in individuosAMutar)
                {
                    logicaPrincipal.imprimirDatos(ind, "individuos a mutar");
                }
                for (int i = 0; i < 2; i++)
                {
                    numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                    if (numAleatorio < 0.2)
                    {

                        individuoMutado = mutación.MutarIndividuosPrueba(individuosAMutar[i], ciclo);
                        individuosMutados.Add(individuoMutado);
                        Console.WriteLine("Mutado");
                    }
                    else
                    {
                        individuoMutado = individuosAMutar[i];
                        individuosMutados.Add(individuoMutado);

                    }
                }

                foreach (List<List<double>> ind in individuosMutados)
                {
                    logicaPrincipal.imprimirDatos(ind, "individuo mutado");
                }
                List<List<List<double>>> listaIndACompetir = new List<List<List<double>>>();

                List<List<double>> ind2 = new List<List<double>>();
                List<List<double>> indAux = new List<List<double>>();
                for (int i = 0; i < 2; i++)
                {
                    List<List<double>> ind1 = new List<List<double>>();
                    ind1 = selección.SeleccionarIndividuoAleatorio(poblaciónPrueba);
                    while (ind1 == indAux)
                    {
                        ind1 = selección.SeleccionarIndividuoAleatorio(individuosMutados);
                    }
                    indAux = ind1;
                    listaIndACompetir.Add(ind1);
                }
                List<List<double>> listaIndASeleccionar = new List<List<double>>();
                List<List<List<double>>> listaIRNodos = new List<List<List<double>>>();
                
                foreach (List<List<double>> ind in listaIndACompetir)
                {
                    IRIntersecciones = new List<List<double>>();
                    logicaPrincipal.imprimirDatos(ind, "competidores");
                    foreach(List<double> nodo in ind)
                    {
                        IRIntersecciones = funciónFitness.ObtenerIR(Convert.ToInt32(nodo[0]), datosFlujoNodos, datosCicloNodos, listaFlujoSaturación, IRIntersecciones, nodo[3], 1);
                    }
                    listaIRNodos.Add(IRIntersecciones);
                }
                List<List<double>> IRCompetidores = new List<List<double>>();
                foreach (List<List<double>> IRIND in listaIRNodos)
                {
                    List<double> IRCompetidor = new List<double>();

                    sumaIRTotal = funciónFitness.SumaIR(IRIND, 0);
                    IRCompetidor.Add(sumaIRTotal);
                    IRCompetidores.Add(IRCompetidor);

                    Console.WriteLine("Suma total: "+sumaIRTotal);
                }
                List<double> IRIND1 = IRCompetidores[0];
                List<double> IRIND2 = IRCompetidores[1];
                double ganador = Math.Min(IRIND1[0], IRIND2[0]);
                List<List<double>> individuoGanador = new List<List<double>>();
                if(ganador == IRIND1[0])
                {
                    individuoGanador = listaIndACompetir[0];
                }
                else
                {
                    individuoGanador = listaIndACompetir[1];
                }

                poblaciónPrueba = selección.CrowdingPrueba(individuoGanador, poblaciónPrueba);



            }


            //logicaPrincipal.imprimirDatos(listaIndASeleccionar, "Individuos seleccionados");


            //logicaPrincipal.imprimirDatos(listaPoblación, "Población inicial:");
            //sumaIR = funciónFitness.SumaIR(IRIntersecciones, 0);
            //Console.WriteLine("IR total GEN 0: " + sumaIR);


            for (int generacion = 0; generacion < cantGeneraciones; generacion++)
            {
                List<List<double>> probabilidadNodos = new List<List<double>>();
                List<List<double>> probabilidadesAcumuladas = new List<List<double>>();
                List<List<double>> listaPadres = new List<List<double>>();
                //double numAleatorio = 0;
                //logicaPrincipal.imprimirDatos(IRIntersecciones, "Índice de Rendimiento de nodos:");
                IRIntersecciones = new List<List<double>>();
                foreach (List<double> nodo in listaPoblación)
                {
                    List<double> IRNodo = new List<double>();
                    flujo = intersección.RecuperarFlujo(nodo[0], datosFlujoNodos);
                    flujoSaturación = intersección.RecuperarFlujo(nodo[0], listaFlujoSaturación);
                    cicloNodo1 = datosCicloNodos[0];
                    ciclo = cicloNodo1[1];
                    double IRInt = funciónFitness.FFitness(flujo, nodo[3], flujoSaturación, ciclo);
                    IRNodo.Add(nodo[0]);
                    IRNodo.Add(IRInt);
                    IRIntersecciones.Add(IRNodo);
                }
                //logicaPrincipal.imprimirDatos(IRIntersecciones, "Índice de Rendimiento de nodos:");
                //logicaPrincipal.imprimirDatos(datosCicloNodos, "Datos de ciclo por nodo:");
                //logicaPrincipal.imprimirDatos(datosFlujoNodos, "Datos de flujo por nodo");
                //logicaPrincipal.imprimirDatos(listaFlujoSaturación, "Datos de flujo de saturación por nodo:");
                //logicaPrincipal.imprimirDatos(IRIntersecciones, "Índice de rendimiento por nodo:");

                sumaIR = funciónFitness.SumaIR(IRIntersecciones, 0);
                //Console.WriteLine("IR total Gen "+generacion+ ": " + sumaIR);
                for (int i = 0; i < cantIntersecciones; i++)
                {
                    probabilidadNodos = logicaPrincipal.ObtenerProbabilidad(listaNodos[i], IRIntersecciones, sumaIR, probabilidadNodos);
                }

                probabilidadesAcumuladas = logicaPrincipal.ObtenerProbabilidadAcumulada(probabilidadNodos, 0, probabilidadesAcumuladas);

                //logicaPrincipal.imprimirDatos(probabilidadNodos, "Probabilidades por nodo: ");
                //logicaPrincipal.imprimirDatos(probabilidadesAcumuladas, "Probabilidad acumulada por nodo");
                /*listaPadres = new List<List<double>>();
                listaRandom = new List<double>();
                for (int i = 0; i < 2; i++)
                {
                    List<double> padre = new List<double>();
                    numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                    //Console.WriteLine("número aleatorio: " + numAleatorio);
                    //padre = logicaPrincipal.SeleccionarPadre(listaPoblación, probabilidadesAcumuladas, numAleatorio, padre, listaPadres);
                    listaPadres.Add(padre);
                */
            }
            //logicaPrincipal.imprimirDatos(listaPadres, "Padres:");
            /*numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
            List<List<double>> individuosAMutar = new List<List<double>>();
            if (numAleatorio < 0.6){individuosAMutar = casamiento.CruzarIndividuos(listaPadres);}
            else {individuosAMutar = listaPadres;}
            //logicaPrincipal.imprimirDatos(individuosAMutar, "Individuos a mutar:");
            */
            /*cicloNodo1 = datosCicloNodos[0];
            ciclo = cicloNodo1[1];
            double entreverde = cicloNodo1[4];
            List<List<double>> individuosMutados = new List<List<double>>();
            for (int i = 0; i < 2; i++)
            {
                numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                if (numAleatorio < 0.2)
                {
                    individuosMutados.Add(mutación.MutarIndividuos(individuosAMutar[i], ciclo, entreverde));
                }
                else
                {
                    individuosMutados.Add(individuosAMutar[i]);
                }
            }
            */
            //logicaPrincipal.imprimirDatos(individuosMutados, "Individuos mutados");
            /* int cantidadIndividuosPobOrig = listaPoblación.Count;
             Random r = new Random();
             numAleatorio = r.Next(0, cantidadIndividuosPobOrig);
             List<double> ind1 = new List<double>();
             List<double> ind2 = new List<double>();
             ind1 = selección.SeleccionarIndividuoAleatorio(listaPoblación);
             ind2 = selección.SeleccionarIndividuoAleatorio(individuosMutados);
             List<List<double>> listaIndASeleccionar = new List<List<double>>();
             listaIndASeleccionar.Add(ind1);
             listaIndASeleccionar.Add(ind2);
             //logicaPrincipal.imprimirDatos(listaIndASeleccionar, "Individuos seleccionados");
             flujo = intersección.RecuperarFlujo(ind1[0], datosFlujoNodos);
             flujoSaturación = intersección.RecuperarFlujo(ind1[0], listaFlujoSaturación);
             List<double> indGanador = new List<double>();
             indGanador = selección.SelecciónPorTorneo(ind1, ind2, flujo, ciclo, flujoSaturación);
             listaPoblación = selección.Crowding(listaPoblación, indGanador);
             //logicaPrincipal.imprimirDatos(listaPoblación, "Nueva población: ");
            */
     
            //logicaPrincipal.imprimirDatos(listaPoblación, "Nueva población: ");
        }

        internal void SetSelección(Selección selección)
        {
            this.selección = selección;
        }

        internal void SetMutación(Mutación mutación)
        {
            this.mutación = mutación;
        }

        internal void SetCasamiento(Casamiento casamiento)
        {
            this.casamiento = casamiento;
        }

        internal void SetIntersección(Intersección intersección)
        {
            this.intersección = intersección;
        }
    }
}
