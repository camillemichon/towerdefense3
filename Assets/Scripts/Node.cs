using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

	[HideInInspector] public GameObject turret;
	[HideInInspector] public TurretBlueprint turretBlueprint;
	[HideInInspector] public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
    }

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (turret != null)
		{
			buildManager.SelectNode(this);
			return;
		}

		if (!buildManager.CanBuild)
			return;

		BuildTurret(buildManager.GetTurretToBuild());
	}

	private void BuildTurret(TurretBlueprint blueprint)
	{
		if (PlayerStats.money < blueprint.cost)
		{
			Debug.Log("Not enough money to build!");
			return;
		}

		PlayerStats.money -= blueprint.cost;

		GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		turretBlueprint = blueprint;

		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Debug.Log("Turret build!");
	}

	public void UpgradeTurret()
	{
		if (PlayerStats.money < turretBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade!");
			return;
		}

		PlayerStats.money -= turretBlueprint.upgradeCost;
		Destroy(turret);

		GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;

		Debug.Log("Turret upgraded!");
	}

	public void SellTurret()
	{
		PlayerStats.money += turretBlueprint.GetSellAmount();

		GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(turret);
		
		turretBlueprint = null;
	}

	private void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject() || !buildManager.CanBuild)
			return;

		rend.material.color = buildManager.HasMoney ? hoverColor : notEnoughMoneyColor;
	}

	private void OnMouseExit()
	{
		rend.material.color = startColor;
    }
}
