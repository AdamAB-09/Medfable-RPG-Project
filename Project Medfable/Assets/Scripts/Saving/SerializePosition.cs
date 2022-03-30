using UnityEngine;

namespace Medfable.Saving
{
    [System.Serializable]
    public class SerializePosition
    {
        private float[] position;

        // Constructor in order to serialize a position of an object
        public SerializePosition(Vector3 nonSerializedPos)
        {
            position = new float[3];
            position[0] = nonSerializedPos.x;
            position[1] = nonSerializedPos.y;
            position[2] = nonSerializedPos.z;
        }
          
        // Turns the serialized position back into an original Vector3 position
        public Vector3 GetVector3()
        {
            float x = position[0];
            float y = position[1];
            float z = position[2];
            return new Vector3(x, y, z);
        }


    }
}
