using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

// bird 객체 관련 값
// 전역 접근 변수로 사용하는 대신에 읽기 전용 프로퍼티 적용할것  

public class Bird : MonoBehaviour
{
    public Bird Instance { get { return Instance; } }
    public BirdBehaviour Behaviour { get; private set; }
    public BirdStateMachine BirdStateMachine { get; private set; }
    public IBirdState CuurrentState { get; private set; }

    [SerializeField] private Rigidbody rigidBody;
    public Rigidbody RigidBody => rigidBody;
    [SerializeField] private Animator _animator;
    public Animator Animator
    {
        get { return _animator; }
    }

    [SerializeField] private GameObject particlePrefab;
    public GameObject ParticlePrefab => particlePrefab;

    [SerializeField] private Coroutine _birdCoroutine;
    public Coroutine BirdCoroutine
    {
        get { return _birdCoroutine; }
        set { _birdCoroutine = value; }
    }

    [Header("Flying")]
    [SerializeField] private float _speed;
    public float Speed => _speed;
    [SerializeField] private float _rotationSpeed; 
    public float RotationSpeed => _rotationSpeed;
    [SerializeField] private float _detection; 
    public float Detection => _detection;

    [SerializeField] private Vector3 _direction;
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    [SerializeField] private Transform Target;
    public Transform Birdtarget
    {
        get { return Target; }
        set { Target = value; }
    }

    [Header("Wander")]
    [SerializeField] private float _wanderStrength;
    [SerializeField] private float _nextWanderTime;

    [Header("Avoidance")]
    [SerializeField] private float _minAvoidStrength;
    [SerializeField] private float _maxAvoidStrength;

    [Header("Spot")]
    [SerializeField] private GameObject _spotContainer;
    public List<Spot> _perchingSpots {  get; private set; }
   

    [Header("TestUI")]
    [SerializeField] private ScriptableObject TestUI;
    public void Awake()
    {
        Behaviour = GetComponent<BirdBehaviour>();
        BirdStateMachine = GetComponent<BirdStateMachine>();
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        //상태 추가시 반드시 Add States로 추가할것  
        //No Interaction State
        BirdStateMachine.AddState(StateEnum.Idle, new IdleState());
        BirdStateMachine.AddState(StateEnum.Fly, new FlyState());
        BirdStateMachine.AddState(StateEnum.Wander, new WanderState());
        BirdStateMachine.AddState(StateEnum.Sit, new SitState());

        //Interaction State
        BirdStateMachine.AddState(StateEnum.Feel, new FeelState());

        //최초 상태 :Idle 
        BirdStateMachine.SetState(StateEnum.Idle);

        /*Add State 체크 
        Debug.Log("States Count: " + _birdStateMachine.states.Count);
        Debug.Log("Current State" + _birdStateMachine.CurrentState.ToString()); */
    }
}
