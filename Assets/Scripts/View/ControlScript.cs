using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (VirtualUIFunctions.IsInTouchState && !VirtualUIFunctions.IsInSecondFingerTouchState && !VirtualUIFunctions.ClickUp)
        {

            if (VirtualUIFunctions.IsModal)
            {
                return;
            }

            if (VirtualUIFunctions.FirstFingerObjectTag != "") return;

            //Debug.Log("HERE");
            float xCenter = Screen.width / 2f;
            float yCenter = Screen.height / 2f;

            float xDistance = VirtualUIFunctions.FirstFingerCurrentPosition.x - xCenter;
            float yDistance = VirtualUIFunctions.FirstFingerCurrentPosition.y - yCenter;

            Quaternion Bueler = Camera.main.transform.rotation;
            //Vector3 Bueler2 = new Vector3(0f,0f,0f);
            //Bueler2.x = yDistance * 0.25f;
            //Bueler2.y = -1f * xDistance * 0.25f;


            Quaternion Bueler2 = Quaternion.Euler(-yDistance * 0.25f, xDistance * 0.25f, 0f);
            if (Bueler2.x > 0.5f) Bueler2.x = 0.5f;
            if (Bueler2.y > 0.5f) Bueler2.y = 0.5f;
            if (Bueler2.z > 0.5f) Bueler2.z = 0.5f;
            if (Bueler2.x < -0.5f) Bueler2.x = -0.5f;
            if (Bueler2.y < -0.5f) Bueler2.y = -0.5f;
            if (Bueler2.z < -0.5f) Bueler2.z = -0.5f;

            Camera.main.transform.localEulerAngles = Quaternion.Lerp(Bueler, Bueler2, Time.deltaTime * 2f).eulerAngles;
        }
        else if (VirtualUIFunctions.IsInSecondFingerTouchState && !VirtualUIFunctions.ClickUp && !VirtualUIFunctions.ClickUpSecond)
        {
            float xDistanceStart = VirtualUIFunctions.SecondFingerStartPosition.x - VirtualUIFunctions.FirstFingerStartPosition.x;
            float yDistanceStart = VirtualUIFunctions.SecondFingerStartPosition.y - VirtualUIFunctions.FirstFingerStartPosition.y;
            float xDistanceCurrent = VirtualUIFunctions.SecondFingerCurrentPosition.x - VirtualUIFunctions.FirstFingerCurrentPosition.x;
            float yDistanceCurrent = VirtualUIFunctions.SecondFingerCurrentPosition.y - VirtualUIFunctions.FirstFingerCurrentPosition.y;
            float distanceStart = Mathf.Sqrt(xDistanceStart * xDistanceStart + yDistanceStart * yDistanceStart);
            float distanceCurrent = Mathf.Sqrt(xDistanceCurrent * xDistanceCurrent + yDistanceCurrent * yDistanceCurrent);
            float deltaDistance = (distanceCurrent - distanceStart);

            Camera.main.fieldOfView -= (deltaDistance / 100.0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(Vector3.forward * 1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(Vector3.left * 1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.Translate(Vector3.back * 1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(Vector3.right * 1f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Camera.main.transform.Translate(Vector3.up * 1f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Camera.main.transform.Translate(Vector3.down * 1f);
        }
    }
}
