using UnityEngine;
using System.Collections.Generic;

public class ShelfController : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> shelfSlots;

    Dictionary<Drop, int> dropToShelfMapping = new Dictionary<Drop, int>();

    public void RegisterDropSet(DropSet set)
    {
        dropToShelfMapping.Clear();
        int i = 0;
        for (; i < shelfSlots.Count; i++)
        {
            if (i >= set.drops.Count)
                break;
            shelfSlots[i].enabled = true;
            shelfSlots[i].sprite = set.drops[i].icon;
            shelfSlots[i].color = Color.black;
            dropToShelfMapping[set.drops[i]] = i;
        }

        for (; i< shelfSlots.Count; i++)
        {
            shelfSlots[i].enabled = false;
        }
    }

    public void RegisterDrop(Drop drop)
    {
        if (dropToShelfMapping.ContainsKey(drop))
        {
            shelfSlots[dropToShelfMapping[drop]].color = Color.white;  // Get active color in the future 
        }
    }
}
