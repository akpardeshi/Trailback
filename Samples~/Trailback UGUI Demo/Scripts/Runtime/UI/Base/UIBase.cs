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

        public virtual void Show()
        {
            ManageCanvas(true);
            ManageCanvasGroup(true);

            SelectInitialButton();
        }

        public virtual void Hide()
        {
            ManageCanvas(false);
            ManageCanvasGroup(false);

            SelectPreviousButton();
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
            if (_previousSelection == null)
            {
                return;
            }

            EventSystem.current.SetSelectedGameObject(_previousSelection);
        }
    }
}