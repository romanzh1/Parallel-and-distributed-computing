using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_sorts
{
    class Sorts
    {
        Random c = new Random();
        int N;
        public int p;

        public Sorts()
        {
            N = 0;
            p = 0;
        }

        public int[] genArr(int lenArr)
        {
            N = lenArr;
            int[] arr = new int[lenArr];
            for (int i = 0; i < lenArr; i++)
            {
                arr[i] = c.Next(0, 100000);
            }
            return arr;
        }

        public void sortChoice(int[] arr)
        {
            for (int i = 0; i < N; i++)
            {
                int t = 0;
                int min = arr[i];
                int ke = 0;
                for (int q = i; q <= N - 1; q++)
                {
                    //s++; сравнения
                    if (arr[q] < min)
                    {
                        min = arr[q];
                        ke = q;
                        t = t + 1;
                    }
                }
                if (t != 0)
                {
                    //p++; перестановки
                    arr[ke] = arr[i];
                    arr[i] = min;
                }

            }
        }

        public void sortInsert(int[] arr)
        {
            for (int q = 1; q < N; q++)
            {
                int ke = arr[q];
                int w = q;
                //s++; сравнения
                int t = 0;
                while (w > 0 && ke < arr[w - 1])
                {
                    arr[w] = arr[w - 1];
                    w--;
                    //s++; сравнения
                    t++;
                }
                if (t != 0)
                {
                    arr[w] = ke;
                    // p++; перестановки
                }
                t = 0;
            }
        }

        public void superSortQuick(int[] arr)
        {
            int left = 1;
            int right = arr.Length - 1;
            sortQ(arr, left, right);
        }

        private void sortQ(int[] arr, int left, int right)
        {
            int pivot; // разрешающий элемент
            int l_hold = left; //левая граница
            int r_hold = right; // правая граница
            pivot = arr[left];
            while (left < right) // пока границы не сомкнутся
            {
                while ((arr[right] >= pivot) && (left < right))
                    right--; // сдвигаем правую границу пока элемент [right] больше [pivot]
                if (left != right) // если границы не сомкнулись
                {
                    arr[left] = arr[right]; // перемещаем элемент [right] на место разрешающего
                    left++; // сдвигаем левую границу вправо
                }
                while ((arr[left] <= pivot) && (left < right))
                    left++; // сдвигаем левую границу пока элемент [left] меньше [pivot]
                if (left != right) // если границы не сомкнулись
                {
                    arr[right] = arr[left]; // перемещаем элемент [left] на место [right]
                    right--; // сдвигаем правую границу вправо
                }
            }
            arr[left] = pivot; // ставим разрешающий элемент на место
            pivot = left;
            left = l_hold;
            right = r_hold;
            if (left < pivot) // Рекурсивно вызываем сортировку для левой и правой части массива
                sortQ(arr, left, pivot - 1);
            if (right > pivot)
                sortQ(arr, pivot + 1, right);
        }

        public void QuickSort(int[] array, int init, int end)
        {
            if (init < end)
            {
                int pivot = Partition(array, init, end);
                QuickSort(array, init, pivot - 1);
                QuickSort(array, pivot + 1, end);
            }
        }

        //O(n)
        private static int Partition(int[] array, int init, int end)
        {
            int last = array[end];
            int i = init - 1;
            for (int j = init; j < end; j++)
            {
                if (array[j] <= last)
                {
                    i++;
                    Exchange(array, i, j);
                }
            }
            Exchange(array, i + 1, end);
            return i + 1;
        }

        private static void Exchange(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
