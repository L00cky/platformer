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

namespace Platformer.Entities
{
	public partial class HealthPoint
	{
        public static float Width { get; set; }

		private void CustomInitialize()
		{
            Width = this.SpriteInstance.Width;
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{
            this.SpriteInstance.Visible = false;
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
