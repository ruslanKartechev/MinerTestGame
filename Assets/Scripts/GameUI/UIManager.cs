using System;
using UnityEngine;

namespace GameUI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIPage _hudPage;
        [SerializeField] private UIPage _controlsPage;
        [SerializeField] private UIChannel _uiChannel;

        private void Awake()
        {
            HideAll();
        }

        private void OnEnable()
        {
            _uiChannel.ShowHUD = ShowHud;
            _uiChannel.HideAll = HideAll;
        }

        private void ShowHud()
        {
            _hudPage.Show();
            _controlsPage.Show();
        }

        private void HideAll()
        {
            _hudPage.Hide();
            _controlsPage.Hide();
        }
        
    }
}