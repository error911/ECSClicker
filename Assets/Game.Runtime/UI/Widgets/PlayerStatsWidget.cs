using TMPro;
using UnityEngine;

namespace Game
{
    public class PlayerStatsWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerMoney;

        public void SetMoney(int value)
        {
            _playerMoney.text = $"Баланс: {value.MoneyShort()}";
        }
    }
}
