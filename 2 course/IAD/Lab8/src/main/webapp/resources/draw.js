document.getElementById("R1").addEventListener('click', function() {
	draw(1);
});
document.getElementById("R15").addEventListener('click', function() {
	draw(1.5);
});
document.getElementById("R2").addEventListener('click', function() {
	draw(2);
});
document.getElementById("R25").addEventListener('click', function() {
	draw(2.5);
});
document.getElementById("R3").addEventListener('click', function() {
	draw(3);
});

function draw(r) {

	d3.select("#svg").remove();
	d3.select("#div_svg").append("svg").attr("id", "svg").attr("width", 300)
			.attr("height", 300);
	d3.select("#svg").append("polygon").attr(
			"points",
			"150,150 " + String(150 - r * 20) + ",150 150,"
					+ String(150 - r * 40)).style("fill", "blue");
	d3.select("#svg").append("path").attr(
			"d",
			"M 150 150 L 150 " + String(150 - 20 * r) + " A " + String(40 * r)
					+ " " + String(40 * r) + " 0 0 1 " + String(150 + 20 * r)
					+ " 150 L 150 150").style("fill", "blue");
	d3.select("#svg").append("rect").attr("x", 150).attr("y", 150).attr(
			"width", String(20 * r)).attr("height", String(40 * r)).style(
			"fill", "blue");

	d3.select("#svg").append("line").attr("x1", 0).attr("x2", 300).attr("y1",
			150).attr("y2", 150).style("stroke", "black");

	d3.select("#svg").append("line").attr("x1", 150).attr("x2", 150).attr("y1",
			0).attr("y2", 300).style("stroke", "black");

	/*
	 * d3.select("body").append("svg").attr("width", 50).attr("height", 50)
	 * .append("circle").attr("cx", 25).attr("cy", 25).attr("r", 25)
	 * .style("fill", "purple");
	 */
	// var svg = d3.select('svg').append('svg');
	//
	// svg.append('text').text('click somewhere').attr('x', 50).attr('y', 50);
	//
	// var events = [];
	// svg.on('click', function() {
	// events.push(d3.event);
	// svg.enter().append('polygone').attr(
	// 'points',
	// "150,150 " + String(150 - 20 * r) + ",150 150,"
	// + String(150 - 40 * r));
	// if (events.length > 5)
	// events.shift();
	// var circles = svg.selectAll('circle').data(events, function(e) {
	// return e.timeStamp
	// }).attr('fill', 'gray');
	// circles.enter().append('circle').attr('cx', function(d) {
	// return d.x || d.pageX
	// }).attr('cy', function(d) {
	// return d.y || d.pageY
	// }).attr('fill', 'red').attr('r', 10);
	// circles.exit().remove();
	// });
}