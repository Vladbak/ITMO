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

var x_coord, y_coord;

function drawPoint(event) {
    x_coord = event.pageX - $('#svg').offset().left;
    y_coord = event.pageY - $('#svg').offset().top;

    var x = Number((x_coord - 150) / 40).toPrecision(2);
    var y = Number((150 - y_coord) / 40).toPrecision(2);

    document.getElementById("actual_form:x_hidden").value = x;
    document.getElementById("actual_form:y_hidden").value = y;
    document.getElementById("actual_form:submit_button_hidden").click();


}

function q(){
	var color="grey";
	if (document.getElementById("actual_form:result_hidden").value == "true")
		color="green";
	else
		color="red";
    d3.select("#svg").append("circle").attr("cx", String(x_coord))
        .attr("cy", String(y_coord))
        .attr("r", "3").attr("fill", color);

}





