// Lab1.cpp: ���������� ����� ����� ��� ����������.
//

#include "stdafx.h"
#include "Lab1.h"

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
	RECT rcClient;
	GetClientRect(hWnd, &rcClient);


	hBitmap = (HBITMAP)LoadImageW(NULL, TEXT(NAME_OF_BMP_FILE), IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE | LR_CREATEDIBSECTION);
	GetObject(hBitmap, sizeof(bm), &bm);

	hdc = GetDC(hWnd);
	HDC hTempHdc = CreateCompatibleDC(hdc);
	
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
	char *lpbitmap = (char *)GlobalLock(hDIB);

	//����� �� �������� � ������ lpbitmap ����� �� �������� ��� ����������� ���������
	
	 
	BITMAPINFO bmi;
	bmi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bmi.bmiHeader.biHeight = bm.bmHeight;
	bmi.bmiHeader.biWidth = bm.bmWidth;
	bmi.bmiHeader.biPlanes = 1;
	bmi.bmiHeader.biBitCount = 24;
	bmi.bmiHeader.biCompression = BI_RGB;

	int a = GetDIBits(hTempHdc, hBitmap, 0, (UINT)bm.bmHeight, lpbitmap, (BITMAPINFO *)&bmi, DIB_RGB_COLORS);

	int size = 3 * bmi.bmiHeader.biHeight*bmi.bmiHeader.biWidth;
	for (int i = 0; i < size;i += 3)
		lpbitmap[i] = 0;

	

	HBITMAP hNewBitmap = CreateDIBitmap(hTempHdc, &bmi.bmiHeader, CBM_INIT, lpbitmap, &bmi, DIB_RGB_COLORS);
	HDC hNewDC = CreateCompatibleDC(hdc);
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

	DeleteObject(hNewBitmap);
	DeleteDC(hNewDC);
	GlobalUnlock(hDIB);
	GlobalFree(hDIB);

	ReleaseDC(hWnd, hdc);


	return 0;
}
