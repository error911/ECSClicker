using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _startMoney;
        [SerializeField] private BuisinessConfig[] _buisinesses;       //Бизнесы

        public int StartMoney => _startMoney;
        public BuisinessConfig[] Buisinesses => _buisinesses;
    }
}