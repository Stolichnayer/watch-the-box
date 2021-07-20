using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    public static KnightController instance;
    
    public Rigidbody2D rb;
    public Animator anim;
    public Animator effectsAnim;
    public Animator currentEffectAnim;

    public GameObject healthBar1;
    public GameObject healthBar2;
    public GameObject healthBar3;
    public GameObject healthBox;
    public GameObject auraBox;
    public GameObject ScorePanel;
    public GameObject HighScorePanel;
    public GameObject HealthObject;
    public GameObject Aura;
    public BoxCollider2D bc2D;

    public Text scoreText;
    public Text highScoreText;
    public Text currentScore;
    public Text highScorePanelText;

    public AudioSource boxBreakSound;
    public AudioSource buffSound;
    public AudioSource coinSound;
    public AudioSource healthSound;
    public AudioSource loseSound;
    public AudioSource multiplierSound;
    public AudioSource highscoreSound;
    public AudioSource clapSound;


    public int score = 0;

    bool movingRight = false;
    bool movingLeft = false;
    bool isRotated = false;
    public bool dead = false;
    
    int health = 3;

    int boxDropped = 0;
    int boxThreshold = 30;
    int num;
    float moveSpeed = 10;
    int specialDropCount = 0;
    int specialDropThreshold = 4;

    int HighScore;

    public bool canTakeCoin = false;
    public BoxCollider2D bc;

    float multiTime = 10;
    float auraTime = 10;
    int multiplier = 1;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        HighScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "" + PlayerPrefs.GetInt("HighScore");
        //PlayerPrefs.DeleteAll();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!dead)
        {
            if (movingRight)
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            else if (movingLeft)
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.D))
            {
                Run();
                if (isRotated)
                {
                    transform.Rotate(0, 180, 0);
                    isRotated = false;
                }
                
                movingRight = true;


            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                Idle();
                movingRight = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Run();
                if (!isRotated)
                {
                    transform.Rotate(0, 180, 0);
                    isRotated = true;
                }
                
                movingLeft = true;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                Idle();
                movingLeft = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }


        }
    }

    void Run()
    {
        anim.SetTrigger("Run");
    }

    void Idle()
    {
        anim.SetTrigger("Idle");
    }

    void Die()
    {

       
        rb.velocity = new Vector2(0, 0);
        anim.SetTrigger("Die");
        dead = true;
        if(score > HighScore)
        {
            //Play High Score sound
            highscoreSound.Play();
            clapSound.Play();
            HighScorePanel.SetActive(true);
            highScorePanelText.text = "" + score;

        }
        else
        { 
            //Play Die Sound
            loseSound.Play();
            ScorePanel.SetActive(true);
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
      



        if (collision.gameObject.CompareTag("Box"))
        {

            
            health--;
            if(health == 0)
            {                
                healthBar1.SetActive(false);
                Die();
            }
            if(health == 2)
            {
                healthBar3.SetActive(false);
                healthBar2.SetActive(true);
            }
            if(health == 1)
            {
                healthBar2.SetActive(false);
                healthBar1.SetActive(true);
            }
            
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("AuraBuff"))
        {
            buffSound.Play();
            effectsAnim.SetTrigger("DodgeAura");
            currentEffectAnim.SetTrigger("DodgeAuraStable");
            Aura.SetActive(true);
            bc2D.enabled = false;
            Invoke("resetAura", auraTime);
            Invoke("startAuraOnOff", auraTime - 1.5f);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Multi"))
        {
            multiplierSound.Play();
            effectsAnim.SetTrigger("Multiplier");
            currentEffectAnim.SetTrigger("2xStable");
            multiplier = 2;
            Invoke("resetMulti", multiTime);
            Invoke("startMultiOnOff", multiTime - 1.5f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            score = score + multiplier;
            scoreText.text = "" + score;
            currentScore.text = "" + score;

            if(score > HighScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScoreText.text = "" + score;

            }

            Destroy(collision.gameObject);
            //Play Sound of Coin collected;
            coinSound.Play();
            
            // ---------Health Drop Check----------
            boxDropped++;
            if (boxDropped == boxThreshold)
            {
                num = Random.Range(-7, 7);
                Instantiate(auraBox, new Vector2(num, 10), Quaternion.identity);
                boxDropped = 0;
            }
            // ---------Health Drop Check End------

            
            // ---------Special Drop Check---------
            specialDropCount++;
            if (specialDropCount == specialDropThreshold)
            {
                num = Random.Range(-7, 7);
                Instantiate(auraBox, new Vector2(num, 10), Quaternion.identity);
                boxDropped = 0;
            }
            // ---------Special Drop Check End-----

            if (BoxScript.instance.dropSpeed < 0.2)
                return;
            else
                BoxScript.instance.dropSpeed -= 0.004f;
        }

        if (collision.gameObject.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            //Play Sound of Health Collected
            healthSound.Play();

            if (health < 3)
                health++;
            else
                return;

            if (health == 3)
            {
                healthBar3.SetActive(true);
                healthBar2.SetActive(false);
            }
            if (health == 2)
            {
                healthBar1.SetActive(false);
                healthBar2.SetActive(true);
            }

        }
    }

    void resetMulti()
    {
        multiplier = 1;
    }

    void startMultiOnOff()
    {
        currentEffectAnim.SetTrigger("2xOnOff");
    }

    void startAuraOnOff()
    {
        currentEffectAnim.SetTrigger("DodgeAuraOnOff");
    }

    void resetAura()
    {
        bc2D.enabled = true;
        Aura.SetActive(false);
    }




}
