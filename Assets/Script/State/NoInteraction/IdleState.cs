using System.Collections;
using UnityEngine;

public class IdleState : IBirdState
{
    public void Enter(Bird bird)
    {
         Debug.Log("Enters : {nameof(IdleState)}");
    }

    public void Update(Bird bird)
    {
        //bird.Animator.SetTrigger("ToFly");

        Debug.Log(" Idle no problem");
        bird._birdStateMachine.SetState(StateEnum.Fly);
    }

    public void FixedUpdate(Bird bird)
    {
        //Idle 물리 이동 없음 
    }

    public void OnDone(Bird bird)
    {
        //완료 후 행동 없음 
    }

    public void Exit(Bird bird)
    {
        Debug.Log("Exits : {nameof(IdleState)}");

        //bird.BehaviourCoroutine = null;
    }

}