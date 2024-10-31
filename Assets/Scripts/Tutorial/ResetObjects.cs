using System.Collections.Generic;
using UnityEngine;

public class ResetObjects : MonoBehaviour
{
    List<GameObject> resetableObjects = new List<GameObject>();

    public void SaveResetItems(GameObject item)
    {
        resetableObjects.Add(item);
    }

    public void ReplayInit()
    {
        foreach(GameObject obj in resetableObjects)
        {
            obj.SetActive(true);
        }
        resetableObjects.Clear();
    }
}
