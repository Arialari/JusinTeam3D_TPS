using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsMethod
{
    //Use Only when gameobject have MeshRenderer
    public static void SetVisible(this GameObject gameObject, bool _bool, bool _balsoChildren = true)
    {
        MeshRenderer meshRenderer;
        gameObject.TryGetComponent(out meshRenderer);
        if (meshRenderer)
        {
            meshRenderer.enabled = _bool;
        }
        if(_balsoChildren)
        {
            for(int i=0; i < gameObject.transform.childCount; ++i)
            {
                gameObject.transform.GetChild(i).SetVisible(_bool, true);
            }
        }
    }

    public static void SetVisible(this Transform transform, bool _bool, bool _balsoChildren = true)
    {
        MeshRenderer meshRenderer;
        transform.TryGetComponent(out meshRenderer);
        if (meshRenderer)
        {
            meshRenderer.enabled = _bool;
        }
        if (_balsoChildren)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).SetVisible(_bool,true);
            }
        }
    }

    public static GameObject GetTopParent(this GameObject gameObject)
    {
        GameObject resultGameObject = null;

        if(gameObject.transform.parent)
        {
            resultGameObject = gameObject.transform.parent.gameObject.GetTopParent();
        }
        else 
        {
            resultGameObject = gameObject;
        }
        return resultGameObject;
    }

    public static MeshRenderer GetFollowingMeshRendererComponent(this GameObject gameObject)
    {
        Transform transformMeshObject = gameObject.transform.Find("Mesh Object");
        MeshRenderer meshRenderer;
        transformMeshObject.TryGetComponent(out meshRenderer);
        return meshRenderer;
    }
    public static Transform GetTransformInAllChildren(this Transform transform, string n)
    {
        if (transform.childCount == 0)
            return null;

        Transform transformReturn;
        transformReturn = transform.Find(n);
        if (transformReturn)
            return transformReturn;
        foreach( Transform child in transform)
        {
            transformReturn = child.GetTransformInAllChildren(n);
            if (transformReturn)
                return transformReturn;
        }
        return null;
    }

}

