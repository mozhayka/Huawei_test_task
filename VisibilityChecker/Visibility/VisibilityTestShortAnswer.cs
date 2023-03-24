using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class VisibilityTestShortAnswer
    {
        public List<int> VisibleIds { get; set; }
        public List<int> PartiallyIds { get; set; }
        public List<int> InvisibleIds { get; set; }

        public VisibilityTestShortAnswer()
        {
            VisibleIds = new();
            PartiallyIds = new();
            InvisibleIds = new();
        }
    }

    //public class VisibilityTestFullAnswer
    //{
    //    public List<int> VisibleIds { get; set; }
    //    public List<int> PartiallyIds { get; set; }
    //    public List<int> InvisibleIds { get; set; }

    //    public VisibilityTestFullAnswer()
    //    {
    //        VisibleIds = new();
    //        PartiallyIds = new();
    //        InvisibleIds = new();
    //    }
    //}
}
