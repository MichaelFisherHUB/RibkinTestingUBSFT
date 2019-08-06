using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveLoadComponnent))]
public class SolarSystemBuilder : MonoBehaviour
{
    public StarBase _star;
    public List<PlanetsBuildData> _planets = new List<PlanetsBuildData>();
    [SerializeField] private SolarSystemDataSource solarSystemDataSource;
    [SerializeField] private List<AmmoBase> allWeapons = new List<AmmoBase>();

    [SerializeField] private SaveLoadComponnent _saveLoad;
    private SaveLoadComponnent SaveLoad
    {
        get
        {
            return _saveLoad ?? (_saveLoad = GetComponent<SaveLoadComponnent>());
        }
    }

    private void Start()
    {
        BuildStarSystem();
    }

    public void BuildStarSystem()
    {
        BuildRandomStarSystem(_star, _planets);
    }

    public void BuildRandomStarSystem(StarBase star, List<PlanetsBuildData> planets)
    {
        #region Inst Star

        GameObject starTmp = Instantiate(star.gameObject, Vector3.zero, Quaternion.identity);
        starTmp.transform.SetParent(gameObject.transform);
        #endregion

        #region Inst Planet

        planets.ForEach(x => 
        {
            GameObject planetTmp = Instantiate(x.planet.gameObject, x.position, Quaternion.identity);
            planetTmp.transform.SetParent(gameObject.transform);

            OrbitalAroundHeandler orbitalScript = planetTmp.GetComponent<OrbitalAroundHeandler>();
            if(orbitalScript != null)
            {
                orbitalScript.orbitingAround = starTmp;

                if (solarSystemDataSource == SolarSystemDataSource.Generate)
                {
                    float coef = Vector3.Distance(planetTmp.transform.position, starTmp.transform.position);
                    if (coef < 1) { coef = 1; }
                    float orbitalSpeed = 60 / coef;
                    orbitalScript.orbitalSpeed = Random.value == 0 ? -orbitalSpeed : orbitalSpeed;
                }
                else
                {
                    //TODO Set orbital speed from save file
                }

            }

            ShootingManager shoot = planetTmp.GetComponent<ShootingManager>();
            if(shoot != null)
            {
                if (allWeapons.Count > 0)
                {
                    shoot.awalibleWeapons.Clear();
                    shoot.awalibleWeapons.Add(allWeapons[Random.Range(0, allWeapons.Count)]);
                }
            }
        });
        #endregion
    }
}


[System.Serializable]
public class PlanetsBuildData
{
    public Planet planet;
    public Vector3 position;
    public float orbitalSpeed;
}

public enum SolarSystemDataSource
{
    Generate,
    SaveFile
}

