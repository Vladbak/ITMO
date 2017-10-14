
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
	  alert("loadxml");
  	req = null;
  	if (window.XMLHttpRequest) 
  	{
  		try {
  			alert("try1");
  			req = new XMLHttpRequest();
  		} catch (e) 
  		{
  			alert("catch1");
  		}
	} else 
		if (window.ActiveXObject) 
	{
  		try 
  		{
  			alert("try2");
  			req = new ActiveXObject('Msxml2.XMLHTTP');
	  	} 
  		catch (e) 
	  	{
	  		alert("cathc2");
	  			try {
	  				alert("try3");
	  				req = new ActiveXObject('Microsoft.XMLHTTP');
	  			} catch (e) 
	  			{
	  				alert("catch3");
	  			} 
  		}
  	}
  	if (req) {
  		alert("if");
  		var body = "x=" + encodeURIComponent(x) + "&y=" + encodeURIComponent(y) + "&r=" + encodeURIComponent(r);
  		req.open("POST", url, true);
  		req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
  		req.onreadystatechange = processReqChange;
  		req.send(body);
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

	      
	      