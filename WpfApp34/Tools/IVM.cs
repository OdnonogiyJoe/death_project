using System;

namespace WpfApp34.Tools
{
    public interface IVM
    {
        (Type, Action<object>) RegisterTypeMessage()
        {
            return default;
        }
    }
}