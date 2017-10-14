
  var req;
  function processReqChange() {
  	try { // Важно!
  		// только при состоянии "complete"
  		if (req.readyState == 4) {
  			// для статуса "OK"
  			if (req.status == 200) {
  				// обработка ответа
  			} else {
  				alert("Не удалось получить данные:\n" + req.statusText);
  			}
  		}
  	} catch (e) {
  		// alert('Ошибка: ' + e.description);
  		// В связи с багом XMLHttpRequest в Firefox
  		// приходится отлавливать ошибку
  	}
  }

  function loadXMLDoc(url, x, y, r) {
  	req = null;
  	if (window.XMLHttpRequest) {
  		try {
  			req = new XMLHttpRequest();
  		} catch (e) {
  		}
  	} else if (window.ActiveXObject) {
  		try {
  			req = new ActiveXObject('Msxml2.XMLHTTP');
  		} catch (e) {
  			try {
  				req = new ActiveXObject('Microsoft.XMLHTTP');
  			} catch (e) {
  			}
  		}
  	}
  	if (req) {
  		var params = "x=" + String(x) + "&y=" + String(y) + "&r=" + String(r);
  		req.open("POST", url, true);
  		req.onreadystatechange = processReqChange;
  		req.send(params);
  	}
  }

  
	function getMousePos(canvas, evt) {
	        var rect = canvas.getBoundingClientRect();
	        return {
	          x: evt.clientX - rect.left,
	          y: evt.clientY - rect.top
	        };
	      }
	 
	    var canvas = document.getElementById('myCanvas');
	    var context = canvas.getContext('2d');
	      
	     canvas.addEventListener('click', function(evt) {
			  if (!isNumeric(form.r.value))
			  {
			  	alert("R не установлен! Невозможно выполнить запрос!");
			  	return false;
			  }
		
		 alert("1");
		var r = form.r.value;
		
		var mousePos = getMousePos(canvas, evt);
		var x_mouse = mousePos.x;
		var y_mouse = mousePos.y;
		var x_pixels, y_pixels;

		x_pixels = (x_mouse-canvas.width/2);
		y_pixels = (canvas.height/2 - y_mouse);
		
		var x = (x_pixels/(canvas.width/8));
		var y = (y_pixels/(canvas.height/8));

		context.beginPath();
		context.fillStyle="black";
		context.fillText(String(x) + " "+String(y), x_mouse,y_mouse);
		context.fill();
		context.closePath();
		
		loadXMLDoc("ControllerServlet", x,y,r);
		});

	      
	      