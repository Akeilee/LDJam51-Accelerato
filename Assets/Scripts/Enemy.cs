using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] RatPlayer player;

    public float speed;
    public int startingPoint;
    public Transform[] points;
    public float proximity;

    private int index;
    private AudioSource audioSource;


    void Start()
    {
        transform.position = points[startingPoint].position;
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[index].position) < 0.02f)
        {
            index++;
            if (index == points.Length)
            {
                index = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);


        if (player.transform.position.x >= this.transform.position.x - proximity &&
               player.transform.position.x <= this.transform.position.x + proximity) 
        {
            audioSource.Play();
        }

        else
            audioSource.Stop();
    }

}
