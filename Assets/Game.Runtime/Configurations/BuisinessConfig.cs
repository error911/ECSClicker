using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class BuisinessConfig : ScriptableObject
    {
        //[SerializeField] private AssetReferenceGameObject _prefabRef;
        [SerializeField] private string _name;
        [SerializeField][Min(1)] private float _incomeDelayTime = 1; //Задержка дохода
        [SerializeField][Min(1)] private int _baseCost = 1;        //Базовая стоимость
        [SerializeField][Min(1)] private int _baseIncome = 1;      //Базовый доход
        //[SerializeField] private ImprovementConfig[] _improvements;       //Улучшения
        [SerializeField] private ImprovementConfig _improvement1;       //Улучшения
        [SerializeField] private ImprovementConfig _improvement2;       //Улучшения

        //public AssetReferenceGameObject prefabRef => _prefabRef;
        public string Name => _name;
        public float IncomeDelayTime => _incomeDelayTime;
        public int BaseCost => _baseCost;
        public int BaseIncome => _baseIncome;
        public ImprovementConfig Improvement1 => _improvement1;
        public ImprovementConfig Improvement2 => _improvement2;
    }
}