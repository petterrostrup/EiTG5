using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFloor : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }
	    
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        position.y += 10;
        transform.position = position;
    }
}
