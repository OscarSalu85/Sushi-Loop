using TMPro;
using UnityEngine;

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
        counter.text =  ((int)time).ToString();
        if (time <= 0) time = roundTime;
    }
}
