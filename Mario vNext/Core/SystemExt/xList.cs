using Mario_vNext.Core.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Mario_vNext.Core.SystemExt
{
    class xList<T> : List<T>, ICore
    {
        public xList() { }

        public xList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public object DeepCopy()
        {
            xList<T> retValue = new xList<T>();

            foreach (T item in this.ToList())
            {
                retValue.Add((T)(item as ICore).DeepCopy());
            }

            return (xList<T>)retValue;
        }

        public void Render(int x, int y, byte[] imageBuffer)
        {
            var temp = this.Where(item => Finder((I3Dimensional)item, x, y)).Select(item => item).ToList();

            while (true)
            {
                if (temp.Count == 0) return;

                int tempHeight = temp.Max(item => ((I3Dimensional)item).Z);
                var toRender = temp.Where(item => ((I3Dimensional)item).Z == tempHeight).Select(item => item).ToList();

                List<Task> awaitTaskList = new List<Task>();

                foreach (ICore item in toRender)
                {
                    awaitTaskList.Add(Task.Factory.StartNew(() => item.Render(x, y, imageBuffer)));
                }

                Task.WaitAll(awaitTaskList.ToArray());

                temp.RemoveAll(item => toRender.FirstOrDefault(item2 => ReferenceEquals(item, item2)) != null);

                /*T tmp;

                if (temp.Count == 0) return null;
                else if (temp.Count == 1) tmp = temp[0];
                else tmp = temp.Aggregate((t1, t2) => FindBiggerZ(t1 as I3Dimensional, t2 as I3Dimensional) ? t1 : t2);

                Color? tmpColor = (tmp as ICore).RenderPixel(x, y);

                temp.Remove(tmp);

                if (tmpColor != null) return tmpColor;*/
                //a.RemoveAll(ic => b.FirstOrDefault(ic2 => ReferenceEquals(ic, ic2)) != null);
            }
        }

        private bool Finder(I3Dimensional obj, int x, int y)
        {
            return (obj.X + obj.width >= x && obj.X < x + Shared.RenderWidth && obj.Y + obj.height >= y && obj.Y < y + Shared.RenderHeight);
        }

        private bool FindBiggerZ(I3Dimensional item1, I3Dimensional item2)
        {
            return (item1.Z > item2.Z);
        }
    }
}
