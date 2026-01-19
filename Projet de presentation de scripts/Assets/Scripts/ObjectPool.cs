using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   public WeaponDataSO bulletSO;
   private Queue<GameObject> pool = new Queue<GameObject>();

   public GameObject GetObject()
   {
      if (pool.Count > 0)
      {
         GameObject obj = pool.Dequeue();
         obj.SetActive(true);
         return obj;
      }

      return Instantiate(bulletSO.prefab);
      
   }

   public void ReturnObject(GameObject obj)
   {
      if (obj != null)
      {
         obj.SetActive(false);
         pool.Enqueue(obj);
      }
      
   }
}
