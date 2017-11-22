package lab8;

import org.hibernate.annotations.GenericGenerator;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.faces.context.FacesContext;
import javax.persistence.*;
import java.sql.SQLException;


@Entity
@Table(name="test207606")
@ManagedBean
@SessionScoped
public class CheckBean {
    private long id;
    private double x;
    private double y;
    private double r;
    private boolean result;

    public CheckBean(double x, double y, double r, boolean result) {
        this.x = x;
        this.y = y;
        this.r = r;
        this.result = result;
    }

    public CheckBean() {
        this.x = 0;
        this.y = 0;
        this.r = 0;
        this.result = false;
    }

    @Id
    @GeneratedValue(generator="increment")
    @GenericGenerator(name="increment", strategy = "increment")
    @Column(name="id")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Column(name="result")
    public boolean isResult() {
        return result;
    }

    public void setResult(boolean result) {
        this.result = result;
    }
    @Column(name="x")
    public double getX() {
        return x;
    }

    public void setX(double x) {
        this.x = x;
    }
    @Column(name="y")
    public double getY() {
        return y;
    }

    public void setY(double y) {
        this.y = y;
    }
    @Column(name="r")
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


    public void Check()
    {
       result =CheckCircle(x,y,r) || CheckRect(x,y,r) || CheckTriangle(x,y,r);

    /*    try {
            Factory.getInstance().getCheckBeanDAOimpl().addCheck(this);
        } catch (SQLException s)
        {
            System.out.println("fail");
        }*/
    }

    public void Submit(){
        Check();
        Bank.addNewCheck(
                new CheckBean(this.x, this.y, this.r, this.result));
    }

    public void SubmitFromPicture(){
      String xString=FacesContext.getCurrentInstance().getExternalContext().getRequestParameterMap().get("actual_form:x_hidden");
      String yString=FacesContext.getCurrentInstance().getExternalContext().getRequestParameterMap().get("actual_form:y_hidden");


      double x = Double.parseDouble(xString);
      double y = Double.parseDouble(yString);
      CheckBean temp_cb = new CheckBean(x, y, this.r, false);
      temp_cb.Check();
        Bank.addNewCheck(temp_cb);
    }
	
	
	
}
