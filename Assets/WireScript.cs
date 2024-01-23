using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    LineRenderer line;
    public Transform start, end;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

    }
    private void Update()
    {
        line.SetPosition(0, start.position);
        line.SetPosition(1, end.position);
    }
}
