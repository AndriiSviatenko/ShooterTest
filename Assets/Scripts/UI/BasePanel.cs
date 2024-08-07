using DG.Tweening;
using Interfaces;
using System;
using UnityEngine;

namespace UI
{
    public class BasePanel : MonoBehaviour, IHideneable
    {
        public event Action ShowCompleteEvent;
        public event Action HideCompleteEvent;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float duration = 0.2f;

        public virtual void Show()
        {
            canvasGroup.DOFade(1, duration).onComplete +=
                () => ShowCompleteEvent?.Invoke();

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.ignoreParentGroups = true;
        }
        public virtual void Hide()
        {
            canvasGroup.DOFade(0, duration).onComplete +=
                () => HideCompleteEvent?.Invoke();

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.ignoreParentGroups = false;
        }
    }
}

