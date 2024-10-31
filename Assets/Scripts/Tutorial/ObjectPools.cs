using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public GameObject Prefab;
    public int InitialObjectNumber = 5;

    List<GameObject> objs;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        objs = new List<GameObject>();

        for(int i = 0; i < InitialObjectNumber; i++)
        {
            GameObject ob = Instantiate(Prefab, transform);
            ob.SetActive(false);
            objs.Add(ob);
        }
    }

    public GameObject GetObject()
    {
        foreach(GameObject ob in objs)
        {
            if(!ob.activeSelf)
            {
                ob.SetActive(true);
                return ob;
            }
        }

        return null;
    }

    public void SetFalseAllObject()
    {
        foreach(GameObject ob in objs)
        {
            if(ob.activeSelf)
            {
                ob.SetActive(false);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
