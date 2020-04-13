using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ItemHolder : CustomInteractible
{
    /*this item holder needs to be capable of the following 
     * 
     * When an item is dropped in the bounds of this object it is "holstered" 
     * any item dropped in the bounds of this object can be picked up later 
     * piking up an item from this object can either be one time or instantiate a new object
     * 
     * to do this I will need
     *  a game object prefab which stores the "holstered item" 
     *  an enum which tracks the behavior of picking up this item
     *      Endless - an endless number of the item in this holster can be pulled out
     *      Limited - only the one specific item in this holster can be pulled out
     *      Magnetic/Sticky - only the one specific item in this holster can be pulled out and when it is let go it goes back to this holster
     * */

    // private components

    // public components
    
    public StorageType storageType;
    public enum StorageType
    {
        Endless,
        Limited,
        Magnetic,
    }

    public PhysicalObject storedObject;
    public List<PhysicalObject> possibleSorageObjects;

    //private fields

    // public fields 
    public bool hasStartingObject;

    [System.Serializable]
    public struct SaveVariables
    {
        public float maxAngelarVelicity, mass, drag, angularDrag;
        public Vector3 centerOfMass;
        public bool isKinematic, useGravity;
        public void SaveProperty(Rigidbody rigidbody)
        {
            useGravity = rigidbody.useGravity;
            isKinematic = rigidbody.isKinematic;
            maxAngelarVelicity = rigidbody.maxAngularVelocity;
            centerOfMass = rigidbody.centerOfMass;
            mass = rigidbody.mass;
            drag = rigidbody.drag;
            angularDrag = rigidbody.angularDrag;
            //Debug.Log("Saved Rigid body component");
        }

        public void LoadProperty(Rigidbody rigidbody)
        {
            rigidbody.useGravity = useGravity;
            rigidbody.isKinematic = isKinematic;
            rigidbody.maxAngularVelocity = maxAngelarVelicity;
            rigidbody.centerOfMass = centerOfMass;
            rigidbody.mass = mass;
            rigidbody.drag = drag;
            rigidbody.angularDrag = angularDrag;
            //Debug.Log("Loaded Rigid body component");
        }
    }
    public SaveVariables saveVariables;

    private void Start()
    {
        if (hasStartingObject)
        {
            
            
            PhysicalObject newStoredObject = Instantiate(storedObject, transform.position, transform.rotation);
            storedObject.MyRigidbody = storedObject.GetComponent<Rigidbody>();
            newStoredObject.saveVariables.SaveProperty(storedObject.MyRigidbody);
            StoreObject(newStoredObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PhysicalObject>()
            && (other.GetComponentInParent<PhysicalObject>().leftHand || other.GetComponentInParent<PhysicalObject>().rightHand))
        {
            if (possibleSorageObjects.Count > 0)
            {
                foreach (PhysicalObject ci in possibleSorageObjects)
                {
                    if (other.GetComponentInParent<PhysicalObject>().name == ci.name)
                    {
                       // Debug.Log("duplicate interactable object skipped");
                        break;
                    }
                  //  Debug.Log("interactable object detected");
                    possibleSorageObjects.Add(other.GetComponentInParent<PhysicalObject>());
                }
            }
            else
            {
               // Debug.Log("interactable object detected");
                possibleSorageObjects.Add(other.GetComponentInParent<PhysicalObject>());
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PhysicalObject>() &&
           (other.GetComponentInParent<PhysicalObject>().leftHand || other.GetComponentInParent<PhysicalObject>().rightHand))
        {
            if (possibleSorageObjects.Count > 0)
            {
                foreach (PhysicalObject ci in possibleSorageObjects)
                {
                    if (other.GetComponentInParent<PhysicalObject>().name == ci.name)
                    {
                       // Debug.Log("interactable object detected");
                        possibleSorageObjects.Remove(other.GetComponentInParent<PhysicalObject>());
                        break;
                    }
                   
                }
            }

        }
    }

    public void Update()
    {
        //myCollider.isTrigger = isTrigger;

        if (!storedObject)
        {
            foreach (PhysicalObject ci in possibleSorageObjects)
            {
                if (!ci.leftHand && !ci.rightHand)
                {
                    StoreObject(ci);
                    possibleSorageObjects.Remove(ci);
                    //isTrigger = false;
                    isInteractible = true;
                    break;
                }
            }
        }
        if (possibleSorageObjects.Count == 0 && storedObject)
        {
            isInteractible = true;
        }
        if (storageType == StorageType.Magnetic && (!storedObject.leftHand && !storedObject.rightHand) && !GetComponentInChildren<PhysicalObject>())
        {
            StoreObject(storedObject);
        }
        
    }

    public void GrabStart(CustomHand hand)
    {
        //Debug.Log("Grabbing Item Holder");
        Collider[] allcollliders = storedObject.GetComponentsInChildren<Collider>();
        isInteractible = false;
        switch (storageType)
        {

            case StorageType.Endless:
                {
                storedObject.transform.SetParent(hand.GetComponentInParent<SteamVR_PlayArea>().transform);
                hand.SelectedGpibInteractible = storedObject;
                hand.GrabInteractible = storedObject;
                hand.grabPoser = storedObject.grabPoints[0];

                this.saveVariables.LoadProperty(storedObject.MyRigidbody);
                storedObject.saveVariables.SaveProperty(storedObject.MyRigidbody);
                PhysicalObject newStoredObject = Instantiate(storedObject, transform.position, transform.rotation);

                switch (hand.handType)
                {
                    case SteamVR_Input_Sources.LeftHand:
                        storedObject.leftHand = hand;
                        storedObject.leftMyGrabPoser = hand.grabPoser;
                        break;
                    case SteamVR_Input_Sources.RightHand:
                        storedObject.rightHand = hand;
                        storedObject.rightMyGrabPoser = hand.grabPoser;
                        break;
                    default:
                        Debug.Log(" input source not recognized");
                        break;
                }
                foreach (Collider col in allcollliders)
                {
                        if (!col.GetComponentInParent<Bullet>())
                        {
                            
                            col.enabled = true;
                        }
                        else
                        {
                            //Debug.Log(col.name);
                            col.enabled = false;
                        }
                }

                //storedObject = null;
                StoreObject(newStoredObject);
                break;
        }
            case StorageType.Limited:
                {
                    storedObject.transform.SetParent(hand.GetComponentInParent<SteamVR_PlayArea>().transform);
                    hand.SelectedGpibInteractible = storedObject;
                    hand.GrabInteractible = storedObject;
                    hand.grabPoser = storedObject.grabPoints[0];
                    this.saveVariables.LoadProperty(storedObject.MyRigidbody);
                    storedObject.saveVariables.SaveProperty(storedObject.MyRigidbody);
                    switch (hand.handType)
                    {
                        case SteamVR_Input_Sources.LeftHand:
                            storedObject.leftHand = hand;
                            storedObject.leftMyGrabPoser = hand.grabPoser;
                            break;
                        case SteamVR_Input_Sources.RightHand:
                            storedObject.rightHand = hand;
                            storedObject.rightMyGrabPoser = hand.grabPoser;
                            break;
                        default:
                            Debug.Log(" input source not recognized");
                            break;
                    }

                    foreach (Collider col in allcollliders)
                    {
                        if (!col.GetComponentInParent<Bullet>())
                        {

                            col.enabled = true;
                        }
                        else
                        {
                            Debug.Log(col.name);
                            col.enabled = false;
                        }
                    }
                    storedObject = null;
                    break;
                }
            case StorageType.Magnetic:
                storedObject.transform.SetParent(hand.GetComponentInParent<SteamVR_PlayArea>().transform);
                hand.SelectedGpibInteractible = storedObject;
                hand.GrabInteractible = storedObject;
                hand.grabPoser = storedObject.grabPoints[0];
                this.saveVariables.LoadProperty(storedObject.MyRigidbody);
                storedObject.saveVariables.SaveProperty(storedObject.MyRigidbody);
                switch (hand.handType)
                {
                    case SteamVR_Input_Sources.LeftHand:
                        storedObject.leftHand = hand;
                        storedObject.leftMyGrabPoser = hand.grabPoser;
                        break;
                    case SteamVR_Input_Sources.RightHand:
                        storedObject.rightHand = hand;
                        storedObject.rightMyGrabPoser = hand.grabPoser;
                        break;
                    default:
                        Debug.Log(" input source not recognized");
                        break;
                }

                foreach (Collider col in allcollliders)
                {
                    if (!col.GetComponentInParent<Bullet>())
                    {

                        col.enabled = true;
                    }
                    else
                    {
                        //Debug.Log(col.name);
                        col.enabled = false;
                    }
                }
                // when released snaps back to holster
                break;
        }
    }

    public void StoreObject(PhysicalObject newStoredObject)
    {
        //Debug.Log("objectStored");
        storedObject = newStoredObject;
        this.saveVariables.SaveProperty(storedObject.MyRigidbody);
        Collider[] colliders = storedObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        storedObject.rightHand = null;
        storedObject.leftHand = null;
        storedObject.rightMyGrabPoser = null;
        storedObject.leftMyGrabPoser = null;
        storedObject.MyRigidbody.isKinematic = true;
        storedObject.MyRigidbody.useGravity = false;
        storedObject.transform.SetParent(this.transform);
        storedObject.transform.localPosition = Vector3.zero;
        storedObject.transform.rotation = transform.rotation;
        
    }

}
