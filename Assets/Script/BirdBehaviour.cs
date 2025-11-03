using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bird 움직임 관련 
public class BirdBehaviour : MonoBehaviour 
{
    public Bird bird { get; private set; }

    private Transform _target;
    private Vector3 _direction;
    private float _detection;
    private float _rotationSpeed;
    private float _speed;
    private Rigidbody _rb;

    private void Awake()
    {
        if (bird != null)
        {
            _target = bird._target;
            _direction = bird._direction;
            _detection = bird.Detection;
            _rotationSpeed = bird.RotationSpeed;
            _speed = bird.Speed;
            _rb = bird.RigidBody;
        }
    }

    public IEnumerator HangAround(Action<BirdBehaviour> onDone)
    {
        float waitTime = UnityEngine.Random.Range(5f, 15f);
        yield return new WaitForSeconds(waitTime);
        onDone?.Invoke(this);
    }


    public IEnumerator HoverAround(Action<BirdBehaviour> onDone)
    {
        while (true)
        {
            Vector3 toTarget = _target.position - transform.position;
            float distanceToTarget = toTarget.magnitude;
            float factor = Mathf.Clamp01(distanceToTarget / _detection);

            _direction = Vector3.SlerpUnclamped(_direction, toTarget.normalized, Time.deltaTime).normalized;

            Quaternion rot = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, _rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * _speed * Time.deltaTime;

            yield return null;
        }
    }
}
