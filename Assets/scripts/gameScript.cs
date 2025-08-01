using TMPro;
using UnityEngine;

public class Player
{
    public int vida;
    public int def;
}

public class Enemy
{
    public int vida;
    public int def;
}

public class gameScript : MonoBehaviour
{
    public TMP_Text counter;
    float time;
    public float roundTime;

    public GameObject mesa;

    public Player player;
    public Enemy enemy;

    public GameObject cintaManager;

    public TMP_Text vidaEnemy;
    public TMP_Text vidaPlayer;

    public TMP_Text defEnemy;
    public TMP_Text defPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = new Player();
        player.vida = 5;
        player.def = 6;

        enemy = new Enemy();
        enemy.vida = 15;
        enemy.def = 6;

        time = roundTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0) 
        {   
            time = roundTime;
            mesa.GetComponent<mesaManager>().vaciarMesa();
        }

        //counter.text = time > 4 ? ((int)time).ToString() : Math.Round(time,2).ToString(); va raro 
        counter.text = ((int)time).ToString();       

        updateText();
        
    }

    void updateText()
    {
        vidaEnemy.text = enemy.vida.ToString();
        vidaPlayer.text = player.vida.ToString();

        defEnemy.text = enemy.def.ToString();
        defPlayer.text = player.def.ToString();
    }
}
