using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsLights : ConsObj {
    // Setup relevant properties
    private enum Mode { On, Off }
    private enum Type { LED, Halogen, CFL, Incandescent}
    private Mode currentMode = Mode.On;
    private Type currentType = Type.Incandescent;
    int[,] powerConsArray = new int[2, 4] { { 7, 46, 12, 60 }, { 0, 0, 0, 0 } };
    int[,] waterConsArray = new int[2, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };

    // Setup lights
    public Light[] attachedLights;
    private float[] lightIntensities;

    // Event handlers
    public override void HandleOver()
    {
        base.HandleOver();

        // Just to test. Turs on and changes type every time we look at it.
        switch (currentType)
        {
            case Type.LED:
                currentType = Type.Halogen;
                break;
            case Type.Halogen:
                currentType = Type.CFL;
                break;
            case Type.CFL:
                currentType = Type.Incandescent;
                break;
            case Type.Incandescent:
                currentType = Type.LED;
                break;
            default:
                break;
        }

        TurnOn();
    }

    public override void HandleOut()
    {
        base.HandleOut();
        TurnOff();
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
