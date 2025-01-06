
namespace CoreLib.Base;

public class CArray<T> where T : struct
{
    private readonly T[] _arr;

    public CArray(int n)
    {
        if (n < 0)
            Logger.Info("n must be >= 0");

        _arr = new T[n];
    }

    public T GetValue(int n)
    {
        if (n < _arr.Length)
            return _arr[n];
        else
            Logger.Info("Index out of range");
        return _arr[0];
    }

    public void InsertValue(int n, T value)
    {
        for (int i = _arr.Length - 1; i > n; i--)
        {
            _arr[i] = _arr[i - 1];
        }

        _arr[n] = value;
    }

    public void DeleteValue(int n)
    {
        for (int i = n; i < _arr.Length - 1; i++)
        {
            _arr[i] = _arr[i + 1];
        }

        _arr[_arr.Length - 1] = default(T);
    }

    public int FindValue(T value)
    {
        for (int i = 0; i < _arr.Length; i++)
        {
            if (_arr[i].Equals(value))
                return i;
        }
        return -1;
    }
}
