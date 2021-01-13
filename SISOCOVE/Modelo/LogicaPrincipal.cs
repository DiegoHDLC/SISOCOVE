using IronXL;
using SISOCOVE.Controlador;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SISOCOVE.Modelo
{

    class LogicaPrincipal
    {

        Coordinador miCoordinador;
        internal void SetCoordinador(Coordinador miCoordinador)
        {
            this.miCoordinador = miCoordinador;
            throw new NotImplementedException();
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

    

        internal double SumaIR(List<List<double>> iRIntersecciones, double sumaIR)
        {
            
            int i = 0;
            foreach(List<double> IRNodo in iRIntersecciones)
            {
                sumaIR = sumaIR + IRNodo[1];
                i++;
            }
            
            return sumaIR;
        }

        internal List<List<double>> ObtenerProbabilidad(int nodo, List<double> probabiliad, List<List<double>> iRIntersecciones, double sumaIR)
        {
            List<List<double>> probabilidadNodos = new List<List<double>>();
           
            foreach(List<double> IRNodo in iRIntersecciones)
            {
                if (IRNodo[0] == nodo)
                {
                    probabiliad.Add(nodo);
                    probabiliad.Add(IRNodo[1] / sumaIR);
                    probabilidadNodos.Add(probabiliad);
                }

            }

            return probabilidadNodos;
        }

        internal List<List<double>> ObtenerProbabilidadAcumulada(int nodo, List<List<double>> probabilidadNodos, double suma)
        {
            List<double> probabilidadAcumuladaNodo = new List<double>();
            List<List<double>> probabilidadAcumuladaNodos = new List<List<double>>();
            
            foreach(List<double> probNodo in probabilidadNodos)
            {
                if(probNodo[1] == nodo)
                {
                  
                        
                    suma = probNodo[1]+ suma;
                    probabilidadAcumuladaNodo.Add(nodo);
                    probabilidadAcumuladaNodo.Add(suma);
                    
                   
                    probabilidadAcumuladaNodos.Add(probabilidadAcumuladaNodo);
                }
            }

            
            return probabilidadNodos;
        }

  

        internal List<List<double>> ObtenerFlujoSaturación(int nodo, List<List<double>> listaFlujoNodos, List<List<double>> listaFlujoSaturación)
        {
            double flujoSaturación = 0;

            List<double> listaFlujoSaturaciónNodo = new List<double>();
            foreach (List<double> flujoNodo in listaFlujoNodos)
            {
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

        internal List<List<double>> ObtenerDatosCicloNodo(int nodo)
        {
            List<List<double>> listaDatosCicloNodo = new List<List<double>>();
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

        internal List<List<double>> ObtenerDatosFlujoNodo(int nodo)
        {
            List<List<double>> listaDatosFlujoNodo = new List<List<double>>();
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

        


        }

      
  


    
}
