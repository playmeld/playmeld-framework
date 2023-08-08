using UnityEngine;
using UnityEngine.Events;

namespace Playmeld
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField]
        GameObject dialogueUI;

        [SerializeField]
        DialogueText dialogueText;

        [SerializeField]
        AudioSource audioSource;

        [SerializeField]
        Animator animator;

        UnityAction onFadeInComplete;
        UnityAction onFadeOutComplete;

        public bool isActive()
        {
            return dialogueUI.activeSelf;
        }

        public void handleFadeInComplete()
        {
            onFadeInComplete?.Invoke();
        }

        public void ShowDialogueWindow(UnityAction onNext)
        {
            onFadeInComplete = onNext;
            dialogueUI.SetActive(true);
            animator.SetTrigger("FadeIn");
        }

        public void handleFadeOutComplete()
        {
            onFadeOutComplete.Invoke();
            dialogueUI.SetActive(false);
        }

        public void HideDialogueWindow(UnityAction onNext)
        {
            onFadeOutComplete = onNext;
            dialogueText.ClearText();
            animator.SetTrigger("FadeOut");
        }

        public void Say(string text, AudioClip audioClip = null, UnityAction onNext = null)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }

            dialogueText.Write(
                text,
                onNext == null
                    ? onNext
                    : () =>
                    {
                        audioSource.Stop();
                        onNext?.Invoke();
                    }
            );
        }
    }
}
