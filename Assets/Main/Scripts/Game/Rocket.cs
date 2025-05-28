using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class Rocket:MonoBehaviour
{
    [SerializeField] private GameObject lightPointExplore;
    [SerializeField] private GameObject lightPointFly;
    [SerializeField] private float force = 20f, duration = 5;
    [SerializeField] private bool isRandomFlight = false;
    [SerializeField] private ParticleSystem fire, explore;

    private Rigidbody rb;
    private SoundController soundController;
    private bool flag = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        soundController = GetComponent<SoundController>();
    }

    public void StartFly()
    {
        StartCoroutine(DelayedAction());
        rb.useGravity = false;
        rb.isKinematic = false;
        soundController.PlaySound(0, volume:soundController.Volume);
        lightPointFly.SetActive(true);
    }

    private void Flying()
    {
        fire.Play();
        StartCoroutine(FlyRocket());
    }

    private void Explore()
    {
        StartCoroutine(Boom());
        lightPointFly.SetActive(false);
        lightPointExplore.SetActive(true);
    }

    IEnumerator DelayedAction()
    {
        Flying();
        yield return new WaitForSeconds(duration);
        flag = false;
        fire.Stop();
        Explore();
        yield break;
    }

    IEnumerator FlyRocket()
    {
        while (flag)
        {
            yield return new WaitForSeconds(0.2f);
            int xRand = 0, zRand = 0;
            if (isRandomFlight)
            {
                xRand = Random.Range(-6, 6);
                zRand = Random.Range(-6, 6);
            }
            rb.AddForce((new Vector3(xRand, force, zRand)), ForceMode.Impulse);
        }
        yield break;
    }

    IEnumerator Boom()
    {
        explore.Play();
        soundController.PlaySound(1, isDestroyed: true, volume: 0.3f, isGlobal:true);
        yield return new WaitForSeconds(explore.main.startLifetimeMultiplier);
        Destroy(gameObject);
    }

}
