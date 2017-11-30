using Platformer.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using Platformer.Performance;

namespace Platformer.Factories
{
    public class HealthPointFactory : IEntityFactory
    {
        public static FlatRedBall.Math.Axis? SortAxis { get; set;}
        public static HealthPoint CreateNew (float x = 0, float y = 0)
        {
            return CreateNew(null, x, y);
        }
        public static HealthPoint CreateNew (Layer layer, float x = 0, float y = 0)
        {
            if (string.IsNullOrEmpty(mContentManagerName))
            {
                throw new System.Exception("You must first initialize the factory to use it. You can either add PositionedObjectList of type HealthPoint (the most common solution) or call Initialize in custom code");
            }
            HealthPoint instance = null;
            instance = new HealthPoint(mContentManagerName, false);
            instance.AddToManagers(layer);
            instance.X = x;
            instance.Y = y;
            if (mScreenListReference != null)
            {
                if (SortAxis == FlatRedBall.Math.Axis.X)
                {
                    var index = mScreenListReference.GetFirstAfter(x, Axis.X, 0, mScreenListReference.Count);
                    mScreenListReference.Insert(index, instance);
                }
                else if (SortAxis == FlatRedBall.Math.Axis.Y)
                {
                    var index = mScreenListReference.GetFirstAfter(y, Axis.Y, 0, mScreenListReference.Count);
                    mScreenListReference.Insert(index, instance);
                }
                else
                {
                    // Sort Z not supported
                    mScreenListReference.Add(instance);
                }
            }
            if (EntitySpawned != null)
            {
                EntitySpawned(instance);
            }
            return instance;
        }
        
        public static void Initialize (FlatRedBall.Math.PositionedObjectList<HealthPoint> listFromScreen, string contentManager)
        {
            mContentManagerName = contentManager;
            mScreenListReference = listFromScreen;
        }
        
        public static void Destroy ()
        {
            mContentManagerName = null;
            mScreenListReference = null;
            SortAxis = null;
            mPool.Clear();
            EntitySpawned = null;
        }
        
        private static void FactoryInitialize ()
        {
            const int numberToPreAllocate = 20;
            for (int i = 0; i < numberToPreAllocate; i++)
            {
                HealthPoint instance = new HealthPoint(mContentManagerName, false);
                mPool.AddToPool(instance);
            }
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (HealthPoint objectToMakeUnused)
        {
            MakeUnused(objectToMakeUnused, true);
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (HealthPoint objectToMakeUnused, bool callDestroy)
        {
            if (callDestroy)
            {
                objectToMakeUnused.Destroy();
            }
        }
        
        
            static string mContentManagerName;
            static PositionedObjectList<HealthPoint> mScreenListReference;
            static PoolList<HealthPoint> mPool = new PoolList<HealthPoint>();
            public static Action<HealthPoint> EntitySpawned;
            object IEntityFactory.CreateNew ()
            {
                return HealthPointFactory.CreateNew();
            }
            object IEntityFactory.CreateNew (Layer layer)
            {
                return HealthPointFactory.CreateNew(layer);
            }
            public static FlatRedBall.Math.PositionedObjectList<HealthPoint> ScreenListReference
            {
                get
                {
                    return mScreenListReference;
                }
                set
                {
                    mScreenListReference = value;
                }
            }
            static HealthPointFactory mSelf;
            public static HealthPointFactory Self
            {
                get
                {
                    if (mSelf == null)
                    {
                        mSelf = new HealthPointFactory();
                    }
                    return mSelf;
                }
            }
    }
}
