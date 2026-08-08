using ModularForge.Trailback.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModularForge.Trailback.Demo.UI
{
    public class UIBase : MonoBehaviour, IBackNavigable
    {
        private Canvas _canvas;
        
        private CanvasGroup _canvasGroup;

        [SerializeField] protected Selectable initialGameObject;

        private GameObject _previousSelection;

        protected virtual void Awake()
        {
            CacheComponents();
        }

        private void CacheComponents()
        {
            TryGetComponent(out _canvas);
            TryGetComponent(out _canvasGroup);
        }

        /// <summary>
        /// Makes the UI element visible.
        ///
        /// This method manages the visibility lifecycle and invokes <see cref="OnShown"/>
        /// after the element successfully becomes visible.
        /// </summary>
        public void Show()
        {
            if (IsVisible())
            {
                return;
            }
            
            ManageCanvas(true);
            ManageCanvasGroup(true);

            SelectInitialButton();
            
            OnShown();
        }

        /// <summary>
        /// Hides the UI element.
        ///
        /// This method manages the visibility lifecycle and invokes <see cref="OnHidden"/>
        /// after the element successfully becomes hidden.
        /// </summary>
        public void Hide()
        {
            if (!IsVisible())
            {
                return;
            }
            
            ManageCanvas(false);
            ManageCanvasGroup(false);

            SelectPreviousButton();
            
            OnHidden();
        }
        
        [field: SerializeField] public NavigationCategorySo NavigationCategory { get; private set; }

        private void ManageCanvasGroup(bool isActive)
        {
            _canvas.enabled = isActive;
        }

        private void ManageCanvas(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.blocksRaycasts = isActive;
            _canvasGroup.interactable = isActive;
        }

        private void SelectInitialButton()
        {
            if (!initialGameObject)
            {
                return;
            }

            _previousSelection = EventSystem.current.currentSelectedGameObject;

            EventSystem.current.SetSelectedGameObject(initialGameObject.gameObject);
        }

        private void SelectPreviousButton()
        {
            if (!_previousSelection)
            {
                return;
            }

            EventSystem.current.SetSelectedGameObject(_previousSelection);
        }
        
        private bool IsVisible()
        {
            return _canvas.enabled; 
        }

        /// <summary>
        /// Called after the UI element successfully becomes visible.
        /// Override this method to perform initialization or other logic
        /// that should run after the element is shown.
        /// </summary>
        protected virtual void OnShown()
        {
            
        }

        /// <summary>
        /// Called after the UI element successfully becomes hidden.
        /// Override this method to perform cleanup or other logic
        /// that should run after the element is hidden.
        /// </summary>
        protected virtual void OnHidden()
        {
            
        }
    }
}