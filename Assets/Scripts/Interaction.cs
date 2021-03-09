using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public virtual void Activate()
    {
        Debug.Log(this.name + " activated.");
        GameManager.instance.textbox.Open("Woot!");
        StartCoroutine(CloseTextbox());
    }

    IEnumerator CloseTextbox()
    {
        yield return new WaitForSeconds(4);
        GameManager.instance.textbox.Close();
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
