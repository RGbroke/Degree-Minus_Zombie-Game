using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pivot : MonoBehaviour
{
    public GameObject myPlayer;
    [SerializeField] private FieldOfView fieldOfView;

    public void HandleAiming(Vector2 aim)
    {
        Vector3 difference = (Vector3.right * aim.x + Vector3.up * aim.y);
        difference.Normalize();
        Vector3 adj = Quaternion.Euler(0, 0, fieldOfView.fov) * difference;
        fieldOfView.SetAimDirection(adj);
        fieldOfView.SetOrigin(transform.position);

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        myPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);


        if (rotationZ < -90 || rotationZ > 90)
        {
            if (myPlayer.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
            else if (myPlayer.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            }
            myPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void HandleAiming()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        Vector3 adj = Quaternion.Euler(0, 0, fieldOfView.fov) * difference;
        fieldOfView.SetAimDirection(adj);
        fieldOfView.SetOrigin(transform.position);

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        myPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);


        if (rotationZ < -90 || rotationZ > 90)
        {
            if (myPlayer.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
            else if (myPlayer.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            }
            myPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
