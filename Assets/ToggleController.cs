using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ToggleController : MonoBehaviour
{
    // Funktio, joka k‰‰nt‰‰ FirstPersonControllerin tilan
    public void ToggleControllerState(bool enabled)
    {
        // Etsi FirstPersonController t‰m‰n GameObjectin lapsista
        FirstPersonController[] controllers = GetComponentsInChildren<FirstPersonController>();

        // K‰‰nn‰ kaikkien lˆydettyjen FirstPersonController-olioiden tila
        foreach (FirstPersonController controller in controllers)
        {
            controller.enabled = enabled;
        }
    }
}
