using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Slider))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider m_Slider;
        [SerializeField] private float _lerpTime = 0.5f;

        private void Start()
        {
            SetMin(0);
            SetMax(100);
        }

        public ProgressBar SetMin(float min)
        {
            m_Slider.minValue = min;
            m_Slider.value = min;
            return this;
        }

        public ProgressBar SetMax(float max)
        {
            m_Slider.maxValue = max;
            return this;
        }

        public void SetValue(float value)
        {
            m_Slider.value = value;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_Slider == null)
                m_Slider = GetComponent<Slider>();
        }
#endif

    }
}
