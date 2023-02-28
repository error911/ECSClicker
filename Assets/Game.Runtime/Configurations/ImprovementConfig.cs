using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class ImprovementConfig : ScriptableObject
    {
        //[SerializeField] private AssetReferenceGameObject _prefabRef;
        [SerializeField] private string _name;
        [SerializeField][Min(1)] private int _price = 1;                //Цена
        [SerializeField][Min(1)] private int _percentModIncome = 1;     //Множитель дохода

        //public AssetReferenceGameObject prefabRef => _prefabRef;
        public string Name => _name;
        public int Price => _price;
        public int PercentModIncome => _percentModIncome;
        
    }
}