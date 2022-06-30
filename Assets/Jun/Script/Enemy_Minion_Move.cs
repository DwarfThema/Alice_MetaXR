using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Minion_Move : MonoBehaviour
{
    // HP 존재
    // 거리에 따라 공격 스테이트 변경
    // 거리 5미터 외부면 원거리 공격 / 내부면 근거리공격 스테이트
    // 외부공격 시에는 따로 움직이지 않고 원거리 총공격
    // 근거리 공격시에는 플레이어를 따라가서 공격 모션

    public State state;
    public enum State
    {
        LongAttack,
        Move,
        ShortAttack
    }

    GameObject target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();

        state = State.LongAttack;

    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target.transform);

        if (state == State.ShortAttack)
        {
            UpdateSD();
        }
        else if(state == State.Move)
        {
            UpdateMove();
        }
        else
        {
            UpdateLD();
        }
    }


    float moveDistance = 100f;
    private void UpdateLD()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < moveDistance)
        {
            state = State.Move;
        }
    }

    float shortDistance = 40f;

    private void UpdateMove()
    {


        float distance = Vector3.Distance(transform.position, target.transform.position);

        agent.destination = target.transform.position;

        if(distance < shortDistance)
        {
            state = State.ShortAttack;
        }
    }


    private void UpdateSD()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);


        if (distance > shortDistance)
        {
            state = State.Move;
        }
    }
}
