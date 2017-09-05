using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public Rigidbody hand;
    Animator run;

    public int force = 100;

	// Use this for initialization
	void Start () {
        run = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if  (Input.GetKeyDown(KeyCode.Space)) {
            if (run.GetCurrentAnimatorStateInfo(0).IsName("Run Sciccors W")) {
                Debug.Log("Jump");
                hand.AddForce(Vector3.up * force);
            }
            else {
                run.SetTrigger("RoShamBo");
            }
        }
	}
}
