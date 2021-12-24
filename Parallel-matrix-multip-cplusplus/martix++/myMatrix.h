#pragma once
class myMatrix
{

public: 
	int m;
	double** matrix;
	myMatrix(int N);
	myMatrix(double** matr, int size);
	myMatrix* Multi(myMatrix B);
	double getSpur();
	myMatrix* MultiParal(myMatrix B);
};

