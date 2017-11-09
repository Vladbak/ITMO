// Lab1.cpp: определяет точку входа для приложения.
//


#include "stdafx.h"
#include "Lab1.h"
#include <fstream>
#include <iostream>
#include <ctime>
#include <string>
#include <iomanip>

#define MAX_LOADSTRING 100



// Глобальные переменные:
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

static HBITMAP hBitmap;
static BITMAP bm;
HDC hdc;



int Action(HWND, wchar_t*);
int DeleteBlueChannelByBits(HDC hdc, HBITMAP hBitmap, BITMAP bm, double *, HBITMAP * );
int DeleteBlueChannelByPixels(HDC hdc, HBITMAP hBitmap, BITMAP bm, double *);


// Отправить объявления функций, включенных в этот модуль кода:
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

    // TODO: разместите код здесь.






    // Инициализация глобальных строк
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_LAB1, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Выполнить инициализацию приложения:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB1));

    MSG msg;

    // Цикл основного сообщения:
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
//  ФУНКЦИЯ: MyRegisterClass()
//
//  НАЗНАЧЕНИЕ: регистрирует класс окна.
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
//   ФУНКЦИЯ: InitInstance(HINSTANCE, int)
//
//   НАЗНАЧЕНИЕ: сохраняет обработку экземпляра и создает главное окно.
//
//   КОММЕНТАРИИ:
//
//        В данной функции дескриптор экземпляра сохраняется в глобальной переменной, а также
//        создается и выводится на экран главное окно программы.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Сохранить дескриптор экземпляра в глобальной переменной

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
//  ФУНКЦИЯ: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  НАЗНАЧЕНИЕ:  обрабатывает сообщения в главном окне.
//
//  WM_COMMAND — обработать меню приложения
//  WM_PAINT — отрисовать главное окно
//  WM_DESTROY — отправить сообщение о выходе и вернуться
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{


    switch (message)
    {
	case WM_CREATE:
	{
		//double bits[10];
		//double pixels[10];
		//for (int i = 200; i <= 2000; i += 200)
		//{
		//	wchar_t* str = new wchar_t[5];
		//	_itow(i, str, 10);
		//	str = wcscat(str, L".bmp");
		//	//Загружаем файл с пикчей в битмап
		//	hBitmap = (HBITMAP)LoadImageW(NULL, str, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE | LR_CREATEDIBSECTION);
		//	//Получаем ее свойства в bm
		//	GetObject(hBitmap, sizeof(bm), &bm);
		//	HDC hdcBuffer = GetDC(hWnd);
		//	hdc = CreateCompatibleDC(hdcBuffer);
		//	ReleaseDC(hWnd, hdcBuffer);
		//	//привязываем наш битмап к контексту
		//	SelectObject(hdc, hBitmap);
		//	double t;
		//	DeleteBlueChannelByBits(hdc, hBitmap, bm, &t);
		//	bits[i / 200 - 1] = t;
		//}
	
		//for (int i = 0; i < 10; i++)
		//{
		//	double a = bits[i];
		//}
		//
		break;

	}

    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Разобрать выбор в меню:
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
			//Загружаем файл с пикчей в битмап
			hBitmap = (HBITMAP)LoadImageW(NULL, L"600.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE | LR_CREATEDIBSECTION);

			//Получаем ее свойства в bm
			GetObject(hBitmap, sizeof(bm), &bm);
			HDC hdcBuffer = GetDC(hWnd);
			hdc = CreateCompatibleDC(hdcBuffer);
	
			//привязываем наш битмап к контексту
			SelectObject(hdc, hBitmap);
			
			RECT rcClient;
			GetClientRect(hWnd, &rcClient);


			BitBlt(hdcBuffer,
				0, 0,
				rcClient.right / 2, rcClient.bottom,
				hdc,
				0, 0,
				SRCCOPY);

		


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

// Обработчик сообщений для окна "О программе".
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


int DeleteBlueChannelByBits(HDC hdc, HBITMAP hBitmap, BITMAP bm, double *res, HBITMAP * hNewBitmap)
{

	//какая-то хрень, хранящая размер нужной нам памяти для выделения
	DWORD dwBmpSize = ((bm.bmWidth * 32 + 31) / 32) * 4 * bm.bmHeight;

	HANDLE hDIB = GlobalAlloc(GHND, dwBmpSize);
	//lpbitmap - массив байт, куда помещаются цветобайты из изображения
	char *lpbitmap = (char *)GlobalLock(hDIB);
	BITMAPINFO bmi;

	bmi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bmi.bmiHeader.biHeight = bm.bmHeight;
	bmi.bmiHeader.biWidth = bm.bmWidth;
	bmi.bmiHeader.biPlanes = 1;
	bmi.bmiHeader.biBitCount = 24;
	bmi.bmiHeader.biCompression = BI_RGB;

	//=========================================================ЗДЕСЬ ПРОИСХОДИТ УДАЛЕНИЕ СИНЕГО КАНАЛА=========================================================================

	double start = clock();
	int a = GetDIBits(hdc, hBitmap, 0, (UINT)bm.bmHeight, lpbitmap, (BITMAPINFO *)&bmi, DIB_RGB_COLORS);

	int size = 3 * bmi.bmiHeader.biHeight*bmi.bmiHeader.biWidth;
	for (int i = 0; i < size;i += 3)
		lpbitmap[i] = 0;

	double finish = clock();
	*res = (finish - start) / CLOCKS_PER_SEC;


	//====================================================================================================================================================================

	//Создаем битмап, в который запишем новый массив rgb-цветов и новый контекст, чтобы вывести на экран новое изображение
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
	//lpbitmap - массив байт, куда помещаются цветобайты из изображения
	char * lpbitmap = (char *)GlobalLock(hDIB);

	//=================================================ЗДЕСЬ Я УДАЛЯЮ СИНИЙ КАНАЛ ЧЕРЕЗ SET/GET PIXELS===========================================================================================================


	double start = clock();
	COLORREF obgr;
	for (int i = 0; i < bm.bmHeight; i++)
	{
		for (int j = 0; j < bm.bmWidth; j++)
		{
			obgr = GetPixel(hdc, j, i);			// получаем пиксель в формате 0bgr
			obgr = obgr << 16;							// сдвигаем до состояния gr00
			obgr = obgr >> 16;							// сдвигаем назад и получаем 00gr
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
