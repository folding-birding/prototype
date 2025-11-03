using System;
using System.Collections;
using UnityEngine;

//Fly -> Wander , Fly -> Perch

public class FlyState : IBirdState
{
    public void Enter(Bird bird)
    {
        Debug.Log("Enters : {nameof(FlyState)}");
       
    }
    public void Update(Bird bird)
    {
        Debug.Log("구현중");
    }
    public void FixedUpdate(Bird bird)
    {

        Debug.Log("비행 구현은 FixedUpdate로 예정");

    }
    public void OnDone(Bird bird)
    {
        //완료 후 행동 없음 
    }


    public void Exit(Bird bird)
    {
        Debug.Log("Exits : {nameof(FlyState)}");
    }

}