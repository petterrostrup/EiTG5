using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed = 3;

    // Use this for initialization
    void Start () {
        Vector3 position = transform.position;
        position.y += 10;
        transform.position = position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;

        bool touchPad = Input.GetMouseButton(0);
        if (touchPad)
        {
            position.x += Time.deltaTime * speed;
        }

        if (OVRInput.Get(OVRInput.Button.Any))
        {
            position.x += Time.deltaTime * speed;
        }
        transform.position = position;
    }
}
