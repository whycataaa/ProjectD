using System.Collections.Generic;
using UnityEngine;

namespace MY_Framework.FSM
{
    public enum StateType
    {
        Action_Idle,
        Action_Move,
        Action_JumpUp,
        Action_JumpDown,
        Action_Ladder,
        Action_Attack,
        Action_Die,

        GameProcedure_Init,
        GameProcedure_Start,

    }


    public interface IState
    {
        void OnEnter();
        void OnExit();
        void OnUpdate();
        //void OnFixedUpdate();
    }

    //需要手动挂载
    [System.Serializable]
    public abstract class BlackBoard:ScriptableObject
    {

    }
    public class FSM
    {
        public IState CurrentState;
        public Dictionary<StateType,IState> StateDic;
        public BlackBoard blackBoard;

        public FSM(BlackBoard _blackBoard)
        {
            StateDic = new Dictionary<StateType, IState>();
            this.blackBoard = _blackBoard;
        }

        public void AddState(StateType type, IState state)
        {
            if(StateDic.ContainsKey(type))
            {
                Debug.Log("重复添加状态:"+type);
                return;
            }

            StateDic.Add(type, state);

        }

        public void ChangeState(StateType stateType)
        {
            if(!StateDic.ContainsKey(stateType))
            {
                Debug.Log("字典不存在状态:"+stateType);
                return;
            }

            if(CurrentState!=null)
            {
                CurrentState.OnExit();
            }
            CurrentState = StateDic[stateType];
            CurrentState.OnEnter();
        }

        public void OnUpdate()
        {
            CurrentState.OnUpdate();
        }

        // public void OnFixedUpdate()
        // {
        //     CurrentState.OnFixedUpdate();
        // }

    }

}
