using TMPro;
using UnityEngine;

namespace Game.Spots
{
    public class SpotStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;

        public void UpdateResourceCount(int count, int total)
        {
            _text.text = $"{count}/{total}";
        }

        public void ShowProduction()
        {
            _text.text = "...Working...";
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}