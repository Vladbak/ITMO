#include<iostream>
#include<Windows.h>
#include<WinBase.h>
#include <tchar.h>

void printError(TCHAR* msg);

using namespace std;
int main()
{
	setlocale(0, "russian");

	SYSTEM_INFO si;
	OSVERSIONINFO osvi;
	DWORD size_c=MAX_COMPUTERNAME_LENGTH, size_u=MAX_COMPUTERNAME_LENGTH;
	TCHAR COMP, USER;
	DWORD BUF_CHAR = 32767;

	ZeroMemory(&osvi, sizeof(OSVERSIONINFO));
	ZeroMemory(&si, sizeof(SYSTEM_INFO));

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);

	GetVersionExW(&osvi);
	GetSystemInfo(&si);
	
	
	GetComputerName(&COMP, &BUF_CHAR);
	GetUserName(&USER, &BUF_CHAR);
	


	cout << "Порядковый номер " << osvi.dwBuildNumber<<endl;
	cout <<"Номер Версии "<< osvi.dwMajorVersion<<"."<< osvi.dwMinorVersion<<endl;
	cout << "Количество процессоров " << si.dwNumberOfProcessors << endl <<
		"Тип процессора " << si.dwProcessorType << endl;

#define INFO_BUFFER_SIZE 32767
	DWORD i;
	TCHAR  infoBuf[INFO_BUFFER_SIZE];
	DWORD  bufCharCount = INFO_BUFFER_SIZE;

	// Get and display the name of the computer. 
	bufCharCount = INFO_BUFFER_SIZE;
	if (!GetComputerName(infoBuf, &bufCharCount))
		printError(TEXT("GetComputerName"));
	_tprintf(TEXT("\nComputer name:      %s"), infoBuf);

	// Get and display the user name. 
	bufCharCount = INFO_BUFFER_SIZE;
	if (!GetUserName(infoBuf, &bufCharCount))
		printError(TEXT("GetUserName"));
	_tprintf(TEXT("\nUser name:          %s\n"), infoBuf);


	system("pause");
	return 0;
}

void printError(TCHAR* msg)
{
	DWORD eNum;
	TCHAR sysMsg[256];
	TCHAR* p;

	eNum = GetLastError();
	FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM |
		FORMAT_MESSAGE_IGNORE_INSERTS,
		NULL, eNum,
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		sysMsg, 256, NULL);

	// Trim the end of the line and terminate it with a null
	p = sysMsg;
	while ((*p > 31) || (*p == 9))
		++p;
	do { *p-- = 0; } while ((p >= sysMsg) &&
		((*p == '.') || (*p < 33)));

	// Display the message
	_tprintf(TEXT("\n\t%s failed with error %d (%s)"), msg, eNum, sysMsg);
}