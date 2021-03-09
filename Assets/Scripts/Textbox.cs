using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textbox : MonoBehaviour
{
    public Text textElement;

    public void Open(string text)
    {
        SetText(text);
        gameObject.SetActive(true);
    }

    public void SetText(string text)
    {
        textElement.text = text;
    }

    public void Close()
    {
        SetText("");
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
