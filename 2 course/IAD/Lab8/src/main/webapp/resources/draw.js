document.getElementById("svg").addEventListener("click", drawPoint(event));

function draw(r) {

	d3.select("#svg").remove();
	d3.select("#div_svg").append("svg").attr("id", "svg").attr("width", 300)
			.attr("height", 300).attr("onclick", " drawPoint(event)");
	d3.select("#svg").append("polygon").attr(
			"points",
			"150,150 " + String(150 - r * 20) + ",150 150,"
					+ String(150 - r * 40)).style("fill", "blue");
	d3.select("#svg").append("path").attr(
			"d",
			"M 150 150 L 150 " + String(150 - 20 * r) + " A " + String(20 * r)
					+ " " + String(20 * r) + " 0 0 1 " + String(150 + 20 * r)
					+ " 150 L 150 150").style("fill", "blue");
	d3.select("#svg").append("rect").attr("x", 150).attr("y", 150).attr(
			"width", String(20 * r)).attr("height", String(40 * r)).style(
			"fill", "blue");

	d3.select("#svg").append("line").attr("x1", 0).attr("x2", 300).attr("y1",
			150).attr("y2", 150).style("stroke", "black");

	d3.select("#svg").append("line").attr("x1", 150).attr("x2", 150).attr("y1",
			0).attr("y2", 300).style("stroke", "black");
    document.getElementById("svg").addEventListener("click", drawPoint);

}

function drawPoint(event){
	var x = event.pageX - $('#svg').offset().left;
    var y = event.pageY - $('#svg').offset().top;

    var x_real, y_real;

    x_real=Number((x-150)/40).toPrecision(2);
    y_real=Number((150-y)/40).toPrecision(2);
    document.getElementById("actual_form:x_hidden").value= x_real;
    document.getElementById("actual_form:y_hidden").value= y_real;
    var ev_click = new Event('click');
    document.getElementById("actual_form:hidden_submit_button").dispatchEvent(ev_click);

   }



