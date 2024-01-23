using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class SerialManager : MonoBehaviour
{
    public string portName;
    SerialPort port;
    public static SerialManager instance;

    private void Awake()
    {
        if(instance != null && instance!=this)
        {
            print("Duplicate of SerialManager");
            return;
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        port = new SerialPort(portName,9600);
        port.Open();
    }

    public void sendData(string args)
    {
        if (port == null || !port.IsOpen)
            return;
        try
        {
            port.WriteLine(args);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error writing to serial port: " + e.Message);
        }
    }

    public string readMessage()
    {
        if (port == null || !port.IsOpen)
        {
            return null;
        }

        try
        {
            return port.ReadLine();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading from serial port: " + e.Message);
            return null;
        }
    }
    private void OnDestroy()
    {
        if (port != null && port.IsOpen)
        {
            port.Close();
        }
    }
}
