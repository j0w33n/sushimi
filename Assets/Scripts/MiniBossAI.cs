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
    //Charge
    public float chargerange = 10f;
    public float chargespeed = 1f;
    public float chargerate = 3f;
    float nextchargetime = 0f;
	public bool isCharging = false;
	// Smash
    public GameObject dustwave;
    public Transform dustPos;
	float nextsmashtime;
	public float smashrate;
	public float smashrange;
	[SerializeField]bool downed;
	public GameObject doublebarrelgun;
	public AudioClip attacksound, smashsound, chargesound, deathsound;
	//public GameObject dust;
	Coroutine charge;
	public float ogdowntime;
	[SerializeField]float downtime;
	protected override void Start() {
		gameObject.SetActive(false);
		isCharging = false;
		isAttack = false;
		downed = false;
		downtime = ogdowntime;
		base.Start();
    }
    protected override void Update()
	{
		anim.SetBool("Charge", isCharging);
        anim.SetBool("Attacking", isAttack);
		anim.SetBool("Death", dead);
		anim.SetBool("Downed", downed);
		if (!canMove) return;
		float dist = (transform.position - player.transform.position).magnitude;
		if (((hitpoints / maxhitpoints) * 100) % 20 == 0 && hitpoints < maxhitpoints && downtime > 0) { 
			downed = true; 
		}
		if (downed) {
			downtime -= Time.deltaTime;
		}
		if(downtime <= 0) {
			downed = false;
			downtime = ogdowntime;
        }
        if (!downed) {
			if (dist <= chargerange && dist > smashrange && Time.time >= nextchargetime) {
				charge = StartCoroutine(BossCharge());
			}
			if (charge == null && dist <= smashrange && dist > attackRange && Time.time >= nextsmashtime) {
				anim.SetTrigger("Smash");
			}
			if (dist <= attackRange && Time.time >= nextattacktime) {
				isAttack = true;
			} 
			else {
				isAttack = false;
			}
		}
        base.Update();
    }
	protected override void FixedUpdate()
	{
		if (canMove && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Smash") && !downed) Move(movement); healthbar.gameObject.transform.position = transform.position + new Vector3(0, 9, 0);
	}
	private void Move(Vector2 direction)
	{
		rb.MovePosition((Vector2)transform.position + (movespeed * Time.deltaTime * direction));
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Player>() && anim.GetCurrentAnimatorStateInfo(0).IsName("MiniBossCharge")) {
			collision.gameObject.GetComponent<Player>().knockbackforce = 12;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Player>() && !anim.GetCurrentAnimatorStateInfo(0).IsName("MiniBossCharge")) {
            collision.gameObject.GetComponent<Player>().knockbackforce = 3;
        }
    }
    private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.DrawWireSphere(transform.position, chargerange);
		Gizmos.DrawWireSphere(transform.position, smashrange);
	}
    IEnumerator BossCharge() 
    {
		Vector2 dir = (player.transform.position - transform.position).normalized;
        if (Time.time < nextchargetime) yield return null;
		isCharging = true;
		movement = Vector2.zero;
		yield return new WaitForSeconds(0.72f);
		movement = chargespeed * dir;
		damage = 3;
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		isCharging = false;
		damage = 1;
		//Destroy(Instantiate(dust, transform.position, transform.rotation), 1f);
		nextchargetime = Time.time + chargerate;
		charge = null;
	}
    public void BossAttack() {
		attackrate = anim.GetCurrentAnimatorStateInfo(0).length;
		nextattacktime = Time.time + attackrate;
		AudioManager.instance.PlaySFX(attacksound);
    }
    public void BossSmash()
    {
		movement = Vector2.zero;
		Instantiate(dustwave, dustPos.position, Quaternion.identity);
		AudioManager.instance.PlaySFX(smashsound);
		nextsmashtime = Time.time + smashrate;
	}
    public void PlayDeathSound() {
		AudioManager.instance.StopMusic();
		AudioManager.instance.PlaySFX(deathsound);
    }
	public void PlayChargeSound() {
		AudioManager.instance.PlaySFX(chargesound);
	}
	public override IEnumerator Death()
	{
		canMove = false;
		if (!dead) dead = true;
		healthbar.gameObject.SetActive(false);
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
		//Destroy(Instantiate(maskvfx, transform.position, transform.rotation), 1);
		for (int i = 0; i < Random.Range(1, dropamt + 1); i++)
		{
			Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
		}
		Instantiate(doublebarrelgun, transform.position, transform.rotation);
		Destroy(gameObject);
	}
	public void WhiteScreen() {
		StartCoroutine(FindObjectOfType<FadeIn>(true).Appear(true));
		StartCoroutine(FindObjectOfType<FadeIn>(true).Disappear());
	}
}