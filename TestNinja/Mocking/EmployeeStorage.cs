namespace TestNinja.Mocking;

public interface IEmployeeStorage
{
    void DeleteEmployee(int id);
}

public class EmployeeStorage : IEmployeeStorage
{
    private EmployeeController.EmployeeContext _db;

    public EmployeeStorage(IEmployeeStorage employeeStorage)
    {
        _db = new EmployeeController.EmployeeContext();
    }
    
    public void DeleteEmployee(int id)
    {
        _db.Employees.Remove(_db.Employees.Find(id));
        _db.SaveChanges();
    }
}