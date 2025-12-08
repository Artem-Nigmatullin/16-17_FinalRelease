using System;

public class ReactiveVariable<T> : IDisposable where T : IEquatable<T>
{
    public event Action<T, T> ChangedDetailed;
    public event Action<T> Changed;
    public event Action Dead;
    private T _value;


    public ReactiveVariable() => _value = default(T);

    public ReactiveVariable(T value) => _value = value;


    public T Value
    {

        get => _value;
        set
        {
            T oldValue = _value;
            _value = value;
            if (_value.Equals(oldValue) == false)
            {
                ChangedDetailed?.Invoke(oldValue, value);
                Changed?.Invoke(value);

                if (Convert.ToInt16(value) == 0)
                {
                    Dead?.Invoke();
                }
            }
        }
    }

    public void Dispose()
    {




    }
}
