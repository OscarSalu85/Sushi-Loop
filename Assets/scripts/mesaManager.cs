using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mesaManager : MonoBehaviour
{
    public GameObject manager;
    public List<GameObject> platos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void vaciarMesa()
    {
        comprobarSushi();
        turnoEnemigo();
        foreach (GameObject obj in platos)
        {
            GameObject sushi = obj.GetComponent<platoScript>().sushi;
            Destroy(sushi);
            obj.GetComponent<platoScript>().sushi = null;
        }
    }

    public void turnoEnemigo()
    {
        Player player = manager.GetComponent<gameScript>().player;
        Enemy enemy = manager.GetComponent<gameScript>().enemy;
        bool rep = true;
        while (rep)
        {
            int dec = Random.Range(0, 5);
            switch (dec)
            {
                case 0:
                    if(player.def <= 0)
                    {
                        player.vida -= 1;
                        rep = false;
                    }
                    break;
                case 1:
                    player.def = Mathf.Max(player.def-2, 0);
                    rep = false;
                    break;
                case 2:
                    if(manager.GetComponent<gameScript>().cintaManager.GetComponent<cintaManager>().velocity < 56)
                    {
                        manager.GetComponent<gameScript>().cintaManager.GetComponent<cintaManager>().velocity *= 2;
                        rep = false;
                    }
                    
                    break;
                case 3:
                    manager.GetComponent<gameScript>().cintaManager.GetComponent<cintaManager>().velocity /= 2;
                    rep = false;
                    break;
                case 4:
                    enemy.def += 2;
                    rep = false;
                    break;


            }
        }
    }

    public void comprobarSushi()
    {
        int dañoVida = 0;
        int dañoDef = 0;
        int defensa = 0;
        float vel = 1;

        foreach(GameObject obj in platos)
        {
            if (obj.GetComponent<platoScript>().sushi != null)
            {
                Sushi currentSushi = obj.GetComponent<platoScript>().sushi.GetComponent<sushiManager>().sushi;
                if (obj.GetComponent<platoScript>().delante)
                {
                    switch (currentSushi.type)
                    {
                        case "atk":
                            dañoVida += currentSushi.num;
                            break;
                        case "def":
                            dañoDef += currentSushi.num;
                            break;
                        case "vel":
                            vel *= currentSushi.num;
                            break;
                    }
                }
                else
                {
                    switch (currentSushi.type)
                    {
                        case "atk":
                            dañoVida *= 2;
                            break;
                        case "def":
                            defensa += currentSushi.num;
                            break;
                        case "vel":
                            vel /= currentSushi.num;
                            break;
                    }
                }
            }
        }

        Player player = manager.GetComponent<gameScript>().player;
        Enemy enemy = manager.GetComponent<gameScript>().enemy;

        if(enemy.def > 0)
        {
            enemy.def = Mathf.Max(enemy.def-dañoDef, 0);
        }
        else
        {
            enemy.vida = Mathf.Max(enemy.vida-dañoVida, 0);
        }

        player.def += defensa;

        manager.GetComponent<gameScript>().cintaManager.GetComponent<cintaManager>().velocity *= vel;

    }
}
