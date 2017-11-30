#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using Color = Microsoft.Xna.Framework.Color;
using Platformer.Entities;
using Platformer.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math;
namespace Platformer.Screens
{
    public partial class Level1 : FlatRedBall.Screens.Screen
    {
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        protected static FlatRedBall.TileGraphics.LayeredTileMap PlatformerLevel1;
        
        private Platformer.Entities.Player PlayerInstance;
        private FlatRedBall.Math.PositionedObjectList<Platformer.Entities.HealthPoint> PositionedObjectListInstance;
        public string HealthList;
        public Level1 ()
        	: base ("Level1")
        {
        }
        public override void Initialize (bool addToManagers)
        {
            LoadStaticContent(ContentManagerName);
            PlayerInstance = new Platformer.Entities.Player(ContentManagerName, false);
            PlayerInstance.Name = "PlayerInstance";
            PositionedObjectListInstance = new FlatRedBall.Math.PositionedObjectList<Platformer.Entities.HealthPoint>();
            PositionedObjectListInstance.Name = "PositionedObjectListInstance";
            
            
            PostInitialize();
            base.Initialize(addToManagers);
            if (addToManagers)
            {
                AddToManagers();
            }
        }
        public override void AddToManagers ()
        {
            PlatformerLevel1.AddToManagers(mLayer);
            HealthPointFactory.Initialize(PositionedObjectListInstance, ContentManagerName);
            PlayerInstance.AddToManagers(mLayer);
            base.AddToManagers();
            AddToManagersBottomUp();
            CustomInitialize();
        }
        public override void Activity (bool firstTimeCalled)
        {
            if (!IsPaused)
            {
                
                PlayerInstance.Activity();
                for (int i = PositionedObjectListInstance.Count - 1; i > -1; i--)
                {
                    if (i < PositionedObjectListInstance.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        PositionedObjectListInstance[i].Activity();
                    }
                }
            }
            else
            {
            }
            base.Activity(firstTimeCalled);
            if (!IsActivityFinished)
            {
                CustomActivity(firstTimeCalled);
            }
        }
        public override void Destroy ()
        {
            base.Destroy();
            HealthPointFactory.Destroy();
            PlatformerLevel1.Destroy();
            PlatformerLevel1 = null;
            
            PositionedObjectListInstance.MakeOneWay();
            if (PlayerInstance != null)
            {
                PlayerInstance.Destroy();
                PlayerInstance.Detach();
            }
            for (int i = PositionedObjectListInstance.Count - 1; i > -1; i--)
            {
                PositionedObjectListInstance[i].Destroy();
            }
            PositionedObjectListInstance.MakeTwoWay();
            CustomDestroy();
        }
        public virtual void PostInitialize ()
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            PlayerInstance.MovementSpeed = 100f;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp ()
        {
            CameraSetup.ResetCamera(SpriteManager.Camera);
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers ()
        {
            PlayerInstance.RemoveFromManagers();
            for (int i = PositionedObjectListInstance.Count - 1; i > -1; i--)
            {
                PositionedObjectListInstance[i].Destroy();
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements)
        {
            if (callOnContainedElements)
            {
                PlayerInstance.AssignCustomVariables(true);
            }
            PlayerInstance.MovementSpeed = 100f;
        }
        public virtual void ConvertToManuallyUpdated ()
        {
            PlayerInstance.ConvertToManuallyUpdated();
            for (int i = 0; i < PositionedObjectListInstance.Count; i++)
            {
                PositionedObjectListInstance[i].ConvertToManuallyUpdated();
            }
        }
        public static void LoadStaticContent (string contentManagerName)
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
            #if DEBUG
            if (contentManagerName == FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                HasBeenLoadedWithGlobalContentManager = true;
            }
            else if (HasBeenLoadedWithGlobalContentManager)
            {
                throw new System.Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
            }
            #endif
            PlatformerLevel1 = FlatRedBall.TileGraphics.LayeredTileMap.FromTiledMapSave("content/screens/level1/platformerlevel1.tmx", contentManagerName);
            Platformer.Entities.Player.LoadStaticContent(contentManagerName);
            CustomLoadStaticContent(contentManagerName);
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName)
        {
            switch(memberName)
            {
                case  "PlatformerLevel1":
                    return PlatformerLevel1;
            }
            return null;
        }
        public static object GetFile (string memberName)
        {
            switch(memberName)
            {
                case  "PlatformerLevel1":
                    return PlatformerLevel1;
            }
            return null;
        }
        object GetMember (string memberName)
        {
            switch(memberName)
            {
                case  "PlatformerLevel1":
                    return PlatformerLevel1;
            }
            return null;
        }
    }
}
