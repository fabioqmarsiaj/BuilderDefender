using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Transform btnTemplate;
    [SerializeField] private Sprite arrowSprite;

    private Dictionary<BuildingTypeSO, Transform> _btnTransformDictionary;
    private Transform _arrowBtn;


    private void Awake()
    {
        btnTemplate.gameObject.SetActive(false);
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        _btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        _arrowBtn = Instantiate(btnTemplate, transform);
        _arrowBtn.gameObject.SetActive(true);

        float offsetAmount = +130f;
        _arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        _arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;
        _arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        _arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            offsetAmount = +130f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            _btnTransformDictionary[buildingType] = btnTransform;

            index++;
        }
    }

    private void Update()
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        _arrowBtn.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in _btnTransformDictionary.Keys)
        {
            Transform btnTransform = _btnTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if (activeBuildingType == null)
        {
            _arrowBtn.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            _btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }

}
