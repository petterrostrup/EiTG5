using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsBar : MonoBehaviour {

    public RectTransform validRange;
    public RectTransform currentCons;

    int maxCons = 0;
    int width;
    int height;
    ConsObj[] consObjects;

	// Use this for initialization
	void Awake () {
        // Find maximum possible consumption
        consObjects = FindObjectsOfType<ConsObj>();
        foreach (ConsObj obj in consObjects)
        {
            maxCons += obj.GetMaxCons();
        }

        width = (int)gameObject.GetComponent<RectTransform>().rect.width;
        height = (int)gameObject.GetComponent<RectTransform>().rect.height;
        SetValidRange(0, 263/2);
        HideLimits();
    }

    public int GetMaxCons()
    {
        return maxCons;
    }

    public void HideLimits()
    {
        Vector3 pos = validRange.localPosition;
        pos.y = 1000;
        validRange.localPosition = pos;
    }

    public void ShowLimits()
    {
        Vector3 pos = validRange.localPosition;
        pos.y = 0;
        validRange.localPosition = pos;
    }

    public void SetValidRange(int lowerLimit, int upperLimit)
    {
        RectTransform validRangeRect = validRange.GetComponent<RectTransform>();
        int newWidth = width * (upperLimit - lowerLimit) / maxCons;
        validRangeRect.sizeDelta = new Vector2(newWidth, height);
        Vector3 pos = validRangeRect.localPosition;
        pos.x = (upperLimit / 2 + 0.5f * lowerLimit) * (width) / maxCons - width/2;
        validRange.localPosition = pos;
    }

    public void SetCurrentCons(int curCons)
    {
        RectTransform curConsRect = currentCons.GetComponent<RectTransform>();
        curConsRect.sizeDelta = new Vector2(width * curCons / maxCons, height/2);
        Vector3 pos = curConsRect.localPosition;
        pos.x = -(width - width * curCons / maxCons) / 2;
        currentCons.localPosition = pos;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
