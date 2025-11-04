using UnityEngine;

//Fly -> Wander 
public class WanderState : IBirdState
{
    public void Enter(Bird bird)
    {
        Debug.Log($"{bird.name} Enters : {nameof(WanderState)}");
    }

    public void Exit(Bird bird)
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate(Bird bird)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Bird bird)
    {
        throw new System.NotImplementedException();
    }

}
