using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class RatPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text controlsText;
    [SerializeField] private TMP_Text points;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Points pointsTimeCalculation;

    [Header("Music")]
    [SerializeField] private AudioSource BGMAudioSource;
    [SerializeField] private AudioSource collisionAudioSource;
    [SerializeField] private AudioSource characterAudioSource;
    [SerializeField] private AudioClip[] BGMs;
    [SerializeField] private AudioClip[] menuSounds;

    [Header("Collision SFX")]
    [SerializeField] private AudioClip cheeseSFX;
    [SerializeField] private AudioClip sheetMusicSFX;
    [SerializeField] private AudioClip crumbleSFX;
    [SerializeField] private AudioClip drumSFX;
    [SerializeField] private AudioClip finishLineSFX;

    [Header("Player SFX")]
    [SerializeField] private AudioClip ratJumpSFX;
    [SerializeField] private AudioClip ratLandSFX;
    [SerializeField] private AudioClip ratHurtSFX;
    [SerializeField] private AudioClip ratLoseSFX;
    

    [Header("Movement")]
    public float speed;
    public float jumpSpeed;
    public float drumJumpSpeed;
    public float stringsSpeed;
    public float stringsJumpSpeed;
    public bool isGrounded = true;
    public bool isJumping = false;
    public bool isOnCrumble = false;
    private float startingSpeed;
    private float startingJumpSpeed;

    private float movementDirection;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode jump = KeyCode.W;

    private System.Random rand = new System.Random();
    private int previousRandomNumber = 0;
    private int maxKeySwitches = 6;
    private float timer = 0.0f;
    private float nextActionTime = 10.0f;

    private int cheesePoints = 5;
    private int musicPoints = 1;
    private int totalPoints = 0;

    private int totalCheeseCollected = 0;
    private int totalSheetsCollected = 0;

    private Animator animator;
    private Vector3 characterScale;
    
    public KeyCode GetLeftKey() {return left;}
    public KeyCode GetRightKey() {return right;}
    public KeyCode GetJumpKey() {return jump;}


    void Start()
    {
        animator = this.GetComponent<Animator>();
        characterScale = this.transform.localScale;

        Time.timeScale = 1;
        UpdatePoints(totalPoints);
        pointsTimeCalculation.UpdateCheeseSmall(totalCheeseCollected);
        pointsTimeCalculation.UpdateSheetsSmall(totalSheetsCollected);

        startingSpeed = speed;
        startingJumpSpeed = jumpSpeed;
        
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        pauseScreen.SetActive(false);
        nextActionTime = 10.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > nextActionTime)
        {
            KeySwitch();
            nextActionTime = timer + 10.0f;
        }

        FormatTime();

        MovementHorizontal(left, right);
        MovementJump(jump);

        if (isJumping)
            animator.SetBool("isJumping", true);
        else if (!isJumping)
            animator.SetBool("isJumping", false);

        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.F1)) 
        {
            OpenPauseMenu();
        }
    }

    private void UpdatePoints(int point)
    {
        points.text = "Points: " + point.ToString();
        pointsTimeCalculation.UpdateScoreSmall(point);
    }

    private void FormatTime()
    {
        timerText.text = timer.ToString("N0");
        pointsTimeCalculation.UpdateTimeSmall((int)timer);
        pointsTimeCalculation.UpdateTimeBig((int)timer);
    }

    private void KeySwitch()
    {
        int randomNumber = rand.Next(0, maxKeySwitches);

        for (int i = 0; i <= maxKeySwitches; i++)
        {
            do
            {
                randomNumber = rand.Next(0, maxKeySwitches);
            }
            while (randomNumber == previousRandomNumber);
        }

        switch (randomNumber)
        {
            case 0:
                left = KeyCode.A;
                right = KeyCode.D;
                jump = KeyCode.W;
                BGMAudioSource.clip = BGMs[0];
                break;
            case 1:
                left = KeyCode.S;
                right = KeyCode.F;
                jump = KeyCode.E;
                BGMAudioSource.clip = BGMs[1];
                break;
            case 2:
                left = KeyCode.D;
                right = KeyCode.G;
                jump = KeyCode.R;
                BGMAudioSource.clip = BGMs[2];
                break;
            case 3:
                left = KeyCode.F;
                right = KeyCode.H;
                jump = KeyCode.T;
                BGMAudioSource.clip = BGMs[3];
                break;
            case 4:
                left = KeyCode.G;
                right = KeyCode.J;
                jump = KeyCode.Y;
                BGMAudioSource.clip = BGMs[4];
                break;
            case 5:
                left = KeyCode.H;
                right = KeyCode.K;
                jump = KeyCode.U;
                BGMAudioSource.clip = BGMs[5];
                break;
            case 6:
                left = KeyCode.J;
                right = KeyCode.L;
                jump = KeyCode.I;
                BGMAudioSource.clip = BGMs[6];
                break;
            case 7:
                left = KeyCode.Z;
                right = KeyCode.C;
                jump = KeyCode.S;
                BGMAudioSource.clip = BGMs[7];
                break;
            case 8:
                left = KeyCode.X;
                right = KeyCode.V;
                jump = KeyCode.D;
                BGMAudioSource.clip = BGMs[8];
                break;
            case 9:
                left = KeyCode.C;
                right = KeyCode.B;
                jump = KeyCode.F;
                BGMAudioSource.clip = BGMs[9];
                break;
            case 10:
                left = KeyCode.V;
                right = KeyCode.N;
                jump = KeyCode.G;
                BGMAudioSource.clip = BGMs[10];
                break;
            case 11:
                left = KeyCode.B;
                right = KeyCode.M;
                jump = KeyCode.H;
                BGMAudioSource.clip = BGMs[11];
                break;
        }

        isGrounded = true; //reset

        BGMAudioSource.Play();
        previousRandomNumber = randomNumber;

        controlsText.text = (left + " " + right + " " + jump);
    }

    private void MovementHorizontal(KeyCode left, KeyCode right)
    {
        if (Input.GetKey(left))
        {
            movementDirection = -1;
            characterScale.x = -0.35f;
        }
        else if (Input.GetKey(right)) 
        {
            movementDirection = 1;
            characterScale.x = 0.35f;
        }
        else
            movementDirection = 0;

        this.transform.localScale = characterScale;
        rigidBody.velocity = new Vector2(speed * movementDirection, rigidBody.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(movementDirection));
    }

    private void MovementJump(KeyCode jumpKey)
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            characterAudioSource.clip = ratJumpSFX;
            characterAudioSource.Play();
            isJumping = true;

            rigidBody.AddForce(new Vector2(rigidBody.velocity.x, jumpSpeed * speed));
            animator.SetBool("isJumping", true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Crumble") || collision.gameObject.CompareTag("Untagged"))
        {
            if (collision.gameObject.CompareTag("Crumble") && !isOnCrumble)
            {
                collisionAudioSource.clip = crumbleSFX;
                collisionAudioSource.Play();
                isOnCrumble = true;
            }
            if (isJumping)
            {
                characterAudioSource.clip = ratLandSFX;
                characterAudioSource.Play();
                isJumping = false;
            }

            isGrounded = true;
        }

        else if (collision.gameObject.CompareTag("Drum"))
        {
            characterAudioSource.clip = ratJumpSFX;
            characterAudioSource.Play();
            collisionAudioSource.clip = drumSFX;
            collisionAudioSource.Play();

            isJumping = true;

            rigidBody.AddForce(new Vector2(rigidBody.velocity.x, drumJumpSpeed * speed));
        }

        else if (collision.gameObject.CompareTag("MusicSheet"))
        {
            Destroy(collision.gameObject);
            totalPoints += musicPoints;
            UpdatePoints(totalPoints);

            totalSheetsCollected += 1;
            pointsTimeCalculation.UpdateSheetsSmall(totalSheetsCollected);

            collisionAudioSource.clip = sheetMusicSFX;
            collisionAudioSource.Play();
        }

        else if (collision.gameObject.CompareTag("Cheese"))
        {
            Destroy(collision.gameObject);
            totalPoints += cheesePoints;
            UpdatePoints(totalPoints);

            totalCheeseCollected += 1;
            pointsTimeCalculation.UpdateCheeseSmall(totalCheeseCollected);

            collisionAudioSource.clip = cheeseSFX;
            collisionAudioSource.Play();
        }

        else if (collision.gameObject.CompareTag("Strings"))
        {
            speed += stringsSpeed;
            jumpSpeed = stringsJumpSpeed;
            isGrounded = true;
            animator.SetBool("isOnStrings", true);
        }

        else if (collision.gameObject.CompareTag("Killzone") || collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
            pointsTimeCalculation.UpdateScoreBig(totalPoints);
            pointsTimeCalculation.UpdateCheeseBig(totalCheeseCollected);
            pointsTimeCalculation.UpdateSheetsBig(totalSheetsCollected);

            loseScreen.SetActive(true);

            BGMAudioSource.Stop();
            characterAudioSource.clip = ratHurtSFX;
            characterAudioSource.Play();
            collisionAudioSource.clip = ratLoseSFX;
            collisionAudioSource.Play();
        }

        else if (collision.gameObject.CompareTag("FinishLine")) 
        {
            Time.timeScale = 0;
            pointsTimeCalculation.UpdateScoreBig(totalPoints);
            pointsTimeCalculation.UpdateCheeseBig(totalCheeseCollected);
            pointsTimeCalculation.UpdateSheetsBig(totalSheetsCollected);

            winScreen.SetActive(true);

            characterAudioSource.clip = finishLineSFX;
            characterAudioSource.Play();
            BGMAudioSource.Stop();
            collisionAudioSource.Stop();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Crumble"))
        {
            isGrounded = false;

            if (collision.gameObject.CompareTag("Floor")) 
            {
                isOnCrumble = false;

                if (rigidBody.velocity.y <= 0.05f)
                    isGrounded = true;
            }

        }
        else if (collision.gameObject.CompareTag("Strings"))
        {
            speed = startingSpeed;
            jumpSpeed = startingJumpSpeed;
            isGrounded = false;
            animator.SetBool("isOnStrings", false);
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(ReturnToLevelStart());
    }

    public void MainMenu()
    {
        StartCoroutine(ReturnToMainMenu());
    }

    private void OpenPauseMenu() 
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        BGMAudioSource.Pause();
        collisionAudioSource.Pause();
        characterAudioSource.Pause();
    }

    public void ReturnToGame() 
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        BGMAudioSource.UnPause();
        collisionAudioSource.UnPause();
        characterAudioSource.UnPause();
    }

    IEnumerator ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        yield return new WaitForSeconds(1);
        Time.timeScale = 1;
    }
    IEnumerator ReturnToLevelStart()
    {
        SceneManager.LoadScene("Level_1");
        yield return new WaitForSeconds(1);
        Time.timeScale = 1;
    }

    public void PlayMenuSounds(int menuSound)
    {
        collisionAudioSource.clip = menuSounds[menuSound];
        collisionAudioSource.Play();
    }

}
