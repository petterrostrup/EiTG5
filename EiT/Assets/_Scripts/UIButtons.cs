using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour {

    private enum Choise
    {
        mode, type
    }

    [SerializeField] private Choise choise = Choise.mode;

    public string GetChoise()
    {
        return choise.ToString();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
