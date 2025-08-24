using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainUiModel", menuName = "Scriptable Objects/MainUiModel")]
public class MainUiModel : ScriptableObject
{
    public int Gold;

    public List<BannerManager.ActiveBanner> Banners;

    public List<CoinGeneratorGroup> CoinGenerators;
}
