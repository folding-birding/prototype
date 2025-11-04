using System;
using System.Collections;
using UnityEngine;

//Fly -> Wander , Fly -> Perch

public class FlyState : IBirdState
{
    public void Enter(Bird bird)
    {
        Debug.Log($"{bird.name} Enters : {nameof(FlyState)}");
        Time.timeScale = 1.0f;
       
    }
    public void Update(Bird bird)
    {
        
    }
    public void FixedUpdate(Bird bird)
    {
        Debug.Log("FixedUpdate 작동 중! Time Scale: " + Time.timeScale);
        while (true)
        {
            bird.RigidBody.AddForce(bird.transform.forward * 0.3f);
        }
    }
    public void OnDone(Bird bird)
    {
        //완료 후 행동 없음 
    }


    public void Exit(Bird bird)
    {
        Debug.Log($"{bird.name} Exits : {nameof(FlyState)}");
        
        //bird.BirdStateMachine.SetState(StateEnum.Wander);
        //bird.BirdStateMachine.SetState(StateEnum.Sit);
    }

}