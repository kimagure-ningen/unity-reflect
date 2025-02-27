using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Utility
{
    public class PhysicsUtil
    {
        public static List<Vector3> RefrectionLinePoses(Vector3 position, Vector3 direction, float length, LayerMask layerMask)
        {
            var points = new List<Vector3>() { position };
            while (Physics.Raycast(position, direction, out var hit, length, layerMask))
            {
                if (points.Count < 10f)
                {
                    position = hit.point;
                    points.Add(position);
                    // Debug.Log(position);
                    length -= hit.distance;
                    // Debug.Log(direction);
                
                    if (hit.collider.tag == "Enemy")
                    {
                        // Debug.Log(hit.collider.gameObject.name);
                        hit.collider.gameObject.GetComponent<Enemy>().Damage();
                    
                    } else if (hit.collider.tag == "Ice")
                    {
                        hit.collider.gameObject.GetComponent<IceBlock>().Melt();
                        hit.collider.gameObject.GetComponent<FragmentEffect>().EmmitFragment(position);
                        return points;
                    } else if (hit.collider.tag == "Wall" || hit.collider.CompareTag("Ground") || hit.collider.CompareTag("SideWall"))
                    {
                        hit.collider.gameObject.GetComponent<FragmentEffect>().EmmitFragment(position);
                    } else if (hit.collider.CompareTag("Orc"))
                    {
                        hit.collider.gameObject.GetComponent<Orc>().Damage();
                    }
                
                    Vector3 ranDir = new Vector3 (direction.x, direction.y + 2.5f, direction.z);
                    direction = Vector3.Reflect(ranDir, hit.normal); // direction 変える
                    // Debug.Log(hit.collider.gameObject.name);
                }
                else
                {
                    break;
                }
            }
            points.Add(position + direction * length);
            return points;
        }
        public static List<Vector3> RefrectionLinePoses(Vector3 position, float radius, Vector3 direction, float length, LayerMask layerMask)
        {
            var points = new List<Vector3>() { position };
            while (Physics.SphereCast(position, radius, direction, out var hit, length, layerMask))
            {
                position = hit.point + hit.normal * radius;
                points.Add(position);
                length -= hit.distance;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            points.Add(position + direction * length);
            return points;
        }
    }
}