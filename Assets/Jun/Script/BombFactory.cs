using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFactory : MonoBehaviour
{
    // 1. 시간이되면
    // 2. bomb를 instanciate
    // 3. postion은 내 포지션

    //필요속성 : 시간
    float throwTime;

    //필요속성 : 폭탄
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
