using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//StateEnum내의 State Name과 instance를 Dictionary로 관리 

public class BirdStateMachine : MonoBehaviour 
{
    public Bird bird { get; private set; }
    public IBirdState CurrentState { get; set; }

    public Dictionary<StateEnum, IBirdState> states;

    private void Awake()
    {
        bird = GetComponent<Bird>();
        states = new Dictionary<StateEnum, IBirdState>();
    }
    public void Update()
    {
        UpdateState();
    }

    //상태 등록
    public void AddState(StateEnum stateEnum, IBirdState state)
    {
        if (states.ContainsKey(stateEnum)) return;

        states.Add(stateEnum, state);
    }

    //상태 세팅-전환 
    public void SetState(StateEnum stateEnum)
    {
        if(states.ContainsKey(stateEnum))
        {
            if(CurrentState != null)
            {
                CurrentState.Exit(bird);
            }
            CurrentState = states[stateEnum];
            CurrentState?.Enter(bird); 
        }
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
        CurrentState?.OnDone(bird);
    }

    public void UpdateState()
    {
        CurrentState?.Update(bird);
    }

    public void FixedupdateState()
    {
        CurrentState?.FixedUpdate(bird);
    }
}


