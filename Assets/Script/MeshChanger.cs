using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    [SerializeField] private MeshFilter filtrer;
    [SerializeField] private List<Mesh> meshesList = new List<Mesh>();
    private int index = 0;

    private void Awake()
    {
        if (meshesList.Count == 0)
        {
            Debug.LogError($"{name}: List is 0.\nPlease check and assign one.\nDisabled component..");
            enabled = false;
            return;
        }
        if (!filtrer) 
        {
            Debug.LogError($"{name}: MeshFilter is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    [ContextMenu("ChangeMesh")]
    private void Change()
    {
        if (index >= meshesList.Count)
        {
            index = 0;
        }
        if(meshesList[index] != null)
        {
            filtrer.mesh = meshesList[index];
        }
        index++;
    }
}