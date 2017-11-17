package lab8;

import javax.faces.bean.ManagedBean;
import javax.faces.context.FacesContext;

@ManagedBean
public class CheckBean {
    private double x;
    private double y;
    private double r;
    private boolean result;

    public double getX() {
        return x;
    }

    public void setX(double x) {
        this.x = x;
    }

    public double getY() {
        return y;
    }

    public void setY(double y) {
        this.y = y;
    }

    public double getR() {
        return r;
    }

    public void setR(double r) {
        this.r = r;
    }

    public void ChangeCommandButtonR(String value){
        setR(Double.parseDouble(value));
    }


    private boolean CheckCircle(double x, double y, double r)
    {
        return ((x>0)&&(y>0)&&(x*x+y*y<(r/2)*(r/2)));
    }
    private boolean CheckRect(double x, double y, double r)
    {
        return ((x>0)&&(y<0)&&(x<r/2)&&(y>-r));
    }
    private boolean CheckTriangle(double x, double y, double r)
    {
        return ((x<0)&&(y>0)&&(y<2*x+r));
    }


    public boolean Check(double x, double y, double r)
    {
        return (CheckCircle(x,y,r) || CheckRect(x,y,r) || CheckTriangle(x,y,r));
    }
	
	
	
}
