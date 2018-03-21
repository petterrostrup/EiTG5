using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsObjTV : ConsOnOffStandby
{
    public GameObject TVimage;

    public override void SetMode(int modeIndex)
    {
        base.SetMode(modeIndex);
        if (GetMode() == Mode.On)
        {
            TVimage.SetActive(true);
        }
        else
        {
            TVimage.SetActive(false);
        }
    }
}
