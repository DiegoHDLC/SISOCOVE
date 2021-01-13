using SISOCOVE.Controlador;
using SISOCOVE.Modelo;
using SISOCOVE.Modelo.AlgoritmoGenético;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Principal
{
    class Principal
    {
        public static void main()
        {
            Coordinador miCoordinador = new Coordinador();
            VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
            LogicaPrincipal logicaPrincipal = new LogicaPrincipal();
            Población población = new Población();
            FunciónFitness funciónFitness = new FunciónFitness();

            ventanaPrincipal.SetCoordinador(miCoordinador);
            logicaPrincipal.SetCoordinador(miCoordinador);
            población.SetCoordinador(miCoordinador);
            funciónFitness.SetCoordinador(miCoordinador);


            miCoordinador.setPoblación(población);
            miCoordinador.setVentanaPrincipal(ventanaPrincipal);
            miCoordinador.setLogicaPrincipal(logicaPrincipal);
            miCoordinador.SetFunciónFitness(funciónFitness);


        }
    }
}
