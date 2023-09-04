using UnityEngine;
using System.Collections;

public class ChestLocked : MonoBehaviour
{
    public GameObject chest02;
    [SerializeField] private int timeToShowUI = 1;
    [SerializeField] private GameObject showChestLockedUI = null;

    public IEnumerator ShowChestLocked()
    {
        showChestLockedUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        showChestLockedUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chest02Trigger"))
        {
            StartCoroutine(ShowChestLocked());
        }
    }
}
