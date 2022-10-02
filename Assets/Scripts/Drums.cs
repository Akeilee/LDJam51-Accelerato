using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drums : MonoBehaviour
{
    [SerializeField] private SpriteRenderer drumSpriteRenderer;
    [SerializeField] private Sprite drumIdle;
    [SerializeField] private Sprite drumBounce;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            drumSpriteRenderer.sprite = drumBounce;          
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(BounceSpriteWait());
    }

    IEnumerator BounceSpriteWait() 
    {
        yield return new WaitForSeconds(0.15f);
        drumSpriteRenderer.sprite = drumIdle;
    }

}
