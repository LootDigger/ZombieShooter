using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    #region serializable fields

    [SerializeField]
    private Transform playerTransform;
    #endregion

    #region private fields

    private float cameraShooth = 5f;
    private Vector3 offset;
    #endregion

    #region Unity lifecycle

    void Start ()
    {
        offset = transform.position - playerTransform.position;
	}
	
	void FixedUpdate ()
    {
        Vector3 targetCamPos = playerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, cameraShooth * Time.deltaTime);
	}

    #endregion
}
