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
    public float WanderStrength => _wanderStrength;
    [SerializeField] private float _nextWanderTime;
    public float NextWanderTime => _nextWanderTime;

    [Header("Avoidance")]
    [SerializeField] private float _minAvoidStrength;
    public float MinAvoidStrength => _minAvoidStrength;
    [SerializeField] private float _maxAvoidStrength;
    public float MaxAvoidStrength => _maxAvoidStrength;

    [Header("Spot")]
    [SerializeField] private Spot _curSpot;
    public Spot CurSpot
    {
        get { return _curSpot; }
        set { _curSpot = value; }
    }
    [SerializeField] private Spot _preSpot;
    public Spot PreSpot
    {
        get { return _preSpot; }
        set { _preSpot = value; }
    }

    [SerializeField] private GameObject _spotContainer;
    [SerializeField] private List<Spot> _sittingSpots;
    public List<Spot> SittingSpots => _sittingSpots;

    [Header("TestUI")]
    [SerializeField] private ScriptableObject TestUI;

    //Component 초기화 & Spot transform 자동 할당  
    public void Awake()
    {
        Behaviour = GetComponent<BirdBehaviour>();
        BirdStateMachine = GetComponent<BirdStateMachine>();

        if (_spotContainer != null)
        {
            for (int i = 0; i < _spotContainer.transform.childCount; i++)
            {
                _sittingSpots.Add(_spotContainer.transform.GetChild(i).GetComponent<Spot>());
            }
           transform.rotation = UnityEngine.Random.rotation;
           _direction = transform.forward;
          
        }
    }
    //State 등록 
    public void Start()
    {
        //상태 추가시 반드시 Add States로 추가할것  
        BirdStateMachine.AddState(StateEnum.Idle, new IdleState());
        BirdStateMachine.AddState(StateEnum.Fly, new FlyState());
        BirdStateMachine.AddState(StateEnum.Sit, new SitState());

        //Interaction State
        BirdStateMachine.AddState(StateEnum.Feel, new FeelState());

        BirdStateMachine.SetState(StateEnum.Idle);
        /*Add State 체크 
        Debug.Log("States Count: " + _birdStateMachine.states.Count);
        Debug.Log("Current State" + _birdStateMachine.CurrentState.ToString()); */
    }
}
