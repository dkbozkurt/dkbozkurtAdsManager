using System;
using DkbozkurtAdsManager.Scripts.Managers;
using UnityEngine;

namespace DkbozkurtAdsManager.Scripts.Helpers
{
    public class AdTest : MonoBehaviour
    {
        private void Start()
        {
            AdMobManager.Instance.CallRewarded();
        }
    }
}
