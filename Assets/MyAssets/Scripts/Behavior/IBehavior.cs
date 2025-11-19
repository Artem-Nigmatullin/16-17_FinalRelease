using System.Numerics;

public interface IBehavior
{
    string Name { get; }
    void Enter();
    void Update();
    void Exit();
}
