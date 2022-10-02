using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCrumble : MonoBehaviour
{
    [SerializeField] private RatPlayer player;
    [SerializeField] private AudioClip [] crumbleSoundsSFX;

    private AudioSource audioSource;
    private SpriteRenderer spriteRend;
    private Animator animator;
    private float disableCrumbleBlockTime = 3;

    public bool isOnCrumbleBlock = false;
    public float timer = 0;
    private Vector3 initialPosition;
    private Vector3 directionOfShake;
    public float frequency;


    private void Start()
    {
        animator = this.GetComponent<Animator>();
        spriteRend = this.GetComponent<SpriteRenderer>();
        audioSource = this.GetComponent<AudioSource>();

        initialPosition = this.transform.position;
        animator.enabled = false;
    }

    private void Update()
    {
        if (isOnCrumbleBlock)
        {
            System.Random rand = new System.Random();
            int directionBool = rand.Next(2);

            if (directionBool == 0)
                directionOfShake = new Vector3(1, 0, 0);
            else
                directionOfShake = new Vector3(0, 1, 0);

            transform.position = initialPosition + (directionOfShake * Mathf.Sin(Time.deltaTime / 1.25f) * frequency);


            timer += Time.deltaTime;

            if (timer >= disableCrumbleBlockTime)
            {
                ChangeSFX(2);
                audioSource.Play();
                timer = 0;
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void ChangeSFX(int audioNumber) 
    {     
        switch (audioNumber) 
        {
            case 0:
                audioSource.clip = crumbleSoundsSFX[0];
                break;
            case 1:
                audioSource.clip = crumbleSoundsSFX[1];
                break;
            case 2:
                audioSource.clip = crumbleSoundsSFX[2];
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.transform.position.y > (this.transform.position.y + (spriteRend.size.y / 2)))
            {
                isOnCrumbleBlock = true;
                animator.enabled = true;
                audioSource.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnCrumbleBlock = false;
            animator.enabled = false;
            audioSource.Stop();
        }
    }
}
