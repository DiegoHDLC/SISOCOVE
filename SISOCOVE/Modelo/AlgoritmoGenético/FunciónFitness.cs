using SISOCOVE.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Modelo.AlgoritmoGenético
{
    class FunciónFitness
    {
        Coordinador miCoordinador;
        internal List<List<double>> ObtenerIR(int nodo, List<List<double>> datosFlujoNodo, List<List<double>> datosCicloNodo, List<List<double>> datosSaturaciónNodo, List<List<double>> IRintersecciones, double VerdeEfectivo, int generación)
        {
            
            List<double> IRNodo = new List<double>();
            double IR = 0;
            double flujo = 0;
            double ciclo = 0;
            double verdeEfectivo = 0;
            double saturación = 0;

            foreach (List<double> datoFlujo in datosFlujoNodo)
            {
                if (datoFlujo[0] == nodo)
                {
                    flujo = datoFlujo[1];
                }
            }
            foreach (List<double> datoTiempos in datosCicloNodo)
            {
                if (datoTiempos[0] == nodo)
                {
                    ciclo = datoTiempos[1];
                    if(generación == 0)
                    {
                        verdeEfectivo = datoTiempos[4];
                    }
                    else
                    {
                        verdeEfectivo = VerdeEfectivo;
                    }
                    
                }
            }
            foreach (List<double> datoSaturación in datosSaturaciónNodo)
            {
                if (datoSaturación[0] == nodo)
                {
                    saturación = datoSaturación[1];
                }
            }
            IR = FFitness(flujo, verdeEfectivo, saturación, ciclo);
            IRNodo.Add(nodo);
            IRNodo.Add(IR);
            IRintersecciones.Add(IRNodo);

            return IRintersecciones;
        }

        internal void SetCoordinador(Coordinador miCoordinador)
        {
            this.miCoordinador = miCoordinador;
        }

        internal double SumaIR(List<List<double>> iRIntersecciones, double sumaIR)
        {
            foreach (List<double> IRNodo in iRIntersecciones)
            {
                sumaIR = sumaIR + IRNodo[1];
            }
            return sumaIR;
        }

        public double FFitness(double flujo, double verdeEfectivo, double flujoDeSaturación, double ciclo)
        {

            double q = flujo / 3600;
            double Q = (verdeEfectivo / ciclo) * flujoDeSaturación;
            //Console.WriteLine("Q: ", Q);
            double x = q / Q;
            //Console.WriteLine("x: ", x);
            double t = 15 * 60;
            double x0 = 0.67 + (flujoDeSaturación * verdeEfectivo) / 600;
            //Console.WriteLine("x0: ", x0);
            double N = ((Q * t) / 4) * ((x - 1) + Math.Sqrt(Math.Pow((x - 1), 2) + (12 * (x - x0)) / (Q * t)));
            //Console.WriteLine("N: ", N);
            double D = Math.Abs(N * x);
            return D;
        }

        internal List<List<double>> DeterminarIRCompetidores(List<List<double>> IRCompetidores, List<List<List<double>>> listaIRNodos)
        {
            double sumaIRTotal;
            foreach (List<List<double>> IRIND in listaIRNodos)
            {
                List<double> IRCompetidor = new List<double>();

                sumaIRTotal = SumaIR(IRIND, 0);
                IRCompetidor.Add(sumaIRTotal);
                IRCompetidores.Add(IRCompetidor);

                Console.WriteLine("Suma total: " + sumaIRTotal);
            }
            return IRCompetidores;
        }

        internal List<List<List<double>>> calcularIRNodos(List<List<List<double>>> listaIRNodos, List<List<double>> IRIntersecciones, List<List<double>> datosFlujoNodos, List<List<double>> datosCicloNodos, List<List<double>> listaFlujoSaturación, List<List<List<double>>> listaIndACompetir)
        {

            foreach (List<List<double>> ind in listaIndACompetir)
            {
                IRIntersecciones = new List<List<double>>();
                foreach (List<double> nodo in ind)
                {
                    IRIntersecciones = ObtenerIR(Convert.ToInt32(nodo[0]), datosFlujoNodos, datosCicloNodos, listaFlujoSaturación, IRIntersecciones, nodo[3], 1);
                }
                listaIRNodos.Add(IRIntersecciones);
            }
            return listaIRNodos;
        }
    }
}
