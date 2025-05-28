using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FootstepSound : MonoBehaviour
{
    [SerializeField] private SoundController soundController;
    [SerializeField] private float stepDistance = 0.5f;

    private CharacterController characterController;
    private float accumulatedDistance;
    private Vector3 lastPosition;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Confined;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.01f)
        {
            accumulatedDistance += Vector3.Distance(transform.position, lastPosition);

            if (accumulatedDistance >= stepDistance)
            {
                soundController.PlaySound(0, soundController.Volume);
                //Debug.Log("Footstep sound played");
                accumulatedDistance = 0f;
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
        lastPosition = transform.position;
    }
}
