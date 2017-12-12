package lab8;


import javax.persistence.*;


@Entity
@Table(name="test207606")
public class DBCheckBean {
    @Column()
    private int id;
    @Column(precision = 4, scale = 2)
    private Double x;
    @Column(precision = 4, scale = 2)
    private Double y;
    @Column
    private Double r;
    @Column
    private String result;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }



    public Double getX() {
        return x;
    }

    public void setX(Double x) {
        this.x = x;
    }

    public Double getY() {
        return y;
    }

    public void setY(Double y) {
        this.y = y;
    }

    public Double getR() {
        return r;
    }

    public void setR(Double r) {
        this.r = r;
    }

    public String getResult() {
        return result;
    }

    public void setResult(String result) {
        this.result = result;
    }



    public DBCheckBean() {
    }

    public DBCheckBean(double x, double y, double r, String result) {
        this.x = x;
        this.y = y;
        this.r = r;
        this.result = result;
    }
}
