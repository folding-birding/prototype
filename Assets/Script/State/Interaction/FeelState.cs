using System.Collections;
using UnityEngine;

//Stroke Hand pose에 따른 bird의 Feeling(감정 표현)
//추후에는 Enter() 대신 Hand Pose Method일 때 업데이트되도록 진행 
public class FeelState : IBirdState 
{
    public void Enter(Bird bird)
    {
      Debug.Log($"{bird.name} Enters : {nameof(FeelState)}");

       bird.BirdCoroutine = bird.StartCoroutine(bird.Behaviour.Feeling(Exit));
        
    }

    public void Update(Bird bird)
    {
       
    }
    public void FixedUpdate(Bird bird)
    {
        //물리 동작 없거나 추후 추가. 
    }

    public void Exit(Bird bird)
    {
        Debug.Log($"{bird.name} Exits : {nameof(FeelState)}");
        bird.BirdStateMachine.SetState(StateEnum.Handle);
    }


}
