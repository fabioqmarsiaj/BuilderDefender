﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{


    private BuildingTypeSO _buildingType;
    private BuildingTypeListSO _buildingTypeList;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;

        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        _buildingType = _buildingTypeList.list[0];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            _buildingType = _buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _buildingType = _buildingTypeList.list[1];
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
