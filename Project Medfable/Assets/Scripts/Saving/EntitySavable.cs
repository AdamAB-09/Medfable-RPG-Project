using UnityEngine;

namespace Medfable.Saving
{
    public class EntitySavable : MonoBehaviour
    {
        //Gets the GUID of the savable entity
        public string GetGUID()
        {
            return "";
        }

        //Allows the current object state to be saved
        public object CatchObjState()
        {
            print("Capturing state for: " + GetGUID());
            return null;
        }

        //Restores the prior state of the object
        public void RestoreGameObj(object obj)
        {
            print("Restoring state for: " + GetGUID());
        }

    }
}
