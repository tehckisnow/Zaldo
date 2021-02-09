using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI rupeeText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI arrowText;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues(int rupees, int bombs, int arrows)
    {
        rupeeText.text = rupees.ToString();
        bombText.text = bombs.ToString();
        arrowText.text = arrows.ToString();
    }
}
