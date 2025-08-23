using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDropSet", menuName = "Gacha/Drop Set")]
public class DropSet : ScriptableObject
{
    [Header("Drops in Set")]
    public List<Drop> drops = new List<Drop>();

    [Header("Bonus Banner")]
    public Banner bonusBanner;   // reference to a banner
}
