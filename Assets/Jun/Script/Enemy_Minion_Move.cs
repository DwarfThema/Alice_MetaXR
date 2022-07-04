using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Minion_Move : MonoBehaviour
{
    // HP ����
    // �Ÿ��� ���� ���� ������Ʈ ����
    // �Ÿ� 5���� �ܺθ� ���Ÿ� ���� / ���θ� �ٰŸ����� ������Ʈ
    // �ܺΰ��� �ÿ��� ���� �������� �ʰ� ���Ÿ� �Ѱ���
    // �ٰŸ� ���ݽÿ��� �÷��̾ ���󰡼� ���� ���

    public State state;
    public enum State
    {
        LongAttack,
        Move,
        ShortAttack
    }

    GameObject target;

    NavMeshAgent agent;

    BombFactory factory;

    Enemy_Minion_Tall_ShortAttack shortAttack;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();
        factory = GetComponent<BombFactory>();
        shortAttack = GetComponent<Enemy_Minion_Tall_ShortAttack>();

        state = State.LongAttack;

        

    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target.transform);

        if (state == State.ShortAttack)
        {
            factory.enabled = false;
            shortAttack.enabled = true;
            UpdateSD();
        }
        else if(state == State.Move)
        {
            factory.enabled = true;
            shortAttack.enabled = false;
            UpdateMove();
        }
        else
        {
            factory.enabled = true;
            shortAttack.enabled = false;
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
