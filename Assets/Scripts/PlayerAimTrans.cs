using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimTrans : MonoBehaviour
{
    public Transform playerTrans;
    private void Update()
    {
        this.transform.position = playerTrans.position;
    }
}
