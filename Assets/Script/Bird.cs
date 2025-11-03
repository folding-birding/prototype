using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

// bird 객체 관련 값
// 전역 접근 변수로 사용하는 대신에 읽기 전용 프로퍼티 적용할것  

public class Bird : MonoBehaviour
{
    public Bird Instance { get { return Instance; } }
    public BirdBehaviour Behaviour { get; private set; }
    public BirdStateMachine BirdStateMachine { get; private set; }
    public IBirdState CuurrentState;

    public Rigidbody RigidBody;
    public Animator _animator { get; private set; }
    public Animator Animator
    {
        get { return _animator; }
    }

    public Coroutine _birdCoroutine { get; private set; } 
    public Coroutine BirdCoroutine
    {
        get { return _birdCoroutine; }
        set { _birdCoroutine = value; }
    }

    [Header("Flying")]
    public Vector3 _direction { get; private set;}
    public float Speed { get; private set;}
    public float RotationSpeed {  get; private set;}
    public float Detection {  get; private set; }
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public Transform _target {get; private set;}
    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }

    [Header("Wander")]
    [SerializeField] private float _wanderStrength;
    public float _nextWanderTime { get; private set; }

    [Header("Avoidance")]
    [SerializeField] private float _minAvoidStrength;
    [SerializeField] private float _maxAvoidStrength;

    [Header("Spot")]
    [SerializeField] private GameObject _spotContainer;
    [SerializeField] private List<Spot> _perchingSpots;


    public void Awake()
    {
        BirdStateMachine = GetComponent<BirdStateMachine>();
        RigidBody = GetComponent<Rigidbody>();

    }

    public void Start()
    {

        BirdStateMachine.AddState(StateEnum.Idle, new IdleState());
        BirdStateMachine.AddState(StateEnum.Fly, new FlyState());
        BirdStateMachine.AddState(StateEnum.Wander, new WanderState());

        //상태 추가시 반드시 Add States로 추가할것  

        BirdStateMachine.SetState(StateEnum.Idle);

        /*Add State 체크 
        Debug.Log("States Count: " + _birdStateMachine.states.Count);
        Debug.Log("Current State" + _birdStateMachine.CurrentState.ToString()); */

    }
}
