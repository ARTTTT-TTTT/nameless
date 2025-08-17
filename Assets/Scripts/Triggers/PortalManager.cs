using UnityEngine;

namespace ART
{
    public class PortalManager : MonoBehaviour
    {
        public Vector3 portalOutPosition;

        private void Awake()
        {
            portalOutPosition = transform.GetChild(0).position;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.transform.position = portalOutPosition;
        }
    }
}