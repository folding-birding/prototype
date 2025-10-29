using System;
using System.Collections.Generic;
using UnityEngine;

// bird 객체 관련 정보용 
// speed, direction 등 

public class Bird : MonoBehaviour 
{
    public Bird instance { get { return instance; } }
    public BirdStateMachine stateMachine { get; private set; }
    public Rigidbody rigidBody { get; private set; }    

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

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            rigidBody = GetComponent<Rigidbody>();
           
        }
    }

    //상태 전환 체크 

    public void Start()
    {
        InitStateMachine();

    }

    private void Update()
    {
        stateMachine?.UpdateState();
    }

    private void FixedUpdate()
    {
        stateMachine?.FixedupdateState();
    }

    private void OnDone()
    {
        stateMachine?.OnDoneState();
    }

    //상태 등록 
    private void InitStateMachine()
    {
        throw new NotImplementedException();
    }
}
