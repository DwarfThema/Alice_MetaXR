using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_EnemyFactory : MonoBehaviour
{
    public GameObject enemy;

    public float generateTime = 10f;
    float currentTime;

    public GameObject throwingPosition;

    GameObject enemyIns;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ThrowingEvent()
    {
        enemyIns = Instantiate(enemy);
        enemyIns.transform.position = throwingPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        

    }

}
