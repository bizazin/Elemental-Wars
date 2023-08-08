using UnityEngine;
using Zenject;

namespace UI.FX.Core
{
    public interface IFxPool<T> : IMemoryPool<Transform, T> where T : FxView
    {
    }
}