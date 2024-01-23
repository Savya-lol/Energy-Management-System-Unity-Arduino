using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    [SerializeField] int id;

    private void OnMouseDown()
    {
        print("clicked" + id);
        if (!EMSController.instance.breakdown || EMSController.instance.storage > 0)
        {
            LightManagement.instance.toggleLight(id);
        }
    }
}
