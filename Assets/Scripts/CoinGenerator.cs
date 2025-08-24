using UnityEngine;

[CreateAssetMenu(fileName = "CoinGenerator", menuName = "Scriptable Objects/CoinGenerator")]
public class CoinGenerator : ScriptableObject
{
    public Sprite icon;
    public float coinPerSecond;
    public int id;
}
