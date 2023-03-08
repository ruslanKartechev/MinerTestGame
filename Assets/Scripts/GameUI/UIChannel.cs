using System;
using UnityEngine;

namespace GameUI
{
    [CreateAssetMenu(fileName = nameof(UIChannel), menuName = "SO/Channels/" + nameof(UIChannel))]
    public class UIChannel : ScriptableObject
    {
        public Action ShowStart;
        public Action HideAll;
        public Action ShowHUD;
        public Action ShowWin;
        public Action ShowLoose;
    }
}