using System.Collections.Generic;
using UnityEngine;
using MY_Framework.Singleton;
using Unity.VisualScripting;

namespace MY_Framework.Time
{
    /// <summary>
    /// 管理所有的计时器
    /// </summary>
    public class TimerManager : SingletonMono<TimerManager>
    {
        // 存放所有计时器
        public Dictionary<int,Timer> TimerDic = new Dictionary<int,Timer>();

        int ID=1;

        public int CreateTimer()
        {
            TimerDic.Add(ID,this.AddComponent<Timer>());
            ID++;
            return ID-1;
        }

        public void StartTimer  (
                                int _ID,
                                float _TimeTarget,
                                CompleteEvent _CompleteEvent,
                                UpdateEvent _UpdateEvent,
                                bool _IsIgnoreTimeScale,
                                int _RepeatTime,
                                float _Offset,
                                bool _IsLog,
                                bool _IsCountDown,
                                bool _IsDestroy
                                )
        {
            if(!TimerDic.ContainsKey(_ID))
            {
                var id=CreateTimer();
                Debug.Log($"不存在ID为{_ID}的计时器,已创建ID为{id}的计时器");
            }
            if(!(TimerDic[_ID] is null))
            {
                TimerDic[_ID].StartTimer(
                                            _ID,_TimeTarget,_CompleteEvent,_UpdateEvent,
                                            _IsIgnoreTimeScale,_RepeatTime,_Offset,_IsLog,_IsCountDown,_IsDestroy
                                        );
            }
            else
            {
                Debug.Log("ID:"+_ID+"的计时器不存在");
            }
        }



        public void PauseTimer(int _ID)
        {
            if(!(TimerDic[_ID] is null))
            {
                TimerDic[_ID].PauseTimer();
            }
            else
            {
                Debug.Log("ID:"+_ID+"的计时器不存在");
            }
        }
        public void DestroyTimer(int _ID)
        {
            if(TimerDic.ContainsKey(_ID))
            {
                //移除脚本并移出字典
                Destroy(TimerDic[_ID]);
                TimerDic.Remove(_ID);
            }
        }

    }
}
