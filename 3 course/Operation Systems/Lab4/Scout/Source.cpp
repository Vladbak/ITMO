
#include <iostream>
#include <Windows.h>
#include <cstdio>

int main()
{
	HANDLE	hDashEvent = OpenEventW(EVENT_MODIFY_STATE, FALSE, L"DashEvent");
	HANDLE	hPointEvent = OpenEventW(EVENT_MODIFY_STATE, FALSE, L"PointEvent");
	HANDLE	hEndScoutEvent = OpenEventW(EVENT_MODIFY_STATE, FALSE, L"EndScoutEvent");
	HANDLE hBossMutex = OpenMutex(SYNCHRONIZE, FALSE, L"Mutex");
	char character;
	
	std::cout << "write \'.\' or \'-\', Write \'e\' to end Scout" << std::endl;
	do
	{
		character = getchar(); // считать введённый со стандартногопотока  ввода  символ
		if (character == '.')
		{
			WaitForSingleObject(hBossMutex, INFINITE);
			SetEvent(hPointEvent);

			ReleaseMutex(hBossMutex);
		}

		if (character == '-')
		{
			WaitForSingleObject(hBossMutex, INFINITE);
			SetEvent(hDashEvent);

			ReleaseMutex(hBossMutex);
		}


		if (character == 'e')
		{
			WaitForSingleObject(hBossMutex, INFINITE);
			SetEvent(hEndScoutEvent);
			ReleaseMutex(hBossMutex);
		}

	} while (character != 'e'); // пока не были введены точка и тире



	return 0;
}
