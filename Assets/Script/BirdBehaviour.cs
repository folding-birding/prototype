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

    private Transform _target;
    private Vector3 _direction;
    private float _detection;
    private float _rotationSpeed;
    private float _speed;
    private Rigidbody _rb;

    private GameObject _ParticlePrefab;
    private List<Spot> _perchingSpots;

    private void Start()
    {
        bird = GetComponent<Bird>();

        if (bird != null)
        {
            _rb = bird.RigidBody;

            _target = bird.Birdtarget;
            _direction = bird.Direction;
            _detection = bird.Detection;
            _rotationSpeed = bird.RotationSpeed;
            _speed = bird.Speed;

            _ParticlePrefab = bird.ParticlePrefab;
        }
    }

    //Fly-wander 분리
    //FlyAround : Fly With Avoid 
    public IEnumerator FlyAround(Action<BirdBehaviour> onDone)
    {
  
        yield return null;
    }
    public IEnumerator WanderAround(Action<BirdBehaviour> onDone)
    {

        yield return null;
    }
    public IEnumerator FollowTo(Action<BirdBehaviour> onDone)
    {
        //Follow
;       yield return null;
    }

    //Feeling : Particle 
    public IEnumerator Feeling(Action <Bird>onDone)
    {   
        GameObject HeartParticle = Instantiate(_ParticlePrefab,bird.transform.position, Quaternion.identity);
        ParticleSystem ps = HeartParticle.GetComponent<ParticleSystem>();
        
        if(ps != null)
        {
            ps.Play();
            yield return new WaitForSeconds(4.0f);
        }

        onDone?.Invoke(bird);

    }


    //Spot 탐색 
    //public Spot FindSittingSpot()


    //public IEnumerator Wander()

}
