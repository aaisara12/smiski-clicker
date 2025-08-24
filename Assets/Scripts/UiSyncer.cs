using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class UiSyncer : MonoBehaviour
    {
        [SerializeField] private MainUiModel _mainUiModel;
        [SerializeField] private UIDocument _mainUiDocument;
        [SerializeField] private BannerManager _bannerManager;
        [SerializeField] private GameManager _gameManager;

        private Button _mineButton;
        
        private void Start()
        {
            _bannerManager.OnBannerChanged.AddListener(HandleOnBannerChanged);
            _gameManager.CoinGeneratorsUpdated.AddListener(HandleCoinGeneratorsUpdated);

            _mineButton = _mainUiDocument.rootVisualElement.Q<Button>("mine-button");
            _mineButton.RegisterCallback<ClickEvent>(OnClickMineButton);
        }

        private void HandleCoinGeneratorsUpdated()
        {
            List<CoinGeneratorGroup> newCointGeneratorGroups = new List<CoinGeneratorGroup>();
            
            foreach (var activeCoinGeneratorPair in _gameManager.activeCoinGenerators)
            {       
                var coinGenerator = activeCoinGeneratorPair.Key;
                var coinGeneratorCount = activeCoinGeneratorPair.Value;

                var coinGeneratorGroup = new CoinGeneratorGroup();
                coinGeneratorGroup.coinGenerator = coinGenerator;
                coinGeneratorGroup.count = coinGeneratorCount;
                
                newCointGeneratorGroups.Add(coinGeneratorGroup);
            }
            
            _mainUiModel.CoinGenerators = newCointGeneratorGroups;
        }

        private void OnClickMineButton<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
        {
            _gameManager.MineBitCoin();
        }

        private void Update()
        {
            _mainUiModel.Gold = _gameManager.Coins;
        }

        private void HandleOnBannerChanged()
        {
            _mainUiModel.Banners = _bannerManager.activeBanners;
        }

        private void OnDestroy()
        {
            _bannerManager.OnBannerChanged.RemoveListener(HandleOnBannerChanged);
            _gameManager.CoinGeneratorsUpdated.RemoveListener(HandleCoinGeneratorsUpdated);
        }
    }
}