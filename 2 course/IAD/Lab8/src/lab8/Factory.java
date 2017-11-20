package lab8;

public class Factory {

    private static CheckBeanDAOimpl checkBeanDAOimpl = null;
    private static Factory instance = null;

    public static synchronized Factory getInstance(){
        if (instance == null){
            instance = new Factory();
        }
        return instance;
    }

    public static synchronized CheckBeanDAOimpl getCheckBeanDAOimpl(){
        if (checkBeanDAOimpl == null){
            checkBeanDAOimpl = new CheckBeanDAOimpl();
        }
        return checkBeanDAOimpl;
    }



}
