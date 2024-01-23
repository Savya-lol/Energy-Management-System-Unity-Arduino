using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakdownScript : MonoBehaviour
{

    private void OnMouseDown()
    {
        if(EMSController.instance.breakdown)
        {
            EMSController.instance.breakdown = false;  
        }
        else
        {
            EMSController.instance.breakdown = true;
        }
    }
}
