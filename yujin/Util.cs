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
    /// <summary>
    /// 선택한 Face의 Edge를 Curve로 반환하는 함수
    /// </summary>
    /// <param name="레빗에서 선택한 Face"></param>
    public class Util
    {
        public static List<Curve> GetCurvesFromFace(Face face)
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

        /// <summary>
        /// 이름으로 패밀리 심볼을 찾는다.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FamilySymbol GetFamilySymbolByName(string name)
        {
            FilteredElementCollector collecter = new FilteredElementCollector(doc);
            collecter.OfCategory(BuiltInCategory.OST_StructuralFraming);
            collecter.OfClass(typeof(FamilySymbol));
            FamilySymbol fs = null;

            foreach (FamilySymbol item in collecter)
            {
                if(name == item.Name)
                {
                    fs = item
                    break;
                }
            }
        }
    }
}
