﻿using NabaGame.Core.Runtime.Utils;
using UnityEngine;

namespace NabaGame.Core.Runtime.Process
{
    public class CountDownProcess : Process
    {
        public delegate void OnTimeUpdate(int _current);

        public event OnTimeUpdate TimeUpdateCallback;

        protected int startNumber;
        protected int endNumber;
        private float timer = 0f;
        private float pausedDuration = 0;
        private float startPauseTime = 0;
        public int Current { get; set; } = 0;

        public CountDownProcess()
        {
            this.startNumber = 0;
            this.endNumber = 0;
        }

        public CountDownProcess(int _startNumber, int _endNumber)
        {
            this.startNumber = _startNumber;
            this.endNumber = _endNumber;
        }

        public void SetInfo(int _startNumber, int _endNumber)
        {
            this.startNumber = _startNumber;
            this.endNumber = _endNumber;
        }

        public override void Update(float dt)
        {
            timer += dt;
            if (timer >= 1)
            {
                Current--;
                TimedUpdate();

                if (Current <= endNumber)
                {
                    Terminate();
                    return;
                }

                timer = 0;
            }
        }

        public override void OnBegin()
        {
            timer = 0;
            pausedDuration = 0;
            startPauseTime = 0;
            Current = startNumber;
            if (TimeUpdateCallback != null)
            {
                TimeUpdateCallback.Invoke(Current);
            }
        }

        public override void OnTerminate()
        {
            Current = endNumber;
            //TimedUpdate();
        }

        public virtual void TimedUpdate()
        {
            if (TimeUpdateCallback != null)
            {
                TimeUpdateCallback.Invoke(Current);
            }
        }

        public override void Pause(bool isPause)
        {
            if (isPause)
            {
                startPauseTime = TimeUtils.ConvertToTimeInSecond(UnbiasedTime.Instance.Now());
            }
            else
            {
                if (startPauseTime > 0)
                {
                    pausedDuration = TimeUtils.ConvertToTimeInSecond(UnbiasedTime.Instance.Now()) - startPauseTime;
                    Current -= Mathf.FloorToInt(pausedDuration);
                    Current = Current >= endNumber ? Current : endNumber;
                    if (Current <= endNumber)
                    {
                        Terminate();
                    }
                }
            }
        }
    }
}