using System.Collections;
using UnityEngine;

namespace Medfable.SceneManagement

{
    public class FadeEffect : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float fadeOutTime = 1f;
        [SerializeField]
        private float fadeInTime = 1f;

        //Animates the scene with a fading out sequence for a certain duration
        public IEnumerator FadeOutScene()
        {
            animator.SetTrigger("fadeOut");
            yield return new WaitForSeconds(fadeOutTime);
        }

        //Animates the scene with a fading in sequence for a certain duration
        public IEnumerator FadeInScene()
        {
            animator.SetTrigger("fadeIn");
            yield return new WaitForSeconds(fadeInTime);
        }
    }
}
