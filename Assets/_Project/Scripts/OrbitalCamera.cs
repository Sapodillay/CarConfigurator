using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class OrbitalCamera : MonoBehaviour
{

    //https://stackoverflow.com/a/21646259
    //https://gist.github.com/3dln/c16d000b174f7ccf6df9a1cb0cef7f80



    [SerializeField] Transform target;
    [SerializeField] float _distance = 10.0f;
    [SerializeField] float _minDistance;

    private float _lastDistance;

    [SerializeField] float _xSpeed = 100f;
    [SerializeField] float _ySpeed = 100f;

    private float x = 0.0f;
    private float y = 0.0f;

    private void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }



    private void Update()
    {
        UpdateCamera();
    }



    private bool isOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    private void UpdateCamera()
    {
        //Clamp distance
        if (_distance < _minDistance) _distance = _minDistance;

        //Mouse scroll zoom.
        _distance -= Input.GetAxis("Mouse ScrollWheel") * 2f;



        if (target && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !isOverUI())
        {
            Vector2 mousePosition = Input.mousePosition;

            //Lock and hide cursor when dragging.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            x += Input.GetAxis("Mouse X") * _xSpeed * 0.05f;
            y -= Input.GetAxis("Mouse Y") * _ySpeed * 0.05f;

            ApplyTransform();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        if (_lastDistance != _distance)
        {
            ApplyTransform();
        }
        _lastDistance = _distance;

    }



    private void ApplyTransform()
    {
        //Calculate rotation and position.
        var rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * new Vector3(0.0f, 0.0f, -_distance) + target.transform.position;

        //Apply position and rotation to camera.
        transform.rotation = rotation;
        transform.position = position;
    }


}