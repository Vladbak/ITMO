// Lab1.cpp: ���������� ����� ����� ��� ����������.
//


#include "stdafx.h"
#include "Lab1.h"
#include <fstream>
#include <iostream>
#include <ctime>
#include <string>
#include <iomanip>

#define MAX_LOADSTRING 100



// ���������� ����������:
HINSTANCE hInst;                                // ������� ���������
WCHAR szTitle[MAX_LOADSTRING];                  // ����� ������ ���������
WCHAR szWindowClass[MAX_LOADSTRING];            // ��� ������ �������� ����

static HBITMAP hBitmap;
static BITMAP bm;
HDC hdc;



int Action(HWND, wchar_t*);
int ChangeRedAndBlueChannels(HDC hdc, HBITMAP hBitmap, BITMAP bm, double *, HBITMAP * );

// ��������� ���������� �������, ���������� � ���� ������ ����:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);


    // ������������� ���������� �����
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_LAB1, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // ��������� ������������� ����������:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB1));

    MSG msg;

    // ���� ��������� ���������:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  �������: MyRegisterClass()
//
//  ����������: ������������ ����� ����.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB1));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_LAB1);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   �������: InitInstance(HINSTANCE, int)
//
//   ����������: ��������� ��������� ���������� � ������� ������� ����.
//
//   �����������:
//
//        � ������ ������� ���������� ���������� ����������� � ���������� ����������, � �����
//        ��������� � ��������� �� ����� ������� ���� ���������.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // ��������� ���������� ���������� � ���������� ����������

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  �������: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  ����������:  ������������ ��������� � ������� ����.
//
//  WM_COMMAND � ���������� ���� ����������
//  WM_PAINT � ���������� ������� ����
//  WM_DESTROY � ��������� ��������� � ������ � ���������
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{


    switch (message)
    {
	case WM_CREATE:
	{
		
		break;

	}

    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // ��������� ����� � ����:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
			
            HDC hdc = BeginPaint(hWnd, &ps);
			//��������� ���� � ������ � ������
			hBitmap = (HBITMAP)LoadImageW(NULL, L"Earth.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE | LR_CREATEDIBSECTION);

			//�������� �� �������� � bm
			GetObject(hBitmap, sizeof(bm), &bm);
			HDC hdcBuffer = GetDC(hWnd);
			hdc = CreateCompatibleDC(hdcBuffer);
	
			//����������� ��� ������ � ���������
			SelectObject(hdc, hBitmap);
			
			RECT rcClient;
			GetClientRect(hWnd, &rcClient);


			BitBlt(hdcBuffer,
				0, 0,
				rcClient.right / 2, rcClient.bottom,
				hdc,
				0, 0,
				SRCCOPY);

			double res;
			HBITMAP hNewBitmap;

			ChangeRedAndBlueChannels(hdc, hBitmap, bm, &res, &hNewBitmap);
			SelectObject(hdc, hNewBitmap);

			BitBlt(hdcBuffer,
				rcClient.right / 2, 0,
				rcClient.right / 2, rcClient.bottom,
				hdc,
				0, 0, SRCCOPY);

			ReleaseDC(hWnd, hdcBuffer);
			EndPaint(hWnd, &ps);
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// ���������� ��������� ��� ���� "� ���������".
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}


int ChangeRedAndBlueChannels(HDC hdc, HBITMAP hBitmap, BITMAP bm, double *res, HBITMAP * hNewBitmap)
{

	//�����-�� �����, �������� ������ ������ ��� ������ ��� ���������
	DWORD dwBmpSize = ((bm.bmWidth * 32 + 31) / 32) * 4 * bm.bmHeight;

	HANDLE hDIB = GlobalAlloc(GHND, dwBmpSize);
	//lpbitmap - ������ ����, ���� ���������� ���������� �� �����������
	char *lpbitmap = (char *)GlobalLock(hDIB);
	BITMAPINFO bmi;

	bmi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bmi.bmiHeader.biHeight = bm.bmHeight;
	bmi.bmiHeader.biWidth = bm.bmWidth;
	bmi.bmiHeader.biPlanes = 1;
	bmi.bmiHeader.biBitCount = 24;
	bmi.bmiHeader.biCompression = BI_RGB;

	//=========================================================����� ���������� ����� ������ � �������� ������=========================================================================

	double start = clock();
	int a = GetDIBits(hdc, hBitmap, 0, (UINT)bm.bmHeight, lpbitmap, (BITMAPINFO *)&bmi, DIB_RGB_COLORS);

	int size = 3 * bmi.bmiHeader.biHeight*bmi.bmiHeader.biWidth;
	char blue;
	for (int i = 0; i < size;i += 3)
	{
		blue = lpbitmap[i];
		lpbitmap[i] = lpbitmap[i + 2];
		lpbitmap[i + 2] = blue;
	}

	double finish = clock();
	*res = (finish - start) / CLOCKS_PER_SEC;


	//====================================================================================================================================================================

	//������� ������, � ������� ������� ����� ������ rgb-������ � ����� ��������, ����� ������� �� ����� ����� �����������
	 *hNewBitmap = CreateDIBitmap(hdc, &bmi.bmiHeader, CBM_INIT, lpbitmap, &bmi, DIB_RGB_COLORS);

	BITMAPFILEHEADER   bmfHeader;

	HANDLE hFile = CreateFile(TEXT("Result.BMP"),
		GENERIC_WRITE,
		0,
		NULL,
		CREATE_ALWAYS,
		FILE_ATTRIBUTE_NORMAL, NULL);

	// Add the size of the headers to the size of the bitmap to get the total file size
	DWORD dwSizeofDIB = dwBmpSize + sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER);

	//Offset to where the actual bitmap bits start.
	bmfHeader.bfOffBits = (DWORD)sizeof(BITMAPFILEHEADER) + (DWORD)sizeof(BITMAPINFOHEADER);

	//Size of the file
	bmfHeader.bfSize = dwSizeofDIB;

	//bfType must always be BM for Bitmaps
	bmfHeader.bfType = 0x4D42; //BM   

	DWORD dwBytesWritten = 0;
	WriteFile(hFile, (LPSTR)&bmfHeader, sizeof(BITMAPFILEHEADER), &dwBytesWritten, NULL);
	WriteFile(hFile, (LPSTR)&bmi, sizeof(BITMAPINFOHEADER), &dwBytesWritten, NULL);
	WriteFile(hFile, (LPSTR)lpbitmap, dwBmpSize, &dwBytesWritten, NULL);

	GlobalUnlock(hDIB);
	GlobalFree(hDIB);

	CloseHandle(hFile);
	return 0;
}

int DeleteBlueChannelByPixels(HDC hdc, HBITMAP hBitmap, BITMAP bm, double * res)
{

	DWORD dwBmpSize = ((bm.bmWidth * 32 + 31) / 32) * 4 * bm.bmHeight;

	HANDLE hDIB = GlobalAlloc(GHND, dwBmpSize);
	//lpbitmap - ������ ����, ���� ���������� ���������� �� �����������
	char * lpbitmap = (char *)GlobalLock(hDIB);

	//=================================================����� � ������ ����� ����� ����� SET/GET PIXELS===========================================================================================================


	double start = clock();
	COLORREF obgr;
	for (int i = 0; i < bm.bmHeight; i++)
	{
		for (int j = 0; j < bm.bmWidth; j++)
		{
			obgr = GetPixel(hdc, j, i);			// �������� ������� � ������� 0bgr
			obgr = obgr << 16;							// �������� �� ��������� gr00
			obgr = obgr >> 16;							// �������� ����� � �������� 00gr
			SetPixel(hdc, j, i, obgr);
		}
	}
	double finish = clock();
	*res = (finish - start) / CLOCKS_PER_SEC;


	//====================================================================================================================================================================

	BITMAPINFO bmi_pixels;
	bmi_pixels.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bmi_pixels.bmiHeader.biHeight = bm.bmHeight;
	bmi_pixels.bmiHeader.biWidth = bm.bmWidth;
	bmi_pixels.bmiHeader.biPlanes = 1;
	bmi_pixels.bmiHeader.biBitCount = 24;
	bmi_pixels.bmiHeader.biCompression = BI_RGB;

	int b = GetDIBits(hdc, hBitmap, 0, (UINT)bm.bmHeight, lpbitmap, (BITMAPINFO *)&bmi_pixels, DIB_RGB_COLORS);

	HANDLE hPixelsFile = CreateFile(TEXT("Result_pixels.BMP"),
		GENERIC_WRITE,
		0,
		NULL,
		CREATE_ALWAYS,
		FILE_ATTRIBUTE_NORMAL, NULL);

	DWORD dwSizeofDIB = dwBmpSize + sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER);

	BITMAPFILEHEADER   bmfHeader_pixels;
	// Add the size of the headers to the size of the bitmap to get the total file size
	dwSizeofDIB = dwBmpSize + sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER);

	//Offset to where the actual bitmap bits start.
	bmfHeader_pixels.bfOffBits = (DWORD)sizeof(BITMAPFILEHEADER) + (DWORD)sizeof(BITMAPINFOHEADER);

	//Size of the file
	bmfHeader_pixels.bfSize = dwSizeofDIB;

	//bfType must always be BM for Bitmaps
	bmfHeader_pixels.bfType = 0x4D42; //BM   

	DWORD dwBytesWritten = 0;
	WriteFile(hPixelsFile, (LPSTR)&bmfHeader_pixels, sizeof(BITMAPFILEHEADER), &dwBytesWritten, NULL);
	WriteFile(hPixelsFile, (LPSTR)&bmi_pixels, sizeof(BITMAPINFOHEADER), &dwBytesWritten, NULL);
	WriteFile(hPixelsFile, (LPSTR)lpbitmap, dwBmpSize, &dwBytesWritten, NULL);

	GlobalUnlock(hDIB);
	GlobalFree(hDIB);

	CloseHandle(hPixelsFile);
	return 0;

}
