using SISOCOVE.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISOCOVE.Modelo.AlgoritmoGenético
{
    class Mutación
    {
        Coordinador miCoordinador;
        internal void SetCoordinador(Coordinador miCoordinador)
        {
            this.miCoordinador = miCoordinador;
        }

        internal List<double> MutarIndividuos(List<double> individuoAMutar, double ciclo, double entreverde)
        {
            bool verificado = false;
            while (verificado == false) {
                Random r = new Random();
                int genElegido = r.Next(1, 4);
                int genbin = r.Next(0, 1);
                int genNoBin = r.Next(1, Convert.ToInt32(ciclo));
                if (genElegido == 1)
                {
                    individuoAMutar[1] = genbin;
                }
                if (genElegido != 1)
                {
                    individuoAMutar[3] = genNoBin;
                }
                verificado = verificarAbominación(individuoAMutar, entreverde, ciclo);
            }
            return individuoAMutar;
        }

        private bool verificarAbominación(List<double> individuoAMutar, double entreverde, double ciclo)
        {
            double gen1 = individuoAMutar[1];
            double gen2 = individuoAMutar[2];
            double gen3 = individuoAMutar[3];
            double gen4 = individuoAMutar[4];
 
            if (gen3+gen4+entreverde <=ciclo)
            {
                return true;
            }
            return false;
        }

        internal List<List<double>> MutarIndividuosPrueba(List<List<double>> individuoAMutar, double ciclo)
        {
            List<List<double>> individuoMutado = new List<List<double>>();
            
            Random r = new Random();
            foreach (List<double> nodo in individuoAMutar)
            {
                List<double> nuevoNodo = new List<double>();
                nuevoNodo.Add(nodo[0]);
                nuevoNodo.Add(nodo[1]);
                nuevoNodo.Add(nodo[2]);
                nuevoNodo.Add(r.Next(1, Convert.ToInt32(ciclo)));
                nuevoNodo.Add(nodo[4]);
                individuoMutado.Add(nuevoNodo);
            }
            return individuoMutado;
        }
    }
}
