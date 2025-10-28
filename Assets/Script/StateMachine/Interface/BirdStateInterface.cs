using UnityEngine;


//Bird State 기본 interface
public interface BirdStateInterface
{
    void Enter(BirdStateMachine bird);
    void Update(BirdStateMachine bird);
    void Exit(BirdStateMachine bird);
}
