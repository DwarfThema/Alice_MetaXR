using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_EnemyFactory : MonoBehaviour
{
    public GameObject enemy;

    public float generateTime = 10f;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(generateTime < currentTime)
        {
            Instantiate(enemy);
            enemy.transform.position = transform.position;
            currentTime = 0;
        }

    }
}
