using Meta.WitAi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

//Bird 움직임 관련 
public class BirdBehaviour : MonoBehaviour 
{
    public Bird bird { get; private set; }
    public event Action<Transform> OnTargetChanged;
    private Transform _target
    {
        get => bird.Birdtarget;
        set
        {
            if (_target != value)
            {
                _target = value;
                OnTargetChanged?.Invoke(_target);
            }
        }
    }

    private Vector3 _direction;
    private float _detection;
    private float _rotationSpeed;
    private float _speed;

    private float _minAvoidStrength;
    private float _maxAvoidStrength;
    private float _wanderStrength;
    private float _nextWanderTime;

    private GameObject _ParticlePrefab;
    private List<Spot> _sittingSpots;

    private void Start()
    {
        bird = GetComponent<Bird>();
        OnTargetChanged += BirdTargetChanged;

        if (bird != null)
        {
            _target = bird.Birdtarget;
            _direction = bird.Direction;
            _detection = bird.Detection;
            _rotationSpeed = bird.RotationSpeed;
            _speed = bird.Speed;

            _minAvoidStrength = bird.MinAvoidStrength;
            _maxAvoidStrength = bird.MaxAvoidStrength;

            _wanderStrength = bird.WanderStrength;
            _nextWanderTime = bird.NextWanderTime;

            _sittingSpots = bird.SittingSpots;
            _ParticlePrefab = bird.ParticlePrefab;
        }
    }

    //FlyAround : Fly With Avoid 
    public IEnumerator FlyAround(Action<Bird> onDone)
    {
        
        while (true)
        {
            //Fly


            //Avoidance
            //1) Ray Hit로 전방 탐지 2) Sample Ray로 회피 방향 확인 
           
            Vector3 heading = transform.forward;
            Vector3[] samples = {
                Quaternion.AngleAxis(15, transform.up) * heading,
                Quaternion.AngleAxis(-15, transform.up) * heading,
                Quaternion.AngleAxis(15, transform.right) * heading,
                Quaternion.AngleAxis(-15, transform.right) * heading
            };

            RaycastHit hit;
            // if (Physics.Raycast(transform.position, heading, out hit, _detection))
            if (Physics.SphereCast(transform.position, 0.1f, heading, out hit, _detection))
            {
                var count = 0;
                var noramlSum = Vector3.zero;
                var sampleRange = hit.distance;

                RaycastHit sampleHit;
                foreach (var rayDir in samples)
                {
                    if (Physics.Raycast(transform.position, rayDir, out sampleHit, sampleRange))
                    {
                        count++;
                        noramlSum += sampleHit.normal;
                    }
                }
                var avgNormal = (noramlSum / count).normalized;

                Vector3 avoidDir;
                if (count == 0) {   avoidDir = -_direction;  }
                else
                {
                    avoidDir = Vector3.Reflect(_direction, avgNormal).normalized;
                }

                float t = Mathf.Clamp01(1f - hit.distance / _detection);
                float avoidStrength = Mathf.Lerp(_minAvoidStrength, _maxAvoidStrength, t);

                _direction = Vector3.SlerpUnclamped(_direction, avoidDir, count * avoidStrength * Time.deltaTime).normalized;
            }

            // Wandering
            else
            {
                if (Time.time >= _nextWanderTime)
                {
                    Vector3 randomJitter = _wanderStrength * UnityEngine.Random.onUnitSphere;
                    _direction = (_direction + randomJitter).normalized;
                    _nextWanderTime = Time.time + UnityEngine.Random.value;
                }
            }

            Quaternion rot = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, _rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * _speed * Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator FollowTo(Action<Bird> onDone)
    {
        //Follow
;       yield return null;
    }

    //Spot 탐색 & Target 업데이트 
    public Spot FindSittingSpot()
    {
        Debug.Log("앉을곳 탐색");
        _detection *= 0.5f;

        foreach (var spot in _sittingSpots)
        {
            float sqrDistance = Vector3.SqrMagnitude(bird.transform.position - spot.transform.position);

            if(sqrDistance < _detection)
            {
                return spot;
            }
        }
        return null;
    }
    public void BirdTargetChanged(Transform Target)
    {
        _target = Target;
    }

    //sit : Handle, Sitting State에서 사용 
    public IEnumerator Sitting(Action<Bird> onDone)
    {
        yield return null;
    }

    public IEnumerator SitStay(Action<Bird> onDone)
    {
        float waitTime = UnityEngine.Random.Range(5f, 15f);
        yield return new WaitForSeconds(waitTime);
        onDone?.Invoke(bird);
    }

    //Feeling : With Particle 
    public IEnumerator Feeling(Action<Bird> onDone)
    {
        GameObject HeartParticle = Instantiate(_ParticlePrefab, bird.transform.position, Quaternion.identity);
        ParticleSystem ps = HeartParticle.GetComponent<ParticleSystem>();

        if (ps != null)
        {
            ps.Play();
            yield return new WaitForSeconds(3.0f);
        }
        onDone?.Invoke(bird);

    }

}
