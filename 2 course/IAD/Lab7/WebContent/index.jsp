
<%@page import="lab7_package.AreaCheckServlet"%>
<%@page import="lab7_package.Results"%>
<%@page import="lab7_package.Result"%>
<%@ page language="java" contentType="text/html; charset=utf-8"
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta charset="utf-8" />
		<title>Лабораторная работа 7</title>
		<link rel="stylesheet" type="text/css" href="style.css" />
		<script src="script.js"></script>

	</head>
	<body onload="draw(1)">
	
		<h1>Бакшенов Владимир</h1>
		<div id="secondLine">группа <a>P3218</a> вариант <a>2977</a></div>
		
		<table width="100%" height="100%" border="1" cellspacing="1" cellpadding="1" align = "center">
			<tr>
				<td width="40%"  rowspan="2">
					<div class="imgCenter" >
						<img  width="300" height="255" src="areas.png" />
					</div>
				</td>
				<td width="40%" rowspan="2">
					<canvas  id="myCanvas" width="400" height="400">
						<script src="graph.js" async>
						</script>
					</canvas>
					<script src="mouse.js" async></script>
				</td>
				<td  valign="top" align ="center" width=20% >
					Форма
				</td>
			</tr>
			
			<tr>
				<td height=80%  align = "left">
					<form name="form" action="ControllerServlet" method="post">
					Server Version: <%= application.getServerInfo() %><br>
Servlet Version: <%= application.getMajorVersion() %>.<%= application.getMinorVersion() %>
JSP Version: <%= JspFactory.getDefaultFactory().getEngineInfo().getSpecificationVersion() %> <br>
					
					
							X = <br>
							<input type="hidden" name="x">
							<input type="button" value="-3" onclick="{document.form.x.value = this.value}" /><br>
							<input type="button" value="-2" onclick="{document.form.x.value = this.value}"/><br>
							<input type="button" value="-1" onclick="{document.form.x.value = this.value}" /><br>
							<input type="button" value="0" onclick="{document.form.x.value = this.value}"/><br>
							<input type="button" value="1" onclick="{document.form.x.value = this.value}"/><br>
							<input type="button" value="2" onclick="{document.form.x.value = this.value}"/><br>
							<input type="button" value="3" onclick="{document.form.x.value = this.value}"/><br>
							<input type="button" value="4" onclick="{document.form.x.value = this.value}"/><br>
							<input type="button" value="5" onclick="{document.form.x.value = this.value}"/><br>
							<br>
							Y =
							<input type="text" name="y" value="1" />
							<br>
							R =<br>
							<input type="hidden" name="r"> 
							<input type="button" value="1" onclick="{document.form.r.value = this.value}"/><br>
							<input type="button" value="1.5" onclick="{document.form.r.value = this.value}"/><br>
							<input type="button" value="2" onclick="{document.form.r.value = this.value}"/><br>
							<input type="button" value="2.5" onclick="{document.form.r.value = this.value}"/><br>
							<input type="button" value="3" onclick="{document.form.r.value = this.value}"/><br>
							<br>
							
						<input type="submit"></p>
					</form>
					
				</td>
			</tr>
		</table>
		
		<br>
		<table id="result_table" width="50%" height="100%" border="1" cellspacing="1" cellpadding="1" align = "center">
			<tr>
				<td>X</td>
				<td>Y</td>
				<td>R</td>
				<td>Result</td>
				
			</tr>
			<%
				for(int i=0; i< AreaCheckServlet.results.size(); i++ )
				{
					
					%>
					<tr>
						<td><%=AreaCheckServlet.results.get(i).getX() %></td>
						<td><%=AreaCheckServlet.results.get(i).getY() %></td>
						<td><%=AreaCheckServlet.results.get(i).getR() %></td>
						<td><%=AreaCheckServlet.results.get(i).getRes() %></td>	
					</tr>
					<%
				}

			%>
			
			
		</table>
		
	</body>
</html>