
public interface IHealthObservable
{
    void AddObserver(IHealthObserver observer);
    void RemoveObserver(IHealthObserver observer);

}
