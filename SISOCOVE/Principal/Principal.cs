using SISOCOVE.Controlador;
using SISOCOVE.Modelo;
using SISOCOVE.Modelo.AlgoritmoGenético;
using SISOCOVE.Modelo.Intersecciones;
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
            Intersección intersección = new Intersección();
            Casamiento casamiento = new Casamiento();
            Mutación mutación = new Mutación();
            Selección selección = new Selección();

            ventanaPrincipal.SetCoordinador(miCoordinador);
            logicaPrincipal.SetCoordinador(miCoordinador);
            población.SetCoordinador(miCoordinador);
            funciónFitness.SetCoordinador(miCoordinador);
            intersección.SetCoordinador(miCoordinador);
            casamiento.SetCoordiador(miCoordinador);
            mutación.SetCoordinador(miCoordinador);
            selección.SetCoordinador(miCoordinador);


            miCoordinador.setPoblación(población);
            miCoordinador.setVentanaPrincipal(ventanaPrincipal);
            miCoordinador.setLogicaPrincipal(logicaPrincipal);
            miCoordinador.SetFunciónFitness(funciónFitness);
            miCoordinador.SetIntersección(intersección);
            miCoordinador.SetCasamiento(casamiento);
            miCoordinador.SetMutación(mutación);
            miCoordinador.SetSelección(selección);


        }
    }
}
