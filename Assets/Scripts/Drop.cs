using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDrop", menuName = "Gacha/Drop")]
public class Drop : ScriptableObject
{
    [Header("Drop Data")]
    public Sprite icon;          // png reference
    public string identifier;    // unique name or ID
}
