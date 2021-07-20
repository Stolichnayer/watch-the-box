using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroy : MonoBehaviour {
    public Animator anim;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public GameObject coin;
    public GameObject fire;
    public AudioSource boxBreakSound;
    

    float coinDestroyTime = 3;

    


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {

        
        if (!col.gameObject.CompareTag("Box"))
        {
            boxBreakSound.Play();
            if (col.gameObject.CompareTag("Ground"))//
                coinDestroyTime = 3;//
            else
                coinDestroyTime = 0;

            Destroy(fire.gameObject);
            rb.velocity = new Vector2(0, 0);
            bc.enabled = false;
            rb.Sleep();
            anim.SetTrigger("Break");
            transform.DetachChildren();
            Destroy(this.gameObject, 0.8f);            
            Destroy(coin.gameObject, coinDestroyTime);
            
            rb.gravityScale = 0;
        }
    }
}
