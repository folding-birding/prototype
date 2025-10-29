using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

// bird 객체 관련 값
// speed, direction 등 

public class Bird : MonoBehaviour 
{
    public Bird instance { get { return instance; } }
    public BirdStateMachine _birdStateMachine { get; private set; }
    public IBirdState _currentState;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Animator _animator;
    public Animator Animator
    {
        get { return _animator; }
    }

    [Header("Flying")]
    [SerializeField] private Vector3 _direction;
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _detection;
    [SerializeField] private Transform _target;

    [Header("Wander")]
    [SerializeField] private float _wanderStrength;
    private float _nextWanderTime;

    [Header("Avoidance")]
    [SerializeField] private float _minAvoidStrength;
    [SerializeField] private float _maxAvoidStrength;

    [Header("Spot")]
    [SerializeField] private GameObject _spotContainer;
    [SerializeField] private List<Spot> _perchingSpots;


    public void Awake()
    {
        _birdStateMachine = GetComponent<BirdStateMachine>();
        _rigidBody = GetComponent<Rigidbody>();

    }

    public void Start()
    {

        _birdStateMachine.AddState(StateEnum.Idle, new IdleState());
        _birdStateMachine.AddState(StateEnum.Fly, new FlyState());
        //상태 추가시 반드시 생성 

        _birdStateMachine.SetState(StateEnum.Idle);

        /*Add State 체크 
        Debug.Log("States Count: " + _birdStateMachine.states.Count);
        Debug.Log("Current State" + _birdStateMachine.CurrentState.ToString()); */

    }
}
