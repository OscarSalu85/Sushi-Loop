using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sushi
{
    public string name;
    public string type;
    public int num;
    public string color;
}

public class cintaManager : MonoBehaviour
{

    public TextAsset csv;

    public GameObject prefab;
    float time = 0;

    float spawnTime;
    public float velocity;

    public float rate;

    public List<Sushi> sushiList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = Random.Range(0.5f, rate);
        readCSV();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTime) {
            Instantiate(prefab);
            prefab.GetComponent<sushiManager>().velocity = velocity;

            int sushiType = Random.Range(0, sushiList.Count);

            prefab.GetComponent<sushiManager>().sushi = sushiList[sushiType];
            //Debug.Log(prefab.GetComponent<sushiManager>().sushi.name);

            spawnTime = Random.Range(0.5f, rate);
            time = 0;
        }
    }

    public void readCSV()
    {
        sushiList = new List<Sushi>();

        string content = csv.ToString();
        string[] splt = content.Split("\n");
        for (int i = 1; i < splt.Length; i++)
        {
            string[] split = splt[i].Split(",");
            Sushi sushi = new Sushi();
            sushi.name = split[0];
            sushi.type = split[1];
            sushi.num = int.Parse(split[2]);
            sushi.color = split[3];
            sushiList.Add(sushi);
            Debug.Log(sushi.color);
        }
    }
}
