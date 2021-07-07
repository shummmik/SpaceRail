using System;
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

   public MeshFilter meshFilter;


   private void Start()
   {
      meshFilter = GetComponent<MeshFilter>();
   }

   public void Placed(Layout layoutOwner)
   {
      Owner = layoutOwner;
      ConnectorConnections = new Piece[Connectors.Length];
   }

   public void  Removed()
   {   
      if (ConnectorConnections != null)
      {
         foreach (var connectorConnection in ConnectorConnections)
         {
            if (connectorConnection != null)
            {
               var connectorProp = connectorConnection.ConnectorConnections;
               
               for (int k = 0; k < connectorProp.Length; ++k)
               {
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