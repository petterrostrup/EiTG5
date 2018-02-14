using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int speed = 3;
    //public GameObject playerCamera;

    // Use this for initialization
    void Start () {
        Vector3 position = transform.position;
        //playerCamera = GameObject.Find("Main Camera");
        position.y += 10;
        transform.position = position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) {
            position.x += 3 * Time.deltaTime;
        }

        position.x += 1 * Time.deltaTime;
        
        transform.position = position;
    }
}
