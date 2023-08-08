using UnityEngine;
using Zenject;

namespace UI.FX.Core
{
    public class FxPool<T> : MemoryPool<Transform, T>, IFxPool<T> where T : FxView
    {
        protected override void Reinitialize(Transform parent, T item)
        {
            item.gameObject.SetActive(true);
            var transform = item.transform;
            transform.SetParent(parent, false);
        }
    }
}