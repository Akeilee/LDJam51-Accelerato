using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    [SerializeField] RatPlayer player;
    [SerializeField] private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;
    private AudioSource audioSource;
    private Animator animator;

    public float proximity;
    public float speed;


    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        animator = this.GetComponent<Animator>();
        animator.speed = speed;
    }

    private void Update()
    {
        if (player.transform.position.x >= this.transform.position.x - proximity &&
            player.transform.position.x <= this.transform.position.x + proximity)
                audioSource.enabled = true;
        else
            audioSource.enabled = false;
    }

    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }

    public void PlaySFX()
    {
        audioSource.Play();
    }
}
