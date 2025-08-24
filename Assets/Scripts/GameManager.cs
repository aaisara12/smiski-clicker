using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public int Coins { get { return (int) Mathf.Round(coins); } set { coins = value; } }
    private float coins = 0;

    [SerializeField] public RollController rollController;
    [SerializeField] public BannerManager bannerManager;

    [SerializeField] List<DropSet> sets;
    [SerializeField] List<BannerSet> bannerSets;

    public int prestigeLevel = 0;
    private int currentActiveSet = 0;  // Index of dropset and bannerset to use. Mod the result is greater than available sets 
    HashSet<Drop> currentCollectedDrops = new HashSet<Drop>();  // The drops collected in the current set 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Prestige(); 
    }

    public void AddDropToCollection(Drop drop)
    {
        // TODO: Handle special drop collection

        currentCollectedDrops.Add(drop);
        if (currentCollectedDrops.Count == sets[currentActiveSet].drops.Count)
        {
            Prestige();
        }

    }

    public void Prestige()
    {
        prestigeLevel++;
        Banner bonusBanner = sets[currentActiveSet].bonusBanner;
        currentActiveSet = prestigeLevel % sets.Count;
        currentCollectedDrops.Clear(); 
        bannerManager.ResetBanners();
        foreach(Banner banner in bannerSets[currentActiveSet].banners)
        {
            bannerManager.QueueBanner(banner); 
        }
        if (bonusBanner != null)
            bannerManager.QueueBanner(bonusBanner);
    }


    #region Money Generation
    public Dictionary<CoinGenerator, int> activeCoinGenerators = new Dictionary<CoinGenerator, int>();
    public UnityEvent CoinGeneratorsUpdated = new UnityEvent();
    float coinPerSecondTotal = 0;

    private void Update()
    {
        coins += coinPerSecondTotal * Time.deltaTime;
    }

    public void AddCoinGenerator(CoinGenerator generator, int amt = 1)
    {
        if (!activeCoinGenerators.ContainsKey(generator))
            activeCoinGenerators[generator] = 0;
        activeCoinGenerators[generator] += amt;

        coinPerSecondTotal += amt * generator.coinPerSecond;
        CoinGeneratorsUpdated.Invoke();
    }

    public void RemoveCoinGenerator(CoinGenerator generator, int amt = 1)
    {
        int delta = 0;
        if (activeCoinGenerators.ContainsKey(generator))
        {
            int orig = activeCoinGenerators[generator];
            activeCoinGenerators[generator] -= amt;
            if (activeCoinGenerators[generator] < 0)
                activeCoinGenerators[generator] = 0;
            delta = activeCoinGenerators[generator] - orig;
        }

        coinPerSecondTotal += delta * generator.coinPerSecond;
        CoinGeneratorsUpdated.Invoke();
    }


    #endregion

}
