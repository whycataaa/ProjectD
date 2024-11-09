using UnityEngine;

namespace MY_Framework.Time
{
    public delegate void CompleteEvent();
    public delegate void UpdateEvent(float t);
    //计时器
    public class Timer : MonoBehaviour
    {
        UpdateEvent onUpdateEvent;
        CompleteEvent onCompleted;
        // 标识这个timer的唯一ID号;
        public int TimerID;
        // 要触发的次数;
        public int Repeat;
        // 计时目标时间
        public float TimeTarget;
        // 开始计时的时间;
        public float TimeStart;
        // 计时偏差
        public float TimeOffset;
        // 计时时间（正计时）
        public float Now;
        // 计时时间（倒计时）
        public float DownNow;
        // 暂停时间
        public float PauseTime;
        // 定时器触发的时间间隔;
        public float RepeatRate;

        // 这个Timer过去的时间;
        public float TimePassed;
        // 是否正在计时
        public bool IsRunning=false;
        // 是否暂停
        public bool IsPause=false;
        // 是否结束
        public bool IsEnd=false;
        // 是否在计时后删除
        public bool IsDestroy;
        // 是否忽略时间速率
        public bool IsIgnoreTimeScale;

        // 是否打印日志
        public bool IsLog;
        //是否是倒计时
        public bool IsCountDown;

        //当前时间
        public float TimeNow
        {
            get
            {
                return IsIgnoreTimeScale ? UnityEngine.Time.realtimeSinceStartup: UnityEngine.Time.time;
            }
        }

        /// <summary>
        /// 开启计时器
        /// </summary>
        /// <param name="_TimerID">ID</param>
        /// <param name="_TimeTarget">目标时间</param>
        /// <param name="_CompleteEvent">完成计时回调</param>
        /// <param name="_UpdateEvent">进行计时回调</param>
        /// <param name="_IsIgnoreTimeScale">是否忽略时间倍率</param>
        /// <param name="_RepeatTime">重复次数</param>
        /// <param name="_Offset">计时偏差</param>
        /// <param name="_IsLog">是否打印日志</param>
        /// <param name="_IsCountDown">是否为倒计时</param>
        /// <param name="_IsDestroy">是否结束后销毁计时器</param>
        public void StartTimer(
                                int _TimerID,
                                float _TimeTarget,
                                CompleteEvent _CompleteEvent=null,
                                UpdateEvent _UpdateEvent=null,
                                bool _IsIgnoreTimeScale=true,
                                int _RepeatTime=0,
                                float _Offset=0f,
                                bool _IsLog=true,
                                bool _IsCountDown=false,
                                bool _IsDestroy=true
                                )
        {
            TimerID=_TimerID;
            TimeTarget=_TimeTarget;
            IsIgnoreTimeScale=_IsIgnoreTimeScale;
            Repeat=_RepeatTime;
            TimeOffset=_Offset;
            IsLog=_IsLog;
            IsCountDown=_IsCountDown;
            IsDestroy=_IsDestroy;


            TimeStart=TimeNow;

            if(_CompleteEvent!=null)
            {
                onCompleted=_CompleteEvent;
            }
            if(_UpdateEvent!=null)
            {
                onUpdateEvent=_UpdateEvent;
            }
            IsRunning=true;
        }
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(IsRunning)
            {
                Now= TimeNow-TimeOffset-TimeStart;
                DownNow=TimeTarget-Now;
                
                if(IsCountDown)
                {
                    if(onUpdateEvent!=null)
                    onUpdateEvent(DownNow);
                }
                else
                {
                    if(onUpdateEvent!=null)
                    onUpdateEvent(Now);
                }

                if(Now>TimeTarget)
                {
                    if(onCompleted!=null)
                    {
                        onCompleted();
                    }
                    if(Repeat==0)
                    {
                        StopTimer();
                    }
                    else
                    {
                        RestartTimer();
                    }
                }
            }
        }

        public float GetLastTime()
        {
            return Mathf.Clamp(TimeTarget-Now,0,TimeTarget);
        }

        /// <summary>
        /// 重新计时
        /// </summary>
        private void RestartTimer()
        {
            TimeStart = TimeNow;
            TimeOffset=0;
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        private void StopTimer()
        {
            IsRunning = false;
            IsEnd = true;
            if(IsDestroy)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 暂停计时器
        /// </summary>
        public void PauseTimer()
        {
            if(IsEnd)
            {
                if(IsLog)
                {
                    Debug.Log("Timer is end");
                }
            }
            else
            {
                if(IsRunning)
                {
                    IsRunning = false;
                    PauseTime = TimeNow;
                }
            }
        }

        public void ChangeTargetTime(float _TargetTime)
        {
            TimeTarget=_TargetTime;
            TimeStart=TimeNow;
        }


    }
}
