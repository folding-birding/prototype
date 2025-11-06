using UnityEngine;

public class SitState : IBirdState
{
    public void Enter(Bird bird)
    {
        Debug.Log($"{bird.name} Enters : {nameof(SitState)}");
        bird.BirdCoroutine = bird.StartCoroutine(bird.Behaviour.SitStay((bird) => OnDone(bird)));
    }
    public void Update(Bird bird)
    {
        throw new System.NotImplementedException();
    }
    public void OnDone(Bird bird)
    {
        if (bird.CurSpot == null)
            return;

        bird.CurSpot.Vacate();
        bird.PreSpot = bird.CurSpot;
        bird.CurSpot = null;
        bird.Behaviour.BirdTargetChanged(null);
        bird.Direction = bird.transform.forward;
        bird.BirdStateMachine.SetState(StateEnum.Fly);
    }

public void Exit(Bird bird)
    {
        Debug.Log($"{bird.name} Exits : {nameof(SitState)}");
        bird.BirdCoroutine = null;
    }
}