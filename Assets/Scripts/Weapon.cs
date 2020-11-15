using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject fireball;
    public Transform firepoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        playerController = new PlayerController();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.shoot == true)
        {
            shoot();
            playerController.shoot = false;
        }
    }

    private void shoot()
    {
        Debug.Log("Shoot");
        Instantiate(fireball, firepoint.position, firepoint.rotation);
    }
}
