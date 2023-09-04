using UnityEngine;

public class CannonBehavior : MonoBehaviour
{
    public Transform m_cannonRot;
    public Transform m_muzzle;
    public GameObject m_shotPrefab;

    private GameObject currentShot;
    private ActivateCannon activateCannon;

    private void Start()
    {
        activateCannon = FindObjectOfType<ActivateCannon>();
    }

    void Update()
    {
        if (activateCannon.IsPlayerOnCannon())
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (Input.GetMouseButton(0))
            {
                m_cannonRot.transform.Rotate(Vector3.up, mouseX * Time.deltaTime * 100f);
                m_cannonRot.transform.Rotate(Vector3.left, mouseY * Time.deltaTime * 100f);

                if (currentShot == null)
                {
                    currentShot = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_cannonRot.rotation) as GameObject;
                }
                else
                {
                    currentShot.transform.position = m_muzzle.position;
                    currentShot.transform.rotation = m_cannonRot.rotation;
                }
            }
            else
            {
                if (currentShot != null)
                {
                    GameObject.Destroy(currentShot);
                    currentShot = null;
                }
            }
        }
    }
}