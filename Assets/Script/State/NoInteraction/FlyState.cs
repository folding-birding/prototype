using System;
using System.Collections;
using UnityEngine;

public class FlyState : IBirdState
{
    public void Enter(Bird bird)
    {
        Debug.Log("Enters : {nameof(FlyState)}");

    }

    public void Update(Bird bird)
    {
        Debug.Log("미구현");
    }
    public void FixedUpdate(Bird bird)
    {
        Debug.Log("미구현");
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