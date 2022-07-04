using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFactory : MonoBehaviour
{
    // 1. �ð��̵Ǹ�
    // 2. bomb�� instanciate
    // 3. postion�� �� ������

    //�ʿ�Ӽ� : �ð�
    float throwTime;

    //�ʿ�Ӽ� : ��ź
    public GameObject bomb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        throwTime += Time.deltaTime;

        if (throwTime > 5)
        {
            GameObject bombIns = Instantiate(bomb);
            bombIns.transform.position = transform.position;
            throwTime = 0;
        }

    }
}
