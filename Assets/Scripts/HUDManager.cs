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
        SetHearts();
    }

    public void SetValues(int rupees, int bombs, int arrows)
    {
        rupeeText.text = rupees.ToString();
        bombText.text = bombs.ToString();
        arrowText.text = arrows.ToString();
    }

    private void SetHearts()
    {
        int maxHealth = playerHealth.GetMaxHealth();
        int health = playerHealth.GetHealth();

        for(int i = 0; i<heartContainers.Length; i++)
        {
            heartContainers[i].gameObject.SetActive(true);
            if(i < health / 2)
            {
                heartContainers[i].sprite = fullHeart;
            }
            else if(i < (health / 2) + 1)
            {
                heartContainers[i].sprite = halfHeart;
            }
            else if(i < maxHealth / 2)
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
