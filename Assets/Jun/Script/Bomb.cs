using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public State state;
    public enum State
    {
        Throw,
        Explosion
    }

    Transform target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    float currentTime;
    public float explosionTime = 2f;

    public GameObject vfx_Explosion;

    private Transform myTransform;


    private void Awake()
    {
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IThrowAction());
        target = GameObject.Find("Player").transform;

        state = State.Throw;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Explosion)
        {
            currentTime += Time.deltaTime;
            if(currentTime > explosionTime)
            {
                UpdateExplosion();
            }
        }
    }

    private void UpdateExplosion()
    {
        GameObject bombVFX = Instantiate(vfx_Explosion);
        bombVFX.transform.position = transform.position;
        Destroy(gameObject);
    }

    IEnumerator IThrowAction()
    {
        //살짝 시간을 준뒤
        yield return new WaitForSeconds(0.5f);

        //// 특정 위치로 폭탄을 던진다 필요하면 Vector3을 이용해 offset 을 준다.
        transform.position = myTransform.position + new Vector3(10, 0.0f, 10);

        //타겟과 거리를 계산한다. (이때 벡터3으로 랜덤값을준다)
        float targetDistance = Vector3.Distance(transform.position, target.position + new Vector3(UnityEngine.Random.value * 20,0, UnityEngine.Random.value * 20) );

        //특정 앵글로 특정위치에 던지는 속도를 계산한다.
        float bombVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        //X Y컴포넌트의 속도를 추출한다.
        float Vx = Mathf.Sqrt(bombVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(bombVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //비행 속도를 계산한다
        float flightDuration = targetDistance / Vx;

        //타겟 위치로 폭탄방향을 조정한다.
        transform.rotation = Quaternion.LookRotation(target.position + new Vector3(UnityEngine.Random.value * 20, 0, UnityEngine.Random.value * 20) - transform.position);

        float elapseTime = 0;

        float time = Time.deltaTime * 3;

        while(elapseTime < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapseTime)) * time, Vx * time);

            elapseTime += time;

            yield return null;
        }

        state = State.Explosion;
    }

}
