using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Slider))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider m_Slider;
        [SerializeField] private float _lerpTime = 0.5f;

        private float _prevValue;
        private float _currentValue;

        private void Start()
        {
            SetMin(0);
            SetMax(100);
        }

        public ProgressBar SetMin(float min)
        {
            m_Slider.minValue = min;
            m_Slider.value = min;
            _prevValue = min;
            return this;
        }

        public ProgressBar SetMax(float max)
        {
            m_Slider.maxValue = max;
            return this;
        }

        private Coroutine _coroutine;
        public void SetValue(float value)
        {
//            if (_coroutine != null) StopCoroutine(_coroutine);
//            _currentValue = value;
            m_Slider.value = value;
//            _coroutine = StartCoroutine(LerpSlide());
        }

        /*
        private IEnumerator LerpSlide()
        {
            _prevValue = m_Slider.value;
            float t = 0;
            while (m_Slider.value < _currentValue)
            {
                var dt = Time.deltaTime;
                t += dt * _lerpTime;
                m_Slider.value = Mathf.Lerp(_prevValue, _currentValue, t);

                yield return new WaitForEndOfFrame();
            }
            m_Slider.value = _currentValue;
        }
        */

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_Slider == null)
                m_Slider = GetComponent<Slider>();
        }
#endif

    }
}
