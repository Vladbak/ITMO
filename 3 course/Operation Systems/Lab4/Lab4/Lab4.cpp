// Lab4.cpp: определяет точку входа для консольного приложения.
//

#define _CRT_SECURE_NO_WARNINGS




#include "stdafx.h"
#include <iostream>
#include <windows.h>
#include <cstdio>

using namespace std;
int main()
{
	wchar_t AppName[] = L"C:\\Users\\Corrector\\ITMO\\3 course\\Operation Systems\\Lab4\\Debug\\Scout.exe";
	int NumberofProcs;
	

	cout << "Write number of processes to create:\n";
	cin >> NumberofProcs;
	HANDLE *hEvents = new HANDLE[3];
	
	HANDLE hMutex = CreateMutexW(NULL, FALSE, L"Mutex");
	STARTUPINFO *si = new STARTUPINFO[NumberofProcs];
	PROCESS_INFORMATION *pi = new PROCESS_INFORMATION[NumberofProcs];

	char *source = new char[2];
	wchar_t *dest = new wchar_t[6];

	hEvents[0] = CreateEventW(NULL, FALSE, FALSE, L"PointEvent");
	hEvents[1] = CreateEventW(NULL, FALSE, FALSE, L"DashEvent");
	hEvents[2] = CreateEventW(NULL, FALSE, FALSE, L"EndScoutEvent");
	
	for (int i = 0; i < NumberofProcs; i++)
	{

		ZeroMemory(&si[i], sizeof(STARTUPINFO));
		si[i].cb = sizeof(STARTUPINFO);
		if (!CreateProcessW(AppName, NULL, NULL, NULL, FALSE,
			CREATE_NEW_CONSOLE, NULL, NULL, &si[i], &pi[i]))
			return 0;
		
	}
	
	DWORD Signal;
	while (NumberofProcs > 0)
	{
		Signal= WaitForMultipleObjects(3, hEvents, false, INFINITE);
	
		switch (Signal) {

			case 0:
			{
				cout << "Point was entered" << endl;
				ResetEvent(hEvents[0]);
				break;
			}
			case 1:
			{
				cout << "Dash was entered" << endl;
				ResetEvent(hEvents[1]);

				break;
			}
			case 2:
			{
				cout << "one scout closed" << endl;
				NumberofProcs--;
				ResetEvent(hEvents[2]);

				break;
			}
			default: {
				cout << "error in switch" << endl;
				return 0;
			}
		}
	}

    return 0;
}



