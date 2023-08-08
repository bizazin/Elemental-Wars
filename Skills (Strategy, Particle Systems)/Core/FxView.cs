using UnityEngine;

namespace UI.FX.Core
{
    public abstract class FxView : MonoBehaviour
    {
        public abstract void Play();

        public abstract void Stop();
    }
}