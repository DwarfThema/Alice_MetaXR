using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    public float degree;

    private void Start()
    {
        degree = 0;
    }

    // Update is called once per frame
    void Update()
    {
        degree += Time.deltaTime * 25;
        if(degree >= 360)
        {
            degree = 0;
        }

        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }
}
