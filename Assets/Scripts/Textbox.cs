using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Textbox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textfield = null;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private AudioSource sfx = null;
    [SerializeField] private string openAnimation = "Open";
    [SerializeField] private string closeAnimation = "Close";
    [SerializeField] private float delay = 0.5f;

    private bool currentlyTyping = false;
    private string textContent;
    private int currentPage = 0;
    private Animator animator;
    private Action callback = null;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        textfield.overflowMode = TextOverflowModes.Page;
        callback += () => {Debug.Log("default callback");}; //generic callback
        //why is the above line overriding callback = act in Open()?
        //because Open is called before this gameObject is activated
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Open(string text, Action act)
    {
        callback += act;
        Open(text);
    }

    public void Open(string text)
    {
        textContent = text;
        gameObject.SetActive(true);
        //animation
        if(animator != null)
        {
            animator.Play(openAnimation);
        }
        textfield.text = text;
        textfield.maxVisibleCharacters = 0;
        textfield.pageToDisplay = 1;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        //delay for animation
        yield return new WaitForSeconds(delay);
        currentlyTyping = true;
        while(textfield.maxVisibleCharacters <= textfield.textInfo.pageInfo[currentPage].lastCharacterIndex)
        {
            if(sfx != null)
            {
                sfx.Play();
            }
            textfield.maxVisibleCharacters++;
            yield return new WaitForSeconds(textSpeed);
        }
        currentlyTyping = false;
    }

    public void Advance()
    {
        //handle interrupt
        if(currentlyTyping)
        {
            Interrupt();
        }
        else
        //if there is overflow
        if(textfield.textInfo.pageCount > currentPage + 1)
        {
            //int toRemove = textfield.maxVisibleCharacters;
            //textfield.maxVisibleCharacters = 0;
            //textfield.text = textfield.text.Substring(toRemove);
            textfield.pageToDisplay++;
            currentPage++;
            StartCoroutine(TypeText());
        }
        else 
        {
            //if no overflow, call Close()
            Close();
        }
    }

    public void Close()
    {
        currentPage = 0;
        textfield.text = "";
        //animation
        if(animator != null)
        {
            animator.Play(closeAnimation);
        }
        else 
        {
            Disable();
        }
        callback();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Interrupt()
    {
        textfield.maxVisibleCharacters = textfield.textInfo.pageInfo[currentPage].lastCharacterIndex;
    }
}