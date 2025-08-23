using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class BannerManager : MonoBehaviour
{
    [System.Serializable]
    public class ActiveBanner
    {
        public Banner banner;
        public int timeRemaining; // in seconds
    }

    [Header("Banner Settings")]
    public int cycleInterval = 15; // seconds between banner swaps

    [Header("Active Banners")]
    public List<ActiveBanner> activeBanners = new List<ActiveBanner>();

    private int currentBannerIndex = 0;
    private Coroutine cycleRoutine;

    // Event triggered when a banner is displayed
    public UnityEvent OnBannerChanged = new UnityEvent();

    // Add a new banner
    public void AddBanner(Banner banner)
    {
        if (banner == null) return;

        activeBanners.Add(new ActiveBanner()
        {
            banner = banner,
            timeRemaining = banner.timeUnitsOffered
        });

        if (cycleRoutine == null)
            cycleRoutine = StartCoroutine(CycleBanners());
    }

    // Remove a banner explicitly
    public void RemoveBanner(Banner banner)
    {
        activeBanners.RemoveAll(b => b.banner == banner);

        if (activeBanners.Count == 0 && cycleRoutine != null)
        {
            StopCoroutine(cycleRoutine);
            OnBannerChanged.Invoke();
            cycleRoutine = null;
            currentBannerIndex = 0;
        }
    }

    public void ResetBanners()
    {
        activeBanners.Clear();
        StopCoroutine(cycleRoutine);
        OnBannerChanged.Invoke();
        cycleRoutine = null;
        currentBannerIndex = 0;
    }

    private IEnumerator CycleBanners()
    {
        while (activeBanners.Count > 0)
        {
            if (currentBannerIndex >= activeBanners.Count)
                currentBannerIndex = 0;

            ActiveBanner current = activeBanners[currentBannerIndex];

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
                        RemoveBanner(activeBanners[i].banner);

                        // Adjust index to not skip banners
                        if (i <= currentBannerIndex)
                            currentBannerIndex--;
                    }
                }

                if (activeBanners.Count == 0)
                    yield break;
            }

            currentBannerIndex++;
        }
    }
}
