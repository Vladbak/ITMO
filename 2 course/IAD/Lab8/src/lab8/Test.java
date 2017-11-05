package lab8;

import org.hibernate.Session;

public class Test {
	   public static void main(String[] args) {
	       Session session = HibernateUtil.getSessionFactory().openSession();

	       session.beginTransaction();
	       LoginBean loginBean = new LoginBean();
	       
	       loginBean.setId(1);
	       loginBean.setLogin("John");
	       loginBean.setPassword("123");
	       session.save(loginBean);
	       session.getTransaction().commit();

	   }
}


