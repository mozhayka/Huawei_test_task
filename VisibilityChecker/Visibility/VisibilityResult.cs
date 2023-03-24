using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class VisibilityResult
    {
        public List<int> VisibleIds { get; set; }
        public List<int> PartiallyIds { get; set; }
        public List<int> InvisibleIds { get; set; }

        public VisibilityResult()
        {
            VisibleIds = new();
            PartiallyIds = new();
            InvisibleIds = new();
        }

        internal void Clear()
        {
            VisibleIds.Clear();
            PartiallyIds.Clear();
            InvisibleIds.Clear();
        }

        internal void Add(int Id, Visibility_ visibility)
        {
            switch (visibility)
            {
                case Visibility_.Partially:
                    PartiallyIds.Add(Id);
                    break;
                case Visibility_.Visible:
                    VisibleIds.Add(Id);
                    break;
                case Visibility_.Invisible:
                    InvisibleIds.Add(Id);
                    break;
            }
        }
    }
}
