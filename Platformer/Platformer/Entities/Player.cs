using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework.Input;
using Platformer.Screens;
using FlatRedBall.Debugging;
using Platformer.Factories;

namespace Platformer.Entities
{
    public partial class Player
    {
        private I2DInput MovementInput { get; set; }
        private IPressableInput Escape { get; set; }
        private IPressableInput Jump { get; set; }

        private AnimationChainList PlayerAnimations { get; set; }

        private double mJumpPushTime { get; set; }
        private float JumpVelocity { get; set; }
        private double MaxVelocityApplyTime { get; set; }
        private bool FlipTexture { get; set; }

        public float Mass { get; set; }
        public bool ApplyGravity { get; set; }
        public bool CanJump { get; set; }
        public bool CanClimb { get; set; }
        public bool RecentlyDamaged { get; internal set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public int Health { get; set; }
        public List<HealthPoint> HealthList { get; set; }

        public enum State
        {
            STATE_IDLE,
            STATE_JUMPING,
            STATE_RUNNING,
            STATE_FALLING,
            STATE_CLIMB,
            STATE_CLIMB_IDLE,
            STATE_HIT
        };

        public State currentState { get; set; }

        private void CustomInitialize()
        {
            this.Z = 10;
            this.X = 20;
            this.Y = -454;

            Health = 3;

            currentState = State.STATE_IDLE;

            Width = SpriteInstance.Width;
            Height = SpriteInstance.Height;

            Mass = 100.0f;

            CanJump = true;
            JumpVelocity = 150.0f;
            MaxVelocityApplyTime = 0.01;
            FlipTexture = false;
            CanClimb = false;

            HealthList = new List<HealthPoint>();

            SetAnimations();
            RenderHealth();

            AssignInput();
        }

        private void RenderHealth()
        {
            // Render Health
            var offset = 1.0f;
            for (var i = 0; i < Health; i++)
            {
                var hp = HealthPointFactory.CreateNew((i * HealthPoint.Width) + offset, 20);
                HealthList.Add(hp);
            }
        }

        private void AssignInput()
        {
            MovementInput = InputManager.Keyboard.Get2DInput(Keys.A, Keys.D, Keys.W, Keys.S);
            Escape = InputManager.Keyboard.GetKey(Keys.Escape);
            Jump = InputManager.Keyboard.GetKey(Keys.Space);
        }

        private void SetAnimations()
        {
            // Load animation file through content pipeline
            PlayerAnimations = FlatRedBallServices.Load<AnimationChainList>(@"Content/Entities/Player/AnimationChainListFile.achx", "Global");

            SpriteInstance.AnimationChains = PlayerAnimations;
            SpriteInstance.Animate = true;
        }

        private void CustomActivity()
        {
            HandleInput();
            SpriteInstance.FlipHorizontal = FlipTexture;
        }

        private void HandleInput()
        {
            if (MovementInput.X > 0)
            {
                FlipTexture = false;
            }
            else if (MovementInput.X < 0)
            {
                FlipTexture = true;
            }

            switch (currentState)
            {
                case State.STATE_IDLE:
                    SpriteInstance.CurrentChainName = "Idle";
                    this.XVelocity = MovementInput.X * MovementSpeed;
                    if (MovementInput.X > 0)
                    {
                        currentState = State.STATE_RUNNING;
                    }
                    else if (MovementInput.X < 0)
                    {
                        currentState = State.STATE_RUNNING;
                    }

                    if (Jump.WasJustPressed)
                    {
                        mJumpPushTime = TimeManager.CurrentTime;
                        this.YVelocity = JumpVelocity;
                        currentState = State.STATE_JUMPING;
                    }

                    if(!CanClimb && YVelocity < 0)
                    {
                        
                    }

                    break;
                case State.STATE_RUNNING:
                    SpriteInstance.CurrentChainName = "Run";
                    this.XVelocity = MovementInput.X * MovementSpeed;
                    if (MovementInput.X == 0)
                    {
                        currentState = State.STATE_IDLE;
                    }

                    if (Jump.WasJustPressed)
                    {
                        mJumpPushTime = TimeManager.CurrentTime;
                        this.YVelocity = JumpVelocity;
                        currentState = State.STATE_JUMPING;
                    }
                    break;
                case State.STATE_JUMPING:
                    SpriteInstance.CurrentChainName = "Jump";
                    this.XVelocity = MovementInput.X * MovementSpeed;
                    if (SpriteInstance.JustCycled)
                    {
                        currentState = State.STATE_FALLING;
                    }
                    break;
                case State.STATE_FALLING:
                    SpriteInstance.CurrentChainName = "Fall";
                    this.YVelocity = -JumpVelocity;
                    this.XVelocity = MovementInput.X * MovementSpeed;
                    if (Level1.collisionCollection.CollideAgainst(this.AxisAlignedRectangleInstance))
                    {
                        currentState = State.STATE_IDLE;
                    }
                    break;
                case State.STATE_CLIMB:
                    if (CanClimb)
                    {
                        SpriteInstance.CurrentChainName = "Climb";
                        XVelocity = 0;
                        YVelocity = 0;
                        this.YVelocity = MovementInput.Y * MovementSpeed;
                        this.XVelocity = MovementInput.X * MovementSpeed;

                        if (Jump.WasJustPressed)
                        {
                            mJumpPushTime = TimeManager.CurrentTime;
                            this.YVelocity = JumpVelocity;
                            currentState = State.STATE_JUMPING;
                        }

                        if (YVelocity == 0)
                        {
                            currentState = State.STATE_CLIMB_IDLE;
                        }
                    }
                    else
                    {
                        currentState = State.STATE_FALLING;
                    }

                    break;
                case State.STATE_CLIMB_IDLE:
                    if (CanClimb)
                    {
                        SpriteInstance.CurrentChainName = "ClimbIdle";
                        this.YVelocity = MovementInput.Y * MovementSpeed;
                        this.XVelocity = MovementInput.X * MovementSpeed;

                        if (Jump.WasJustPressed)
                        {
                            mJumpPushTime = TimeManager.CurrentTime;
                            this.YVelocity = JumpVelocity;
                            currentState = State.STATE_JUMPING;
                        }

                        if (YVelocity != 0)
                        {
                            currentState = State.STATE_CLIMB;
                        }
                    }
                    else
                    {
                        currentState = State.STATE_FALLING;
                    }

                    break;
                case State.STATE_HIT:
                    SpriteInstance.CurrentChainName = "Hit";
                    break;
            }



            //QuitGame
            if (Escape.WasJustPressed)
            {
                FlatRedBallServices.Game.Exit();
            }
        }

        private void CustomDestroy()
        {


        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void DoDamage()
        {
            Health--;
            if(Health > 0)
            {
                var hpToDestroy = HealthList[Health - 1];
                hpToDestroy.Destroy();
                this.RecentlyDamaged = true;
                currentState = State.STATE_HIT;
            }
        }
    }
}
