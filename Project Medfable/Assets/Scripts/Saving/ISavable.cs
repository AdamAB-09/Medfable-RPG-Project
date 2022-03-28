namespace Medfable.Saving
{
    public interface ISavable
    {
        //Captures all the data for the object in order to be saved (health, position etc)
        object CatchObjAttributes();
        //Restores the data when loading the object to whenever the user last saved
        void RestoreObjAttributes(object obj);
    }
}
