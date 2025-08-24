using System;
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

            _mineButton = _mainUiDocument.rootVisualElement.Q<Button>("mine-button");
            _mineButton.RegisterCallback<ClickEvent>(OnClickMineButton);
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
        }
    }
}