using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Medfable.SceneManagement
{
    /* Idea of using animations for fade in/out between scenes inpsired by Brackey's video 
    * "How to Fade Between Scenes in Unity": https://www.youtube.com/watch?v=Oadq-IrOazg&t=240s
    */
    public class FadeEffect : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float fadeOutTime = 1f;
        [SerializeField]
        private float fadeInTime = 1f;
        [SerializeField]
        private float fadeInstantTime = 0.5f;
        private Image image;

        // Instantiate all the variables from the first frame and disable animator to avoid glitches
        private void Start()
        {
            animator.enabled = false;
            image = GetComponent<Image>();
        }

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

        //Instantly have a black fade effect for a duration
        public IEnumerator FadeOutInstant()
        {
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
            yield return new WaitForSeconds(fadeInstantTime);
        }

        //Re-enable the fade effects after fading out instantly previously
        public void EnableFade()
        {
            animator.enabled = true;
        }
    }
}
