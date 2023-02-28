using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BuisenessUI : MonoBehaviour
    {
        [SerializeField] private Transform _contentContainer;
        [SerializeField] private BuisenessWidget _buisenessPanelPref;
        private List<BuisenessWidget> _buisenessList = new List<BuisenessWidget>();

        public BuisenessWidget Add()
        {
            var newBis = Instantiate(_buisenessPanelPref, _contentContainer);
            newBis.gameObject.SetActive(true);
            _buisenessList.Add(newBis);
            return newBis;
        }

        public BuisenessWidget Get(int id)
        {
            return _buisenessList[id];
        }

        public void Remove(int id)
        {
            var bis = _buisenessList[id];
            bis.gameObject.SetActive(false);
            _buisenessList.RemoveAt(id);
            Destroy(bis);
        }
    }
}
