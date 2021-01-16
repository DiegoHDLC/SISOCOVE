using IronXL;
using SISOCOVE.Controlador;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Modelo.Intersecciones
{
    class Intersección
    {
        Coordinador miCoordinador;
        internal void SetCoordinador(Coordinador miCoordinador)
        {
            this.miCoordinador = miCoordinador;
        }

        internal List<List<double>> ObtenerDatosFlujoNodo(int nodo, List<List<double>> listaDatosFlujoNodo)
        {
         
            String path = @"C:\Users\di_eg\Desktop\Datos UOCT\Mediciones de Flujo por Periodo Centro LS.xlsx";
            SLDocument sl = new SLDocument(path);
            int iRow = 2;

            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                if (nodo == int.Parse(sl.GetCellValueAsString(iRow, 1)))
                {
                    List<double> datosFlujoNodo = new List<double>();
                    int VEQ = int.Parse(sl.GetCellValueAsString(iRow, 19));
                    int CM2E = int.Parse(sl.GetCellValueAsString(iRow, 17)); //Camión de más de dos ejes
                    int CME = int.Parse(sl.GetCellValueAsString(iRow, 16)); //Camión de dos ejeS
                    int BIU = int.Parse(sl.GetCellValueAsString(iRow, 15));
                    int BU = int.Parse(sl.GetCellValueAsString(iRow, 14)); //Bus Urbano
                    int TXB = int.Parse(sl.GetCellValueAsString(iRow, 13)); //Taxi Bus
                    int VEHPesados = CM2E + CME + BIU + BU + TXB;
                    datosFlujoNodo.Add(nodo);
                    datosFlujoNodo.Add(VEQ);
                    datosFlujoNodo.Add(VEHPesados);
                    listaDatosFlujoNodo.Add(datosFlujoNodo);
                    /*
                    for(int i = 0; i< datosFlujoNodo.Count; i++)
                    {
                        Console.Write(datosFlujoNodo[i]+" ");
                    }
                    Console.WriteLine("");
                   */
                }

                iRow++;
                if (iRow == 1681)
                {
                    break;
                }
            }
            return listaDatosFlujoNodo;
        }

        internal List<List<double>> ObtenerDatosCicloNodo(int nodo, List<List<double>> listaDatosCicloNodo)
        {
           
            String path = @"C:\Users\di_eg\Desktop\Datos UOCT\Programaciones Centro LS.xls";
            List<double> datosCicloNodo = new List<double>();
            WorkBook workbook = WorkBook.Load(path);
            WorkSheet sheet = workbook.WorkSheets[nodo];
            int ciclo = sheet.GetCellAt(63, 3).IntValue; //Ciclo
            int TF1 = sheet.GetCellAt(63, 4).IntValue; //Inicio F1 
            int TF2 = sheet.GetCellAt(63, 5).IntValue; //Inicio F2
            int EV = sheet.GetCellAt(63, 7).IntValue; //Entreverde
            int IVTF1 = sheet.GetCellAt(63, 10).IntValue; //Inicio verde F1
            int IVTF2 = sheet.GetCellAt(63, 11).IntValue; //Inicio verde F2
            int TVF1 = sheet.GetCellAt(63, 13).IntValue; //Tiempo verde F1
            int TVF2 = sheet.GetCellAt(63, 14).IntValue; //Tiempo verde F2

            datosCicloNodo.Add(nodo);
            datosCicloNodo.Add(ciclo);
            datosCicloNodo.Add(TF1);
            datosCicloNodo.Add(TF2);
            datosCicloNodo.Add(EV);
            datosCicloNodo.Add(IVTF1);
            datosCicloNodo.Add(IVTF2);
            datosCicloNodo.Add(TVF1);
            datosCicloNodo.Add(TVF2);
            /*Console.Write("nodo[" + nodo + "]");
            for (int i = 0; i < datosCicloNodo.Count; i++)
            {
                Console.Write(datosCicloNodo[i] + " ");
            }
            Console.WriteLine("");
            */
            listaDatosCicloNodo.Add(datosCicloNodo);

            return listaDatosCicloNodo;
        }

        internal int CalcularCantIntersecciones(List<int> listaNodos)
        {
            int cantIntersecciones = 0;
            foreach (int i in listaNodos)
            {
                cantIntersecciones++;
            }
            return cantIntersecciones;
        }

        internal List<List<double>> ObtenerFlujoSaturación(int nodo, List<List<double>> listaFlujoNodos, List<List<double>> listaFlujoSaturación)
        {
            double flujoSaturación = 0;


            foreach (List<double> flujoNodo in listaFlujoNodos)
            {
                List<double> listaFlujoSaturaciónNodo = new List<double>();
                if (flujoNodo[0] == nodo)
                {
                    flujoSaturación = 1900 * 0.9 * (100 / (100 + (flujoNodo[2] * 100 / flujoNodo[1])));
                    listaFlujoSaturaciónNodo.Add(nodo);
                    listaFlujoSaturaciónNodo.Add(flujoSaturación);
                    listaFlujoSaturación.Add(listaFlujoSaturaciónNodo);
                    break;
                }
            }
            return listaFlujoSaturación;
        }

        internal double RecuperarFlujo(double nodo, List<List<double>> datosFlujoNodos)
        {
            foreach(List<double> flujoNodo in datosFlujoNodos)
            {
                if(flujoNodo[0] == nodo)
                {
                    return flujoNodo[1];
                }
            }
            return 0;
        }

        
    }
}
