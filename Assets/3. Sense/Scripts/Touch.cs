using System;
using UnityEngine;

namespace Study
{
    public class Touch : Sense
    {
        private void OnTriggerEnter(Collider other)
        {
            var aspect = other.GetComponent<Aspect>();
            if (aspect != null)
            {
                if (aspect.aspectName == aspectName)
                {
                    Debug.Log("Enemy Touch Detected.");
                }
            }
        }
    }
}