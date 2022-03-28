using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Medfable.Saving
{
    [ExecuteAlways]
    public class EntitySavable : MonoBehaviour
    {
        static Dictionary<string, EntitySavable> dictGuids = new Dictionary<string, EntitySavable>();

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
            
            //If the GUID is empty or taken then it will generate a GUID for the object in editor mode
            if (string.IsNullOrEmpty(property.stringValue) || GuidTaken(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObj.ApplyModifiedProperties();
            }
            dictGuids[property.stringValue] = this;
        }

        /*Checks whether a GUID doesn't exist in the dictionary or belong to this entity,
        * otherwise the GUID is taken already
        */
        private bool GuidTaken(string entityGuid) 
        {
            if (!dictGuids.ContainsKey(entityGuid)) { return false; }
            if (dictGuids[entityGuid] == this) { return false; }
            return true;
        }

        //Whenever the savable entity is destroyed, its GUID is removed from the dictionary
        private void OnDestroy()
        {
            dictGuids.Remove(guid);
        }
#endif

        //Gets the GUID of the savable entity
        public string GetGuid()
        {
            return guid;
        }

        //Allows the current object state to be saved
        public Dictionary<string, object> CatchObjState()
        {
            Dictionary<string, object> gameState = new Dictionary<string, object>();
            foreach (ISavable savable in GetComponents<ISavable>())
            {
                gameState[savable.GetType().ToString()] = savable.CatchObjAttributes();
            }
            return gameState;
        }

        //Restores the prior state of the object
        public void GetObjState(object gameState)
        {
            Dictionary<string, object> dictState = (Dictionary<string, object>)gameState;
            foreach (ISavable savable in GetComponents<ISavable>())
            {
                string savableType = savable.GetType().ToString();
                if (dictState.ContainsKey(savableType))
                {
                    savable.RestoreObjAttributes(dictState[savableType]);
                }
            }
        }

    }
}
