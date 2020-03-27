using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
public class Magazine : MonoBehaviour
{
	public bool Revolver = false; //revolver, can shout from built-in mags, same as shotgun
	public bool canLoad = true;  //ammo allowed to be inserted
	public string ammoType; // ammo type
	public int capacity, ammo; // capacity and current ammo amount
	public List<Bullet> stickingAmmo; // sticking ammo
	public Transform[] ContainAmmo; // loaded ammo posistion
	public PrimitiveWeapon primitiveWeapon; // weapon which is attached to
	public Collider[] MagazineColliders; //IgnoreCollider
	PrimitiveWeapon primitiveWeaponRevolver; // revolver, which is attached to

	public float ang, id; //drum angle, id of current ammo
	[Header("Sounds Events")]
	public UnityEvent addBullet;

	[Header("MyEdits")]
	public GameObject bulletprefab;
	public  FillBehavior fillBehavior;
	bool fillCommandClick;
	public SteamVR_Action_Boolean LowerButtonClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("LowerButtonClick"); //input
	public enum FillBehavior
	{
		FillOnStart,
		FillOnCommand,
		DontFill
	}
	public DropBehavior dropBehavior;
	public enum DropBehavior
	{
		DestroyEmpty,
		DestroyAll,
		DestroyNone
	}
	public float TimeToDestroy;
	/*
	 * Changes to the magazine that I want 
	 * 
	 * ADD
	 * a method which can fill the magazine with ammo on start up - complete 
	 * an enum determining the behavior of the magazine after it is dropped - complete 
	 *	Destroyed if it has no ammo
	 *	Allways destroyed 
	 *	Never destroyed 
	 * 
	 */
    void Start()
    {
		
		MagazineColliders = GetComponentsInChildren<Collider> ();
        if (Revolver)
        {
            primitiveWeaponRevolver = GetComponentInParent<PrimitiveWeapon>();
            stickingAmmo.AddRange(new Bullet[capacity]);
            enabled = true;
        }
        else
        {
            enabled = false;
        }
		if (fillBehavior == FillBehavior.FillOnStart)
		{
			FillMagazine();
		}
	}

	void Update(){
		ang = ((id<0?capacity+id:id) * 360 / capacity)%360;
	}

	public int getBulletIdRevolver(float tempAngle){
		float TempId = capacity/360f *(tempAngle<0?360f+tempAngle:tempAngle);
		return Mathf.RoundToInt (TempId)%capacity;
	}

	public void GrabStart(CustomHand hand){
		if (primitiveWeapon) {
			primitiveWeapon.attachMagazine = null;
            transform.parent = null;
			canLoad = true;
			for (int i = 0; i < primitiveWeapon.myCollidersToIgnore.Length; i++) {
				for (int j = 0; j < MagazineColliders.Length; j++) {
					Physics.IgnoreCollision(primitiveWeapon.myCollidersToIgnore[i],MagazineColliders[j],false);
				}
			}
			primitiveWeapon = null;
			
		}
	}

	public void GrabUpdate(CustomHand hand)
	{
		if (fillBehavior == FillBehavior.FillOnCommand)
		{
			FillMagUpdateCheck(hand);
		}
	}
	public void GrabEnd(CustomHand hand)
	{
		//Debug.Log("Dropped mag");
		switch (dropBehavior)
		{
			case DropBehavior.DestroyEmpty:
				if (ammo <= 0)
				{
					GetComponent<PhysicalObject>().isInteractible = false;
					StartCoroutine(DestroySelf(TimeToDestroy));
				}
				break;
			case DropBehavior.DestroyAll:
				GetComponent<PhysicalObject>().isInteractible = false;
				StartCoroutine(DestroySelf(TimeToDestroy));
				break;
			case DropBehavior.DestroyNone:
				break;
		}
	}
	public void DropMagazine()
	{
		this.GetComponent<PhysicalObject>().saveVariables.LoadProperty(this.GetComponent<Rigidbody>());
		//Debug.Log("dropped mag");
		if (primitiveWeapon)
		{
			primitiveWeapon.attachMagazine = null;
			transform.parent = null;
			canLoad = true;
			for (int i = 0; i < primitiveWeapon.myCollidersToIgnore.Length; i++)
			{
				for (int j = 0; j < MagazineColliders.Length; j++)
				{
					Physics.IgnoreCollision(primitiveWeapon.myCollidersToIgnore[i], MagazineColliders[j], false);
				}
			}
			primitiveWeapon = null;			
		}

		switch (dropBehavior)
		{
			case DropBehavior.DestroyEmpty:
				if (ammo <= 0)
				{
					GetComponent<PhysicalObject>().isInteractible = false;
					StartCoroutine(DestroySelf(TimeToDestroy));
				}
				break;
			case DropBehavior.DestroyAll:
				GetComponent<PhysicalObject>().isInteractible = false;
				StartCoroutine(DestroySelf(TimeToDestroy));
				break;
			case DropBehavior.DestroyNone:
				break;
		}

	}
	void AddBullet(Bullet bullet){
		if (!canLoad)
			return;
		bullet.DettachBullet ();
		stickingAmmo.Add (bullet);
		ammo = stickingAmmo.Count;
        SortingBulletInMagazine();
		bullet.EnterMagazine ();
		addBullet.Invoke ();
	}
	private IEnumerator DestroySelf(float val)
	{
		yield return new WaitForSeconds(val);
		Destroy(this.gameObject);
	}
	void FillMagUpdateCheck(CustomHand hand)
	{
		if (LowerButtonClick.GetStateUp(hand.handType))
		{
			fillCommandClick = false;
		}
		if (!fillCommandClick && LowerButtonClick.GetStateDown(hand.handType))
		{
			FillMagazine();
		}
	}

    void SortingBulletInMagazine() {
        for (int i = 0; i < stickingAmmo.Count; i++)
        {
            stickingAmmo[i].transform.parent = ContainAmmo[ammo-i-1];
            stickingAmmo[i].transform.localPosition = Vector3.zero;
            stickingAmmo[i].transform.localRotation = Quaternion.identity;
        }
    }

	void AddBulletClose(Bullet bullet){
		if (!Revolver&&!canLoad)
			return;

		float tempCloseDistance=float.MaxValue;
		int closeId=0;
		for (int i = 0; i < ContainAmmo.Length; i++) {
			if (stickingAmmo[i]==null&&Vector3.Distance(bullet.transform.position,ContainAmmo[i].position)<tempCloseDistance){
				tempCloseDistance = Vector3.Distance (bullet.transform.position, ContainAmmo [i].position);
				closeId = i;
			}
		} 
		bullet.DettachBullet ();
		stickingAmmo [closeId] = bullet;
		ammo++;

		stickingAmmo [closeId].transform.parent = ContainAmmo [closeId];
		stickingAmmo [closeId].transform.localPosition = Vector3.zero;
		stickingAmmo [closeId].transform.localRotation = Quaternion.identity;

		bullet.EnterMagazine ();
	}

	public bool ShootFromMagazineRevolver(){
		if (!Revolver)
			return false;

//		int tempId = (primitiveWeaponRevolver.manualReload.revolverBulletID+1)%capacity;
		int tempId = primitiveWeaponRevolver.manualReload.revolverBulletID;
		if (stickingAmmo [tempId] != null&&stickingAmmo[tempId].armed) {
			stickingAmmo [tempId].ChangeModel ();
			return true;
		}
		return false;
	}

	public bool ShootFromMagazine(){//по очередди
		if (!Revolver)
			return false;

		for (int i = 0; i < stickingAmmo.Count; i++) {
			if (stickingAmmo [i] != null&&stickingAmmo[i].armed) {
				stickingAmmo [i].ChangeModel ();
				return true;
			}
		}
		return false;
	}

	public void FillMagazine()
	{
		for (int i = 0; i <= capacity; i++)
		{
			if (ammo < capacity)
			{
				Bullet bl = Instantiate(bulletprefab, transform.position, transform.rotation).GetComponent<Bullet>();
				bl.MyRigidbody = bl.GetComponent<Rigidbody>();
				bl.saveVariables.SaveProperty(bl.MyRigidbody);
				AddBullet(bl);
			}
		}
		

	}

	public void UnloadMagazine(Vector3 outBulletSpeed){
		if (!Revolver) {
			return;
		}
		for (int i = 0; i < stickingAmmo.Count; i++) {
			if (stickingAmmo [i]) {
				stickingAmmo [i].transform.parent = null;
				stickingAmmo [i].OutMagazine ();
				stickingAmmo [i].MyRigidbody.AddRelativeForce (outBulletSpeed, ForceMode.VelocityChange);
				stickingAmmo [i] = null;
			}
		}
		ammo = 0;
	}
    
	public Bullet GetBullet(){
		Bullet tempReturn=stickingAmmo[ammo-1];
		stickingAmmo.RemoveAt(ammo-1);
		ammo = stickingAmmo.Count;
        SortingBulletInMagazine();
		return tempReturn;
	}

	void OnTriggerEnter(Collider c){
		if (Revolver) {
			if (c.attachedRigidbody && c.attachedRigidbody.GetComponent<Bullet> ()&&c.attachedRigidbody.GetComponent<Bullet> ().ammoType == ammoType) {
				if (ammo < capacity) {
					AddBulletClose (c.attachedRigidbody.GetComponent<Bullet> ());
				}
			}
		} else {
			if (c.attachedRigidbody && c.attachedRigidbody.GetComponent<Bullet> () && c.attachedRigidbody.GetComponent<Bullet> ().ammoType == ammoType) {
				if (ammo < capacity) {
					AddBullet (c.attachedRigidbody.GetComponent<Bullet> ());
				}
			}
		}
	}
}
