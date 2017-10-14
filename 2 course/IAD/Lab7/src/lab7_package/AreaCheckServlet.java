package lab7_package;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.sun.org.apache.bcel.internal.generic.NEW;

/**
 * Servlet implementation class AreaCheckServlet
 */

public class AreaCheckServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
	public static Results results = new Results();
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public AreaCheckServlet() {
        super();
        // TODO Auto-generated constructor stub
    }

    public void init() throws ServletException {

    }
    
	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
	
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		String xString = request.getParameter("x");
		String yString = request.getParameter("y");
		String rString = request.getParameter("r");
		
		
		Double x = Double.parseDouble(xString);
		Double y = Double.parseDouble(yString);
		Double r = Double.parseDouble(rString);
				
		PrintWriter w = response.getWriter();
		
		Result new_res = new Result();
		new_res.setX(x);
		new_res.setY(y);
		new_res.setR(r);
		new_res.setRes(Check(x, y, r).toString());
		results.add(new_res);
		
		
		String htmlResponse = "<html>"
			+
			"<table border=\"1\" cellspacing=\"1\" cellpadding=\"1\" align = \"center\">"+
				"<tr>"+
					"<td>X</td>"+
					"<td>Y</td>"+
					"<td>R</td>"+
					"<td>Result</td>"+
				"</tr>"+
				"<tr>"+
					"<td>"+x+"</td>"+
					"<td>"+y+"</td>"+
					"<td>"+r+"</td>"+
					"<td>"+Check(x, y, r).toString()+"</td>"+
				"</tr>"+
			"</table>"
			
					
					+ "<a style=\"display: block; text-align: center;\" href=\"/lab7/index.jsp\"> back </a>"
			+"</html>";
		w.println(htmlResponse);
		
	}

	private Boolean Check(double x, double y, double r){
		return (Check_if_in_circle(x, y, r)||Check_if_in_square(x, y, r)||Check_if_in_triangle(x, y, r));
	}
	
private boolean Check_if_in_square(double x, double y, double r){
	if(x<0 && x>-r && y>0 && y<r)
		return true;
		else return false;
	
	}
private boolean Check_if_in_circle(double x, double y, double r){
	if (x*x + y*y < r*r)
		return true;
	else return false;
}
private boolean Check_if_in_triangle(double x, double y, double r){
	if (x>0 && y>0 && y<(-2*x +r))
		return true;
	else return false;
}
	
	
}
