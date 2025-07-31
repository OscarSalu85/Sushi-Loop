using System.Drawing;
using UnityEngine;

public class sushiManager : MonoBehaviour
{
    public float velocity;
    bool picked = false;
    bool placed = false;

    bool firstPicked = false;

    public GameObject plato;

    public Sushi sushi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sushi != null) {
            //Debug.Log(sushi.color);
            UnityEngine.Color MyColour = UnityEngine.Color.clear;
            ColorUtility.TryParseHtmlString(sushi.color, out MyColour);

            gameObject.GetComponent<SpriteRenderer>().color = MyColour;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstPicked)
        {
            gameObject.transform.position += new Vector3(0,velocity * Time.deltaTime,0);

            if (gameObject.transform.position.y > 13) 
            {
                velocity *= -1;
                gameObject.transform.position = new Vector3(7,10,gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.y < -13)
            {
                Destroy(gameObject);
            }
        }

    }

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        if (!firstPicked) { 
            firstPicked = true;
        }

        placed = false;
        picked = true;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }

    private void OnMouseUp()
    {
        if (plato != null)
        {
            plato.GetComponent<platoScript>().sushi = null;
            plato = null;
        }
        picked = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "plato" && !picked && !placed)
        {
            if(collision.GetComponent<platoScript>().sushi == null)
            {
                placed = true;
                Debug.Log("En el plato");
                collision.gameObject.GetComponent<platoScript>().sushi = gameObject;
                gameObject.transform.position = collision.transform.position;
                plato = collision.gameObject;
            }
            
        }
    }
}
