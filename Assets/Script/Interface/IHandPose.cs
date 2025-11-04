using System;
using UnityEngine;


//기존 프로젝트 바탕으로 Hand pose Interface 작성 (추후 수정해도 무방) 

public interface IHandPose
{

    void HandlePalmUpSelected(BirdBehaviour bird);
    void HandlePalmUpUnselected(BirdBehaviour bird);
    void HandlePerchSelected(BirdBehaviour bird);
    void HandlePerchUnselected(BirdBehaviour bird);
}
