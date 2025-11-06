using System;
using System.Collections;
using UnityEngine;

//Fly -> Wander , Fly -> Perch

public class FlyState : IBirdState
{
    public void Enter(Bird bird)
    {
        Debug.Log($"{bird.name} Enters : {nameof(FlyState)}");
        bird.BirdCoroutine = bird.StartCoroutine(bird.Behaviour.FlyAround(null));
    }
    public void Update(Bird bird)
    {
        var spot = bird.Behaviour.FindSittingSpot();
        if (spot != bird.PreSpot && !spot.IsOccupied)
        {
            spot.Occupy();
            bird.CurSpot = spot;
            bird.Behaviour.BirdTargetChanged(spot.transform);

            bird.BirdStateMachine.SetState(StateEnum.Sit);
        }
    }
    public void OnDone(Bird bird)
    {
        throw new NotImplementedException();
    }

    public void Exit(Bird bird)
    {
        Debug.Log($"{bird.name} Exits : {nameof(FlyState)}");
        bird.StopCoroutine(bird.BirdCoroutine);
        bird.BirdCoroutine=null;
    }
}