
using Unity.VisualScripting;
using UnityEngine;

//추후 삭제해도 됩니다 

[CreateAssetMenu(fileName = "UIFollow", menuName = "Scriptable Objects/UIFollow")]
public class UIFollow : ScriptableObject
{
    public Bird bird { get; private set; }

    public bool isFollowing = false;
    public bool isHandling = false;
    public bool isFeeling = false;
    public bool isBoid = false;
}
