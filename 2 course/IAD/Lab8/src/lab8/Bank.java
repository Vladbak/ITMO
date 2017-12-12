package lab8;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import java.util.ArrayList;
import java.util.List;
import org.hibernate.Session;


@ManagedBean(eager = true)
@SessionScoped
public class Bank {
    private static Bank instance=null;
    private static List<CheckBean> checkBeans;
    private  Session session;

    public static Bank getInstance(){
        if (instance == null){
            instance = new Bank();
        }
        return instance;
    }


    public List<CheckBean> getCheckBeans() {
        return checkBeans;
    }

    public void setCheckBeans(ArrayList<CheckBean> checkBeans) {
        this.checkBeans = checkBeans;
    }



    public Bank(){
        checkBeans = new ArrayList<CheckBean>();
    }

    public static void addNewCheck(CheckBean checkBean){
        checkBeans.add(checkBean);
      /*  DBCheckBean dbCheckBean = new DBCheckBean(checkBean.getX(), checkBean.getY(), checkBean.getR(), ((Boolean) checkBean.isResult()).toString());
       try {
           session = HibernateUtil.getSessionFactory().openSession();
           session.beginTransaction();
           session.persist(dbCheckBean);
           session.getTransaction().commit();
       }catch(Exception e)
       {
           session.getTransaction().rollback();
       }*/

    }



}
