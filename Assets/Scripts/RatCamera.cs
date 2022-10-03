using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCamera : MonoBehaviour
{
    [SerializeField] GameObject rat;
    [SerializeField] GameObject background;

    void Update()
    {
        if (rat.transform.position.x > 0 && rat.transform.position.x < 349) 
        {
            transform.position = new Vector3(rat.transform.position.x, transform.position.y, -10);
            background.transform.position = new Vector3(rat.transform.position.x, 1.54f, 2.91f);
        }

    }
}
