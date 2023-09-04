using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public ActivatePlatform1 eternalLifePlatform;
    public ActivatePlatform2 wasStaffPlatform;
    public ActivatePlatform3 osirisPlatform;
    public Animator doorLeftAnimator;
    public Animator doorRightAnimator;

    private void Start()
    {
        // Subscribe to the events for all three platforms
        eternalLifePlatform.OnColorChanged += CheckAllPlatforms;
        wasStaffPlatform.OnColorChanged += CheckAllPlatforms;
        osirisPlatform.OnColorChanged += CheckAllPlatforms;
    }

    private void CheckAllPlatforms()
    {
        // Check if all platforms are green
        if (eternalLifePlatform.IsGreen && wasStaffPlatform.IsGreen && osirisPlatform.IsGreen)
        {
            // Trigger animations on doors
            doorLeftAnimator.Play("DoorLeft");
            doorRightAnimator.Play("DoorRight");
        }
    }
}
