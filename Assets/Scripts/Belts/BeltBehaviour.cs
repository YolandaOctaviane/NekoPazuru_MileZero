using UnityEngine;

namespace Belts
{
    public abstract class BeltBehaviour : MonoBehaviour
    {
        // Update is called once per frame
        protected virtual void Update()
        {
            DetectItems();
        }
        
        /*
         * <summery>
         * detect for any items on top of the belt,
         * if there is, move it.
         * </summery>
         */
        protected abstract void DetectItems();
    }
}
