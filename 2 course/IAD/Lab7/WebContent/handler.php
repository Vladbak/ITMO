<?php
	namespace lab6;
	$start = microtime(true);
	date_default_timezone_set('Europe/Moscow');
	class Mark {
		public $x;
		public $y;
		public function __construct($x,$y)
		{
			$this->x = $x;
			$this->y = $y;
		}
	}
	class Area
	{
		public $r;
		public function __construct($r)
		{
			$this->r = $r;
		}
		public function hitInArea(Mark $m)
		{	
			//Circle
			if(pow($m->x, 2)+pow($m->y, 2)<pow($this->r, 2)&&$m->x>0&&$m->y<0
				//Rectangle
			||$m->x<0&&$m->x>-$this->r&&$m->y>0&&$m->y<$this->r/2
				// Triangle			y>-x-r
			||$m->y<0&&$m->x<0&&$m->y>(-$m->x-$this->r)
			
			||($m->x)==0&&($m->y)>=-($this->r)&&($m->y)<=0
			
			||($m->y)==0&&($m->x)>=-($this->r)&&($m->x)<=0
			)
			{
				return "IN";
			}
			else
			return "OUT";
		}
	}
	$x=$_GET['x'];
	$y=$_GET['y'];
	$result = "ERROR";
	$r=$_GET['r'];
	$div = 0.5;
	
	if (!is_numeric($x)||  $x<-2 || $x >2 || abs(fmod($x, $div))!=0 )
			
				header("Location: http://se.ifmo.ru/~s207606/lab6");
		
	if (!is_numeric($y)||$y <-5 || $y>3)
		header("Location: http://se.ifmo.ru/~s207606/lab6");
	if (!is_numeric($r)||$r<1 || $r>3 || abs(fmod($r, $div))!=0 )
		header("Location: http://se.ifmo.ru/~s207606/lab6");
	
	
	$mark = new Mark($x,$y);
	$area = new Area($r);
	$result = $area->hitInArea($mark);
	$time = date("H:i:s");
	$stime = number_format(microtime(true) - $start, 6, '.', '');
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1251" />
		<title>Лабораторная работа 6</title>
		<link rel="stylesheet" type="text/css" href="style.css" />
	</head>
	<body>
		<h1>Бакшенов Владимир</h1>
		<div id="secondLine">группа <a>P3218</a> вариант <a>1987</a></div>
		<br />
		<table border="1" cellspacing="1" cellpadding="1" align = "center">
			<tr>
				<td rowspan="2"><div class="imgCenter"><img src="areas.png" /></td>
				<td valign="top" align = "center" height="20">Параметры</td>
			</tr>
			<tr>
				<td align = "center">
					x = <?php echo $mark->x;?> <br />
					y = <?php echo $mark->y;?> <br />
					R = <?php echo $area->r;?> <br />
					Result: <?php echo $result;?> <br />
					Time: <?php echo $time;?> <br />
					ScriptTime: <?php echo $stime;?>
				</td>
			</tr>
		</table>
	</body>
</html>	