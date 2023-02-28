using UnityEngine;

namespace Game.Services.UI
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

//        private void Awake()
//        {
//            _safeArea = Screen.safeArea;

//            UpdateRectTransform();
//            gameObject.SetActive(false);
//        }

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

        /*
        private void FixedUpdate()
        {
            if (_safeArea == Screen.safeArea)
            {
                return;
            }

            _safeArea = Screen.safeArea;

            UpdateRectTransform();
        }
        */

        #endregion
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        /*
        private Rect _safeArea;
        private RectTransform rectTransform;

        private CanvasScaler canvasScaler;
        private float bottomUnits, topUnits;
        
        private void UpdateRectTransform()
        {
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
//            if (canvasScaler == null) canvasScaler = transform.parent.GetComponent<CanvasScaler>();
//            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottomUnits);
//            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -topUnits);
            //rectTransform.anchorMin = new Vector2(_safeArea.xMin / Screen.width, _safeArea.yMin / Screen.height);
            //rectTransform.anchorMax = new Vector2(_safeArea.xMax / Screen.width, _safeArea.yMax / Screen.height);
            rectTransform.anchorMin = new Vector2(Screen.safeArea.xMin / Screen.width, Screen.safeArea.yMin / Screen.height);
            rectTransform.anchorMax = new Vector2(Screen.safeArea.xMax / Screen.width, Screen.safeArea.yMax / Screen.height);
            rectTransform.ForceUpdateRectTransforms();
            //rectTransform.hasChanged = true;    //
            //rectTransform.ForceUpdateRectTransforms();  //
        }

        #region Unity

        private bool _initialized = false;
        private void Start()
        {
            //_safeArea = Screen.safeArea;
//            rectTransform = (RectTransform) transform;
            //rectTransform.hasChanged = true;    //
//            rectTransform.ForceUpdateRectTransforms();  //

            _initialized = true;
            UpdateRectTransform();
        }
        
        private void OnRectTransformDimensionsChange()
        {
            if (!_initialized)
                return;
            
            UpdateRectTransform();
        }

        #endregion
        */
        
        
        
        
    }
}