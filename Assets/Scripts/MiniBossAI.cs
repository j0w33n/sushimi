using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAI : Enemy
{
	//Attack
	public float attackRange = 2f;
	public bool isAttack = false;
	//Jump
    public float jumpRange = 10f;
    public float jumpRate = 3f;
    float nextJumpTime = 0f;
	//Cast
	float nextCastTime;
	public bool isCast = false;
	public float nextCast = 0.2f;
	public float space = 5f;
	//Attack
	public LayerMask attackMask;
	//Cast
    public GameObject tornado;
    public Transform tornadoPos;
    public bool isJump = false;
	public GameObject doublebarrelgun;
	public AudioClip attacksound,tornadosound,jumpsound;
	protected override void Update()
	{
		anim.SetBool("Jump", isJump);
        anim.SetBool("Attacking", isAttack);
		anim.SetBool("Cast", isCast);
		anim.SetBool("Death", dead);
		if (!canMove) return;
		Collider2D attackdist = Physics2D.OverlapCircle(transform.position, attackRange, attackMask); // Detect player in range of attack
		Collider2D jumpdist = Physics2D.OverlapCircle(transform.position, jumpRange, attackMask);
		if (jumpdist && !attackdist && Time.time >= nextJumpTime) //Condition for Jump
        {
			movement = Vector2.zero;
			isJump = true;
        } 
		else {
			isJump = false;
        }
        if (attackdist) // Condition for attack
        {
			isAttack = true;
        } 
		else {
			isAttack = false;
        }
		if (jumpdist && !isJump){
			anim.SetBool("Cast", true);
        }
		else {
			anim.SetBool("Cast", false);
		} 
		base.Update();
    }
	protected override void FixedUpdate()
	{
		if (canMove && !isJump && !isCast) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 9, 0);
	}
	private void Move(Vector2 direction)
	{
		rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
	}
    private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.DrawWireSphere(transform.position, jumpRange);
	}
    public void BossJump() //Event in animation for MiniBossJump
    {

        if (Time.time < nextJumpTime) return;

        if (transform.position.x < player.transform.position.x)
        {
            rb.position = new Vector2(player.transform.position.x - space, player.transform.position.y);
			audio.PlayOneShot(jumpsound);
        }
        if (transform.position.x > player.transform.position.x)
        {
            rb.position = new Vector2(player.transform.position.x + space, player.transform.position.y);
			audio.PlayOneShot(jumpsound);
		}

        nextJumpTime = Time.time + jumpRate;
        return;
    }
	public void PlayAttackSound() {
		audio.PlayOneShot(attacksound);
    }
    public IEnumerator BossCast() //Event in animation for MiniBossCast
    {
		if (Time.time >= nextCastTime) yield return null;
		isCast = true;
		Vector3 anglevector = Vector3.zero;
		for (int i = 0; i < 5; i++) {
			anglevector += new Vector3(0, 0, 15f);
			Quaternion angle = Quaternion.Euler(anglevector);
			Instantiate(tornado, tornadoPos.position, angle);
			audio.PlayOneShot(tornadosound);
        }
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		for (int i = 0; i < 5; i++) {
			anglevector += new Vector3(0, 0, 45f);
			Quaternion angle = Quaternion.Euler(anglevector);
			Instantiate(tornado, tornadoPos.position, angle);
			audio.PlayOneShot(tornadosound);
		}
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		nextCastTime = Time.time + nextCast;
		isCast = false;
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
		Destroy(gameObject);
		for (int i = 0; i < Random.Range(1, dropamt + 1); i++)
		{
			Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
		}
		Instantiate(doublebarrelgun, transform.position, transform.rotation);
	}
}