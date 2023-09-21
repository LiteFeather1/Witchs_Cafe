﻿using UnityEngine;
using System;

namespace LTFUtils
{
    [Serializable]
    public abstract class Timer
    {
        [SerializeField] protected bool _canTick = false;
        [SerializeField] private TimeType _timeType = TimeType.DeltaTime;

        protected float _elapsedTime;
        public float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }
        public abstract float TimeToDo { get; protected set; }
        public float T => _elapsedTime / TimeToDo;
        public bool CanTick => _canTick;

        private float _speedMultiplier = 1;
        public float SpeedMultiplier => _speedMultiplier;

        public Action TimeEvent { get; set; }

        public void SetMultiplier(float multipler) => _speedMultiplier = multipler;

        private float GetTime()
        {
            return _timeType switch
            {
                TimeType.DeltaTime => Time.deltaTime,
                TimeType.UnscaledDeltaTime => Time.unscaledDeltaTime,
                _ => Time.deltaTime,
            };
        }

        public void Tick()
        {
            if (!_canTick)
                return;

            Ticking();
        }

        protected virtual void Ticking()
        {
            _elapsedTime += GetTime() * _speedMultiplier;
            if (_elapsedTime >= TimeToDo)
            {
                Reset();
                TimeEvent?.Invoke();    
            }
        }

        /// <summary>
        /// Change Time to do Event
        /// </summary>
        public void ChangeTime(float time) => TimeToDo += time;

        /// <summary>
        /// Set Time to do Event
        /// </summary>
        public void SetTime(float time) => TimeToDo = time;

        /// <summary>
        /// Stops ticking
        /// </summary>
        public void Stop()
        {
            _canTick = false;
        }

        /// <summary>
        /// Starts ticking
        /// </summary>
        public void Continue()
        {
            _canTick = true;
        }

        /// <summary>
        /// Changes the time to do the event and starts ticking
        /// </summary>
        public void Continue(float timeToDo)
        {
            TimeToDo = timeToDo;
            _canTick = true;
        }

        //not unity reset zz
        /// <summary>
        /// Resets elapsed time to 0
        /// </summary>
        public void Reset()
        {
            _elapsedTime = 0;
        }

        /// <summary>
        /// Stops ticking and Resets elapsed time to 0
        /// </summary>
        public void StopAndReset()
        {
            Stop();
            Reset();
        }

        /// <summary>
        /// Resets elapsed time to 0 and starts ticking 
        /// </summary>
        public void Restart()
        {
            Reset();
            Continue();
        }

        /// <summary>
        /// Resets elapsed time
        /// Starts Ticking
        /// Sets TimeToDo
        /// </summary>
        public void Restart(float timeToDo)
        {
            Reset();
            Continue(timeToDo);
        }
    }
 }
