using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RollController : MonoBehaviour
{
    public Banner banner;
    [SerializeField] Image icon;
    Drop result;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public Drop GenerateRollResult()
    {
        if (banner == null || banner.dropsWithRates.Count == 0)
        {
            Debug.LogWarning("Invalid banner roll attempted.");
            return null;
        }

        // Calculate total weight (should be <= 1 if normalized, but we allow flexibility)
        float totalWeight = 0f;
        foreach (var dr in banner.dropsWithRates)
            totalWeight += dr.rate;

        if (totalWeight <= 0f)
        {
            Debug.LogWarning("Banner has no valid drop rates.");
            return null;
        }

        // Pick a random value between 0 and total weight
        float roll = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var dr in banner.dropsWithRates)
        {
            cumulative += dr.rate;
            if (roll <= cumulative)
                return dr.drop;
        }

        // Fallback (should not happen if rates are set properly)
        return banner.dropsWithRates[banner.dropsWithRates.Count - 1].drop;
    }

    IEnumerator Roll()
    {
        result = GenerateRollResult();

        if (result == null) yield break;

        List<DropRate> drops = banner.dropsWithRates;
        int ind = 0;

        // Cycle through all options for some seconds
        float timeElapsed = 0;
        while (timeElapsed < 4)
        {
            yield return new WaitForSeconds(.25f);
            timeElapsed += 0.25f;
            icon.sprite = drops[ind].drop.icon;
            ind = (ind + 1) % drops.Count;
        }

        // Tick slower for a few times
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.5f);
            icon.sprite = drops[Random.Range(0, drops.Count - 1)].drop.icon;
        }
        yield return new WaitForSeconds(0.75f);

        icon.sprite = result.icon;
    }

    public void InitiateRoll()
    {
        StartCoroutine(Roll());
    }

    public void ResolveRoll()
    {
        // Register roll result to GameManager
        GameManager.Instance.AddDropToCollection(result); 
        
        // Disable Component
    }

}
