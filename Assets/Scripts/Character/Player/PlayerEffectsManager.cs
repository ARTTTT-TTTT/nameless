using UnityEngine;
using System.Collections;

namespace ART
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        private PlayerManager player;
        private Coroutine disableCoroutine;

        [Header("Debug Delete Later")]
        [SerializeField] private InstantCharacterEffect effectToTest;

        [SerializeField] private bool processEffect = false;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            if (processEffect)
            {
                processEffect = false;
                InstantCharacterEffect effect = Instantiate(effectToTest);
                ProcessInstantEffect(effect);
            }
        }

        public override void EnableVFXAttack()
        {
            var rightWeapon = player?.playerEquipmentManager?.rightWeaponManager;
            if (rightWeapon == null || rightWeapon.weaponParticles == null || rightWeapon.weaponVFX == null)
            {
                return;
            }

            // ✅ หยุด Coroutine ที่กำลังปิด VFX ถ้ามันทำงานอยู่
            if (disableCoroutine != null)
            {
                StopCoroutine(disableCoroutine);
                disableCoroutine = null;
            }

            if (!rightWeapon.weaponVFX.activeSelf)
            {
                rightWeapon.weaponVFX.SetActive(true);
            }

            foreach (var ps in rightWeapon.weaponParticles)
            {
                ps.Play();
            }
        }

        public override void DisableVFXAttack()
        {
            var rightWeapon = player?.playerEquipmentManager?.rightWeaponManager;
            if (rightWeapon == null || rightWeapon.weaponParticles == null || rightWeapon.weaponVFX == null)
            {
                return;
            }

            foreach (var ps in rightWeapon.weaponParticles)
            {
                ps.Stop();
            }

            // ✅ เก็บ Coroutine ไว้ และเริ่มหน่วงเวลา
            disableCoroutine = StartCoroutine(DisableWeaponVFXAfterDelay(rightWeapon.weaponVFX, 2f));
        }

        private IEnumerator DisableWeaponVFXAfterDelay(GameObject weaponVFX, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (weaponVFX != null && weaponVFX.activeSelf)
            {
                weaponVFX.SetActive(false);
            }

            disableCoroutine = null; // ✅ รีเซ็ตตัวแปรเมื่อเสร็จ
        }
    }
}