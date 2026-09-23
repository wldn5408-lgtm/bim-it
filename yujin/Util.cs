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
using Microsoft.VisualBasic;
using Autodesk.Revit.DB.Structure;

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
        public static FamilySymbol GetFamilySymbolByName(string name, Document doc)
        {
            FilteredElementCollector collecter = new FilteredElementCollector(doc);
            collecter.OfCategory(BuiltInCategory.OST_StructuralFraming);
            collecter.OfClass(typeof(FamilySymbol));
            FamilySymbol fs = null;

            foreach (FamilySymbol item in collecter)
            {
                if(name == item.Name)
                {
                    fs = item;
                    break;
                }
            }
        }

        /// <summary>
        /// XYZ 좌표 리스트를 받아서 Curve 리스트로 반환하는 함수
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Curve> GetCurveListFromPts(List<XYZ> points)
        {
            List<Curve> curves = new List<Curve>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                Line line = Line.CreateBound(points[i], points[i + 1]);
                Curve.Add(line);
            }

                return curves;
        }


        public static void CreateFamilyInstanceFromCurve(List<Curve> c, FamilySymbol fs, Level level, Document doc) // 보이드는 리턴받지 않겠다는 뜻.
        {
            foreach (Curve item in c)
            {
                using (Transaction trans = new Transaction(doc, "Create Beam"))
                {
                    trans.Start();
                    fs.Activate();
                    FamilyInstance fi = doc.Create.NewFamilyInstance
                        (item, fs, level, StructuralType.Beam);
                    trans.Commit();
                }
            }
        }
    }
}
