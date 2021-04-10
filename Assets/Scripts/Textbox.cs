using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Textbox : MonoBehaviour
{
    //[SerializeField] private Text textElement;
    [SerializeField] private TextMeshProUGUI textElement;
    [SerializeField] private float textSpeed = 50f;

    public bool isOpen = false;

    public void Write(string textToType)
    {
        if(!isOpen)
        {
            Open();
        }
        StartCoroutine(TypeText(textToType));
    }

    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
        //animate
    }

    public void Close()
    {
        isOpen = false;
        textElement.text = string.Empty;
        //animate
        gameObject.SetActive(false);
    }

    private IEnumerator TypeText(string textToType)
    {
        textElement.text = string.Empty;
        yield return new WaitForSeconds(1);
        float t = 0;
        int charIndex = 0;
        while(charIndex < textToType.Length)
        {
            t += Time.deltaTime * textSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textElement.text = textToType.Substring(0, charIndex);
            yield return null;
        }
        textElement.text = textToType;
    }

}
