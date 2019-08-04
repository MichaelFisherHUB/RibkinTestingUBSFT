using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour {
    [SerializeField]    private List<AmmoBase> awalibleWeapons = new List<AmmoBase>();
    [SerializeField]    private Transform shootingAimPosition;
    private List<AmmoDataContainer> ammoTypes = new List<AmmoDataContainer>();

    private void Start()
    {
        awalibleWeapons.ForEach(x => 
        {
            ammoTypes.Add(new AmmoDataContainer(x));
            GameObjectPool.Instance.CreatePool(x.gameObject);
        });
    }

    private void Update()
    {
        ammoTypes.ForEach(x => 
        {
            x.UpdateTimer();
        });
    }

    public void Shot()
    {
        List<GameObject> objToShoot = new List<GameObject>();

        ammoTypes.ForEach(x => 
        {
            if (x.IsWeaponReady)
            {
                GameObjectPool.Instance.GetGameObjectFromPool(x.ammo.gameObject.name, polledObject =>
                {
                    polledObject.transform.SetPositionAndRotation(shootingAimPosition.position, shootingAimPosition.rotation);
                });
                x.ResetTimer();
            }
        });
    }

    #region SubData

    private class AmmoDataContainer
    {
        public AmmoBase ammo { get; private set; }
        public float timerLimit { get; private set; }

        private float timer = 0;

        public bool IsWeaponReady
        {
            get
            {
                return timer > timerLimit;
            }
        }

        public AmmoDataContainer(AmmoBase ammo)
        {
            this.ammo = ammo;
            this.timerLimit = ammo.reloadTime;
        }

        public void ResetTimer()
        {
            timer = 0;
        }

        //Update this in Monobehavior's Update
        public void UpdateTimer()
        {
            if(!IsWeaponReady)
            {
                timer += Time.deltaTime;
            }
        }
    }
    #endregion
}
