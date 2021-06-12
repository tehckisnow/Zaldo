using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, bool>> data = new Dictionary<string, Dictionary<string, bool>>();

    public bool Exists(string id, string name)
    {
        if(data.ContainsKey(id))
        {
            if(data[id].ContainsKey(name))
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public void RegisterValue(string id, string name, bool value)
    {
        if(data.ContainsKey(id))
        {
            if(data[id].ContainsKey(name))
            {
                SetData(id, name, value);
            }
            else
            {
                RegisterData(id, name, value);
            }
        }
        else
        {
            RegisterId(id);
            RegisterValue(id, name, value);
        }
    }

    public bool GetData(string id, string name)
    {
        return data[id][name];
    }

    private void SetData(string id, string name, bool value)
    {
        data[id][name] = value;
    }

    private void RegisterId(string id)
    {
        data.Add(id, new Dictionary<string, bool>());
    }

    private void RegisterData(string id, string name, bool value)
    {
        data[id].Add(name, value);
    }
}
