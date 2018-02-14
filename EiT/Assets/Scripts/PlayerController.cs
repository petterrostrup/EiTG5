using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            position.x += Time.deltaTime * speed;
        }
        transform.position = position;
    }
}
