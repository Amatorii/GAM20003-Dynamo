using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hamish.player
{
    public class KeneticCamera : MonoBehaviour
    {
        [Range(0, 25)][SerializeField]private float smoothTime;
        private Vector3 velocity = Vector3.zero;
        [SerializeField] private Transform target;
        private Vector3 lastPosition;

        void Start()
        {
            transform.position = target.position;
            lastPosition = target.position;
        }

        void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
            //transform.position = Vector3.Lerp(transform.position, target.position, smoothTime*Time.deltaTime);
            lastPosition = target.position;
        }
    }
}