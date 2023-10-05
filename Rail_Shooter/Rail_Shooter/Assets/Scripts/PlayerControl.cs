using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;

    float xThrow;
    float yThrow;

    [SerializeField] float rotationFactor;
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float controlPitchFactor = 10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = 5f;

    [SerializeField] GameObject[] lasers;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }

    private void SetLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * -positionPitchFactor;
        float pitchDueToControlThrow = -yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * -controlRollFactor;

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);

        transform.localRotation = Quaternion.RotateTowards
            (transform.localRotation, targetRotation, rotationFactor);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
