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
        Mutación mutación= new Mutación();
        Selección selección= new Selección();

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
            
            List<List<double>> listaPoblación = new List<List<double>>();
            List<double> individuo = new List<double>();
            List<List<double>> datosFlujoNodos = new List<List<double>>();
            List<List<double>> datosCicloNodos = new List<List<double>>();
            List<List<double>> listaFlujoSaturación = new List<List<double>>();
            List<List<double>> IRIntersecciones = new List<List<double>>();
            List<List<double>> probabilidadNodos = new List<List<double>>();
            List<List<double>> probabilidadesAcumuladas = new List<List<double>>();
            List<int> listaNodos = new List<int>();
            List<List<double>> listaPadres = new List<List<double>>();

         int cantIntersecciones = 0;
            double sumaIR = 0;
            double suma = 0;
            double numAleatorio = 0;
            logicaPrincipal.InstanciarArchivos();
            listaNodos.Add(8);
            listaNodos.Add(10);
            listaNodos.Add(14);

            cantIntersecciones = intersección.CalcularCantIntersecciones(listaNodos);
            for (int i = 0; i < cantIntersecciones; i++)
            {
                datosFlujoNodos = intersección.ObtenerDatosFlujoNodo(listaNodos[i], datosFlujoNodos);
                datosCicloNodos = intersección.ObtenerDatosCicloNodo(listaNodos[i], datosCicloNodos);
                listaFlujoSaturación = intersección.ObtenerFlujoSaturación(listaNodos[i], datosFlujoNodos, listaFlujoSaturación);
                IRIntersecciones = funciónFitness.ObtenerIR(listaNodos[i], datosFlujoNodos, datosCicloNodos, listaFlujoSaturación, IRIntersecciones);
                listaPoblación = población.GenerarPoblación(listaNodos[i], datosCicloNodos, cantIntersecciones, listaPoblación);
            }

            //logicaPrincipal.imprimirDatos(datosCicloNodos, "Datos de ciclo por nodo:");
            //logicaPrincipal.imprimirDatos(datosFlujoNodos, "Datos de flujo por nodo");
            //logicaPrincipal.imprimirDatos(listaFlujoSaturación, "Datos de flujo de saturación por nodo:");
            //logicaPrincipal.imprimirDatos(IRIntersecciones, "Índice de rendimiento por nodo:");
            //logicaPrincipal.imprimirDatos(listaPoblación, "Población inicial:");

            sumaIR = funciónFitness.SumaIR(IRIntersecciones, sumaIR);
            for (int i = 0; i < cantIntersecciones; i++)
            {
                probabilidadNodos = logicaPrincipal.ObtenerProbabilidad(listaNodos[i], IRIntersecciones, sumaIR, probabilidadNodos);
            }
            probabilidadesAcumuladas = logicaPrincipal.ObtenerProbabilidadAcumulada(probabilidadNodos, suma, probabilidadesAcumuladas);

            //logicaPrincipal.imprimirDatos(probabilidadNodos, "Probabilidades por nodo: ");
            //logicaPrincipal.imprimirDatos(probabilidadesAcumuladas, "Probabilidad acumulada por nodo");

            listaPadres = new List<List<double>>();
            
                List<double> listaRandom = new List<double>();
            for (int i = 0; i < 2; i++)
            {
                List<double> padre = new List<double>();
                numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                Console.WriteLine("número aleatorio: " + numAleatorio);  
                padre = logicaPrincipal.SeleccionarPadre(listaPoblación, probabilidadesAcumuladas, numAleatorio, padre);
                listaPadres.Add(padre);

            }
            logicaPrincipal.imprimirDatos(listaPadres, "Padres:");
            numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
            List<List<double>> individuosAMutar = new List<List<double>>();
            if(numAleatorio < 0.6)
            {
                individuosAMutar = casamiento.CruzarIndividuos(listaPadres);
            }
            else
            {
                individuosAMutar = listaPadres;
            }
            logicaPrincipal.imprimirDatos(individuosAMutar, "Individuos a mutar:");
            
            List<double> cicloNodo1 = datosCicloNodos[0];
            double ciclo = cicloNodo1[1];
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
            logicaPrincipal.imprimirDatos(individuosMutados, "Individuos mutados");
            int cantidadIndividuosPobOrig = listaPoblación.Count;
            Random r = new Random();
            numAleatorio = r.Next(0, cantidadIndividuosPobOrig);
            List<double> ind1 = new List<double>();
            List<double> ind2 = new List<double>();
            ind1 = selección.SeleccionarIndividuoAleatorio(listaPoblación);
            ind2 = selección.SeleccionarIndividuoAleatorio(individuosMutados);
            List<List<double>> listaIndASeleccionar = new List<List<double>>();
            listaIndASeleccionar.Add(ind1);
            listaIndASeleccionar.Add(ind2);
            logicaPrincipal.imprimirDatos(listaIndASeleccionar, "Individuos seleccionados");
            double flujo = intersección.RecuperarFlujo(ind1[0], datosFlujoNodos);
            double flujoSaturación = intersección.RecuperarFlujo(ind1[0], listaFlujoSaturación);
            List<double> indGanador = new List<double>();
            indGanador = selección.SelecciónPorTorneo(ind1, ind2, flujo, ciclo, flujoSaturación);
            listaPoblación = selección.Crowding(listaPoblación, indGanador);
            Console.WriteLine("Ganador: ");
            foreach (double gen in indGanador)
            {
                Console.WriteLine(gen);
            }


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
