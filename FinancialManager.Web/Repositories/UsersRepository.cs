namespace FinancialManager.Web.Repositories;

public interface IUsersRepository
{
    int SelectUserId();
}

public class UsersRepository : IUsersRepository
{
    public int SelectUserId()
    {
        return 1;
    }
}