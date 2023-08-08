using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Playmeld
{
    public interface ITextAnimator
    {
        public IEnumerator AnimateText(string text, UnityAction onAnimationCompleted);
    }
}