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
    Sprite[] sushiSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = Random.Range(0.5f, rate);
        readCSV();

        sushiSprites = Resources.LoadAll<Sprite>("uramaki");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTime)
        {
            SpawnSushi();

            rate = 2 * velocity / 7;
            spawnTime = Random.Range(0.15f, 2f / rate);
            time = 0;
        }

        velocity = Mathf.Min(velocity, 56);
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

    public void SpawnSushi()
    {
        GameObject newSushi = Instantiate(prefab);
        newSushi.transform.SetParent(gameObject.transform); //esto es pa que no haya tanto lio

        sushiManager sushi = newSushi.GetComponent<sushiManager>();
        newSushi.transform.position = new Vector3(newSushi.transform.position.x, -8f, newSushi.transform.position.z);
        sushi.velocity = velocity;

        int sushiType = Random.Range(0, sushiList.Count);

        sushi.sushi = sushiList[sushiType];
        sushi.cinta = this;
        //Debug.Log(prefab.GetComponent<sushiManager>().sushi.name);

        switch (sushiList[sushiType].name)
        {
            case "maki":
                newSushi.GetComponent<SpriteRenderer>().sprite = sushiSprites[0];
                break;

            case "nigiri_gamba":
                newSushi.GetComponent<SpriteRenderer>().sprite = sushiSprites[1];
                break;

            case "nigiri_salmon":
                newSushi.GetComponent<SpriteRenderer>().sprite = sushiSprites[2];
                break;
        }

    }
}
