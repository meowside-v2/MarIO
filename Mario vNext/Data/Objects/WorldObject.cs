using DKBasicEngine_1_0;
using Mario_vNext.Core.SystemExt;
using System;

namespace Mario_vNext.Data.Objects
{
    class WorldObject : GameObject
    {
        public bool IsSecret { get; set; }
        public bool IsBonus { get; set; }
        public bool IsDestroyable { get; set; }

        private ObjectDatabase.WorldObjects _type;

        public ObjectDatabase.WorldObjects Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
                BlockTypeSet(value);
            }
        }

        public WorldObject() { }

        public WorldObject(ObjectDatabase.WorldObjects type, int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Type = type;
        }

        private void BlockTypeSet(ObjectDatabase.WorldObjects type)
        {

            switch (type)
            {
                case ObjectDatabase.WorldObjects.BlockGround1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockGround1];
                    break;
                case ObjectDatabase.WorldObjects.BlockGround2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockGround2];
                    break;
                case ObjectDatabase.WorldObjects.BlockGround3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockGround3];
                    break;
                case ObjectDatabase.WorldObjects.BlockGround4:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockGround4];
                    break;
                case ObjectDatabase.WorldObjects.Bridge:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Bridge];
                    break;
                case ObjectDatabase.WorldObjects.Bush1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Bush1];
                    break;
                case ObjectDatabase.WorldObjects.Bush2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Bush2];
                    break;
                case ObjectDatabase.WorldObjects.Bush3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Bush3];
                    break;
                case ObjectDatabase.WorldObjects.BushSmall:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BushSmall];
                    break;
                case ObjectDatabase.WorldObjects.CastleBig:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.CastleBig];
                    break;
                case ObjectDatabase.WorldObjects.CastleSmall:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.CastleSmall];
                    break;
                case ObjectDatabase.WorldObjects.Cloud1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Cloud1];
                    break;
                case ObjectDatabase.WorldObjects.Cloud2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Cloud2];
                    break;
                case ObjectDatabase.WorldObjects.Cloud3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Cloud3];
                    break;
                case ObjectDatabase.WorldObjects.Fence:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Fence];
                    break;
                case ObjectDatabase.WorldObjects.FinishFlag:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.FinishFlag];
                    break;
                case ObjectDatabase.WorldObjects.FlagPole:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.FlagPole];
                    break;
                case ObjectDatabase.WorldObjects.Mountain:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Mountain];
                    break;
                case ObjectDatabase.WorldObjects.Sky:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Sky];
                    break;
                case ObjectDatabase.WorldObjects.Water1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Water1];
                    break;
                case ObjectDatabase.WorldObjects.Water2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Water2];
                    break;
                case ObjectDatabase.WorldObjects.BlockUnderGround1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockUnderGround1];
                    break;
                case ObjectDatabase.WorldObjects.BlockUnderGround2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockUnderGround2];
                    break;
                case ObjectDatabase.WorldObjects.BlockUnderGround3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockUnderGround3];
                    break;
                case ObjectDatabase.WorldObjects.BlockUnderGround4:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.BlockUnderGround4];
                    break;
                case ObjectDatabase.WorldObjects.UnderGroundBackground1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.UnderGroundBackground1];
                    break;
                case ObjectDatabase.WorldObjects.UnderGroundBackground2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.UnderGroundBackground2];
                    break;
                case ObjectDatabase.WorldObjects.Pipe1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Pipe1];
                    break;
                case ObjectDatabase.WorldObjects.Pipe2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Pipe2];
                    break;
                case ObjectDatabase.WorldObjects.Pipe3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Pipe3];
                    break;
                case ObjectDatabase.WorldObjects.Pipe4:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Pipe4];
                    break;
                case ObjectDatabase.WorldObjects.Pipe5:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Pipe5];
                    break;
                case ObjectDatabase.WorldObjects.Border:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = ObjectDatabase.worldObjectsMaterial[(int)ObjectDatabase.WorldObjects.Border];
                    break;
                default:
                    break;
            }
        }
    }
}
