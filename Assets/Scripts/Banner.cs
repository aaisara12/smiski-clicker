using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DropRate
{
    public Drop drop;
    [Range(0f, 1f)]
    public float rate; // probability (0�1)
}

[CreateAssetMenu(fileName = "NewBanner", menuName = "Gacha/Banner")]
public class Banner : ScriptableObject
{
    [Header("Banner Drops & Rates")]
    public List<DropRate> dropsWithRates = new List<DropRate>();

    [Header("Availability")]
    public int timeUnitsOffered; // e.g. days, hours, or custom units
}
