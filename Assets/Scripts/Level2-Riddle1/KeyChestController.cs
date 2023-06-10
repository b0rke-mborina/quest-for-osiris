using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{

    public class KeyChestController : MonoBehaviour
    {
        private Animator ChestAnim;
        private bool chestOpen = false;

        [Header("Animation Names")]
        [SerializeField] private string openAnimationName = "ChestOpen";
        [SerializeField] private string closeAnimationName = "ChestClose";

        [SerializeField] private Animator boatAnimator = null;
        //private Animator BoatAnim;
        [SerializeField] private string goAnimationName = "goBoat";

        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showChestLockedUI = null;

        [SerializeField] private KeyInventory _keyInventory = null;

        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;

        private void Awake()
        {
            ChestAnim = gameObject.GetComponent<Animator>();
        }

        private IEnumerator PauseChestInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }

        private IEnumerator PlayNextAnimation()
        {
            yield return new WaitForSeconds(ChestAnim.GetCurrentAnimatorStateInfo(0).length);
            boatAnimator.Play(goAnimationName);
        }

        public void PlayAnimation()
        {
            if(_keyInventory.hasKey01)
            {
                //OpenChest();
                if (!chestOpen && !pauseInteraction)
                {
                    ChestAnim.Play(openAnimationName, 0, 0.0f);
                    chestOpen = true;
                    StartCoroutine(PauseChestInteraction());
                    StartCoroutine(PlayNextAnimation());

                }

                else if (chestOpen && !pauseInteraction)
                {
                    ChestAnim.Play(closeAnimationName, 0, 0.0f);
                    chestOpen = false;
                    StartCoroutine(PauseChestInteraction());
                }
            }

          /*  else if(_keyInventory.hasKey02)
            {
                OpenChest();
            }*/
            else
            {
                StartCoroutine(ShowChestLocked());
            }
        }


        IEnumerator ShowChestLocked()
        {
            showChestLockedUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUI);
            showChestLockedUI.SetActive(false);
        }


    }
}
