using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionThrowingTime : MonoBehaviour
{
    public Transform target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    private Transform myTransform;

    public Enemy_Minion_Move em_S;
    public MinionThrowingTime mtt_S;

    private void Awake()
    {
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        em_S.enabled = false;

        StartCoroutine(IThrowAction());
        target = GameObject.Find("ThrowingTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IThrowAction()
    {
        //살짝 시간을 준뒤
        yield return new WaitForSeconds(1f);

        //// 특정 위치로 폭탄을 던진다 필요하면 Vector3을 이용해 offset 을 준다.
        //transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

        //타겟과 거리를 계산한다.
        float targetDistance = Vector3.Distance(transform.position, target.position);

        //특정 앵글로 특정위치에 던지는 속도를 계산한다.
        float bombVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        //X Y컴포넌트의 속도를 추출한다.
        float Vx = Mathf.Sqrt(bombVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(bombVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //비행 속도를 계산한다
        float flightDuration = targetDistance / Vx;

        //타겟 위치로 폭탄방향을 조정한다.
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);

        float elapseTime = 0;

        float time = Time.deltaTime * 3;

        while (elapseTime < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapseTime)) * time, Vx * time);

            elapseTime += time;

            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        em_S.enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        mtt_S.enabled = false;
    }
}
