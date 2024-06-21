using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestDeplacement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject Flamme;
    [SerializeField] private GameObject Fusee;


    [SerializeField] private float stopDistance; // Distance à laquelle la fusée s'arrête
    private CharacterController controller;
    private Vector3 targetPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
        }

        Vector3 direction = (targetPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            controller.Move(direction * speed * Time.deltaTime);
            Flamme.SetActive(true);
        }
        else if (Vector3.Distance(transform.position, targetPosition) < stopDistance)
        {
            Flamme.SetActive(false);
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);


       
    }




    


}

