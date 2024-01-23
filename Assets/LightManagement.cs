using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManagement : MonoBehaviour
{
    public GameObject[] lights;
    public LineRenderer[] wireA,wireB,wireC;
    [SerializeField] int[] lightState = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int lightsOnA, lightsOnB;
    public SpriteRenderer solar, windmill;
    public static LightManagement instance;


    string olddata;

    private void Awake()
    {
        if (instance != null && instance != this) { print("Duplicate of EMSController"); return; }
        instance = this;
    }


    private void Update()
    {
        UpdateLights();
        CheckWire();
        WireA();
        WireB();
        BreakdownCheck();
        readSun();
        SendLightData();
    }
    public void toggleLight(int index)
    {
        if (lightState[index] == 0)
            lightState[index] = 1;
        else
            lightState[index] = 0;
    }

    void LightOff(int index)
    {
        lights[index].transform.GetChild(0).gameObject.SetActive(false);
        lights[index].GetComponent<SpriteRenderer>().color = Color.white;
        print("turned off" + index);
    }
    void LightOn(int index)
    {
        lights[index].transform.GetChild(0).gameObject.SetActive(true);
        lights[index].GetComponent<SpriteRenderer>().color = Color.yellow;
        print("turned on" + index);
    }

    void CheckWire()
    {
        int resultA = 0, resultB = 0;

        for (int i = 0; i < 10; i++)
        {
            if (lightState[i] == 1)
            {
                if (i < 5)
                {
                    resultA += 1;
                }
                else
                {
                    resultB += 1;
                }
            }
        }

        lightsOnA = resultA;
        lightsOnB = resultB;
    }

    void WireA()
    {
        foreach (LineRenderer l in wireA)
        {

            if (lightsOnA > 3)
            {
                l.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else if (lightsOnA > 2)
            {
                l.material.color = new Color(0.8f, 0.0f, 0.0f, 1.0f);
            }
            else if (lightsOnA > 0)
            {
                l.material.color = new Color(0.6f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                l.material.color = new Color(0.2f, 0.0f, 0.0f, 1.0f);
            }
        }
    }

    void WireB()
    {
        foreach (LineRenderer l in wireB)
        {

            if (lightsOnB > 3)
            {
                l.material.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            }
            else if (lightsOnB > 2)
            {
                l.material.color = new Color(0.0f, 0.0f, 0.8f, 1.0f);
            }
            else if (lightsOnB > 0)
            {
                l.material.color = new Color(0.0f, 0.0f, 0.6f, 1.0f);
            }
            else
            {
                l.material.color = new Color(0.0f, 0.0f, 0.2f, 1.0f);
            }
        }
    }

    void SendLightData()
    {
        string msg = "";
        for (int i = 0; i < 10; i++)
        {
                msg += lightState[i] + ",";
        }
        msg += (EMSController.instance.breakdown ? "1" : "0");
        if (msg != olddata)
        {
            SerialManager.instance.sendData(msg);
            olddata = msg;
        }
    }

    void readSun()
    {
        string receivedMessage = SerialManager.instance.readMessage();

        if (receivedMessage != null)
        {
            if (receivedMessage == "1")
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i == 0 || i == 1 || i == 2 || i == 5 || i == 6 || i == 7)
                    {
                        lightState[i] = 1;
                    }
                }
            }

            if (receivedMessage == "0")
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i == 0 || i == 1 || i == 2 || i == 5 || i == 6 || i == 7)
                    {
                        lightState[i] = 0;
                    }
                }
            }
        }
    }

    void UpdateLights()
    {
        for (int i = 0; i < 10; i++)
        {
            if (lightState[i] == 1)
            {
                LightOn(i);
            }
            else
            {
                LightOff(i);
            }
        }
    }

    public void TurnoffAllLights()
    {
        for(int i=0;i<10;i++)
        {
            lightState[i] = 0;
        }
    }

    void BreakdownCheck()
    {
        if(EMSController.instance.breakdown)
        {
            foreach(LineRenderer l in wireC)
            {
                l.material.color = new Color(0.2f, 0.2f, 0.0f, 1.0f);
            }
            solar.color = Color.red;
            windmill.color = Color.red;
        }
        else
        {
            foreach (LineRenderer l in wireC)
            {
                l.material.color = new Color(1f, 1f, 0.0f, 1.0f);
            }
            solar.color = Color.white;
            windmill.color = Color.white;
        }
    }
}