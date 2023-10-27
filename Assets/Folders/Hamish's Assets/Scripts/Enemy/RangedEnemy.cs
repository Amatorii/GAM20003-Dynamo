using System.Collections;
using UnityEngine;

namespace Hamish.Enemy
{
    public class RangedEnemy : Enemy
    {
        private bool currentlyShooting = true;
        [Header("Class Specific Variables")]
        [SerializeField] private GameObject gun;
        public bool runStateMachine;

        protected override void Awake()
        { 
            base.Awake();
            Debug.Log(this);
            isAttacking = false;
        }

        private void Update()
        {
            if (healthScript.health <= 0)
            {
                Death();
                return;
            }
            RunStateMachine();
        }
        
        public override EnemyState AttackPlayer()
        {
            LookAtPlayer();
            AimAtPlayer();
            RaycastHit _hit;
            bool _isAimed = false;

            Physics.Raycast(gunNozzle.position, gunNozzle.TransformDirection(Vector3.forward), out _hit, Mathf.Infinity);
            
            if (_hit.collider != null)
                _isAimed = _hit.collider.CompareTag("Player");

            if (currentlyShooting && _isAimed)
            {
                StartCoroutine(ShootGun(burstAmount));
                currentlyShooting = false;
            }
            
            return currentState;
        }

        private void AimAtPlayer()
        {
                Debug.Log("Aiming");
                Vector3 dir = playerObject.transform.position - gunNozzle.position;
                //dir.x = 0;

                Quaternion rot = Quaternion.LookRotation(dir);
                gunNozzle.rotation = Quaternion.Lerp(gunNozzle.rotation, rot, turningSpeed * 2f * Time.deltaTime);

        }

        private void OnDrawGizmos()
        {
            RaycastHit _hit;
            Physics.Raycast(gunNozzle.position, gunNozzle.forward, out _hit, Mathf.Infinity);
            if (_hit.collider != null)
            {
                if (_hit.collider.CompareTag("Player"))
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.blue;
            }
            Gizmos.DrawLine(gunNozzle.position, transform.forward* 100);
        }

        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform gunNozzle;
        [SerializeField] private float rpm;
        [Range(0, 10)][SerializeField] private int burstAmount;
        [Range(0, 500)][SerializeField] private int bulletSpeed;
        //[SerializeField] private float xModifier;
        //[SerializeField] private float yModifier;
        //[SerializeField] private float zModifier;
        private IEnumerator ShootGun(int _noBullets)
        {
            while (0 != _noBullets)
            {
                GameObject _bullet = GameObject.Instantiate(bullet, gunNozzle.position + gunNozzle.forward, gunNozzle.rotation);
                _bullet.GetComponent<en_projectile_bullet>().SetDamage(damage);
                _bullet.GetComponent<en_projectile_bullet>().SetSpeed(bulletSpeed);
                yield return new WaitForSeconds(60 / rpm);
                _noBullets--;
            }
            yield return new WaitForSeconds(1.0f);
            currentlyShooting = true;
        }
    }
}