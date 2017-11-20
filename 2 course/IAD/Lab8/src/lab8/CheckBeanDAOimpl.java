package lab8;

import org.hibernate.Session;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

@ManagedBean
@SessionScoped
public class CheckBeanDAOimpl implements CheckBeanDAO {

    public void addCheck(CheckBean checkBean) throws SQLException{
        Session session = null;
        try {
            session = HibernateUtil.getSessionFactory().openSession();
            session.beginTransaction();
            session.save(checkBean);
            session.getTransaction().commit();
        } catch (Exception e) {

        } finally {
            if (session != null && session.isOpen()) {
                session.close();
            }
        }

    };
    public CheckBean getCheck(long id) throws SQLException{

        Session session = null;
        CheckBean checkBean = null;
        try {
            session = HibernateUtil.getSessionFactory().openSession();
            checkBean = (CheckBean) session.load(CheckBean.class, id);
        } catch (Exception e) {

        } finally {
            if (session != null && session.isOpen()) {
                session.close();
            }
        }
        return checkBean;
    };
    public List<CheckBean> getAllChecks() throws SQLException{

        Session session = null;
        List<CheckBean> checkBeans =null;
        try {
            session = HibernateUtil.getSessionFactory().openSession();
           checkBeans= session.createCriteria(CheckBean.class).list();
        } catch (Exception e) {

        } finally {
            if (session != null && session.isOpen()) {
                session.close();
            }
        }
        return checkBeans;
    };
}









