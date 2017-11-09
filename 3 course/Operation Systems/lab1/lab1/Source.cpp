#include<iostream>
#include<Windows.h>
#include<WinBase.h>

using namespace std;
int main()
{
	setlocale(0, "russian");

	SYSTEM_INFO si;
	OSVERSIONINFO osvi;
	DWORD size_c=MAX_COMPUTERNAME_LENGTH, size_u=MAX_COMPUTERNAME_LENGTH;
	TCHAR *user = new TCHAR[size_c], *comp = new TCHAR[size_u];
	char *user1=new char[size_c], *comp1 = new char[size_c];

	ZeroMemory(user, sizeof(TCHAR)*size_c);
	ZeroMemory(comp, sizeof(TCHAR)*size_u);
	ZeroMemory(&size_c, sizeof(DWORD));
	ZeroMemory(&size_u, sizeof(DWORD));
	ZeroMemory(&osvi, sizeof(OSVERSIONINFO));
	ZeroMemory(&si, sizeof(SYSTEM_INFO));

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);

	GetVersionExW(&osvi);
	GetSystemInfo(&si);
	
	GetComputerName(comp, &size_c);
	GetUserName(user, &size_u);
	
	wctomb(user1, *user);
	wctomb(comp1, *comp);

	cout << "���������� ����� " << osvi.dwBuildNumber<<endl;
	cout <<"����� ������ "<< osvi.dwMajorVersion<<"."<< osvi.dwMinorVersion<<endl;
	cout << "���������� ����������� " << si.dwNumberOfProcessors << endl <<
		"��� ���������� " << si.dwProcessorType << endl;
	cout<<
		"��� ���������� "<< user1 <<endl<<
		"��� ������������"<< comp1 << endl;


	
	return 0;
}