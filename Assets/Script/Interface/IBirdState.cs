using System.Collections;
using UnityEngine;


//Bird State 기본 interface
public interface IBirdState
{
    void Enter(Bird bird);

    void Update(Bird bird);

    void FixedUpdate(Bird bird);

    void Exit(Bird bird);

}
