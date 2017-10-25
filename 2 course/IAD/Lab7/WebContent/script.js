function isNumeric(n) {
	return !isNaN(parseFloat(n)) && isFinite(n);
}

function validateForm() {

	if (!isNumeric(form.x.value)) {
		alert("Не выбран x!");
		return false;
	}

	if (!isNumeric(form.y.value) || form.y.value < -5 || form.y.value > 5) {
		form.y.value = "1";
		alert("Неверное значение y!");
		return false;
	}

	if (!isNumeric(form.r.value)) {
		alert("Не выбран r!");
		return false;
	}
	return true;
}

function refreshLabels() {
	document.getElementById("temp_value_x").innerHTML = form.x.value;
	document.getElementById("temp_value_r").innerHTML = form.r.value;
}
