using Mario_vNext.Core.Components;
using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext
{
    class Test
    {
        xList<I3Dimensional> thingsToCollide = new xList<I3Dimensional>();

        public Test()
        {
            thingsToCollide.Add(new Foo(thingsToCollide, "center"));
            thingsToCollide.Add(new Boo(thingsToCollide, "1", 0, 0, 0));
            //thingsToCollide.Add(new Boo(thingsToCollide, "2", 1, 0, 0));
            thingsToCollide.Add(new Boo(thingsToCollide, "3", 2, 0, 0));
            //thingsToCollide.Add(new Boo(thingsToCollide, "4", 0, 1, 0));
            //thingsToCollide.Add(new Boo(thingsToCollide, "5", 2, 1, 0));
            thingsToCollide.Add(new Boo(thingsToCollide, "6", 0, 2, 0));
            thingsToCollide.Add(new Boo(thingsToCollide, "7", 1, 2, 0));
            thingsToCollide.Add(new Boo(thingsToCollide, "8", 2, 2, 0));

            Debug.WriteLine(((Foo)thingsToCollide[0]).collider.DebugTestCollision());
        }
    }

    class Foo : I3Dimensional
    {
        public int depth { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Collider collider;

        string name;

        public Foo(xList<I3Dimensional> collidable, string name)
        {
            X = 1;
            Y = 1;
            Z = 1;

            width = 1;
            height = 1;
            depth = 1;

            this.name = name;
            collider = new Collider(this, collidable);
        }



        public override string ToString()
        {
            return "Foo_" + name;
        }
    }

    class Boo : Foo
    {
        public Boo(xList<I3Dimensional> collidable, string name, int x, int y, int z) : base(collidable, name)
        {
            X = x;
            Y = y;
            Z = z;

            width = 1;
            height = 1;
            depth = 1;
        }

        public override string ToString()
        {
            return "Boo";
        }
    }
}
