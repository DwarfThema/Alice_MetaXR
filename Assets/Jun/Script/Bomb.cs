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
            UpdateExplosion();
        }
    }

    private void UpdateExplosion()
    {
        //�����ؾ���
    }

    IEnumerator IThrowAction()
    {
        //��¦ �ð��� �ص�
        yield return new WaitForSeconds(0.5f);

        //// Ư�� ��ġ�� ��ź�� ������ �ʿ��ϸ� Vector3�� �̿��� offset �� �ش�.
        //transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

        //Ÿ�ٰ� �Ÿ��� ����Ѵ�.
        float targetDistance = Vector3.Distance(transform.position, target.position);

        //Ư�� �ޱ۷� Ư����ġ�� ������ �ӵ��� ����Ѵ�.
        float bombVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        //X Y������Ʈ�� �ӵ��� �����Ѵ�.
        float Vx = Mathf.Sqrt(bombVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(bombVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //���� �ӵ��� ����Ѵ�
        float flightDuration = targetDistance / Vx;

        //Ÿ�� ��ġ�� ��ź������ �����Ѵ�.
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);

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
