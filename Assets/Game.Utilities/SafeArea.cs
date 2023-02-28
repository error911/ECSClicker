using UnityEngine;

namespace Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class SafeArea : MonoBehaviour
    {
        [SerializeField]
        private SafeAreaMode _mode = SafeAreaMode.Default;

        private Rect _safeArea;

        private enum SafeAreaMode
        {
            Default,
            HorizontalSafe,
            VerticalSafe
        }
        
        private void UpdateRectTransform()
        {
            var rectTransform = (RectTransform) transform;
            var xMin = _mode == SafeAreaMode.VerticalSafe ? 0f : _safeArea.xMin / Screen.width;
            var xMax = _mode == SafeAreaMode.VerticalSafe ? 1f : _safeArea.xMax / Screen.width;

            var yMin = _mode == SafeAreaMode.HorizontalSafe ? 0f : _safeArea.yMin / Screen.height;
            var yMax = _mode == SafeAreaMode.HorizontalSafe ? 1f : _safeArea.yMax / Screen.height;

            rectTransform.anchorMin = new Vector2(xMin, yMin);
            rectTransform.anchorMax = new Vector2(xMax, yMax);
        }

        #region Unity

        private void Start()
        {
            _safeArea = Screen.safeArea;
            gameObject.SetActive(true);
            UpdateRectTransform();
        }

        private void OnRectTransformDimensionsChange()
        {
            UpdateRectTransform();
        }

        #endregion
        
       
    }
}