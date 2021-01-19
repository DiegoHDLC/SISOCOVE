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

        internal List<List<double>> MutarIndividuosPrueba(List<List<double>> individuoAMutar, double ciclo, Random random)
        {
            List<List<double>> individuoMutado = new List<List<double>>();
            LogicaPrincipal logicaPrincipal = new LogicaPrincipal();

            double posición = random.Next(0, individuoAMutar.Count);
            int i = 0;
            foreach (List<double> nodo in individuoAMutar)
            {
                double numAleatorio = random.Next(1, Convert.ToInt32(ciclo));
                while (numAleatorio+nodo[4]+4 >= ciclo) 
                { 
                    numAleatorio = random.Next(1, Convert.ToInt32(ciclo));
                    //Console.WriteLine("numRand: " + numAleatorio);
                }
                
                List<double> nuevoNodo = new List<double>();
                nuevoNodo.Add(nodo[0]);
                nuevoNodo.Add(nodo[1]);
                nuevoNodo.Add(nodo[2]);
                //double rand = r.Next(1, Convert.ToInt32(ciclo));
                //Console.WriteLine("random: " + num);
                if (i == posición)
                {
                    nuevoNodo.Add(numAleatorio);
                }
                else
                {
                    nuevoNodo.Add(nodo[3]);
                }
                
                nuevoNodo.Add(nodo[4]);
                individuoMutado.Add(nuevoNodo);
                //numAleatorio = logicaPrincipal.GenerarNumAleatorio(ciclo);
                i++;
            }
            return individuoMutado;
        }

        internal List<List<List<double>>> MutarIndividuos(List<List<List<double>>> individuosAMutar, List<double> listaRandom, List<List<List<double>>> individuosMutados, double ciclo, Random random)
        {
            LogicaPrincipal logicaPrincipal = new LogicaPrincipal();
            List<List<double>> individuoMutado = new List<List<double>>();
            double numAleatorio;
            
            for (int i = 0; i < 2; i++)
            {
                numAleatorio = logicaPrincipal.GenerarNumAleatorio(listaRandom);
                
                //Console.WriteLine("num: " + numAleatorio);
                if (numAleatorio < 0.2)
                {

                    individuoMutado = MutarIndividuosPrueba(individuosAMutar[i], ciclo, random);
                    individuosMutados.Add(individuoMutado);
                    //Console.WriteLine("Mutado");
                }
                else
                {
                    individuoMutado = individuosAMutar[i];
                    individuosMutados.Add(individuoMutado);

                }
            }
            return individuosMutados;
        }
    }
}
