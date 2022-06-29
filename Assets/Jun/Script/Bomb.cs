using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Transform target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform bomb;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator IThrowAction()
    {
        //��¦ �ð��� �ص�
        yield return new WaitForSeconds(1.5f);

        // Ư�� ��ġ�� ��ź�� ������ �ʿ��ϸ� Vector3�� �̿��� offset �� �ش�.
        bomb.position = myTransform.position + new Vector3(0, 0.0f, 0);

        //Ÿ�ٰ� �Ÿ��� ����Ѵ�.
        float targetDistance = Vector3.Distance(bomb.position, target.position);

        //Ư�� �ޱ۷� Ư����ġ�� ������ �ӵ��� ����Ѵ�.
        float bombVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        //X Y������Ʈ�� �ӵ��� �����Ѵ�.
        float Vx = Mathf.Sqrt(bombVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(bombVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //���� �ӵ��� ����Ѵ�
        float flightDuration = targetDistance / Vx;

        //Ÿ�� ��ġ�� ��ź������ �����Ѵ�.
        bomb.rotation = Quaternion.LookRotation(target.position - bomb.position);

        float elapseTime = 0;

        while(elapseTime < flightDuration)
        {
            bomb.Translate(0, (Vy - (gravity * elapseTime)) * Time.deltaTime, Vx * Time.deltaTime);

            elapseTime += Time.deltaTime;

            yield return null;
        }

    }
}
