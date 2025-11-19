using System;

public interface IHealth 
{
    event Action<int> HealthChanged;
}
