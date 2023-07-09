using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealth : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GetComponent<Health>().DecreaseHealth(20);
        }if (Input.GetKeyDown(KeyCode.Plus))
        {
            GetComponent<Health>().IncreaseHealth(20);
        }if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GetComponent<Health>().DecreaseHealth(1000);
        }
    }
}
