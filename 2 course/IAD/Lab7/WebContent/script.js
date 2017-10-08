function check_y(){
	if (!isNumeric(form.y.value))
	{
		form.y.value = "1";
		alert("Ошибка при вводе y!");
		
	}

}

function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}