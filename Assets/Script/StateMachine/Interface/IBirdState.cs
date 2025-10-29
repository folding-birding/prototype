using UnityEngine;


//Bird State 기본 interface
public interface IBirdState
{
    void Enter();

    void Update();

    void FixedUpdate();

    void OnDone();

    void Exit();
   
}
