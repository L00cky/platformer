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
using FlatRedBall.Gui;

namespace Platformer.Screens
{
	public partial class Menu
	{
        public Cursor cursor;

		void CustomInitialize()
		{
            MenuButtonPlayInstance.Y = 100;
            MenuButtonExitInstance.Y = -100;

            cursor = GuiManager.Cursor;
            FlatRedBallServices.Game.IsMouseVisible = true;
        }

		void CustomActivity(bool firstTimeCalled)
		{
            if (MenuButtonPlayInstance.WasClickedThisFrame(cursor))
            {
                this.MoveToScreen(typeof(Level1));
            }

		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
