using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsLights : ConsObj {
    // Setup relevant properties
    private enum Mode { On, Off }
    private enum Type { LED, Halogen, CFL, Incandescent}
    private Mode currentMode = Mode.On;
    private Type currentType = Type.Incandescent;
    int[,] powerConsArray = new int[2, 4] { 
        { 7, 46, 12, 60 },  //On
        { 0, 0, 0, 0 }     //Off
        };
    int[,] waterConsArray = new int[2, 4] { 
        { 0, 0, 0, 0 },     //On
        { 0, 0, 0, 0 }      //Off
        };

    // Setup lights
    public Light[] attachedLights;
    private float[] lightIntensities;

    // Change of state 
    public override void SetType(int typeIndex)
    {
        base.SetType(typeIndex);
        currentMode = (Mode)typeIndex;
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    // Event handlers
    public override void HandleOver()
    {
        base.HandleOver();
    }

    public override void HandleOut()
    {
        base.HandleOut();
    }

    public override void HandleClick()
    {
        base.HandleClick();
    }

    // Switching on and off lights
    private void TurnOff()
    {
        currentMode= Mode.Off;
        for (int i = 0; i < attachedLights.Length; i++)
        {
            attachedLights[i].intensity = 0;
        }
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    private void TurnOn()
    {
        currentMode = Mode.On;
        for (int i = 0; i < attachedLights.Length; i++)
        {
            attachedLights[i].intensity = lightIntensities[i];
        }
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    // Used for initialization
    public override void Awake()
    {
        base.Awake();

        // Storing initial light intensities
        lightIntensities = new float[2];
        for (int i = 0; i < attachedLights.Length; i++)
        {
            lightIntensities[i] = attachedLights[i].intensity;
        }
    }
}
