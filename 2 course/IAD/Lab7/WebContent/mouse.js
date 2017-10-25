
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
  	if (window.XMLHttpRequest) 
  	{
  		try {

  			req = new XMLHttpRequest();
  		} catch (e) 
  		{
  		}
	} else 
		if (window.ActiveXObject) 
	{
  		try 
  		{
  		
  			req = new ActiveXObject('Msxml2.XMLHTTP');
	  	} 
  		catch (e) 
	  	{
	  
	  			try {
	  		
	  				req = new ActiveXObject('Microsoft.XMLHTTP');
	  			} catch (e) 
	  			{
	  			
	  			} 
  		}
  	}
  	if (req) {

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

		var mousePos = getMousePos(canvas, evt);
		var x_mouse = mousePos.x;
		var y_mouse = mousePos.y;
		var x_pixels, y_pixels;

		x_pixels = (x_mouse-canvas.width/2);
		y_pixels = (canvas.height/2 - y_mouse);
		
		var x1 = (x_pixels/(canvas.width/8));
		var y1 = (y_pixels/(canvas.height/8));
		var r1 = form.r.value;
	
		var str="red";
		$.ajax
		(
		{
			   url:'ControllerServlet',
	            data:{x:x1,
	            	y:y1,
	            	r:r1},
	            
	            type:'post',
	            success: function(response){ 
	            	
	            	
	                   $('#temp_table').html(response); 
	
	           	    if ($(response).find('#result_tag').text()=="true")
	           	    	  {
	           	    	  	str="green";
	           	   
	           	    	  }
	           	    		
	           	    else
	           	    	  {
	           	    	
	           	    	  	str="red";
	           	    	  }
	           	 context.fillStyle=str;
	           	 context.beginPath();
	           	 context.arc(x_mouse, y_mouse, 5, 0, 2 * Math.PI);
	           	 context.fill();
	           	 context.closePath();
	               },
	            error: function(response){
	                   alert(response); // <------ If errors occur trigger an
										// alert
	               },
	            
		}		
		)
		
		
		
		
		// loadXMLDoc("ControllerServlet", x,y,r);
		});

	      
	      