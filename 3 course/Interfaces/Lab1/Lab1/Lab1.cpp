// Lab1.cpp: ���������� ����� ����� ��� ����������.
//

#include "stdafx.h"
#include "Lab1.h"
#include <fstream>
#include <ctime>
#include <string>

#define MAX_LOADSTRING 100
#define NAME_OF_BMP_FILE "MARBLES.BMP"


// ���������� ����������:
HINSTANCE hInst;                                // ������� ���������
WCHAR szTitle[MAX_LOADSTRING];                  // ����� ������ ���������
WCHAR szWindowClass[MAX_LOADSTRING];            // ��� ������ �������� ����

static HBITMAP hBitmap;
static BITMAP bm;
HDC hdc;

static HDC memBit1;  //  id sovmestimogo konteksta ustroystva

int Action(HWND);


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

    // TODO: ���������� ��� �����.






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
		Action(hWnd);
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
            // TODO: �������� ���� ����� ��� ����������, ������������ HDC...
			//BitBlt(hdc, 0, 0, bm.bmWidth, bm.bmHeight, memBit1, 0, 0, SRCCOPY);//����� �����������
			Action(hWnd);

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

int Action(HWND hWnd)
{
	HDC hPixelsDC;
	HDC hTempHdc;
	HDC hNewDC;

	HBITMAP hNewBitmap;

	BITMAPINFO bmi;
	BITMAPINFO bmi_pixels;

	BITMAPFILEHEADER   bmfHeader_pixels;

	std::ofstream ofs("result.txt");

	RECT rcClient;
	GetClientRect(hWnd, &rcClient);


	hBitmap = (HBITMAP)LoadImageW(NULL, TEXT(NAME_OF_BMP_FILE), IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE | LR_CREATEDIBSECTION);
	GetObject(hBitmap, sizeof(bm), &bm);

	hdc = GetDC(hWnd);
	hTempHdc = CreateCompatibleDC(hdc);
	
	SelectObject(hTempHdc, hBitmap);
	
	// ��� �� �������� � ������������ ����������� ����� �� �������
	SetStretchBltMode(hdc, HALFTONE);
	if (!StretchBlt(
		hdc,
		0,0,
		rcClient.right/2, rcClient.bottom,
		hTempHdc,
		0,0,
		bm.bmWidth, bm.bmHeight,
		SRCCOPY
		))
	{
		return 1;
	}
	//TODO: ����������� ���������� ������� (������� ����� ����� �����������)

	//�����-�� �����, �������� ������ ������ ��� ������ ��� ���������
	DWORD dwBmpSize = ((bm.bmWidth * 32 + 31) / 32) * 4 * bm.bmHeight;


	HANDLE hDIB = GlobalAlloc(GHND, dwBmpSize);
	//lpbitmap - ������ ����, ���� ���������� ���������� �� �����������
	char *lpbitmap = (char *)GlobalLock(hDIB);

	bmi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bmi.bmiHeader.biHeight = bm.bmHeight;
	bmi.bmiHeader.biWidth = bm.bmWidth;
	bmi.bmiHeader.biPlanes = 1;
	bmi.bmiHeader.biBitCount = 24;
	bmi.bmiHeader.biCompression = BI_RGB;

	//=========================================================����� ���������� �������� ������ ������=========================================================================
	
	double start = clock();
	int a = GetDIBits(hTempHdc, hBitmap, 0, (UINT)bm.bmHeight, lpbitmap, (BITMAPINFO *)&bmi, DIB_RGB_COLORS);
	
	int size = 3 * bmi.bmiHeader.biHeight*bmi.bmiHeader.biWidth;
	for (int i = 0; i < size;i += 3)
		lpbitmap[i] = 0;

	double finish = clock();
	ofs << (finish - start) / CLOCKS_PER_SEC;
	ofs.close();

	//====================================================================================================================================================================

	

	//hPixelsDC - ����� ��������, � ������� ���������� �������� �����������. �������� ��� ����� ��� ������ � getsetpixels
	 hPixelsDC = CreateCompatibleDC(hTempHdc);

	BitBlt(hPixelsDC, 0, 0, bm.bmWidth, bm.bmHeight, hTempHdc, 0, 0, SRCCOPY);

	//������� ������, � ������� ������� ����� ������ rgb-������ � ����� ��������, ����� ������� �� ����� ����� �����������
	 hNewBitmap = CreateDIBitmap(hTempHdc, &bmi.bmiHeader, CBM_INIT, lpbitmap, &bmi, DIB_RGB_COLORS);
	 hNewDC = CreateCompatibleDC(hdc);
	SelectObject(hNewDC, hNewBitmap);

	// ��� �� �������� � ������������ ����������� ����� �� �������
	SetStretchBltMode(hdc, HALFTONE);
	if (!StretchBlt(
		hdc,
		rcClient.right / 2, 0,
		rcClient.right / 2, rcClient.bottom,
		hNewDC,
		0, 0,
		bmi.bmiHeader.biWidth, bmi.bmiHeader.biHeight,
		SRCCOPY
		))
	{
		return 1;
	}

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

	 hDIB = GlobalAlloc(GHND, dwBmpSize);
	//lpbitmap - ������ ����, ���� ���������� ���������� �� �����������
	 lpbitmap = (char *)GlobalLock(hDIB);

	//=================================================����� � ������ ����� ����� ����� SET/GET PIXELS===========================================================================================================
	
	COLORREF obgr;
	for (int i = 0; i < bm.bmHeight; i++)
		for (int j = 0; j < bm.bmWidth; j++)
		{
			obgr = GetPixel(hTempHdc, j,i);			// �������� ������� � ������� 0bgr
			obgr = obgr << 16;							// �������� �� ��������� gr00
			obgr = obgr >> 16;							// �������� ����� � �������� 00gr
			SetPixel(hTempHdc, j,i, obgr);
		}
	//====================================================================================================================================================================
	
	bmi_pixels.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bmi_pixels.bmiHeader.biHeight = bm.bmHeight;
	bmi_pixels.bmiHeader.biWidth = bm.bmWidth;
	bmi_pixels.bmiHeader.biPlanes = 1;
	bmi_pixels.bmiHeader.biBitCount = 24;
	bmi_pixels.bmiHeader.biCompression = BI_RGB;
	
	int b = GetDIBits(hTempHdc, hBitmap, 0, (UINT)bm.bmHeight, lpbitmap, (BITMAPINFO *)&bmi_pixels, DIB_RGB_COLORS);

	HANDLE hPixelsFile = CreateFile(TEXT("Result_pixels.BMP"),
		GENERIC_WRITE,
		0,
		NULL,
		CREATE_ALWAYS,
		FILE_ATTRIBUTE_NORMAL, NULL);

	// Add the size of the headers to the size of the bitmap to get the total file size
	 dwSizeofDIB = dwBmpSize + sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER);

	//Offset to where the actual bitmap bits start.
	 bmfHeader_pixels.bfOffBits = (DWORD)sizeof(BITMAPFILEHEADER) + (DWORD)sizeof(BITMAPINFOHEADER);

	//Size of the file
	 bmfHeader_pixels.bfSize = dwSizeofDIB;

	//bfType must always be BM for Bitmaps
	 bmfHeader_pixels.bfType = 0x4D42; //BM   

	 dwBytesWritten = 0;
	WriteFile(hPixelsFile, (LPSTR)&bmfHeader_pixels, sizeof(BITMAPFILEHEADER), &dwBytesWritten, NULL);
	WriteFile(hPixelsFile, (LPSTR)&bmi_pixels, sizeof(BITMAPINFOHEADER), &dwBytesWritten, NULL);
	WriteFile(hPixelsFile, (LPSTR)lpbitmap, dwBmpSize, &dwBytesWritten, NULL);



	//�������� ���� �������� �������� � ������������ ������
	DeleteObject(hNewBitmap);
	DeleteDC(hNewDC);
	GlobalUnlock(hDIB);
	GlobalFree(hDIB);

	CloseHandle(hFile);
	CloseHandle(hPixelsFile);

	ReleaseDC(hWnd, hdc);


	return 0;
}
