using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float cannonBallForce = 50f;
    [SerializeField] float shootCooldown = 0.3f;

    //float xThrow;
    //float yThrow;

    //[SerializeField] float rotationFactor;
    //[SerializeField] float positionPitchFactor = 2f;
    //[SerializeField] float controlPitchFactor = 10f;
    //[SerializeField] float positionYawFactor = 2f;
    //[SerializeField] float controlRollFactor = 5f;

    [SerializeField] GameObject cannonBall;
    [SerializeField] Transform rightPosition;
    [SerializeField] Transform leftPosition;
    [SerializeField] Transform spawnAtRuntime;
    public bool canShoot = true;
    public bool playerAlive = true;

    void Update()
    {
        //ProcessTranslation();
        //ProcessRotation();
        if (Input.GetButton("Fire1"))
        {
            StartCoroutine(FireCannonBalls("Left"));
        }
        else if (Input.GetButton("Fire2"))
        {
            StartCoroutine(FireCannonBalls("Right"));
        }
    }

    private IEnumerator FireCannonBalls(string side)
    {
        if (canShoot && playerAlive)
        {
            canShoot = false;

            if (side == "Right")
            {
                GameObject cannonBallRight = Instantiate(cannonBall, rightPosition.position, rightPosition.rotation);
                cannonBallRight.transform.parent = spawnAtRuntime;
                cannonBallRight.GetComponent<Rigidbody>().velocity = Vector3.forward * cannonBallForce;
                Destroy(cannonBallRight, 3f);
            }
            if (side == "Left")
            {
                GameObject cannonBallLeft = Instantiate(cannonBall, leftPosition.position, leftPosition.rotation);
                cannonBallLeft.transform.parent = spawnAtRuntime;
                cannonBallLeft.GetComponent<Rigidbody>().velocity = Vector3.forward * cannonBallForce;
                Destroy(cannonBallLeft, 3f);
            }

            yield return new WaitForSeconds(shootCooldown);
            canShoot = true;
        }
    }

    //private void ProcessRotation()
    //
    //  float pitchDueToPosition = transform.localPosition.y * -positionPitchFactor;
    //  float pitchDueToControlThrow = -yThrow * controlPitchFactor;
    //  float pitch = pitchDueToPosition + pitchDueToControlThrow;
    //
    //  float yaw = transform.localPosition.x * positionYawFactor;
    //
    //  float roll = xThrow * -controlRollFactor;
    //
    //  Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
    //
    //    transform.localRotation = Quaternion.RotateTowards
    //        (transform.localRotation, targetRotation, rotationFactor);
    //}

    //private void ProcessTranslation()
    //{
    //    xThrow = Input.GetAxis("Horizontal");
    //    yThrow = Input.GetAxis("Vertical");
    //
    //    float xOffset = xThrow * Time.deltaTime * controlSpeed;
    //    float rawXPos = transform.localPosition.x + xOffset;
    //    float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
    //
    //    float yOffset = yThrow * Time.deltaTime * controlSpeed;
    //    float rawYPos = transform.localPosition.y + yOffset;
    //    float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
    //
    //    transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    //}
}
