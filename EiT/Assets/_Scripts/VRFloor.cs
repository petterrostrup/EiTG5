using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class VRFloor : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private VREyeRaycaster m_rayCaster;

    private void OnEnable()
    {
        m_InteractiveItem.OnClick += HandleClick;
        GameObject player = GameObject.Find("Player");
    }

    private void OnDisable()
    {
        m_InteractiveItem.OnClick -= HandleClick;
    }

    //Handle the Click event
    private void HandleClick()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        playerPos = m_rayCaster.m_rayHitPos;
        playerPos.y += 2;
        player.transform.position = playerPos;
    }

    // Use this for initialization
    void Start(){

    }
	    
	// Update is called once per frame
	void Update () {
        
    }
}