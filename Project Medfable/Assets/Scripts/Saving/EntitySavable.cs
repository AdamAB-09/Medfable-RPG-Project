using Medfable.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Medfable.Saving
{
    [ExecuteAlways]
    public class EntitySavable : MonoBehaviour
    {
        [SerializeField]
        private string guid = "";

#if UNITY_EDITOR
        /*Update will only be executed when in Unity Editor - when the game is 
        * built, this code will not be executed. The GUID class variable is turned
        * into a serialized object in order to be saved into a file
        */
        private void Update()
        {
            /*No changes will be made when the game is running or in prefab window - changes
            * to GUID will only be done in editor mode
            */
            if (string.IsNullOrEmpty(gameObject.scene.path)) { return; }
            if (Application.IsPlaying(gameObject)) { return; }

            SerializedObject serializedObj = new SerializedObject(this);
            SerializedProperty property = serializedObj.FindProperty("guid");
            
            //If the GUID is empty then it will generate a GUID for the object in editor mode
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObj.ApplyModifiedProperties();
            }
        }
#endif

        //Gets the GUID of the savable entity
        public string GetGUID()
        {
            return guid;
        }

        //Allows the current object state to be saved
        public object CatchObjState()
        {
            return new SerializePosition(transform.position);
        }

        //Restores the prior state of the object
        public void RestoreObjState(object obj)
        {
            SerializePosition pos = (SerializePosition)obj;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = pos.GetVector3();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<InteractionScheduler>().CancelCurrentAction();
        }

    }
}
