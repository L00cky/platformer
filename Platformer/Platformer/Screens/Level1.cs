using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Localization;
using FlatRedBall.TileCollisions;

using Microsoft.Xna.Framework.Input;
using FlatRedBall.Debugging;

namespace Platformer.Screens
{
    public partial class Level1
    {
        

        private float Gravity { get; set; }

        private IPressableInput CameraUp { get; set; }
        private IPressableInput CameraDown { get; set; }

        // Collision lists
        public static TileShapeCollection collisionCollection { get; set; }
        public static TileShapeCollection climbableCollection { get; set; }
        public static TileShapeCollection damagingCollection { get; set; }

        public int points { get; set; }

        void CustomInitialize()
        {
            Camera.Main.X = PlatformerLevel1.Width / 2;
            Camera.Main.Y = -1 * (PlatformerLevel1.Height / 2);

            points = 0;

            Gravity = -9.81f * 20;

            AssignInput();
            AddCollisions();
        }

        private void AssignInput()
        {
            CameraUp = InputManager.Keyboard.GetKey(Keys.Up);
            CameraDown = InputManager.Keyboard.GetKey(Keys.Down);
        }

        private void AddCollisions()
        {
            collisionCollection = new TileShapeCollection();
            collisionCollection.Visible = true;
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass1");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass2");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass3");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass4");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass5");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass6");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass7");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass8");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass9");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass10");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass11");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Grass12");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock1");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock2");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock3");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock4");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock5");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock6");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock7");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Rock8");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "WoodPlank1");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "WoodPlank2");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "WoodPlank3");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "WoodPlank4");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "WoodPlank5");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "WoodPlank6");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree1");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree2");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree3");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree4");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree5");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree6");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree7");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree8");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree9");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree10");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree11");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree12");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree13");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree14");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree15");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree16");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree17");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree18");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree19");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree20");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree21");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree22");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree23");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree24");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree25");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree26");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree27");
            collisionCollection.AddCollisionFrom(PlatformerLevel1, "Tree28");          

            climbableCollection = new TileShapeCollection();
            climbableCollection.Visible = true;
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine1");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine2");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine3");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine4");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine5");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine6");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine7");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine8");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine9");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine10");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine11");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine12");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine13");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine14");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine15");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine16");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine17");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine18");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine19");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine20");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine21");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine22");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine23");
            climbableCollection.AddCollisionFrom(PlatformerLevel1, "Vine24");

            damagingCollection = new TileShapeCollection();
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike1");
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike2");
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike3");
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike4");
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike5");
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike6");
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike7");
            damagingCollection.AddCollisionFrom(PlatformerLevel1, "Spike8");
        }

        void CustomActivity(bool firstTimeCalled)
        {
            HandleInput();

            //ApplyGravity();

            HandleCollisons();

            if (CameraUp.WasJustPressed)
                Debugger.CommandLineWrite(String.Format("X: {0}, Y: {1}", PlayerInstance.X, PlayerInstance.Y));
        }

        private void HandleCollisons()
        {
            if (!PlayerInstance.CanClimb)
            {
                foreach (var collison in collisionCollection.Rectangles)
                {
                    var thisMass = 10;
                    var otherMass = 0;
                    collison.CollideAgainstMove(PlayerInstance.AxisAlignedRectangleInstance, thisMass, otherMass);
                }                
            }
            else
            {
                foreach (var collison in collisionCollection.Rectangles)
                {
                    if (collison.CollideAgainst(PlayerInstance.AxisAlignedRectangleInstance))
                    {
                        PlayerInstance.Y = (collison.Y + collison.Height / 2) + PlayerInstance.Height / 2;
                        PlayerInstance.currentState = Entities.Player.State.STATE_IDLE;

                        PlayerInstance.CanJump = true;
                    }
                    else
                    {
                        PlayerInstance.CanJump = false;
                    }

                    
                }
            }
            
            foreach(var climbableCollision in climbableCollection.Rectangles)
            {
                if (climbableCollection.CollideAgainst(PlayerInstance.AxisAlignedRectangleInstance))
                {
                    if(PlayerInstance.YVelocity != 0)
                    {
                        PlayerInstance.currentState = Entities.Player.State.STATE_CLIMB;
                    }
                    else
                    {
                        PlayerInstance.currentState = Entities.Player.State.STATE_CLIMB_IDLE;
                    }

                    PlayerInstance.CanClimb = true;
                }
                else
                {
                    PlayerInstance.CanClimb = false;
                }
            }

            foreach(var damageCollision in damagingCollection.Rectangles)
            {
                if (damageCollision.CollideAgainst(PlayerInstance.AxisAlignedRectangleInstance))
                {
                    if (!PlayerInstance.RecentlyDamaged)
                    {
                        PlayerInstance.DoDamage();
                    }
                }
                else
                {
                    if (PlayerInstance.RecentlyDamaged)
                        PlayerInstance.RecentlyDamaged = false;
                }
            }
        }

        private void ApplyGravity()
        {
            PlayerInstance.YAcceleration = Gravity * PlayerInstance.Mass;
        }

        private void HandleInput()
        {
            if (CameraUp.WasJustPressed)
            {
                var x = PlayerInstance.X;
                var y = PlayerInstance.Y;

                Debugger.CommandLineWrite(String.Format("X: {0}, Y: {1}", x, y));
            }

            if (CameraUp.IsDown)
            {
                Debugger.ClearCommandLine();
            }
        }

        void CustomDestroy()
        {
            collisionCollection.RemoveFromManagers();
        }

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

    }
}
