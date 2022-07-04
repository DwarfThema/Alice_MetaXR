using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombParticle : MonoBehaviour
{
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (ps)
        //{
        //    if (!ps.IsAlive())
        //    {
        //        Destroy(gameObject);
        //    }
        //}

        StartCoroutine("Explosion");

    }
    
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(ps.main.duration-1);
        Destroy(gameObject);
    }

}
