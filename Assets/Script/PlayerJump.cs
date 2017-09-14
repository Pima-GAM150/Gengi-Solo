using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    Rigidbody2D hand;
    public Animator run;
    public Rigidbody2D bgBox;
    bool xform = true;  
    bool jump = false;
    float runBPM = 1.06f;

    public int force = 100;

	// Use this for initialization
	void Start () {
        hand = GetComponent<Rigidbody2D>();
        hand.simulated = false;
        bgBox.useFullKinematicContacts = false;
        bgBox.isKinematic = false;
        run.speed = runBPM;
        //run = GetComponent<Animator>();
    }   
	
	// Update is called once per frame
	void FixedUpdate () {
        if (xform && run.GetCurrentAnimatorStateInfo(0).IsName("Run Sciccors W")) {
            xform = false;
            //Debug.Log("Gravity called");
            hand.simulated = true;
            bgBox.isKinematic = true;
            bgBox.useFullKinematicContacts = true;
        }
		if  (Input.GetKeyDown(KeyCode.Space)) {
            if (jump == false && run.GetCurrentAnimatorStateInfo(0).IsName("Run Sciccors W")) {
                //Debug.Log("Jump");
                hand.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                jump = true;
                run.speed = 0.2f;
            }
            else {
                run.SetTrigger("RoShamBo");
            }
        }
	}

    /*void OnCollisionExit2d(Collision2D e) {
        jump = true;
        run.speed = 0.4f;
    }*/

    void OnCollisionEnter2D(Collision2D e) {
        jump = false;
        run.speed = runBPM;
    }
}