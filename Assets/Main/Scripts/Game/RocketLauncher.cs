using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private GameObject[] rockets;

    public void Launch()
    {
        if (rockets == null) return;
        for (int i = 0; i < rockets.Length; i++)
        {
            if (rockets[i] == null || !rockets[i].GetComponent<Rocket>()) continue;
            rockets[i].GetComponent<Rocket>().StartFly();
        }
        rockets = null;
    }
}
