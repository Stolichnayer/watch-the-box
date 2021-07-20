using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

    public static BoxScript instance;

    public GameObject prefab;
    public GameObject coin;
    public GameObject healthBox;

    public float dropSpeed = 1f;

    bool canInst = true;
    bool canInstHealthBox = true;

    int lastNum = 7;
    int boxDropped = 0;
    int boxThreshold = 30;

    //Random rand = new Random();
    int num;

    // Use this for initialization
    void Start ()
    {
        instance = this; 
    }
	
	// Update is called once per frame
	void Update () {

        if(canInst && !KnightController.instance.dead)
        {
            num = Random.Range(-7, 7);
            if (lastNum == num)
                num = Random.Range(-7, 7); 
            canInst = false;
            Instantiate(prefab, new Vector2(num, 7), Quaternion.identity);
            
            Invoke("resetInstCooldown", dropSpeed);
        }  
    }

    void resetInstCooldown()
    {
        canInst = true;
    }

   
}
