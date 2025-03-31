using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class RocketLaunch:MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float force = 20f, duration = 5;
    [SerializeField] private bool isRandomFlight = false;
    [SerializeField] private ParticleSystem fire, explore;
    private bool cal = true;
    private int xRand = 0, zRand = 0;
    private void Start()
    {
        if (isRandomFlight)
        {
            var RndB = new System.Random();
            var xRand = RndB.Next(1, 10);
            var zRand = RndB.Next(1, 10);
        }
    }
    public void StartPolet()
    {
        StartCoroutine(DelayedAction());
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
    }

    IEnumerator DelayedAction()
    {
        Flying();
        yield return new WaitForSeconds(duration);
        cal = false;
        fire.Stop();
        Explore();
        yield break;
    }
    IEnumerator FlyRocket()
    {
        while (cal)
        {
            yield return new WaitForSeconds(0.2f);
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
