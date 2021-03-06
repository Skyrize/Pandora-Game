﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class GameObjectExtension {
    public static void SetColor(this GameObject gameObject, Color color)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = color;
        }
    }
    public static void SetColors(this GameObject gameObject, Color[] colors)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (meshes.Length != colors.Length)
            throw new System.Exception("Meshes and colors have to match in length (" + gameObject.name + "). Colors : " + colors.Length + " / meshes : " + meshes.Length);
        for (int i = 0; i != meshes.Length; i++) {
            meshes[i].material.color = colors[i];
        }
    }
    public static Color[] GetColors(this GameObject gameObject)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (meshes.Length == 0)
            return null;
        Color[] colors = new Color[meshes.Length];

        for (int i = 0; i != meshes.Length; i++) {
            colors[i] = meshes[i].material.color;
        }
        return colors;
    }
    public static Color[] GetSharedColors(this GameObject gameObject)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (meshes.Length == 0)
            return null;
        Color[] colors = new Color[meshes.Length];

        for (int i = 0; i != meshes.Length; i++) {
            colors[i] = meshes[i].sharedMaterial.color;
        }
        return colors;
    }

    public static void SetCollisionActive(this GameObject gameObject, bool activated)
    {
        var colliders = gameObject.GetComponents<Collider>();
        var childColliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (var col in colliders)
        {
            col.enabled = activated;
        }
        foreach (var col in childColliders)
        {
            col.enabled = activated;
        }
    }

    public static void Suicide(this GameObject gameObject)
    {
        if (Application.isPlaying) {
            GameObject.Destroy(gameObject);
        } else {
            GameObject.DestroyImmediate(gameObject);
        }
    }
}
