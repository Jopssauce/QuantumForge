using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level Config", menuName = "Level/Level Config", order = 1)]
public class LevelConfig : ScriptableObject
{
    public float maxRecordTime = 5f;
    public int recordAmount = 3;
}
