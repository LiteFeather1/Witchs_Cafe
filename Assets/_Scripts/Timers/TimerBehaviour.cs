using UnityEngine;

namespace LTFUtils
{
    public abstract class TimerBehaviour<T> : MonoBehaviour where T : Timer
    {
        [SerializeField] protected T _timer;
        public T Timer => _timer;

        private void Update() => _timer.Tick();
    }
 }