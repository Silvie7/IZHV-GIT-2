using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollect : MonoBehaviour
{
    private GameObject coin;
    public TextMeshProUGUI textMeshPro;
    public int coinsCollected = 0;
    public GameManager otherScript;

    // Start is called before the first frame update
    void Start()
    {


        coin = GameObject.FindGameObjectWithTag("Coin");

       
    }

    private void Update()
    {
        textMeshPro.text = coinsCollected.ToString();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)

    {

        if (collision.gameObject.tag == "Coin")
        { 
            
            coinsCollected += 1;
            Destroy(collision.gameObject);

        }   
        if (coinsCollected == 5)
        {
            otherScript.WinGame();
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
        
  
    }
    
}
