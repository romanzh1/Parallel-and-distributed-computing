// martix++.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include "myMatrix.h"
#include <time.h>
using namespace std;

int main()
{
    cout << "Enter size of Matrix" << endl;
    int m;
    cin >> m;
    myMatrix a(m);
    myMatrix b(m);
    clock_t t1 = clock();
    myMatrix* x = a.Multi(b);
    t1 = clock() - t1;
    cout << "Time: " << (float)t1 << endl;
    cout <<  "Spur: " << x->getSpur() << endl;
    cout << endl;
    t1 = clock();
    myMatrix* x2 = a.MultiParal(b);
    t1 = clock() - t1;
    cout << "Time: " << (float)t1 << endl;
    cout << "Spur: " << x2->getSpur() << endl;
    cout << endl;
    /*for (int i = 0; i < 2; i++)
    {
        cout << x->matrix[i][0] << " " << x->matrix[i][1] << endl;
    }*/
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
