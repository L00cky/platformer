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
namespace Platformer.Screens
{
    public partial class Menu : FlatRedBall.Screens.Screen
    {
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        
        private Platformer.Entities.MenuButtonExit MenuButtonExitInstance;
        private Platformer.Entities.MenuButtonPlay MenuButtonPlayInstance;
        public Menu ()
        	: base ("Menu")
        {
        }
        public override void Initialize (bool addToManagers)
        {
            LoadStaticContent(ContentManagerName);
            MenuButtonExitInstance = new Platformer.Entities.MenuButtonExit(ContentManagerName, false);
            MenuButtonExitInstance.Name = "MenuButtonExitInstance";
            MenuButtonPlayInstance = new Platformer.Entities.MenuButtonPlay(ContentManagerName, false);
            MenuButtonPlayInstance.Name = "MenuButtonPlayInstance";
            
            
            PostInitialize();
            base.Initialize(addToManagers);
            if (addToManagers)
            {
                AddToManagers();
            }
        }
        public override void AddToManagers ()
        {
            MenuButtonExitInstance.AddToManagers(mLayer);
            MenuButtonPlayInstance.AddToManagers(mLayer);
            base.AddToManagers();
            AddToManagersBottomUp();
            CustomInitialize();
        }
        public override void Activity (bool firstTimeCalled)
        {
            if (!IsPaused)
            {
                
                MenuButtonExitInstance.Activity();
                MenuButtonPlayInstance.Activity();
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
            
            if (MenuButtonExitInstance != null)
            {
                MenuButtonExitInstance.Destroy();
                MenuButtonExitInstance.Detach();
            }
            if (MenuButtonPlayInstance != null)
            {
                MenuButtonPlayInstance.Destroy();
                MenuButtonPlayInstance.Detach();
            }
            CustomDestroy();
        }
        public virtual void PostInitialize ()
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp ()
        {
            CameraSetup.ResetCamera(SpriteManager.Camera);
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers ()
        {
            MenuButtonExitInstance.RemoveFromManagers();
            MenuButtonPlayInstance.RemoveFromManagers();
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements)
        {
            if (callOnContainedElements)
            {
                MenuButtonExitInstance.AssignCustomVariables(true);
                MenuButtonPlayInstance.AssignCustomVariables(true);
            }
        }
        public virtual void ConvertToManuallyUpdated ()
        {
            MenuButtonExitInstance.ConvertToManuallyUpdated();
            MenuButtonPlayInstance.ConvertToManuallyUpdated();
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
            Platformer.Entities.MenuButtonExit.LoadStaticContent(contentManagerName);
            Platformer.Entities.MenuButtonPlay.LoadStaticContent(contentManagerName);
            CustomLoadStaticContent(contentManagerName);
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName)
        {
            return null;
        }
        public static object GetFile (string memberName)
        {
            return null;
        }
        object GetMember (string memberName)
        {
            return null;
        }
    }
}
