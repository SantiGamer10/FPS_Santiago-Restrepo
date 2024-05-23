using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoolData", fileName = "BoolData")]
public class BoolData : ScriptableObject
{
    private bool boolData;

    public bool _boolData { get { return boolData; } set { boolData = value; } }
}
