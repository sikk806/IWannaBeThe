using System.Collections.Generic;
using UnityEngine;

public class ResetObjects : MonoBehaviour
{
    List<GameObject> resetableObjects = new List<GameObject>();
    List<GameObject> replayInitObjects = new List<GameObject>();

    public void SaveResetItems(GameObject item)
    {
        resetableObjects.Add(item);
    }

    public void SaveResetInitItems(GameObject item)
    {
        replayInitObjects.Add(item);
    }

    public void ReplayInit()
    {
        foreach(GameObject obj in resetableObjects)
        {
            obj.SetActive(true);
        }
        resetableObjects.Clear();
    }

    public void CanReplayInit()
    {
        foreach(GameObject obj in replayInitObjects)
        {
            
            // if(obj.gameObject.GetComponent<LinearMotion>())
            // {
            //     obj.gameObject.GetComponent<LinearMotion>().ReplayInit();
            // }
        }
    }
}
