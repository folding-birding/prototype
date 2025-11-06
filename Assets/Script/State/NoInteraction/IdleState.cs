using System.Collections;
using UnityEngine;

//Idle -> Fly

public class IdleState : IBirdState
{
    public void Enter(Bird bird)
    {
         Debug.Log("Enters : {nameof(IdleState)}");
         bird.Animator.SetTrigger("ToFly");
    }

    public void Update(Bird bird)
    {
       
        Debug.Log(" Idle no problem");
        bird.BirdStateMachine.SetState(StateEnum.Fly);
    }

    public void OnDone(Bird bird)
    {
      
    }

    public void Exit(Bird bird)
    {
        Debug.Log("Exits : {nameof(IdleState)}");
    }


}