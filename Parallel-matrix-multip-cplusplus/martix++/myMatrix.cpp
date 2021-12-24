#include "myMatrix.h"
#include <iostream>
#include <cstdlib>
#include <ctime>
using namespace std;

	myMatrix::myMatrix(int m) {
		this->m = m;
		matrix = new double* [m];
		for (int i = 0; i < m; i++)
		{
			matrix[i] = new double[m];
			for (int j = 0; j < m; j++)
			{
				matrix[i][j] = 0.001 * (rand() % 101);
			}
		}
	}
	myMatrix::myMatrix(double** matr,int size) {
		matrix = matr;
		m = size;
	}

	myMatrix* myMatrix::Multi(myMatrix B) {
		double** a = this->matrix;
		double** b = B.matrix;
		double** c = new double* [B.m];
		for (int i = 0; i < B.m; i++)
		{
			c[i] = new double[B.m];
			for (int j = 0; j < B.m; j++)
			{
				c[i][j] = 0;
				for (int k = 0; k < B.m; k++)
					c[i][j] += a[i][k] * b[k][j];
			}
		}
		return new myMatrix(c,B.m);
	}
	myMatrix* myMatrix::MultiParal(myMatrix B)
	{
		double** a = this->matrix;
		double** b = B.matrix;
		double** c = new double* [B.m];

		#pragma omp parallel for num_threads(6)
		for (int i = 0; i < B.m; i++)
		{
			#pragma omp critical
			c[i] = new double[B.m];
			for (int j = 0; j < B.m; j++)
			{
				c[i][j] = 0;
				for (int k = 0; k < B.m; k++)
					c[i][j] += a[i][k] * b[k][j];
			}
		}
		return new myMatrix(c, B.m);
	}

	double myMatrix::getSpur() {
		double spur = 0;
		for (int i = 0; i < m; i++)
		{
			spur += matrix[i][i];
		}
		return spur;
	}