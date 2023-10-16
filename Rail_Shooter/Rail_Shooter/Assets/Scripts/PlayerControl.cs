using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float cannonBallForce = 50f;
    [SerializeField] float shootCooldown = 0.3f;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 5f;

    float xThrow;

    [SerializeField] GameObject cannonBall;
    [SerializeField] Transform rightPosition;
    [SerializeField] Transform leftPosition;
    [SerializeField] Transform spawnAtRuntime;
    public bool canShoot = true;
    public bool playerAlive = true;

    void Update()
    {
        ProcessTranslation();
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

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
    }
}
