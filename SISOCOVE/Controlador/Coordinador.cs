using SISOCOVE.Modelo;
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
        VentanaPrincipal ventanaPrincipal;
        LogicaPrincipal logicaPrincipal;
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

        public void Evaluar(int cantPoblación)
        {
            logicaPrincipal = new LogicaPrincipal();
            List<List<double>> población = new List<List<double>>();
            List<double> individuo = new List<double>();
            List<List<double>> datosFlujoNodos = new List<List<double>>();
            List<List<double>> datosCicloNodos = new List<List<double>>();
            List<List<double>> listaFlujoSaturación = new List<List<double>>();
            List<List<double>> IRIntersecciones = new List<List<double>>();
            List<List<double>> probabilidadNodos = new List<List<double>>();
            List<List<double>> probabilidadesAcumuladas = new List<List<double>>();
            List<double> probabilidad = new List<double>();

            List<int> listaNodos = new List<int>();
            double sumaIR = 0;
          
            String path = @"C:\Users\di_eg\Desktop\Datos UOCT\Mediciones de Flujo por Periodo Centro LS.xlsx";
            SLDocument sl = new SLDocument(path);
            listaNodos.Add(8);
            listaNodos.Add(10);
            listaNodos.Add(14);
            int cantIntersecciones = 0;
            cantIntersecciones = logicaPrincipal.CalcularCantIntersecciones(listaNodos);
            for (int i = 0; i < cantIntersecciones; i++)
            {
                datosFlujoNodos = logicaPrincipal.ObtenerDatosFlujoNodo(listaNodos[i]);
                datosCicloNodos = logicaPrincipal.ObtenerDatosCicloNodo(listaNodos[i]);
                listaFlujoSaturación = logicaPrincipal.ObtenerFlujoSaturación(listaNodos[i], datosFlujoNodos, listaFlujoSaturación);
                IRIntersecciones = logicaPrincipal.ObtenerIR(listaNodos[i], datosFlujoNodos, datosCicloNodos, listaFlujoSaturación);
               
                foreach (List<double> IRNodo in IRIntersecciones)
                {
                    Console.WriteLine("IR Nodo[" + IRNodo[0] + "]: " + IRNodo[1]);
                }

                población = logicaPrincipal.GenerarPoblación(listaNodos, datosCicloNodos, cantIntersecciones);
                sumaIR = logicaPrincipal.SumaIR(IRIntersecciones, sumaIR);
                
            }
            cantIntersecciones = 0;
            for (int i = 0; i < cantIntersecciones; i++)
            {
                probabilidadNodos = logicaPrincipal.ObtenerProbabilidad(listaNodos[i], probabilidad, IRIntersecciones, sumaIR);
            }
            for (int i = 0; i < cantIntersecciones; i++)
            {
                probabilidadesAcumuladas = logicaPrincipal.ObtenerProbabilidadAcumulada(listaNodos[i], probabilidadNodos);
            }

            Console.WriteLine("IR TOTAL:"+sumaIR);
        }
    }
}
