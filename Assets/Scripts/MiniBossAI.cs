using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAI : Enemy
{
	//Attack
	public float attackRange = 2f;
	public bool isAttack = false;
	float nextattacktime;
	public float attackrate;
	//Jump
    public float jumpRange = 10f;
	public float jumpspeed = 1f;
    public float jumpRate = 3f;
    float nextJumpTime = 0f;
	//Cast
	float nextCastTime;
	public bool isCast = false;
	public float nextCast = 0.2f;
	public float space = 5f;
	public float castrange;
	//Attack
	public LayerMask attackMask;
	//Cast
    public GameObject tornado;
    public Transform tornadoPos;
    public bool isJump = false;
	public GameObject doublebarrelgun;
	public AudioClip attacksound,tornadosound,jumpsound,deathsound;
    protected override void Start() {
		gameObject.SetActive(false);
		base.Start();
    }
    protected override void Update()
	{
		anim.SetBool("Jump", isJump);
        anim.SetBool("Attacking", isAttack);
		anim.SetBool("Cast", isCast);
		anim.SetBool("Death", dead);
		if (!canMove) return;
		Collider2D attackdist = Physics2D.OverlapCircle(transform.position, attackRange, attackMask); // Detect player in range of attack
		Collider2D jumpdist = Physics2D.OverlapCircle(transform.position, jumpRange, attackMask);
		Collider2D castdist = Physics2D.OverlapCircle(transform.position, castrange, attackMask);
		if (castdist && !jumpdist){
			isCast = true;
        } 
		else {
			isCast = false;
        }
		if (jumpdist && !attackdist && Time.time >= nextJumpTime) //Condition for Jump
		{
			movement = Vector2.zero;
			isJump = true;
        } 
		else {
			isJump = false;
        }
		if (attackdist && Time.time >= nextattacktime) // Condition for attack
	    {
			isAttack = true;
        } 
		else {
			isAttack = false;
        }
		base.Update();
    }
	protected override void FixedUpdate()
	{
		if (canMove && !isJump) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 9, 0);
	}
	private void Move(Vector2 direction)
	{
		rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
	}
    private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.DrawWireSphere(transform.position, jumpRange);
		Gizmos.DrawWireSphere(transform.position, castrange);
	}
    public void BossJump() //Event in animation for MiniBossJump
    {

		if (Time.time < nextJumpTime) return;
		transform.position = Vector3.Lerp(transform.position, player.transform.position, jumpspeed);
		audio.PlayOneShot(jumpsound);
        nextJumpTime = Time.time + jumpRate;
        return;
    }
	public void BossAttack() {
		attackrate = anim.GetCurrentAnimatorStateInfo(0).length;
		nextattacktime = Time.time + attackrate;
		audio.PlayOneShot(attacksound);
    }
    public IEnumerator BossCast() //Event in animation for MiniBossCast
    {
		if (Time.time < nextCastTime) yield return null;
		isCast = true;
		Vector3 anglevector = Vector3.zero;
		for (int i = 0; i < 10; i++) {
			anglevector += new Vector3(0, 0, 45f);
			Quaternion angle = Quaternion.Euler(anglevector);
			Instantiate(tornado, tornadoPos.position, angle);
			audio.PlayOneShot(tornadosound);
        }
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		nextCastTime = Time.time + nextCast;
		isCast = false;
	}
	public void PlayDeathSound() {
		audio.PlayOneShot(deathsound);
    }
	public override IEnumerator Death()
	{
		rb.velocity = Vector2.zero;
		if (!dead)
		{
			dead = true;
			Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
		}
		healthbar.gameObject.SetActive(false);
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		//Destroy(Instantiate(maskvfx, transform.position, transform.rotation), 1);
		for (int i = 0; i < Random.Range(1, dropamt + 1); i++)
		{
			Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
		}
		Instantiate(doublebarrelgun, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}