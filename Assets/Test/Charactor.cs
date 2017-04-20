using UnityEngine;
using System.Collections;
using System;
using Jerry;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Charactor : MonoBehaviour
{
    /// <summary>
    /// 敌人
    /// </summary>
    public LayerMask killMask;
    /// <summary>
    /// 阻挡
    /// </summary>
    public LayerMask stopMask;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    public float skinWidth = 0.02f;

    [Range(2, 20)]
    public int totalHorizontalRays = 8;

    [Range(2, 20)]
    public int totalVerticalRays = 4;

    [HideInInspector]
    [NonSerialized]
    public Vector3 velocity;

    [HideInInspector]
    [NonSerialized]
    public BoxCollider2D boxCollider;

    struct CharacterRaycastOrigins
    {
        public Vector3 topLeft;
        public Vector3 topRight;
        public Vector3 bottomRight;
        public Vector3 bottomLeft;
    }

    private Bounds checkBounds;

    void Awake()
    {
        boxCollider = this.transform.GetComponent<BoxCollider2D>();
    }

    public void Move(Vector3 deltaMovement)
    {
        RaycastOrigins();

        if (deltaMovement.x != 0f)
        {
            MoveHorizontally(ref deltaMovement);
        }

        deltaMovement.z = 0;
        transform.Translate(deltaMovement, Space.World);

        if (Time.deltaTime > 0f)
        {
            velocity = deltaMovement / Time.deltaTime;
        }
    }

    private void RaycastOrigins()
    {
        checkBounds = boxCollider.bounds;
        checkBounds.Expand(-2f * skinWidth);
    }

    private void MoveHorizontally(ref Vector3 deltaMovement)
    {
        bool isGoingRight = deltaMovement.x > 0;
        float rayDistance = Mathf.Abs(deltaMovement.x) + skinWidth;
        Vector2 rayDirection = isGoingRight ? Vector2.right : -Vector2.right;
        float x = isGoingRight ? checkBounds.max.x : checkBounds.min.x;

        for (int i = 0; i < 3; i++)
        {
            Vector2 ray = new Vector2(x, checkBounds.min.y + 1.0f * i / 2 * (checkBounds.max.y - checkBounds.min.y));
            DrawRay("h" + i, ray, rayDirection * rayDistance);
        }

        Vector2 r1 = new Vector2(x, checkBounds.min.y + 1.0f * 0 / 2 * (checkBounds.max.y - checkBounds.min.y));
        Vector2 r2 = new Vector2(x, checkBounds.min.y + 1.0f * 2 / 2 * (checkBounds.max.y - checkBounds.min.y));
        DrawRay("t", r1, r2 - r1);
    }

    private void DrawRay(string name, Vector3 from, Vector3 dir)
    {
        DrawerElementPath p = JerryDrawer.GetElement<DrawerElementPath>(name);
        if (p == null)
        {
            p = JerryDrawer.Draw<DrawerElementPath>().SetColor(Color.red).SetID(name);
        }
        p.SetPoints(from, from + dir);
    }
}