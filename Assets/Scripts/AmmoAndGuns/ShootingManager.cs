using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingManager : MonoBehaviour {
    [SerializeField]    private List<AmmoBase> awalibleWeapons = new List<AmmoBase>();
    [SerializeField]    private Transform shootingAimPosition;
    public static UnityEvent onTimerUpdate = new UnityEvent();
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

            if(x.UpdateTimer() && onTimerUpdate != null)
            {
                onTimerUpdate.Invoke();
            }
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
        public bool UpdateTimer()
        {
            if(!IsWeaponReady)
            {
                timer += Time.deltaTime;
                return true;
            }
            return false;
        }
    }
    #endregion
}
