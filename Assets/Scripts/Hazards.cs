using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    [SerializeField] RatPlayer player;
    public float proximity;

    private AudioSource audioSource;


    private void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
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
