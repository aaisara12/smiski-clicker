using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BannerSet", menuName = "Gacha/BannerSet")]
public class BannerSet : ScriptableObject
{
    // Set of banners to be made available for current active set
    public List<Banner> banners;
}
