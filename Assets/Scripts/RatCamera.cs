using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCamera : MonoBehaviour
{
    [SerializeField] GameObject rat;

    void Update()
    {
        transform.position = new Vector3(rat.transform.position.x, transform.position.y, -10);
    }
}
