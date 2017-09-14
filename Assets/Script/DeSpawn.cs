using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawn : MonoBehaviour {

    public GameObject controller;

    void Start ()  { 
	}
	void Update () {
       // Debug.Log("RigidBody is asleep?" + GetComponent<Rigidbody2D>().IsSleeping());
	}

   /* void OnCollisionEnter2D(Collision2D e) {
        Debug.Log("C");
        controller.GetComponent<Generator>().Kill(e.otherCollider.gameObject);
    }*/
    void OnTriggerEnter2D(Collider2D e) {
        //Debug.Log("T");
        controller.GetComponent<Generator>().Kill(e.gameObject);
    }
}
