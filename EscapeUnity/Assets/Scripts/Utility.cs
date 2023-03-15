/**
 * 
 * 
 * 
 * 
 * 
 * This Utility Class was created for Unity by Philip Neuhäuser
 * 
 * 
 * 
 * 
 * 
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Utility
{





    /// <summary>
    /// returns true if the GameObject is visible in any Camera
    /// </summary>
    /// <param name="gameobject">The gameobject you want to check</param>
    /// <returns></returns>
    public static bool GameObjectIsVisible(this GameObject gameobject)
    {
        return gameobject.GetComponent<Renderer>().isVisible;
    }










    /// <summary>
    /// Check for GameObjects (GameObjects require Collider2D)
    /// </summary>
    /// <param name="position">the position you want to check</param>
    /// <param name="radius">the radius of the check</param>
    /// <returns></returns>
    public static List<GameObject> CheckForGameObjects2D(Vector2 position, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        if (colliders == null) return null;

        List<GameObject> gameObjects = new List<GameObject>();
        foreach (var col in colliders)
        {
            gameObjects.Add(col.gameObject);
        }
        return gameObjects;
    }

    /// <summary>
    /// Check for GameObjects (GameObjects require Collider2D)
    /// </summary>
    /// <param name="position">the position you want to check</param>
    /// <param name="radius">the radius of the check</param>
    /// <param name="layer">the layer you want to check for</param>
    /// <returns></returns>
    public static List<GameObject> CheckForGameObjects2D(Vector2 position, float radius, LayerMask layer)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, layer);
        if (colliders == null) return null;

        List<GameObject> gameObjects = new List<GameObject>();
        foreach (var col in colliders)
        {
            gameObjects.Add(col.gameObject);
        }
        return gameObjects;
    }

    /// <summary>
    /// Check for GameObjects (GameObjects require Collider)
    /// </summary>
    /// <param name="position">the position you want to check</param>
    /// <param name="radius">the radius of the check</param>
    /// <returns></returns>
    public static List<GameObject> CheckForGameObjects3D(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        if (colliders == null) return null;

        List<GameObject> gameObjects = new List<GameObject>();
        foreach (var col in colliders)
        {
            gameObjects.Add(col.gameObject);
        }
        return gameObjects;
    }

    /// <summary>
    /// Check for GameObjects (GameObjects require Collider)
    /// </summary>
    /// <param name="position">the position you want to check</param>
    /// <param name="radius">the radius of the check</param>
    /// <param name="layer">the layer you want to check for</param>
    /// <returns></returns>
    public static List<GameObject> CheckForGameObjects3D(Vector3 position, float radius, LayerMask layer)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius, layer);
        if (colliders == null) return null;
        
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (var col in colliders)
        {
            gameObjects.Add(col.gameObject);
        }
        return gameObjects;
    }












    /// <summary>
    /// Use this Methode to get the mouse position in world points without the z coordinate of the camera
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition2D() => GetMouseWorldPosition2D(Camera.main);

    /// <summary>
    /// Use this Methode to get the mouse position in world points without the z coordinate of the camera
    /// </summary>
    /// <param name="worldCamera">The Camera you are currently using</param>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition2D(Camera worldCamera)
    {
        Vector3 mousePos = worldCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }

    /// <summary>
    /// Use this Methode to get the mouse position in world points with the z coordinate of the camera
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition2DWithZ() => GetMouseWorldPosition2DWithZ(Camera.main);

    /// <summary>
    /// Use this Methode to get the mouse position in world points with the z coordinate of the camera
    /// </summary>
    /// <param name="worldCamera">The Camera you are currently using</param>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition2DWithZ(Camera worldCamera)
    {
        Vector3 mousePos = worldCamera.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }

    /// <summary>
    /// Use this Methode to get the mouse position in world points with the z you want
    /// </summary>
    /// <param name="z">The z coordinate of the mouse position</param>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition2DWithZ(float z) => GetMouseWorldPosition2DWithZ(Camera.main, z);

    /// <summary>
    /// Use this Methode to get the mouse position in world points with the z you want
    /// </summary>
    /// <param name="worldCamera">The Camera you are currently using</param>
    /// <param name="z">The z coordinate of the mouse position</param>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition2DWithZ(Camera worldCamera, float z)
    {
        Vector3 mousePos = worldCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = z;
        return mousePos;
    }

    /// <summary>
    /// Use this Methode to get the mouse position in world points (needs collision with collider)
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition3D() => GetMouseWorldPosition3D(Camera.main);

    /// <summary>
    /// Use this Methode to get the mouse position in world points (needs collision with collider)
    /// </summary>
    /// <param name="worldCamera">The Camera you are currently using</param>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition3D(Camera worldCamera)
    {
        Ray ray = worldCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }









    /// <summary>
    /// Detect if you are over an Ui Element
    /// </summary>
    /// <returns></returns>
    public static bool IsOverUi() => IsOverUi(EventSystem.current);

    /// <summary>
    /// Detect if you are over an Ui Element
    /// </summary>
    /// <param name="eventSystem">Current Eventsystem you are using</param>
    /// <returns></returns>
    public static bool IsOverUi(EventSystem eventSystem)
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, results);
        return results.Count > 0;
    }













    /// <summary>
    /// Create Text in World
    /// </summary>
    /// <param name="parent">The parent Object of the game Object</param>
    /// <param name="text">The text you want to write</param>
    /// <param name="localPosition">the position where you want to place the text</param>
    /// <param name="fontSize">the font size of the text</param>
    /// <param name="color">the color of the text</param>
    /// <param name="textAnchor">the anchor of the Text</param>
    /// <param name="textAlignment">the alignment of the text</param>
    /// <param name="sortingOrder">the sorting order of the text</param>
    /// <returns></returns>
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }











    /// <summary>
    /// Use this Methode to get the World position of an Ui Element
    /// </summary>
    /// <param name="element">The Ui element</param>
    /// <returns></returns>
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element) => GetWorldPositionOfCanvasElement(element, Camera.main);

    /// <summary>
    /// Use this Methode to get the World position of an Ui Element
    /// </summary>
    /// <param name="element">The Ui element</param>
    /// <param name="currentCamera">The current Camera you are using</param>
    /// <returns></returns>
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element, Camera currentCamera)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, currentCamera, out var result);
        return result;
    }









    /// <summary>
    /// Use this Methode to rotate to a specific point
    /// </summary>
    /// <param name="transform">The Transform from the Game Object you want to rotate</param>
    /// <param name="targetPoint">The point you want to rotate to</param>
    /// <param name="rotationSpeed">The speed you want to rotate with</param>
    /// <param name="rotationOffset">The offset of the rotation</param>
    public static void RotateToPoint2D(this Transform transform, Vector3 targetPoint, float rotationSpeed, float rotationOffset)
    {
        Vector3 direction = targetPoint - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), Time.deltaTime * rotationSpeed);
    }

    /// <summary>
    /// Use this Methode to rotate to a specific point
    /// </summary>
    /// <param name="transform">The Transform from the Game Object you want to rotate</param>
    /// <param name="targetPoint">The point you want to rotate to</param>
    /// <param name="rotationOffset">The offset of the rotation</param>
    public static void RotateToPoint2D(this Transform transform, Vector3 targetPoint, float rotationOffset)
    {
        Vector3 direction = targetPoint - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    /// <summary>
    /// Use this Methode to rotate to a specific point
    /// </summary>
    /// <param name="transform">The Transform from the Game Object you want to rotate</param>
    /// <param name="targetPoint">The point you want to rotate to</param>
    /// <param name="rotationSpeed">The speed you want to rotate with</param>
    /// <param name="rotationOffset">The offset of the rotation</param>
    public static void RotateToPoint3D(this Transform transform, Vector3 targetPoint, float rotationSpeed, float rotationOffset)
    {
        Vector3 direction = targetPoint - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, angle + rotationOffset, 0f), Time.deltaTime * rotationSpeed);
    }

    /// <summary>
    /// Use this Methode to rotate to a specific point
    /// </summary>
    /// <param name="transform">The Transform from the Game Object you want to rotate</param>
    /// <param name="targetPoint">The point you want to rotate to</param>
    /// <param name="rotationOffset">The offset of the rotation</param>
    public static void RotateToPoint3D(this Transform transform, Vector3 targetPoint, float rotationOffset)
    {
        Vector3 direction = targetPoint - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle + rotationOffset, 0f);
    }








    /// <summary>
    /// This Methode destroys all the child Objects of the parent
    /// </summary>
    /// <param name="transform">The transform from the parent</param>
    public static void DeleteChildren(this Transform transform)
    {
        foreach (Transform child in transform) Object.Destroy(child.gameObject);
    }














    /// <summary>
    /// Return a point in a circle
    /// </summary>
    /// <param name="center">the center of the circle</param>
    /// <param name="radius">the radius of the circle</param>
    /// <returns></returns>
    public static Vector2 GetRandomPositionInCircle2D(Vector2 center, float radius) => center + Random.insideUnitCircle * radius;
}
