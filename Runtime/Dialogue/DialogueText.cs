using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playmeld
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [RequireComponent(typeof(ITextAnimator))]
    [RequireComponent(typeof(Button))]
    public class DialogueText : MonoBehaviour
    {
        Button nextButton;
        ITextAnimator textAnimator;
        TextMeshProUGUI tmp;
        Coroutine animationCoroutine = null;
        string text;
        UnityAction onNext;

        void Awake()
        {
            nextButton = GetComponent<Button>();
            textAnimator = GetComponent(typeof(ITextAnimator)) as ITextAnimator;
            tmp = GetComponent<TextMeshProUGUI>();
            nextButton.onClick.AddListener(handleClick);
        }

        void OnDestroy()
        {
            nextButton.onClick.RemoveListener(handleClick);
        }

        public void ClearText()
        {
            tmp.text = "";
        }

        public void Write(string text, UnityAction onNext = null)
        {
            this.text = text;
            this.onNext = onNext;
            animationCoroutine = StartCoroutine(
                textAnimator.AnimateText(
                    text,
                    () =>
                    {
                        animationCoroutine = null;
                    }
                )
            );
        }

        private void handleClick()
        {
            bool isAnimationRunning = animationCoroutine != null;
            if (isAnimationRunning)
            {
                tmp.text = text;
                StopCoroutine(animationCoroutine);
                animationCoroutine = null;
                return;
            }

            onNext?.Invoke();
        }
    }
}
