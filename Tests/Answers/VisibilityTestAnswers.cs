using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisibilityChecker;

namespace Tests.Answers
{
    internal class VisibilityTestAnswers
    {
        public static VisibilityResult Test1Answer = new() {
            VisibleIds = new List<int>() { 0 },
            PartiallyIds = new List<int>(), 
            InvisibleIds = new List<int>(), 
        };

        public static VisibilityResult Test2Answer = new()
        {
            VisibleIds = new List<int>() { 2 },
            PartiallyIds = new List<int>() { 1 },
            InvisibleIds = new List<int>() { 0 },
        };

        public static bool CompareAnswers(VisibilityResult calculated, VisibilityResult correct)
        {
            bool IsVisibleTheSame = Enumerable.SequenceEqual(calculated.VisibleIds, correct.VisibleIds);
            bool IsPartialTheSame = Enumerable.SequenceEqual(calculated.PartiallyIds, correct.PartiallyIds);
            bool IsInvisibleTheSame = Enumerable.SequenceEqual(calculated.InvisibleIds,correct.InvisibleIds);
            return IsVisibleTheSame && IsPartialTheSame && IsInvisibleTheSame;
        }
    }
}
