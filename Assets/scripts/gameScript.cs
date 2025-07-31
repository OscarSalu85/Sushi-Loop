using TMPro;
using UnityEngine;
using System;

public class gameScript : MonoBehaviour
{
    public TMP_Text counter;
    float time;
    public float roundTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = roundTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0) time = roundTime;

        //counter.text = time > 4 ? ((int)time).ToString() : Math.Round(time,2).ToString(); va raro 
        counter.text = ((int)time).ToString();
       
        
        
    }
}
