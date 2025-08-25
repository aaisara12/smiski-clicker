using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using Random = System.Random;

public class BannerManager : MonoBehaviour
{
    [System.Serializable]
    public class ActiveBanner
    {
        public string name;
        public Banner banner;
        public int timeRemaining; // in seconds
        public int price;
        public int id;
    }

    [Header("Banner Settings")]
    public int cycleInterval = 15; // seconds between banner swaps
    public List<Banner> queuedBanners = new List<Banner>();

    [Header("Active Banners")]
    public List<ActiveBanner> activeBanners = new List<ActiveBanner>();

    private int currentBannerIndex = 0;
    private Coroutine cycleRoutine;

    // Event triggered when a banner is displayed
    public UnityEvent OnBannerChanged = new UnityEvent();


    private int currentBannerId = 0;

    [SerializeField] private List<string> suffixes = new List<string>()
    {
        "Bonanza",
        "Masters",
        "Anniversary",
        "Legends",
        "Xtreme Booster",
        "Court",
        "Guardians"
    };
    
    // Add a new banner
    public void AddBanner(Banner banner)
    {
        if (banner == null) return;

        activeBanners.Add(new ActiveBanner()
        {
            name = banner.packName + " " + suffixes[currentBannerId % suffixes.Count],
            banner = banner,
            price = Mathf.RoundToInt(banner.price * (1f + 0.1f * GameManager.Instance.prestigeLevel)), 
            timeRemaining = banner.timeUnitsOffered,
            id = currentBannerId++
        });

        AudioManager.Instance.PlayNewBanner();
    }

    // Remove a banner explicitly
    public void RemoveBanner(ActiveBanner banner)
    {
        activeBanners.RemoveAll(b => b == banner);
    }

    public void QueueBanner(Banner banner)
    {
        queuedBanners.Add(banner);
    }

    public void ResetBanners()
    {
        ClearBannerQueue();
        activeBanners.Clear();
        if (cycleRoutine != null)
            StopCoroutine(cycleRoutine);
        OnBannerChanged.Invoke();
        currentBannerIndex = 0;
        cycleRoutine = StartCoroutine(CycleBanners());
    }

    public void ClearBannerQueue()
    {
        queuedBanners.Clear();
    }

    private IEnumerator CycleBanners()
    {
        while (true)
        {
            if (queuedBanners.Count <= 0)
            {
                yield return null;
                continue;
            }

            AddBanner(queuedBanners[currentBannerIndex]);

            // Wait for cycleInterval seconds, ticking once per second
            for (int elapsed = 0; elapsed < cycleInterval; elapsed++)
            {
                OnBannerChanged.Invoke();
                yield return new WaitForSeconds(1f);

                // Decrease time remaining on all banners
                for (int i = activeBanners.Count - 1; i >= 0; i--)
                {
                    activeBanners[i].timeRemaining--;

                    if (activeBanners[i].timeRemaining <= 0)
                    {
                        // Time ran out, remove this banner
                        RemoveBanner(activeBanners[i]);
                    }
                }
            }
            OnBannerChanged.Invoke();
            currentBannerIndex = (currentBannerIndex+1) % queuedBanners.Count;
        }
    }
}
