using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interaction, IPersist
{
    public GameObject contents;

    public Persistence PersistenceComponent { get; set; }
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    private GameObject itemInstance;
    private bool opened = false;

    public override void Activate()
    {
        if(!opened)
        {
            Open();
        }
    }

    private void Open()
    {
        TrueState();
        PersistenceComponent?.SetState("main", true); // if(PersistenceComponent != null) { PersistenceComponent.SetState("main", true); }
        DropItem();
    }

    public void TrueState()
    {
        GetComponent<SpriteRenderer>().sprite = openSprite;
        opened = true;
    }

    public void FalseState()
    {
        GetComponent<SpriteRenderer>().sprite = closedSprite;
        opened = false;
    }

    private void DropItem()
    {
        itemInstance = Instantiate(contents, gameObject.transform);
        itemInstance.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
        StartCoroutine("FloatDown");
    }

    IEnumerator FloatDown()
    {
        int t = 16;
        while(t > 0 && itemInstance != null)
        {
            t--;
            itemInstance.transform.position -= new Vector3(0, 0.1f, 0);
            //itemInstance.transform.position = Vector3.MoveTowards(itemInstance.transform.position, itemInstance.transform.position - new Vector3(0, 0.1f, 0), 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
