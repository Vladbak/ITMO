// Lab4_1.cpp: определ€ет точку входа дл€ приложени€.
//

#include "stdafx.h"
#include "Lab4_1.h"

#define MAX_LOADSTRING 100

// ?лобальные переменные:
HINSTANCE hInst;                                // текущий экземпл§р
WCHAR szTitle[MAX_LOADSTRING];                  // Уекст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // им§ класса главного окна

HWND hwnd_Desktop;
HDC hdcDesktop;
HBRUSH hBrush;

unsigned short int PositionX, PositionY;
short int step;
RECT rect;






// Этправить объ§влени§ функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
void Action();

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: разместите код здесь.

	// їнициализаци§ глобальных строк
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_LAB4_1, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// ђыполнить инициализацию приложени§:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB4_1));

	MSG msg;

	// ?икл основного сообщени§:
	while (GetMessage(&msg, nullptr, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
}



//
//  СФН†?ї€: MyRegisterClass()
//
//  НЉЂНЉД?Нї?: регистрирует класс окна.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEXW wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB4_1));
	wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_LAB4_1);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassExW(&wcex);
}

//
//   СФН†?ї€: InitInstance(HINSTANCE, int)
//
//   НЉЂНЉД?Нї?: сохран§ет обработку экземпл§ра и создает главное окно.
//
//   †ЭЮЮ?НУЉЦїї:
//
//        ђ данной функции дескриптор экземпл§ра сохран§етс§ в глобальной переменной, а также
//        создаетс§ и выводитс§ на экран главное окно программы.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // Чохранить дескриптор экземпл§ра в глобальной переменной

	HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		500, 500, 500, 500, nullptr, nullptr, hInstance, nullptr);

	if (!hWnd)
	{
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}

//
//  СФН†?ї€: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  НЉЂНЉД?Нї?:  обрабатывает сообщени§ в главном окне.
//
//  WM_COMMAND „ обработать меню приложени§
//  WM_PAINT „ отрисовать главное окно
//  WM_DESTROY „ отправить сообщение о выходе и вернутьс§
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{



	switch (message)
	{
	case WM_CREATE:
	{
		hwnd_Desktop = GetDesktopWindow();
		hdcDesktop = GetWindowDC(hwnd_Desktop);
		if (hdcDesktop == 0)
			break;

		step = 5;


		PositionX = 100;
		PositionY = 0;
		GetClientRect(hwnd_Desktop, &rect);



		SetTimer(hWnd, 1, 50, NULL);

	}
	break;
	// †ейс таймер как бы проваливаетс§ в ђЮ_пэйнт и обрабатываетс§ там
	case WM_TIMER:
	{
		if (PositionY<rect.top || PositionY + 100>rect.bottom)
			step *= -1;

		hdcDesktop = GetWindowDC(0);
		hBrush = CreateSolidBrush(RGB(255, 0, 0));
		SelectObject(hdcDesktop, hBrush);

		Ellipse(hdcDesktop, PositionX, PositionY, PositionX + 100, PositionY + 100);
		PositionY += step;

	}
	break;
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		hdcDesktop = BeginPaint(hWnd, &ps);


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



void Action() {




}