using Patterns.EventBus;
using Patterns.EventBus.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class Dead : BasePanel
    {
        [SerializeField] private Button restartBtn;
        private void Awake()
        {
            restartBtn.onClick.AddListener(Restart);
        }
        private void OnDestroy()
        {
            restartBtn.onClick.RemoveListener(Restart);
        }
        public override void Show()
        {
            base.Show();
            EventBus.Instance?.Invoke(new Stop());
        }
        private void Restart()
        {
            EventBus.Instance?.Invoke(new Restart());
        }
    }
}

