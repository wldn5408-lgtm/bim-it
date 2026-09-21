using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;


namespace yujan
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //string result = "Hello, Revit!";
            //int count = 0;
            //double sum = 0.0;
            //bool isActive = true;

            List<string> list = new List<string>();
            list.Add("대한");
            list.Add("민국");
            list.Add("만세");

            for(int i = 1; i < 10; i+=2)
            {
                Debug.Print(i.ToString());
            }

            //List<string> list1 = new List<string>();
            //list1.Add("대한AA");
            //list1.Add("민국BB");
            //list1.Add("만세CC");


            //foreach (string a in list)
            //{
            //    foreach (string item in list1)   
            //    {
            //        Debug.Print(a + "@" + item);
            //    }
            //}






            return Result.Succeeded;
        }
    }
}
