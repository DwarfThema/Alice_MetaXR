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
        //��¦ �ð��� �ص�
        yield return new WaitForSeconds(1f);

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
