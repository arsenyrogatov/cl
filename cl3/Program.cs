ServerTest.RunTest(); //запускаем тест

public static class ServerTest
{
    public static void RunTest()
    {
        const int ReaderCount = 5;
        const int WriterCount = 3;

        List<User> users = new(); //создаем список пользователей 
        for (int i = 0; i < ReaderCount; i++)
            users.Add(new ReaderUser()); //добавляем читателей
        for (int i = 0; i < WriterCount; i++)
            users.Add(new WriterUser()); //добавляем писателей

        Parallel.ForEach(users.OrderBy(x => x.Id), user =>
        {
            user.DoSome(); //читаем или записываем в зависимости от типа пользователя
        });
    }
}

public abstract class User //абстрактный пользователь
{
    protected static readonly Random random = new();
    public User()
    {
        Id = random.Next(1000);
    }
    public int Id { get; set; }
    public abstract void DoSome();
}

public class ReaderUser : User //читатель
{
    public override void DoSome()
    {
        int value = Server.GetCount(); //читаем
        Console.WriteLine($"Reader {Id}: Count = {value}");
        Thread.Sleep(random.Next(500, 1000));
    }
}

public class WriterUser : User //писатель
{
    public override void DoSome()
    {
        int value = random.Next(1, 10);
        Server.AddToCount(value); //записываем
        Console.WriteLine($"Writer {Id}: Added {value} to Count");
        Thread.Sleep(random.Next(500, 1000));
    }
}

public static class Server //класс сервера
{
    private static int count; 
    private static ReaderWriterLockSlim locker = new();

    public static void AddToCount(int value)
    {
        locker.EnterWriteLock(); //блокируем
        count += value; //записываем
        locker.ExitWriteLock(); //разблокируем
    }

    public static int GetCount()
    {
        locker.EnterReadLock(); //блокируем 
        try
        {
            return count; //читаем
        }
        finally
        {
            locker.ExitReadLock(); //разблокируем
        }
    }
}