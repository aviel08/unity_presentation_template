using UnityEngine;
using System.Collections;

public class rotation_back_and_forth : MonoBehaviour {

		public float _Angle;
		public float _Period;
		
		private float _Time;
		
		// Update is called once per frame
		void Update () {
			_Time = _Time + Time.deltaTime;
			float phase = Mathf.Sin(_Time / _Period);
			transform.localRotation = Quaternion.Euler( new Vector3(0, phase * _Angle, 0));
		}
	}