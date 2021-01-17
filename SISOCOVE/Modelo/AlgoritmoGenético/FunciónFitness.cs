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
            double x = q / Q;
            double t = 15 * 60;
            double x0 = 0.67 + (flujoDeSaturación * verdeEfectivo) / 600;
            double N = ((Q * t) / 4) * ((x - 1) + Math.Sqrt(Math.Pow((x - 1), 2) + (12 * (x - x0)) / (Q * t)));
            double D = Math.Abs(N * x);
            return D;
        }
    }
}
