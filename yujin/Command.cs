using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;  
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Structure;

namespace yujin
{
    [Transaction(TransactionMode.Manual)]   
    public class Command : IExternalCommand
    {
        // Execute가 주문서 이름이다.
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // 단축키를 만든다.
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;
            // 참조 r은 변수(메모리 저장소의 주소)
            //Reference r = uiDoc.Selection.PickObject(ObjectType.Element, "객체를 선택하세요");
            //Element e = doc.GetElement(r); // 선택한 객체를 가져온다.

            IList<Reference> refs = uiDoc.Selection.PickObjects(ObjectType.Edge, "객체를 선택하세요");
            Face face = doc.GetElement(refs[0]).GetGeometryObjectFromReference(refs[0]) as Face;

            List<Curve> dd = Util.GetCurves(face);
            //던져주고 받으려면 메모리 공간이 필요하므로 메모리 공간을 만든거임. 그 방 이름이 dd임. 함수를 부르는 방법. =은 대입을 의미함.






            //List<Curve> curves = new List<Curve>();
            //  foreach (Reference item in refs) ;
            //    {
            //        Edge edge = doc.GetElement(refs[0]).GetGeometryObjectFromReference(refs[0]) as Edge; // edge를 선택한다. edge의 부모로부터 레퍼런스한다.

            //Curve c = edge.AsCurve(); //curve로 변환한다.
            //    }
            FilteredElementCollector collecter = new FilteredElementCollector(doc);
            collecter.OfCategory(BuiltInCategory.OST_StructuralFraming);
            collecter.OfClass(typeof(FamilySymbol));

            FamilySymbol fs = collecter.FirstElement() as FamilySymbol;
            if(tt == null)
            {
                Autodesk.Revit.UI.TaskDialog.Show("오류", "해당이름의 패밀리 심볼을 찾지 못했습니다.");
                return Result.Failed;
            }

            FamilySymbol tt = Util.GetFamilySymbolByName("G1", doc);



            Level level = doc.ActiveView.GenLevel;

            foreach (Curve c in dd)
            {
                using (Transaction trans = new Transaction(doc, "create beam"))
                {
                    trans.Start();
                    tt.Activate();
                    FamilyInstance fi = doc.Create.NewFamilyInstance(c, fs, level, StructuralType.Beam);
                    trans.Commit();
                }
            }

            //string a = param.get;

            //Autodesk.Revit.Ui.TaskDialog.Show("코멘트의 정보는 : ", a);


            return Result.Succeeded;
        }
    }
}
