using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] List<GameObject> childs;
    [SerializeField] GameObject originalGameObject;
    void Awake()
    {
        originalGameObject = GameObject.Find("Image");
        childs = new List<GameObject>(originalGameObject.transform.childCount);
        for (int i = 0; i < childs.Count; i++)
        {
            childs.Add(originalGameObject.transform.GetChild(i).gameObject);
        }
    }
    void Update()
    {
        
    }
}
