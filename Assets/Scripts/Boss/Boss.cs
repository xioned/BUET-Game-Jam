using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class Boss : MonoBehaviour
{
    [SerializeField] private float bossGotoPositionArea;
    [SerializeField] private int gotoPositionAmount;
    [SerializeField] private int gotoPositionHeight;
    [SerializeField] private GameObject visuPref;

    [SerializeField] private Vector3[] gotoPosition;
    [SerializeField] private Vector3 nextGotoPositionArea;
    private void Start()
    {
        CreateNewGotoPositionArray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateNewGotoPositionArray();
        }
    }

    private void CreateNewGotoPositionArray()
    {
        gotoPosition = new Vector3[gotoPositionAmount];
        for (int i = 0; i < gotoPositionAmount; i++)
        {
            float offsetX = transform.position.x - (bossGotoPositionArea / 2);
            float offsetY = transform.position.y - (bossGotoPositionArea / 2);
            gotoPosition[i] = new Vector3(UnityEngine.Random.Range(offsetX, bossGotoPositionArea), gotoPositionHeight, UnityEngine.Random.Range(offsetY, bossGotoPositionArea));
            Instantiate(visuPref, gotoPosition[i], Quaternion.identity);
        }
    }
}
