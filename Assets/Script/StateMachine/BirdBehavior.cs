using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bird 관련 Interaction 처리 
public class BirdBehavior : MonoBehaviour 
{
    public Bird bird { get; private set; }


    private void Start()
    {
       
    }


    //Flying 관련 함수 수정중 
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


    public IEnumerator ApproachTo(Action<BirdBehaviour> onDone)
    {
        Vector3 p0 = transform.position;
        Vector3 m0 = transform.forward;
        Vector3 p1 = _target.position;
        Vector3 m1 = _target.forward;

        float totalLength = Utils.EstimateHermiteLength(p0, m0, p1, m1);
        float duration = totalLength / _speed;

        float t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / duration;
            Vector3 pos = Utils.Hermite(p0, m0, p1, m1, t);
            Vector3 tangent = Utils.Hermite(p0, m0, p1, m1, t + 0.001f) - pos;

            transform.position = pos;
            transform.rotation = Quaternion.LookRotation(tangent.normalized, Vector3.up);

            yield return null;
        }

        transform.position = p1;
        transform.forward = m1;
        onDone?.Invoke(this);
    }


    public IEnumerator FlyAround(Action<BirdBehaviour> onDone)
    {
        while (true)
        {
            // Avoiding with sampling
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

                RaycastHit sampleHit;
                foreach (var rayDir in samples)
                {
                    if (Physics.Raycast(transform.position, rayDir, out sampleHit, _detection))
                    {
                        count++;
                        noramlSum += sampleHit.normal;
                    }
                }
                var avgNormal = (noramlSum / count).normalized;

                Vector3 avoidDir;
                if (noramlSum == Vector3.zero)
                {
                    avoidDir = -_direction;
                }
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





}
