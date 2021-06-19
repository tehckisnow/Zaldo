using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Health
{
    public int dropChanceOutOf100 = 10;
    public List<GameObject> drops;

    public override void Die()
    {
        if(Random.Range(1, 100) <= dropChanceOutOf100)
        {
            Drop();
        }
        Destroy(this.gameObject);
    }

    public void Drop()
    {
        int choice = Random.Range(0, drops.Count);
        Instantiate(drops[choice], gameObject.transform.position, Quaternion.identity);
    }
}
