using ModularForge.Trailback.Core;
using ModularForge.Trailback.Editor.Styling;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Displays Trailback's runtime diagnostics while the application is running.
    /// </summary>
    public class TrailbackDebuggerWindow : EditorWindow
    {
        #region Fields

        private NavigationView _navigationView;
        private HistoryView _historyView;
        private StatisticsView _statisticsView;

        private Button _refreshButton;

        private const string WindowTitle = "Trailback Debugger";
        
        #endregion


        #region Initialization

        /// <summary>
        /// Opens the Trailback Debugger window.
        /// </summary>
        [MenuItem("Tools/Trailback/Trailback Debugger")]
        public static void ShowWindow()
        {
            var window = GetWindow<TrailbackDebuggerWindow>();

            window.titleContent = new GUIContent(WindowTitle);
            window.minSize = new Vector2(500, 600);
        }

        /// <summary>
        /// Builds and initializes the Trailback Debugger user interface.
        /// </summary>
        private void CreateGUI()
        {
            TrailbackEditorUIStyles.ApplyRoot(rootVisualElement);

            CreateToolbar();
            CreateContent();
            RefreshState();
        }

        /// <summary>
        /// Creates the Runtime Monitor toolbar and refresh controls.
        /// </summary>
        private void CreateToolbar()
        {
            var toolbar = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    marginBottom = 10
                }
            };

            var toolbarTitle = new Label(WindowTitle)
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 16,
                    flexGrow = 1
                }
            };

            _refreshButton = new Button(RefreshButtonClicked)
            {
                text = "Refresh"
            };

            toolbar.Add(toolbarTitle);
            toolbar.Add(_refreshButton);

            rootVisualElement.Add(toolbar);
            rootVisualElement.Add(TrailbackEditorUIStyles.CreateDivider(5));
        }

        /// <summary>
        /// Creates the main Runtime Monitor layout and its child views.
        /// </summary>
        private void CreateContent()
        {
            _navigationView = new NavigationView();
            _statisticsView = new StatisticsView();
            _historyView = new HistoryView();

            var leftPanel = CreateLeftPanel();
            var rightPanel = CreateRightPanel();

            var splitView = new TwoPaneSplitView(
                0,
                200,
                TwoPaneSplitViewOrientation.Horizontal);

            splitView.viewDataKey = "Trailback.RuntimeMonitor.SplitView";
            splitView.style.flexGrow = 1;
            splitView.Add(leftPanel);
            splitView.Add(rightPanel);

            rootVisualElement.Add(splitView);
        }

        /// <summary>
        /// Creates the left panel containing the Navigation and Statistics views.
        /// </summary>
        private VisualElement CreateLeftPanel()
        {
            var panel = new VisualElement
            {
                style =
                {
                    flexGrow = 1,
                    minWidth = 200,
                    paddingLeft = 8,
                    paddingRight = 8,
                    paddingTop = 4,
                    paddingBottom = 4
                }
            };

            panel.Add(_navigationView);
            panel.Add(TrailbackEditorUIStyles.CreateDivider(3));
            panel.Add(_statisticsView);

            return panel;
        }

        /// <summary>
        /// Creates the right panel containing the History view.
        /// </summary>
        private VisualElement CreateRightPanel()
        {
            var panel = new VisualElement
            {
                style =
                {
                    flexGrow = 1,
                    minWidth = 280,
                    paddingLeft = 8,
                    paddingRight = 8,
                    paddingTop = 7,
                    marginTop = 7,
                    paddingBottom = 4
                }
            };

            panel.Add(_historyView);

            return panel;
        }

        #endregion


        #region Unity Events

        /// <summary>
        /// Subscribes to Trailback state updates when the window is enabled.
        /// </summary>
        private void OnEnable()
        {
            TrailbackFacade.OnStateChanged += Refresh;

            RefreshState();
        }

        /// <summary>
        /// Unsubscribes from Trailback state updates when the window is disabled.
        /// </summary>
        private void OnDisable()
        {
            TrailbackFacade.OnStateChanged -= Refresh;
        }

        /// <summary>
        /// Refreshes the Trailback Debugger when the window gains focus.
        /// </summary>
        private void OnFocus()
        {
            RefreshState();
        }

        #endregion


        #region State Management

        /// <summary>
        /// Updates all Trailback Debugger views using the supplied Trailback state.
        /// </summary>
        /// <param name="state">
        /// The current Trailback runtime state.
        /// </param>
        private void Refresh(TrailbackState state)
        {
            if (_navigationView == null ||
                _historyView == null ||
                _statisticsView == null)
            {
                return;
            }

            _navigationView.UpdateState(state);
            _historyView.UpdateState(state);
            _statisticsView.UpdateState(state);
        }

        /// <summary>
        /// Refreshes the Trailback Debugger using the latest runtime state.
        /// </summary>
        private void RefreshButtonClicked()
        {
            RefreshState();
        }

        /// <summary>
        /// Retrieves the latest Trailback state and refreshes the Trailback Debugger.
        /// </summary>
        private void RefreshState()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            Refresh(TrailbackFacade.GetState());
        }

        #endregion
    }
}