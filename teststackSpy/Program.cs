using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
namespace teststackSpy
{
    static class TestMethod
    {
        public static List<AutomationElement> GetAllDescendants(this AutomationElement element, int depth = 0, int maxDepth = 3)
        {
            var allChildren = new List<AutomationElement>();

            if (depth > maxDepth)
            {
                return allChildren;
            }

            AutomationElement sibling = TreeWalker.RawViewWalker.GetFirstChild(element);

            while (sibling != null)
            {
                allChildren.Add(sibling);
                allChildren.AddRange(sibling.GetAllDescendants(depth + 1, maxDepth));
                sibling = TreeWalker.RawViewWalker.GetNextSibling(sibling);
            }

            return allChildren;
        }
    }
    class Program
    {
      
        static void Main(string[] args)
        {
          
            AutomationElement target=null;
            AutomationElementCollection automationCollection = AutomationElement.RootElement.FindAll(TreeScope.Children, Condition.TrueCondition);
            foreach(AutomationElement automation in automationCollection)
            {
                if (automation.Current.Name == "MainWindow")// sửa lại thành cửa sổ đang cần spy
                {
                    target = automation;
                    break;
                }
           
            }
            var automationlist=TestMethod.GetAllDescendants(target);
            foreach(AutomationElement a in automationlist)
            {
                Console.WriteLine(a.Current.AutomationId);
            }
        }
    }
}
