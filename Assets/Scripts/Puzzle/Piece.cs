using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Piece : MonoBehaviour
{
   public Transform[] Connectors;

   public Piece[] ConnectorConnections;

   [HideInInspector]
   public Layout Owner;
   
   public void Placed(Layout layoutOwner)
   {
      Owner = layoutOwner;
      ConnectorConnections = new Piece[Connectors.Length];
   }

   public void  Removed()
   {   
      if (ConnectorConnections != null)
      {
         for (int i = 0; i < ConnectorConnections.Length; ++i)
         {
            if (ConnectorConnections[i] != null)
            {
               var connectorProp = ConnectorConnections[i].ConnectorConnections;
               
               for (int k = 0; k < connectorProp.Length; ++k)
               {
                  var prop = connectorProp[k];
                  if (connectorProp[k] == this)
                  {
                     connectorProp[k] = null;
                  }
               }
            }
         }
      }

   }

}