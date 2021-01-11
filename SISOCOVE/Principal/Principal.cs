using SISOCOVE.Controlador;
using SISOCOVE.Modelo;
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

            ventanaPrincipal.SetCoordinador(miCoordinador);
            logicaPrincipal.SetCoordinador(miCoordinador);

            miCoordinador.setVentanaPrincipal(ventanaPrincipal);
            miCoordinador.setLogicaPrincipal(logicaPrincipal);

        }
    }
}
