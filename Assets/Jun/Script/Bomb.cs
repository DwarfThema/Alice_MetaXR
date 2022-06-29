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
        //살짝 시간을 준뒤
        yield return new WaitForSeconds(1.5f);

        // 특정 위치로 폭탄을 던진다 필요하면 Vector3을 이용해 offset 을 준다.
        bomb.position = myTransform.position + new Vector3(0, 0.0f, 0);

        //타겟과 거리를 계산한다.
        float targetDistance = Vector3.Distance(bomb.position, target.position);

        //특정 앵글로 특정위치에 던지는 속도를 계산한다.
        float bombVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        //X Y컴포넌트의 속도를 추출한다.
        float Vx = Mathf.Sqrt(bombVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(bombVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //비행 속도를 계산한다
        float flightDuration = targetDistance / Vx;

        //타겟 위치로 폭탄방향을 조정한다.
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
