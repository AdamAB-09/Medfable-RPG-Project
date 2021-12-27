using UnityEngine;

namespace Medfable.Combat
{
    /*This empty script is used to attach to an enemy target for the PlayerController and
      EntityCombat classes to reference.
      This idea is inspired by https://chandler-lane.medium.com/setting-up-combat-architecture-in-unity-996096620ef6
    */
    [RequireComponent(typeof(HealthSystem))]
    public class CombatTarget : MonoBehaviour
    {
 
    }
}
