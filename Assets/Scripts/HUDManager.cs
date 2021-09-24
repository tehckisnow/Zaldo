using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI rupeeText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI keyText;

    public Sprite emptyHeart;
    public Sprite halfHeart;
    public Sprite fullHeart;

    public Image[] heartContainers;

    private GameObject player;
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //SetHearts();
    }

    public void SetValues(int rupees, int bombs, int arrows, int keys)
    {
        rupeeText.text = rupees.ToString();
        bombText.text = bombs.ToString();
        arrowText.text = arrows.ToString();
        keyText.text = keys.ToString();
    }

    public void SetHearts(int health, int maxHealth)
    {
        // int maxHealth = playerHealth.GetMaxHealth();
        // int health = playerHealth.GetHealth();

        for(int i = 0; i<heartContainers.Length; i++)
        {
            heartContainers[i].gameObject.SetActive(true);
            if(2 * i < health - 1)
            {
                heartContainers[i].sprite = fullHeart;
            }
            else if(2 * i < health)
            {
                heartContainers[i].sprite = halfHeart;
            }
            else if(2 * i < maxHealth)
            {
                heartContainers[i].sprite = emptyHeart;
            }
            else 
            {
                heartContainers[i].sprite = null;
                heartContainers[i].gameObject.SetActive(false);
            }
        }

    }
}
