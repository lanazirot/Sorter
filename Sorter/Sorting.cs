using System;
using System.Linq;
using DS;
using System.Collections.Generic;

namespace Sorter {
    /// <summary>
    /// Radixable is an Radix Sort needed interface for implement this sorting
    /// with objects. Object needs an integer Primary Key; this PK can be asigned
    /// as any of class Properties / Attributes. Make sure implement correctly this
    /// interface for a great Radix Sort.
    /// </summary>
    public interface IRadixable {
        /// <summary>
        /// Function to asign and return the class' primary key
        /// </summary>
        /// <returns>Primary key from class (any int data)</returns>
        int GetPK();
    }



    public class Sorting {

        private static void Swap<T>(ref T a, ref T b) {
            T auxiliar = a;
            a = b;
            b = auxiliar;
        }

        private static void BubbleSortWithSignal<T>(ref T[] arreglo) where T : IComparable<T> {
            bool blnMovimiento;
            for (var i = 0; i < arreglo.Length; i++) {
                blnMovimiento = false;
                for (var j = 0; j < arreglo.Length - 1; j++) {
                    if (arreglo[j].CompareTo(arreglo[j + 1]) == 1) {
                        Swap(ref arreglo[j], ref arreglo[j + 1]);
                        blnMovimiento = true;
                    }
                }
                if (!blnMovimiento) break;
            }
        }

        private static void BubbleSortLeft<T>( ref T[] arreglo) where T : IComparable<T> {
            for (var i = 0; i < arreglo.Length; i++) {
                for (var j = 0; j < arreglo.Length - i - 1; j++) {
                    if (arreglo[j+1].CompareTo(arreglo[j]) == 1) {
                        Swap(ref arreglo[j+1], ref arreglo[j]);
                    }
                }
            }
        }

        private static void BubbleSortRight<T>(ref T[] arreglo) where T : IComparable<T> {
            for (var i = 0; i < arreglo.Length; i++) {
                for (var j = 0; j < arreglo.Length - i - 1; j++) {
                    if (arreglo[j].CompareTo(arreglo[j+1]) == 1) {
                        Swap(ref arreglo[j], ref arreglo[j+1]);
                    }
                }
            }
        }

        public static void BubbleSort<T>(ref T[] arreglo, BubbleSortingType bubbleSortingType 
            = BubbleSortingType.Signal) where T : IComparable<T> {
            switch (bubbleSortingType) {
                case BubbleSortingType.Signal:
                    BubbleSortWithSignal(ref arreglo);
                    break;
                case BubbleSortingType.Left:
                    BubbleSortLeft(ref arreglo);
                    break;
                case BubbleSortingType.Right:
                    BubbleSortRight(ref arreglo);
                    break;
            }
        }


        private static int GetMaxFromRadixable<T>(T[] arreglo) where T : IRadixable {
            int maxNow = arreglo[0].GetPK();

            for(int i = 1; i < arreglo.Length; i++) {
                if (arreglo[i].GetPK() > maxNow) {
                    maxNow = arreglo[i].GetPK();
                }
            }

            return maxNow;
        }

        public static void RadixSortObject<T>(ref T[] arreglo) where T : IRadixable{
            //Declaro 10 cubetas
            LinkedList<T>[] buckets = new LinkedList<T>[10];

            for (var i = 0; i < 10; i++)
                //Creo 10 cubetas
                buckets[i] = new LinkedList<T>();

            int intRadix; //Me dice en que cubeta me voy a ubicar
            int intPosition = 1; //Me dice la posicion del digito (unidades, decenas, centenas, miles)

            int intMaxFromArray = GetMaxFromRadixable(arreglo); // arreglo.Max(); //El mayor elemento del arreglo
            while (intMaxFromArray / intPosition > 0) {
                foreach (var intElement in arreglo) {
                    intRadix = intElement.GetPK() / intPosition;
                    buckets[intRadix % 10].AddLast(intElement);
                }
                for (int i = 0, a = 0; i < 10; i++) {
                    foreach (var j in buckets[i]) {
                        arreglo[a++] = j;
                    }
                    buckets[i].Clear();
                }
                intPosition *= 10;
            }

        }



        public static void RadixSort(ref int[] arreglo)  {
            //Declaro 10 cubetas
            ListaSimpleOrdenada<int>[] buckets = new ListaSimpleOrdenada<int>[10];

            for (var i = 0; i < 10; i++)
                //Creo 10 cubetas
                buckets[i] = new ListaSimpleOrdenada<int>(true, false);

            int intRadix; //Me dice en que cubeta me voy a ubicar
            int intPosition = 1; //Me dice la posicion del digito (unidades, decenas, centenas, miles)

            int intMaxFromArray = arreglo.Max(); //El mayor elemento del arreglo
            while (intMaxFromArray/intPosition > 0) {
                foreach(var intElement in arreglo) {
                    intRadix = intElement / intPosition;
                    buckets[intRadix % 10].Agregar(intElement);
                }
                for(int i = 0, a = 0; i < 10; i++) {
                    foreach(var j in buckets[i]) {
                        arreglo[a++] = j;
                    }
                    buckets[i].Limpiar();
                }
                intPosition *= 10;
            }

        }

        public enum BubbleSortingType {
            Left, Right, Signal
        }

    }
}
