using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class VRFloor : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;

    private void OnEnable()
    {
        m_InteractiveItem.OnClick += HandleClick;
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        playerPos.z += 10;
        player.transform.position = playerPos;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnClick -= HandleClick;
    }

    //Handle the Click event
    private void HandleClick()
    {
        Debug.Log("Show click state");
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        playerPos.y += 20;
        player.transform.position = playerPos;
    }

    // Use this for initialization
    void Start(){
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        playerPos.y += 20;
        player.transform.position = playerPos;
    }
	    
	// Update is called once per frame
	void Update () {
        
    }
}