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
            this.población = new Población();
            intersección = new Intersección();
            casamiento = new Casamiento();
            mutación = new Mutación();
            selección = new Selección();
            List<int> listaNodos = new List<int>();
            List<List<double>> listaPoblación = new List<List<double>>();
            List<double> cicloNodo1 = new List<double>();
            int cantGeneraciones = 1000;
            logicaPrincipal.InstanciarArchivos();
            double ciclo = 0;

            double sumaIR;
            listaNodos.Add(8);
            listaNodos.Add(10);
            listaNodos.Add(14);
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
                individuoPrueba = this.población.GenerarPoblación(listaNodos[i], datosCicloNodos, cantIntersecciones, individuoPrueba);
                //listaPoblación = población.GenerarPoblación(listaNodos[i], datosCicloNodos, cantIntersecciones, listaPoblación);
            }

            List<List<List<double>>> población = new List<List<List<double>>>();
            //logicaPrincipal.imprimirDatos(individuoPrueba, "Individuo prueba: ");
            Random rand = new Random();

            cicloNodo1 = datosCicloNodos[0];
            ciclo = cicloNodo1[1];

            for (int i = 0; i < 3; i++)
            {
                List<List<double>> individuoPruebaVEIncent = new List<List<double>>();
                double nuevoEV = rand.Next(1, Convert.ToInt32(ciclo));
                int j = 0;
                foreach (List<double> nodo in individuoPrueba)
                {
                    List<double> nodoPrueba = new List<double>();
                    nodoPrueba.Add(nodo[0]);
                    nodoPrueba.Add(nodo[1]);
                    nodoPrueba.Add(nodo[2]);
                    nodoPrueba.Add(nuevoEV);
                    nodoPrueba.Add(nodo[4]);
                    individuoPruebaVEIncent.Add(nodoPrueba);
                    j++;
                }
                población.Add(individuoPruebaVEIncent);
            }
            logicaPrincipal.imprimirDatosGrandes(población, "Individuo nuevo");

            for (int generacion = 0; generacion < cantGeneraciones; generacion++)
            {
                Console.WriteLine("Generación:" + generacion);
                int cont = 0;
            List<List<double>> IRPoblaciónPrueba = new List<List<double>>();
            foreach (List<List<double>> ind in población)
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
                logicaPrincipal.ImprimirDatos(IRIntersecciones, "IR Individuo ");
                sumaIR = funciónFitness.SumaIR(IRIntersecciones, 0);
                IRIndividuoPrueba.Add(cont);
                IRIndividuoPrueba.Add(sumaIR);
                IRPoblaciónPrueba.Add(IRIndividuoPrueba);

                cont++;
            }
            logicaPrincipal.ImprimirDatos(IRPoblaciónPrueba, "IR Población");
            double sumaIRTotal = funciónFitness.SumaIR(IRPoblaciónPrueba, 0);

            //Console.WriteLine("Suma IR total población: "+ sumaIRTotal);
            List<List<double>> probabilidadIndividuos = new List<List<double>>();
            foreach (List<double> IRInd in IRPoblaciónPrueba)
            {
                probabilidadIndividuos = logicaPrincipal.ObtenerProbabilidad(Convert.ToInt32(IRInd[0]), IRPoblaciónPrueba, sumaIRTotal, probabilidadIndividuos);
            }

            logicaPrincipal.ImprimirDatos(probabilidadIndividuos, "Probabilidad individuos");
            List<List<double>> probabilidadesAcumuladasPrueba = new List<List<double>>();
            probabilidadesAcumuladasPrueba = logicaPrincipal.ObtenerProbabilidadAcumulada(probabilidadIndividuos, 0, probabilidadesAcumuladasPrueba);
            logicaPrincipal.ImprimirDatos(probabilidadesAcumuladasPrueba, "Probabilidades acumuladas");

            List<List<List<double>>> listaPadresPrueba = new List<List<List<double>>>();
            List<double> listaRandom = new List<double>();
            listaPadresPrueba = selección.SeleccionarPadres(listaRandom, población, probabilidadesAcumuladasPrueba, listaPadresPrueba);
                
            logicaPrincipal.imprimirDatosGrandes(listaPadresPrueba, "Padre");
          
            double numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);

            List<List<List<double>>> individuosAMutar = new List<List<List<double>>>();
            if (numAleatorio < 0.6) {
                    //Console.WriteLine("entra a cruzar");
                    individuosAMutar = casamiento.CruzarIndividuos(listaPadresPrueba);
                    //individuosAMutar = casamiento.CruceAritmético(listaPadresPrueba);
                }
               
            else { individuosAMutar = listaPadresPrueba; }

                logicaPrincipal.imprimirDatosGrandes(individuosAMutar, "Hijos");
            List<List<List<double>>> individuosMutados = new List<List<List<double>>>();
                Random random = new Random();
                individuosMutados = mutación.MutarIndividuos(individuosAMutar, listaRandom, individuosMutados, ciclo, random);
               
            logicaPrincipal.imprimirDatosGrandes(individuosMutados, "individuo mutado");

            List<List<List<double>>> listaIndACompetir = new List<List<List<double>>>(); 
            listaIndACompetir = selección.SeleccionarCompetidores(población, individuosMutados, listaIndACompetir);

                logicaPrincipal.imprimirDatosGrandes(listaIndACompetir, "Competidores: ");
              
            List<List<double>> listaIndASeleccionar = new List<List<double>>();
            List<List<List<double>>> listaIRNodos = new List<List<List<double>>>();
            listaIRNodos = funciónFitness.calcularIRNodos(listaIRNodos, IRIntersecciones, datosFlujoNodos, datosCicloNodos, listaFlujoSaturación, listaIndACompetir);
                
            List<List<double>> IRCompetidores = new List<List<double>>();
            IRCompetidores = funciónFitness.DeterminarIRCompetidores(IRCompetidores, listaIRNodos);
                //logicaPrincipal.ImprimirDatos(IRCompetidores, "IR Competidores");
            List<double> IRIND1 = IRCompetidores[0];
            double ganador = selección.Ganador(IRCompetidores);

            List<List<double>> individuoGanador = new List<List<double>>();
            individuoGanador = selección.SelecciónPorTorneo(ganador, individuoGanador, listaIndACompetir, IRIND1);
                logicaPrincipal.ImprimirDatos(individuoGanador, "Ganador");
            List<double> listaDistancias = new List<double>();
            listaDistancias = selección.Crowding(individuoGanador, población, listaDistancias);
                
            double posición = 0;
            posición = selección.PosiciónIndSemejante(listaDistancias, posición, listaDistancias[0]);
                //Console.WriteLine("Posición: "+posición);
            logicaPrincipal.imprimirDatosGrandes(población,"Población Original");

            población = selección.InsertarIndividuoGanador(población, individuoGanador, posición);
            logicaPrincipal.imprimirDatosGrandes(población, "Población final");


               
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
