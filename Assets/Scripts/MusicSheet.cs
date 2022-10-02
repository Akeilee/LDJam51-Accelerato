using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheet : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    private float maxHeight;
    private float minHeight;
    private float movingHeightValue = 0.3f;
    private float timer;
    private float speed = 2;


    private void Start()
    {
        maxHeight = this.transform.position.y + movingHeightValue;
        minHeight = this.transform.position.y - movingHeightValue;

        System.Random rand = new System.Random();
        int startPosition = rand.Next(2);

        if (startPosition == 0)
            timer = -Time.deltaTime / speed;
        else
            timer = Time.deltaTime / speed;
    }

    void Update()
    {
        if (this.transform.position.y <= minHeight)
            timer = Time.deltaTime / speed;
        else if (this.transform.position.y >= maxHeight)
            timer = -Time.deltaTime / speed;

        this.transform.SetPositionAndRotation(new Vector2(this.transform.position.x, this.transform.position.y + timer), this.transform.rotation);
    }

    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }

}
