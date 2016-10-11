using Mario_vNext.Core.Components;
using Mario_vNext.Core.SystemExt;
using System;
using System.Windows.Forms;

namespace Mario_vNext.Data.Objects
{
    class Block : ObjectCore
    {
        public bool IsSecret { get; set; }
        public bool IsBonus { get; set; }
        public bool IsDestroyable { get; set; }

        private ObjectDatabase.Blocks _type;

        public ObjectDatabase.Blocks Type
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

        public Block() { }

        public Block(ObjectDatabase.Blocks type, int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Type = type;
        }

        private void BlockTypeSet(ObjectDatabase.Blocks type)
        {

            switch (type)
            {
                case ObjectDatabase.Blocks.BlockGround1:
                    IsSecret = false;
                    IsBonus = true;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockGround1]);

                    break;

                case ObjectDatabase.Blocks.BlockGround2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockGround2]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockGround2, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.BlockGround3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockGround3]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockGround3, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.BlockGround4:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = true;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockGround4]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockGround4, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Sky1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Sky1]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky1, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Sky2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Sky2]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky2, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Sky3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Sky3]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky3, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Sky4:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Sky4]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky4, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Sky5:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Sky5]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky5, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Sky6:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Sky6]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky6, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Sky7:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Sky7]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Sky7, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.BlockUnderGround1:
                    IsSecret = false;
                    IsBonus = true;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockUnderGround1]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround1, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.BlockUnderGround2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockUnderGround2]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround2, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.BlockUnderGround3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockUnderGround3]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround3, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.BlockUnderGround4:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.BlockUnderGround4]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.BlockUnderGround4, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.UnderGroundBackground1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.UnderGroundBackground1]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.UnderGroundBackground1, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.UnderGroundBackground2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.UnderGroundBackground2]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.UnderGroundBackground2, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Pipe1:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Pipe1]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe1, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Pipe2:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Pipe2]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe2, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Pipe3:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Pipe3]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe3, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Pipe4:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Pipe4]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe4, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Pipe5:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Pipe5]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Pipe5, ObjectDatabase.path_block));
                    break;

                case ObjectDatabase.Blocks.Border:
                    IsSecret = false;
                    IsBonus = false;
                    IsDestroyable = false;

                    model = new Material(this, ObjectDatabase.blockMesh[(int)ObjectDatabase.Blocks.Border]); //new Material(ImageLoader.Load(ObjectDatabase.Blocks.Border, ObjectDatabase.path_block));
                    break;

                default:
                    MessageBox.Show("No such block in database", "Out of Range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-1);
                    break;
            }
        }
    }
}
