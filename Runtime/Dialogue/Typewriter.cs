using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Playmeld
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Typewriter : MonoBehaviour, ITextAnimator
    {
        [SerializeField] float typeWriterDelay;
        TextMeshProUGUI tmp;

        void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        public IEnumerator AnimateText(string text, UnityAction onAnimationCompleted)
        {
            var DelayBtwChars = new WaitForSeconds(typeWriterDelay);
            tmp.text = "";
            foreach (char c in text)
            {
                tmp.text += c;
                yield return DelayBtwChars;
            }
            onAnimationCompleted.Invoke();
        }
    }
}