namespace UralHedgehog
{
    namespace UI
    {
        public class UIManager
        {
            private readonly UIRoot _uiRoot;
            
            public UIManager(UIRoot uiRoot)
            {
                _uiRoot = uiRoot;
            }
            
            public void OpenViewMainPanel()
            {
                _uiRoot.Create<IEmptyWidget>(nameof(ViewMainPanel), null);
            }
            
            public void OpenViewTopPanel(ITopPanel topPanel)
            {
                _uiRoot.Create(nameof(ViewTopPanel), topPanel);
            }
            
            public void OpenViewLoseWinWindow()
            {
                _uiRoot.Kill(nameof(ViewTopPanel));
                _uiRoot.Create<IEmptyWidget>(nameof(ViewLoseWinWindow), null);
            }
        }
    }
}