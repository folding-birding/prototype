using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

//StateEnum내의 State Name과 instance를 Dictionary로 관리 

public class BirdStateMachine
{
    public IBirdState CurrentState { get; private set; }
    
    private Dictionary<StateEnum, IBirdState> states = new Dictionary<StateEnum, IBirdState>();

    public BirdStateMachine(StateEnum stateEnum, IBirdState state)
    {
        AddState(stateEnum, state);

    }

    //미등록된 State 자동 등록 
    public void AddState(StateEnum stateEnum, IBirdState state)
    {
        if (!states.ContainsKey(stateEnum))
        {
            states.Add(stateEnum, state);
            CurrentState = GetState(stateEnum);
        }
    }

    public IBirdState GetState(StateEnum stateEnum)
    {
        if (states.TryGetValue(stateEnum, out IBirdState state))
            return state;
        return null;
    }

    //상태 전환 : 종료 후 다음 state 취함 
    public void ChangeState(StateEnum nextstateName)
    {
        CurrentState?.Exit();

        if(states.TryGetValue(nextstateName, out IBirdState newState))
        {
            CurrentState = newState;
        }

        CurrentState?.Enter();
    }


    //중복 상태 방지 
    public void DeleteState(StateEnum stateEnum)
    {
        if(states.ContainsKey(stateEnum))
        {
            states.Remove(stateEnum);
        }
    }

    public void OnDoneState()
    {
        CurrentState?.OnDone();
    }

    public void UpdateState()
    {
        CurrentState?.Update();
    }

    public void FixedupdateState()
    {
        CurrentState?.FixedUpdate();
    }
}


