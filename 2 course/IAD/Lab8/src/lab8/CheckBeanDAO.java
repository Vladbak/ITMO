package lab8;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public interface CheckBeanDAO {
    public void addCheck(CheckBean checkBean) throws SQLException;
    public CheckBean getCheck(long id) throws SQLException;
    public List<CheckBean> getAllChecks() throws SQLException;
}
