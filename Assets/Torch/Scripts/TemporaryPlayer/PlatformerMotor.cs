using UnityEngine;

/// <summary>
/// Klasa odpowiadająca za poruszanie graczem i sprawdzanie kolizji
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class PlatformerMotor : MonoBehaviour
{
    //Prędkość gracza
    public Vector2 velocity = new Vector2(0,0);

    //Długość "skóry"
    public float skinWidth = 0.1f;

    //Właściwości sprawdzania
    public float verticalRays = 3;
    public float horizontalRays = 5;
    public LayerMask ground;

    //Komponenty
    BoxCollider2D _boxCollider2d;

    //Dane o kolizjach
    [System.Serializable]
    public struct Collisions
    {
        public bool up, down, left, right;
    }

    [HideInInspector]
    public Collisions collisions;

    void Awake()
    {
        _boxCollider2d = GetComponent<BoxCollider2D>();
    }

    //Dostosowanie prędkości do kolizji
    void AdjustVerticalVelocity(ref Vector2 realVelocity, Bounds bounds)
    {
        //Oblicz kierunek poruszania się
        float verticalDirection = Mathf.Sign(realVelocity.y);

        if (realVelocity.y != 0)
        {
            collisions.up = false;
            collisions.down = false;
        }

        if (verticalDirection != 0)
        {
            //Oblicz położenie pierwszego raya
            Vector2 rayOrigin = (verticalDirection == 1) ? new Vector2(bounds.min.x, bounds.max.y) : new Vector2(bounds.min.x, bounds.min.y);

            //Oblicz przesunięcie raya
            Vector2 rayDistance = new Vector2(bounds.size.x / (verticalRays - 1), 0);

            //Raycastujemy verticalRays razy
            for (int currentRay = 0; currentRay != verticalRays; ++currentRay)
            {
                //Raycast
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * verticalDirection, Mathf.Abs(realVelocity.y) + skinWidth, ground.value);

                //Sprawdzanie, czy uderzyło
                if (hit.collider != null)
                {
                    //Dostosuj prawdziwą prędkość
                    if (hit.distance - skinWidth > 0.0001f) realVelocity.y = (hit.distance - skinWidth) * verticalDirection;
                    else
                    {
                        realVelocity.y = 0;
                        if (verticalDirection == 1) collisions.up = true;
                        else collisions.down = true;
                    }
                }

                //Rysowanie debugowego raya
                Debug.DrawRay(rayOrigin, Vector2.up * verticalDirection * (realVelocity.y + skinWidth), Color.green);

                //Przesuwanie
                rayOrigin += rayDistance;
            }
        }
    }

    //Dostosowanie prędkości do kolizji
    void AdjustHorizontalVelocity(ref Vector2 realVelocity, Bounds bounds)
    {
        //Oblicz kierunek poruszania się
        float horizontalDirection = Mathf.Sign(realVelocity.x);

        if (realVelocity.x != 0)
        {
            collisions.left = false;
            collisions.right = false;
        }

        if (horizontalDirection != 0)
        {
            //Oblicz położenie pierwszego raya
            Vector2 rayOrigin = (horizontalDirection == 1) ? new Vector2(bounds.max.x, bounds.min.y) : new Vector2(bounds.min.x, bounds.min.y);
            rayOrigin.y += realVelocity.y;

            //Oblicz przesunięcie raya
            Vector2 rayDistance = new Vector2(0, bounds.size.y / (horizontalRays - 1));

            //Raycastujemy verticalRays razy
            for (int currentRay = 0; currentRay != horizontalRays; ++currentRay)
            {
                //Raycast
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * horizontalDirection, Mathf.Abs(realVelocity.x) + skinWidth, ground.value);

                //Sprawdzanie, czy uderzyło
                if (hit.collider != null)
                {
                    //Dostosuj prawdziwą prędkość
                    if ((hit.distance - skinWidth) > 0.0001f) realVelocity.x = (hit.distance - skinWidth) * horizontalDirection;
                    else
                    {
                        realVelocity.x = 0;
                        if (horizontalDirection == 1) collisions.right = true;
                        else collisions.left = true;
                    }
                }

                //Rysowanie debugowego raya
                Debug.DrawRay(rayOrigin, Vector2.right * horizontalDirection * (realVelocity.x + skinWidth), Color.green);

                //Przesuwanie
                rayOrigin += rayDistance;
            }
        }
    }

    void FixedUpdate()
    {
            //Pobierz boundsy
            Bounds bounds = _boxCollider2d.bounds;

            //Zmniejsz boundsy o długość skóry
            bounds.Expand(-2 * skinWidth);

            //Prawdziwa prędkość - dostosowana do kolizji
            Vector2 realVelocity = velocity * Time.fixedDeltaTime;

            //Dostosuj prędkość do kolizji
            AdjustVerticalVelocity(ref realVelocity, bounds);
            AdjustHorizontalVelocity(ref realVelocity, bounds);

            //Dostosuj prędkość i przenieś obiekt
            velocity = realVelocity / Time.fixedDeltaTime;
            transform.Translate(realVelocity);
    }
}