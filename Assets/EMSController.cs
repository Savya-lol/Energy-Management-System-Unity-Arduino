using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EMSController : MonoBehaviour
{
    public float capacity = 0;
    public float storage = 0;
    public float demandA = 0;
    public float demandB = 0;
    public float maxDemand = 0;
    public float totalDemand = 0;
    public bool breakdown = false;
    private float timer = 0f;
    public float interval = 2f;
    public static EMSController instance;
    public TMP_Text batteryText, LaneA, LaneB,system,extra;

    private void Awake()
    {
        if (instance != null && instance != this) { print("Duplicate of EMSController"); return; }
        instance = this;
    }

    private void Update()
    {
        UpdateDemands();
        BatteryManagement();
        UpdateUI();
    }

    public void UpdateDemands()
    {
        demandA = LightManagement.instance.lightsOnA * 10;
        demandB = LightManagement.instance.lightsOnB * 10;
    }

    void BatteryManagement()
    {
        totalDemand = (int)demandA + (int)demandB;
        maxDemand = Mathf.Abs(totalDemand - 100);

        if (breakdown)
        {
            // Decrease storage
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                if (storage > 0 && (LightManagement.instance.lightsOnA > 0 || LightManagement.instance.lightsOnB > 0))
                {
                    float decreaseAmount  = (totalDemand>0?totalDemand * 0.1f:10);
                    storage -= decreaseAmount;
                    storage = Mathf.Max(storage, 0);
                }

                timer = 0f;
            }

            if (storage <= 0 && (LightManagement.instance.lightsOnA >= 1 || LightManagement.instance.lightsOnB >= 1))
            {
                LightManagement.instance.TurnoffAllLights();
            }
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                if (totalDemand < 100)
                {
                    float increaseAmount = (LightManagement.instance.lightsOnA + LightManagement.instance.lightsOnB) > 0
                                     ? 10 - (totalDemand * 0.02f)
                                     : 10;
                    storage += increaseAmount;
                    storage = Mathf.Min(storage, 100);
                }

                timer = 0f;
            }
        }
    }

    void UpdateUI()
    {
        batteryText.text = storage.ToString("F1")+"%";
        LaneA.text = demandA.ToString("F1")+"%";
        LaneB.text = demandB.ToString("F1")+"%";
        extra.text = maxDemand.ToString("F1") +"%";
        if(breakdown)
        {
            system.text = "<color=red>FAILURE</color>";
        }
        else
        {
            system.text = "<color=green>OPERATIONAL</color>";
        }
    }
}

