using System.Collections;
using UnityEngine;

public class RocketLaunch:MonoBehaviour
{
    [SerializeField] private GameObject lightPointExplore;
    [SerializeField] private GameObject lightPointFly;
    [SerializeField] private Rigidbody body;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float force = 20f, duration = 5;
    [SerializeField] private bool isRandomFlight = false;
    [SerializeField] private ParticleSystem fire, explore;
    private bool flag = true;
    private int xRand = 0, zRand = 0;

    private void Start()
    {
        if (isRandomFlight)
        {
            var xRand = Random.Range(-50000,50000);
            var zRand = Random.Range(-50000, 50000);
        }
    }

    public void StartFly()
    {
        StartCoroutine(DelayedAction());
        lightPointFly.SetActive(true);
    }

    private void Flying()
    {
        fire.Play();
        Debug.Log("Полёт");
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
                xRand = Random.Range(-2, 2);
                zRand = Random.Range(-2, 2);
            }
            body.AddForce((new Vector3(xRand, force, zRand)), ForceMode.Impulse);
        }
        yield break;
    }

    IEnumerator Boom()
    {
        explore.Play();
        body.useGravity = false;
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(explore.main.startLifetimeMultiplier);
        Destroy(gameObject);
    }

}
