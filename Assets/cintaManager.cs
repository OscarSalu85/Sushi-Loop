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
        sushiList = readCSV();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTime) {
            Instantiate(prefab);
            prefab.GetComponent<sushiManager>().velocity = velocity;

            int sushiType = Random.Range(0, sushiList.Count);

            prefab.GetComponent<sushiManager>().sushi.name = sushiList[sushiType].name;
            prefab.GetComponent<sushiManager>().sushi.type = sushiList[sushiType].type;
            prefab.GetComponent<sushiManager>().sushi.num = sushiList[sushiType].num;
            prefab.GetComponent<sushiManager>().sushi.color = sushiList[sushiType].color;
            //Debug.Log(prefab.GetComponent<sushiManager>().sushi.name);

            spawnTime = Random.Range(0.5f, rate);
            time = 0;
        }
    }

    public List<Sushi> readCSV()
    {
        List<Sushi> sushiList = new List<Sushi>();

        string content = csv.ToString();
        string[] splt = content.Split("\n");
        for (int i = 0; i < splt.Length; i++)
        {
            if (i != 0)
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
        return sushiList;
    }
}
