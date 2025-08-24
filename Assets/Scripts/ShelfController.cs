using UnityEngine;
using System.Collections.Generic;

public class ShelfController : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> shelfSlots;

    Dictionary<Drop, int> dropToShelfMapping = new Dictionary<Drop, int>();

    [Header("Special Shelf Settings")]
    [SerializeField] Transform anchor;
    [SerializeField] private int itemsPerRow = 5;
    [SerializeField] private Vector3 itemSpacing = new Vector3(1f, 0f, 0f); // spacing between items in a row
    [SerializeField] private Vector3 rowOffset = new Vector3(-0.5f, 0f, -0.5f); // offset applied when starting a new row
    [SerializeField] Drop debugDrop;

    private int itemCount = 0;

    private void Start()
    {
        if (debugDrop)
        {
            for (int i = 0; i < 8; i++)
            {
                AddItemToSpecialShelf(debugDrop);
            }
        }
    }

    /// <summary>
    /// Spawns a new item with a sprite at the next shelf position.
    /// </summary>
    public GameObject AddItemToSpecialShelf(Drop drop)
    {
        // Figure out row and column
        int row = itemCount / itemsPerRow;
        int column = itemCount % itemsPerRow;

        // Base position
        Vector3 position = anchor.position;
        position += column * itemSpacing;
        position += row * rowOffset;

        // Create object
        GameObject newItem = new GameObject("ShelfItem_" + itemCount);
        newItem.transform.position = position;
        newItem.transform.SetParent(anchor);
        newItem.transform.localScale = Vector3.one;

        // Add sprite renderer
        SpriteRenderer renderer = newItem.AddComponent<SpriteRenderer>();
        renderer.sprite = drop.icon;

        itemCount++;
        return newItem;
    }



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

    public void DisplaySpecialDrop(Drop drop)
    {

    }
}
