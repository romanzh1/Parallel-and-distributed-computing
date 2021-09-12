using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Parallel_sorts
{
    class Sorts
    {
        Random c = new Random();
        public int N;
        public int p;
        public int[] arr;
        int left;
        int right;
        public Stopwatch sw;
        public bool fin;

        public Sorts()
        {
            fin = false;
            N = 0;
        }

        public int[] genArr()
        {
            arr = new int[N];
            for (int i = 0; i < N; i++)
            {
                arr[i] = c.Next(0, 100000);
            }
            return arr;
        }

        public void sortChoice()
        {
            p = 0;
            sw = Stopwatch.StartNew();
            for (int i = 0; i < N; i++)
            {
                int t = 0;
                int min = arr[i];
                int ke = 0;
                for (int q = i; q <= N - 1; q++)
                {
                    if (arr[q] < min)
                    {
                        min = arr[q];
                        ke = q;
                        t = t + 1;
                    }
                }
                if (t != 0)
                {
                    p++; //перестановки
                    arr[ke] = arr[i];
                    arr[i] = min;
                }

            }
            sw.Stop();
            fin = true;
        }

        public void sortInsert()
        {
            p = 0;
            sw = Stopwatch.StartNew();
            for (int q = 1; q < N; q++)
            {
                int ke = arr[q];
                int w = q;
                int t = 0;
                while (w > 0 && ke < arr[w - 1])
                {
                    arr[w] = arr[w - 1];
                    w--;
                    t++;
                }
                if (t != 0)
                {
                    arr[w] = ke;
                    p++; //перестановки
                }
                t = 0;
            }
            sw.Stop();
            fin = true;
        }

        public void superSortQuick()
        {
            p = 0;
            left = 0;
            right = N - 1;
            sw = Stopwatch.StartNew();
            sortQ(arr, left, right);
            sw.Stop();
            fin = true;
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
                    p++;
                    arr[left] = arr[right]; // перемещаем элемент [right] на место разрешающего
                    left++; // сдвигаем левую границу вправо
                }
                while ((arr[left] <= pivot) && (left < right))
                    left++; // сдвигаем левую границу пока элемент [left] меньше [pivot]
                if (left != right) // если границы не сомкнулись
                {
                    p++;
                    arr[right] = arr[left]; // перемещаем элемент [left] на место [right]
                    right--; // сдвигаем правую границу вправо
                }
            }
            p++;
            arr[left] = pivot; // ставим разрешающий элемент на место
            pivot = left;
            left = l_hold;
            right = r_hold;
            if (left < pivot) // Рекурсивно вызываем сортировку для левой и правой части массива
                sortQ(arr, left, pivot - 1);
            if (right > pivot)
                sortQ(arr, pivot + 1, right);
        }

        public void sortQuick()
        {
            p = 0;
            left = 0;
            right = N - 1;
            sw = Stopwatch.StartNew();
            QuickSort(arr, left, right);
            sw.Stop();
            fin = true;
        }

        private void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        //O(n)
        private int Partition(int[] array, int init, int end)
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

        private void Exchange(int[] array, int i, int j)
        {
            p++;
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
