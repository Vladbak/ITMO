/**
 * 
 */

function draw(rad) {
	var c = document.getElementById("myCanvas");
	var height = c.height;
	var width = c.width;
	var center_w = width / 2;
	var center_h = height / 2;

	var ctx = c.getContext("2d");

	ctx.clearRect(0, 0, width, height);

	var x, y;
	x = rad * (width / 8);
	y = rad * (height / 8);

	ctx.beginPath();
	ctx.rect(center_w - x, center_h - y, x, y);
	ctx.fillStyle = "blue";
	ctx.fill();

	ctx.beginPath()
	ctx.moveTo(center_w, center_h);
	ctx.lineTo(center_w + x / 2, center_h);
	ctx.lineTo(center_w, center_h - y);
	ctx.lineTo(center_w, center_h);
	ctx.fill();

	ctx.beginPath();
	ctx.moveTo(center_h, center_w);
	ctx.lineTo(center_w + x, center_h);
	ctx.arc(center_w, center_h, x, 0, 0.5 * Math.PI);
	ctx.lineTo(center_w, center_h);
	ctx.fill();

	ctx.moveTo(0, center_h);
	ctx.lineTo(width, center_h);
	ctx.moveTo(center_w, 0);
	ctx.lineTo(center_w, height);
	ctx.stroke();

}
