using UnityEngine;

namespace ElicitIce {
    public class Example4Flyer : SmartMonoBehaviour {
        public Texture2D image {
            set { GetComponent<Renderer>().material.mainTexture = value; }
        }

        Vector3 rotationOffset;
        Transform lookAt;
        public void FixedUpdate() {
            _transform.LookAt(lookAt);
            _transform.Rotate(rotationOffset);
        }

        public void SetPositionAround(Transform lookat, Vector3 worldSpaceCenter, float distance, float yOffset, float angle, Vector3 rotOff) {

            float angleRadians = angle * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(
                distance * Mathf.Cos(angleRadians) + worldSpaceCenter.x,
                worldSpaceCenter.y + yOffset,
                distance * Mathf.Sin(angleRadians) + worldSpaceCenter.z);

            _transform.position = _transform.parent.TransformPoint(pos);

            rotationOffset = rotOff;
            lookAt = lookat;
        }
    }
}
