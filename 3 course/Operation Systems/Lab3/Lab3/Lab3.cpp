// Lab3.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <iostream>
#include <Windows.h>
#include <vector>

using namespace std;

void SwapArrayElements(int* a, int i, int j)
{
	int temp;
	temp = a[i];
	a[i] = a[j];
	a[j] = temp;

}

struct Param {
	int* array;
	int capacity;
	int k;

};

DWORD WINAPI work(LPVOID workParam)
{
	Param *Params = reinterpret_cast<Param*>(workParam);
	int SleepTime;
	cout << "Write amount of time for sleeping: \n";
	cin >> SleepTime;
	cout << endl;
	HANDLE hNewSemaphore = OpenSemaphore(SEMAPHORE_MODIFY_STATE, true, L"hcs");
	int RightBorder = Params->capacity; // index of last added non-unique element, by default we dont have any and in proccess, we move border to th left with addind non-uniq els
	bool isUnique = false;
	int UniqElements = 0;
	for (int i = 0; i < RightBorder; i++)
	{
		int curElem = Params->array[i];
		for (int j = i + 1; j < Params->capacity; j++)
		{
			if (curElem == Params->array[j])
			{
				SwapArrayElements(Params->array, i--, --RightBorder);
				isUnique = false;
				break;
			}
			isUnique = true;
			
		}
		if (isUnique)
		{
			UniqElements++;
			ReleaseSemaphore(hNewSemaphore, 1, NULL);
			Sleep(SleepTime);
		}
	}
	ReleaseSemaphore(hNewSemaphore, Params->capacity - UniqElements, NULL);

	return 0;
}

DWORD WINAPI SumElement(LPVOID sumelementParam)
{
	Param *Params = reinterpret_cast<Param*>(sumelementParam);
	HANDLE hNewSemaphore = OpenSemaphore(SYNCHRONIZE, true, L"hbsi");
	WaitForSingleObject(hNewSemaphore, INFINITE);

	HANDLE hNewSemaphoreOut = OpenSemaphore(SEMAPHORE_ALL_ACCESS, true, L"hbso");
	int Sum = 0;
	for (int i = 0; i < Params->k; i++)
	{
		Sum += Params->array[i];
	}
	cout << endl<< "Sum of first k Elements is "<<  Sum << endl;
	ReleaseSemaphore(hNewSemaphoreOut, 1, NULL);

	return 0;
}



int main()
{
		
	HANDLE hWork, hSumElement, hCountSemaphore, hBinarySemaphoreIn, hBinarySemaphoreOut;
	DWORD idWork, idSumElement;
	int *array;
	int Capacity, k;
	cout << "Write capacity of array:\n";
	cin >> Capacity;
	array = new int[Capacity];
	cout << "Write elements of array:\n";
	for (int i = 0; i < Capacity; i++)
		cin >> array[i];
	cout << "\nWrite k:\n";
	cin >> k;

	Param myParams;
	myParams.array = array;
	myParams.capacity = Capacity;
	myParams.k = k;


	hCountSemaphore = CreateSemaphore(NULL, 0, Capacity, L"hcs");
	hBinarySemaphoreIn = CreateSemaphore(NULL, 0, 1, L"hbsi");
	hBinarySemaphoreOut = CreateSemaphore(NULL, 0, 1, L"hbso");
	hWork = CreateThread(NULL, 0, work, &myParams, 0, &idWork);
	if (hWork == NULL)
		return GetLastError();

	hSumElement = CreateThread(NULL, 0, SumElement, &myParams, 0, &idSumElement);
	if (hSumElement == NULL)
		return GetLastError();

	
	
	for (int i = 0; i < Capacity; i++)
	{
		WaitForSingleObject(hCountSemaphore, INFINITE);
		if (i==0)
			cout << "\n Processed array: \n";
		cout << array[i]<<" ";
	}

	ReleaseSemaphore(hBinarySemaphoreIn, 1, NULL);
	WaitForSingleObject(hBinarySemaphoreOut, INFINITE);
	
    return 0;
}

