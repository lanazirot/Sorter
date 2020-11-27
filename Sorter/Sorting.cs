using System.Linq;
using System.Collections.Generic;

namespace Sorter {
    /// <summary>
    /// Permite utilizar Radix Sort cuando se implementa en el diseño de una clase.
    /// </summary>
    public interface IRadixable {
        /// <summary>
        /// Se debe asignar el atributo por el cual se desea ordenar.
        /// </summary>
        /// <returns>Atributo para ordenar (entero)</returns>
        int ObtenerAtributoParaOrdenar();
    }



    public class Sorting {

 
        private static int ObtenerMaximoDeObjetoRadixable<T>(T[] arreglo) where T : IRadixable {
            int mayorAhora = arreglo[0].ObtenerAtributoParaOrdenar();

            for (int i = 1; i < arreglo.Length; i++) {
                if (arreglo[i].ObtenerAtributoParaOrdenar() > mayorAhora) {
                    mayorAhora = arreglo[i].ObtenerAtributoParaOrdenar();
                }
            }

            return mayorAhora;
        }
        /// <summary>
        /// Radix Sort con objetos que implementen la interface IRadixable.
        /// </summary>
        /// <typeparam name="T">Tipo que se desea ordenar</typeparam>
        /// <see cref="RadixSort(ref int[])"/>
        /// <param name="arreglo">Arreglo de objetos de tipo T</param>
        public static void RadixSort<T>(ref T[] arreglo) where T : IRadixable {
            Queue<T>[] cubetas = new Queue<T>[10];

            for (var i = 0; i < 10; i++)
                cubetas[i] = new Queue<T>();

            int intDivision;
            int intPosicion = 1;

            int intMaximoDelArreglo = ObtenerMaximoDeObjetoRadixable(arreglo); 
            while (intMaximoDelArreglo / intPosicion > 0) {
                foreach (var intElement in arreglo) {
                    intDivision = intElement.ObtenerAtributoParaOrdenar() / intPosicion;
                    cubetas[intDivision % 10].Enqueue(intElement);
                }
                for (int i = 0, posicionEnArreglo = 0; i < 10; i++) {
                    while (cubetas[i].Count != 0) {
                        arreglo[posicionEnArreglo++] = cubetas[i].Dequeue();
                    }
                }
                intPosicion *= 10;
            }
        }


        /// <summary>
        /// Radix Sort de un arreglo de numeros enteros.
        /// </summary>
        /// <param name="arreglo">Arreglo de enteros</param>
        public static void RadixSortDS(ref int[] arreglo) {
            Queue<int>[] cubetas = new Queue<int>[10];

            for (var i = 0; i < 10; i++)
                cubetas[i] = new Queue<int>();

            int intDivision;
            int intPosicion = 1; 

            int intMaximoDelArreglo = arreglo.Max();
            while (intMaximoDelArreglo / intPosicion > 0) {
                foreach (var intElemento in arreglo) {
                    intDivision = intElemento / intPosicion;
                    cubetas[intDivision % 10].Enqueue(intElemento);
                }
                for (int i = 0, posicionEnArreglo = 0; i < 10; i++) {
                    while (cubetas[i].Count != 0) {
                        arreglo[posicionEnArreglo++] = cubetas[i].Dequeue();
                    }
                }
                intPosicion *= 10;
            }
        }

        private static int ObtenerMaximoEnteroArreglo(int[] arreglo) {
            int intMaximo = arreglo[0];
            for (int i = 1; i < arreglo.Length; i++)
                if (arreglo[i] > intMaximo)
                    intMaximo = arreglo[i];
            return intMaximo;
        }

        private static void CountingSort(int[] arreglo, int exp) {
            int[] arregloDeSalida = new int[arreglo.Length];
            int[] cubetas = new int[10];
            for (int i = 0; i < 10; i++)
                cubetas[i] = 0;
            for (int i = 0; i < arreglo.Length; i++)
                cubetas[(arreglo[i] / exp) % 10]++;
            for (int i = 1; i < 10; i++)
                cubetas[i] += cubetas[i - 1];
            for (int i = arreglo.Length - 1; i >= 0; i--) {
                arregloDeSalida[cubetas[(arreglo[i] / exp) % 10] - 1] = arreglo[i];
                cubetas[(arreglo[i] / exp) % 10]--;
            }
            for (int i = 0; i < arreglo.Length; i++)
                arreglo[i] = arregloDeSalida[i];
        }

        public static void RadixSort(ref int[] arreglo) {
            int intMaximo = ObtenerMaximoEnteroArreglo(arreglo);
            for(int i = 1; intMaximo/i>0; i *= 10) {
                CountingSort(arreglo,  i);
            }
        }
    }

}
