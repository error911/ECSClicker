using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BuisenessWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBar m_progressBar;
        [SerializeField] private TMP_Text m_textName;
        [SerializeField] private TMP_Text m_textLevel;
        [SerializeField] private TMP_Text m_textIncome;
        [SerializeField] private TMP_Text m_texttBtnLevelup;
        [SerializeField] private TMP_Text m_texttBtnImprovement1;
        [SerializeField] private TMP_Text m_texttBtnImprovement2;
        
        [SerializeField] private TMP_Text m_texttBtnImp1;
        [SerializeField] private TMP_Text m_texttBtnImp2;
        
        [SerializeField] private Button m_btnLevelup;
        [SerializeField] private Button m_btnImprovement1;
        [SerializeField] private Button m_btnImprovement2;

        private string _nameImprovement1, _nameImprovement2;
        private int _percentModImprovement1, _percentModImprovement2;
        private int _level;
        private int _incoming;
        private int _lvlUpPrice;

        private void Start()
        {
            m_progressBar
                .SetMin(0)
                .SetMax(100)
                .SetValue(0);

            m_btnLevelup.interactable = false;
            m_btnImprovement1.interactable = false;
            m_btnImprovement2.interactable = false;
        }

        public void Initialize(BuisinessConfig config, int lvlUpPrice)
        {
            m_textName.text = config.Name;
            SetIncome(config.BaseIncome);
            SetLvlUpPrice(lvlUpPrice);
            SetLevel(0);
            SetProgress(0);

            _nameImprovement1 = config.Improvement1.Name;
            _nameImprovement2 = config.Improvement2.Name;
            _percentModImprovement1 = config.Improvement1.PercentModIncome;
            _percentModImprovement2 = config.Improvement2.PercentModIncome;

            m_texttBtnImprovement1.text = $"{_nameImprovement1}\r\nДоход: +{_percentModImprovement1}%\r\nЦена: {config.Improvement1.Price.MoneyShort()}";
            m_texttBtnImprovement2.text = $"{_nameImprovement2}\r\nДоход: +{_percentModImprovement2}%\r\nЦена: {config.Improvement2.Price.MoneyShort()}";
        }

        
        public void SetLevel(int value)
        {
            if (_level == value) return;
            _level = value;
            m_textLevel.text = $"LVL\r\n{value}";
        }

        public void SetIncome(int value)
        {
            if (_incoming == value) return;
            _incoming = value;
            m_textIncome.text = $"Доход\r\n{value.MoneyShort()}";
        }
        
        public void SetProgress(float valuePercent) => m_progressBar.SetValue(valuePercent);

        public void SetLvlUpPrice(int value)
        {
            if (_lvlUpPrice == value) return;
            _lvlUpPrice = value;
            m_texttBtnLevelup.text = $"LVL UP\r\nЦена: {value.MoneyShort()}";
        }

        public void ButtonLvlUpInteractable(bool value) => m_btnLevelup.interactable = value;
        public void ButtonImprovementInteractable1(bool value)
        {
            if (_impBuyed1)
            {
                m_btnImprovement1.interactable = true;
                m_btnImprovement1.enabled = false;

                return;
            }
            m_btnImprovement1.interactable = value;
        }
        public void ButtonImprovementInteractable2(bool value)
        {
            if (_impBuyed2)
            {
                m_btnImprovement2.interactable = true;
                m_btnImprovement2.enabled = false;
                return;
            }
            m_btnImprovement2.interactable = value;
        }


        public void OnClickButtonLvlUp(Action onClickAction)
        {
            m_btnLevelup.onClick.AddListener(() => onClickAction?.Invoke());
        }

        private bool _impBuyed1 = false;
        private bool _impBuyed2 = false;
        public void OnClickButtonImprovement1(Action onClickAction)
        {
            m_btnImprovement1.onClick.AddListener(
                () => { 
                    onClickAction?.Invoke();
                    m_texttBtnImprovement1.text = $"{_nameImprovement1}\r\nДоход: +{_percentModImprovement1}%\r\n<B>КУПЛЕНО</B>";
                    m_btnImprovement1.interactable = false;
                    _impBuyed1 = true;
                });
        }

        public void OnClickButtonImprovement2(Action onClickAction)
        {
            m_btnImprovement2.onClick.AddListener(
                () => {
                    onClickAction?.Invoke();
                    m_texttBtnImprovement2.text = $"{_nameImprovement2}\r\nДоход: +{_percentModImprovement2}%\r\n<B>КУПЛЕНО</B>";
                    m_btnImprovement2.interactable = false;
                    _impBuyed2 = true;
                });
        }

        /*private string MoneyToString()
        {

        }*/

    }
}
