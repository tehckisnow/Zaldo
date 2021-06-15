using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistence : MonoBehaviour
{
    //[SerializeField] private Component component;
    [SerializeField] private MonoBehaviour component;
    [SerializeField] private bool startingState = false;

    private string id;
    private PersistentData persistentData;

    // Start is called before the first frame update
    void Start()
    {
        persistentData = GameManager.instance.persistentData;

        //!component.persistence = this;
        component.gameObject.GetComponent<Door>().persistence = this;

        GenerateId();
        SetInitialState();
    }

    public void SetState(string key, bool state)
    {
        persistentData.RegisterValue(id, key, state);
    }

    private void GenerateId()
    {
        string scene = SceneManager.GetActiveScene().name;
        string name = gameObject.name;
        string x = transform.position.x.ToString();
        string y = transform.position.y.ToString();
        id += scene += name += x += y;
    }

    private void SetInitialState()
    {
        if(persistentData.Exists(id, "main"))
        {
            if(persistentData.GetData(id, "main"))
            {
                //!component.TrueState();
                //component.SendMessage("TrueState");
                component.Invoke("TrueState", 0f);
                //component.gameObject.GetComponent<Door>().TrueState();
            }
            else
            {
                //!component.FalseState();
                //component.SendMessage("FalseState");
                component.Invoke("FalseState", 0f);
                //component.gameObject.GetComponent<Door>().FalseState();
            }
        }
        else
        {
            persistentData.RegisterValue(id, "main", startingState);
        }
    }

}
