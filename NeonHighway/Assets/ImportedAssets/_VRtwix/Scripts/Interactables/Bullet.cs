using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PhysicalObject
{
	public string ammoType; // ammo type
	public bool armed=true; // if ammo ready to shoot
	public Mesh shellModel; // object of casing , which will be replaced after shot

	//Particles
	public ParticleSystem sparks;


	public DropBehavior dropBehavior;
	public enum DropBehavior
	{
		DestroyEmpty,
		DestroyAll,
		DestroyNone
	}
	public float TimeToDestroy;
	void Start()
    {
		Initialize ();
    }
    
	new void GrabStart(CustomHand hand)
    {
		GrabStartCustom(hand);
    }



	new public void GrabUpdate(CustomHand hand){
		GrabUpdateCustom (hand);
	}


	new public void GrabEnd(CustomHand hand){
		GrabEndCustom(hand);
		switch (dropBehavior)
		{
			case DropBehavior.DestroyEmpty:
				if (!armed)
				{
					isInteractible = false;
					StartCoroutine(DestroySelf(TimeToDestroy));
				}
				break;
			case DropBehavior.DestroyAll:
				isInteractible = false;
				StartCoroutine(DestroySelf(TimeToDestroy));
				break;
			case DropBehavior.DestroyNone:
				break;
		}
	}
	private IEnumerator DestroySelf(float val)
	{
		yield return new WaitForSeconds(val);
		Destroy(this.gameObject);
	}
	public void ChangeModel(){
        MeshFilter myMeshfilter = GetComponentInChildren<MeshFilter>();
        myMeshfilter.mesh = shellModel;
		armed = false;
	}

	public void DettachBullet(){
		DettachHands ();
		saveVariables.LoadProperty (MyRigidbody);
	}

	public void EnterMagazine(){
		Collider[] tempCollider= GetComponentsInChildren<Collider> ();
		for (int i = 0; i < tempCollider.Length; i++) {
			tempCollider [i].enabled = false;
		}
		MyRigidbody.isKinematic = true;
	}

	public void OutMagazine(){
		Collider[] tempCollider= GetComponentsInChildren<Collider> ();
		for (int i = 0; i < tempCollider.Length; i++) {
			tempCollider [i].enabled = true;
		}
		MyRigidbody.isKinematic = false;

		switch (dropBehavior)
		{
			case DropBehavior.DestroyEmpty:
				if (!armed)
				{
					isInteractible = false;
					StartCoroutine(DestroySelf(TimeToDestroy));
				}
				break;
			case DropBehavior.DestroyAll:
				isInteractible = false;
				StartCoroutine(DestroySelf(TimeToDestroy));
				break;
			case DropBehavior.DestroyNone:
				break;
		}
	}
}
