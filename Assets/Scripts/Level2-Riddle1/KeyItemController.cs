using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{

    public class KeyItemController : MonoBehaviour
    {
        [SerializeField] private bool chest = false;
        [SerializeField] private bool key01 = false;


        [SerializeField] private KeyInventory _keyInventory = null;

        private KeyChestController chestObject;


        private void Start()    
        {
            if (chest)
            {
                chestObject = GetComponent<KeyChestController>();
            }
        }

        public void ObjectInteraction()
        {
            if (chest)
            {
                chestObject.PlayAnimation();
            }
            else if (key01)
            {
                _keyInventory.hasKey01 = true;
                gameObject.SetActive(false);
            }
        }
    }
}
