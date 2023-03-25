using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisibilityChecker;

namespace Tests
{
    internal class LargeFileGenerator
    {
        const int partialSubelements = 900;
        const int visibleSubelements = 50;

        public static VisibilityResult GenerateRightAnswer(int n)
        {
            List<int> Visible = Enumerable.Range(5 * n, 3 * n).ToList();
            List<int> Partial = Enumerable.Range(2 * n, 3 * n).ToList();
            List<int> Invisible = Enumerable.Range(0, 2 * n).ToList();

            Partial.AddRange(Enumerable.Range(8 * n, partialSubelements + 1));
            Visible.Add(8 * n + partialSubelements + 1);

            return new VisibilityResult()
            {
                VisibleIds = Visible,
                PartiallyIds = Partial,
                InvisibleIds = Invisible,
            };
        }

        public static string CreateLargeFileIfNotExists(string path, string name, int n)
        {
            string newName = $"{name}_{n}.txt";
            string newPath = Path.Combine(path, newName);
            if (!File.Exists(newPath))
                GenerateLargeFile(newPath, n);
            return path + newName;
        }

        private static void GenerateLargeFile(string path, int n)
        {
            using FileStream fs = new(path, FileMode.Create);
            using StreamWriter w = new(fs, Encoding.Default);
            string viewport = "0 0 100 100";

            var currentIdx = AddSimpleElements(w, n);
            currentIdx = AddWithSubelements(w, currentIdx);
            w.WriteLine(viewport);
        }

        private static int AddSimpleElements(StreamWriter w, int n)
        {
            string invisible1 = "-1 -2 -2 1 1";
            string invisible2 = "-1 102 10 10 10";
            string partial1 = "-1 -1 -1 1 1";
            string partial2 = "-1 99 99 10 10";
            string partial3 = "-1 -10 10 120 10";
            string visible1 = "-1 0 0 1 1";
            string visible2 = "-1 99 99 1 1";
            string visible3 = "-1 1 1 98 95";


            for (int i = 0; i < n; i++)
                w.WriteLine(invisible1);
            for (int i = 0; i < n; i++)
                w.WriteLine(invisible2);

            for (int i = 0; i < n; i++)
                w.WriteLine(partial1);
            for (int i = 0; i < n; i++)
                w.WriteLine(partial2);
            for (int i = 0; i < n; i++)
                w.WriteLine(partial3);

            for (int i = 0; i < n; i++)
                w.WriteLine(visible1);
            for (int i = 0; i < n; i++)
                w.WriteLine(visible2);
            for (int i = 0; i < n; i++)
                w.WriteLine(visible3);
            return 8 * n;
        }

        private static int AddWithSubelements(StreamWriter w, int currentIdx)
        {
            int prev = currentIdx;
            w.WriteLine("-1 50 50 2000 2000");

            for (int i = 0; i < partialSubelements; i++)
            {
                w.WriteLine($"{prev} 50 50 {1500 - i} {1500 - i}");
                prev++;
            }

            for (int i = 0; i < visibleSubelements; i++)
            {
                w.WriteLine($"{prev} 50 50 {50 - i} {50 - i}");
                prev++;
            }
            return prev + 2;
        }
    }
}
