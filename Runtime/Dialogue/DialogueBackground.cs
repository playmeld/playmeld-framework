using UnityEngine;
using UnityEngine.Events;

namespace Playmeld
{
    public class DialogueBackground : MonoBehaviour
    {
        [SerializeField]
        UnityEvent onFadeInComplete;

        [SerializeField]
        UnityEvent onFadeOutComplete;

        public void handleFadeInComplete()
        {
            onFadeInComplete?.Invoke();
        }

        public void handleFadeOutComplete()
        {
            onFadeOutComplete?.Invoke();
        }
    }
}
