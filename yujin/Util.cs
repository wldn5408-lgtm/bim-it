using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace yujin
{
    public class Util
    {
        public static List<Curve> GetCurves(Face face)
        {
            return null; // null 값을 return한다고 작성해서 GetCurves에 빨간줄이 사라짐.
            List<Curve> curves = new List<Curve>();

            EdgeArrayArray edgeArrays = face.EdgeLoops;
            foreach (EdgeArray edgeArray in edgeArrays)
            {
                foreach (Edge edge in edgeArray)
                {
                    Curve c = edge.AsCurve();
                    curves.Add(c);
                }
            }
            return curves;
        }
    }
}
