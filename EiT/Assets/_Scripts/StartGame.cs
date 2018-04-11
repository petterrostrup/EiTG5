using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;


public class StartGame : MonoBehaviour {

    public ConsBar consBar;
    public enum Init {On, Off, Random, NoChange };
    private VRInteractiveItem m_InterItem;
    private int numChallenge = 0;
    Vector3 pos;
    ConsObj[] consObjects;
    bool challengeEnabled = false;
    public ConsHUD consHUD;
    int lowerLimit = -1;
    int upperLimit = -1;
    bool validCons = false;
    private int[] lowerLimits = { 40, -1, 20, 150, 210, 155, 70 }; // Max 263
    private int[] upperLimits = { -1, 240, 70, 170, 220, 160, 72 }; // Max 263
    private Init[] initalValues = { Init.On, Init.NoChange, Init.NoChange, Init.NoChange, Init.NoChange, Init.NoChange, Init.NoChange };
    public Text challengeText;

    // Use this for initialization
    void Awake () {
        m_InterItem = gameObject.GetComponent<VRInteractiveItem>();
        pos = gameObject.transform.position;
        consObjects = FindObjectsOfType<ConsObj>();
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(pos.x, pos.y + 0.1f*Mathf.Sin(Time.time*2), pos.z);

        // Challenges:
        if (challengeEnabled)
        {
            if ((lowerLimit != -1) && (upperLimit != -1))         // Both upper and lower limits
            {
                if (consHUD.GetTotalConsumption() >= lowerLimit && consHUD.GetTotalConsumption() <= upperLimit) {
                    CompleteChallenge();
                }
            }
            else if (lowerLimit != -1)   // Only lower limit
            {
                if (consHUD.GetTotalConsumption() <= lowerLimit)
                {
                    CompleteChallenge();
                }
            }
            else if (upperLimit != -1)  // Only upper limit
            {
                if (consHUD.GetTotalConsumption() >= upperLimit)
                {
                    CompleteChallenge();
                }
            }
        }
	}

    private void CompleteChallenge()
    {
        lowerLimit = -1;
        upperLimit = -1;
        challengeEnabled = false;

        if (numChallenge == lowerLimits.Length)
        {
            challengeText.text = "Gratulerer! Du klarte alle utfordringene!";
            pos.y = 1.828999f;
            gameObject.transform.position = pos;
            numChallenge = 0;
        }
        else
        {
            challengeText.text = "Du klarte utfordringen! Ny utfordring begynner om 5 sekunder";
            Invoke("StartChallenge", 5);
        }
    }

    // Event handlers (can be overridden by the derived classes)
    private void OnEnable()
    {
        m_InterItem.OnClick += HandleClick;
        m_InterItem.OnOver += HandleOver;
        m_InterItem.OnOut += HandleOut;
    }

    private void OnDisable()
    {
        m_InterItem.OnClick -= HandleClick;
        m_InterItem.OnOver -= HandleOver;
        m_InterItem.OnOut -= HandleOut;
    }

    public virtual void HandleOver(){}
    public virtual void HandleOut() { }
    public virtual void HandleClick() {
        StartChallenge();
        pos.y = -10;
        gameObject.transform.position = pos;
    }

    public void StartChallenge()
    {
        string tempText = "Mål: ";
        if (numChallenge >= lowerLimits.Length)
        {
            //Tell the user there are no more challenges
            Debug.Log("No more challanges");
            return;
        }

        if ((lowerLimits[numChallenge] != -1) && (upperLimits[numChallenge] != -1))         // Both upper and lower limits
        {
            lowerLimit = lowerLimits[numChallenge];
            upperLimit = upperLimits[numChallenge];
            switch (initalValues[numChallenge])
            {
                case Init.On:
                    TurnAllOn();
                    break;
                case Init.Off:
                    TurnAllOff();
                    break;
                case Init.NoChange:
                    break;
                case Init.Random: //Maybe remove random completely, and instead make the challenges sequential 
                    do
                    {
                        RandomizeAll();
                    } while (consHUD.GetTotalConsumption() < lowerLimit && consHUD.GetTotalConsumption() > lowerLimit);
                    break;
                default:
                    break;
            }

            tempText += "Mellom " + lowerLimit + "W og " + upperLimit + "W";
            consBar.SetValidRange(lowerLimit, upperLimit);

        }
        else if (lowerLimits[numChallenge] != -1)   // Only lower limit
        {
            lowerLimit = lowerLimits[numChallenge];
            upperLimit = -1;
            switch (initalValues[numChallenge])
            {
                case Init.On:
                    TurnAllOn();
                    break;
                case Init.Off:
                    TurnAllOff();
                    break;
                case Init.Random:
                    do
                    {
                        RandomizeAll();
                    } while (consHUD.GetTotalConsumption() < lowerLimit);
                    break;
                default:
                    break;
            }

            tempText += "Maks " + lowerLimit + "W";
            consBar.SetValidRange(0, lowerLimit);
        }
        else if (upperLimits[numChallenge] != -1)  // Only upper limit
        {
            lowerLimit = -1;
            upperLimit = upperLimits[numChallenge];
            switch (initalValues[numChallenge])
            {
                case Init.On:
                    TurnAllOn();
                    break;
                case Init.Off:
                    TurnAllOff();
                    break;
                case Init.Random:
                    do
                    {
                        RandomizeAll();
                    } while (consHUD.GetTotalConsumption() > upperLimit);
                    break;
                default:
                    break;
            }

            tempText += "Minst " + upperLimit + "W";
            consBar.SetValidRange(upperLimit, consBar.GetMaxCons());
        }
        challengeText.text = tempText;
        numChallenge += 1;
        challengeEnabled = true;
        pos.y = -10;
    }

    private void RandomizeAll()
    {
        foreach (ConsObj obj in consObjects)
        {
            obj.SetRandomMode();
            obj.SetRandomType();
        }
    }
    private void TurnAllOn()
    {
        foreach (ConsObj obj in consObjects)
        {
            obj.SetModeOn();
        }
    }
    private void TurnAllOff()
    {
        foreach (ConsObj obj in consObjects)
        {
            obj.SetModeOff();
        }
    }
}
