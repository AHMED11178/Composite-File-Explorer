using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_2_SWE_316
{
    public class Folder : Component
    {
        public override string Name { get; set; }
        private List<Component> Subcomponents { get; set; } = new List<Component>();

        public override long CalculateSize()
        {
            long totalSize = 0;
            foreach (var subcomponent in Subcomponents)
            {
                totalSize += subcomponent.CalculateSize();
            }
            return totalSize;
        }

        public override List<Component> GetSubcomponents()
        {
            return Subcomponents;
        }

        public void AddSubcomponent(Component subcomponent)
        {
            Subcomponents.Add(subcomponent);
        }

        public void RemoveSubcomponent(Component subcomponent)
        {
            Subcomponents.Remove(subcomponent);
        }

        public override string DisplayContentVirtically()
        {

            return DisplayContentRecursiveV(this, 3);
        }

        public string DisplayContentRecursiveV(Component component, int depth)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{new string(' ', depth * 5)}{component.Name} ({component.CalculateSize()} bytes)");
            result.AppendLine();

            foreach (Component subcomponent in component.GetSubcomponents())
            {
                if (subcomponent is Folder folder)
                {
                    result.Append(DisplayContentRecursiveV(folder, depth + 5)); // Recursive call for subfolders
                }
                else if (subcomponent is File file)
                {
                    result.AppendLine($"{new string(' ', (depth + 8) * 4)}{file.Name} ({file.Size} bytes)");
                }

                result.AppendLine();
            }

            return result.ToString();
        }

        public override string DisplayContentHorozantally()
        {
            return DisplayContentRecursiveH(this, 3);
        }

        public string DisplayContentRecursiveH(Component component, int depth)
        {
            StringBuilder result = new StringBuilder();

            if (depth > 0)
            {
                result.Append(new string(' ', depth + 2)); // Add indentation for subcomponents
            }

            result.Append($"{component.Name} ({component.CalculateSize()} bytes)");

            foreach (Component subcomponent in component.GetSubcomponents())
            {
                if (subcomponent is Folder folder)
                {
                    result.Append(DisplayContentRecursiveH(folder, depth + 1)); // Recursive call for subfolders
                }
                else if (subcomponent is File file)
                {
                    result.Append($"{file.Name} ({file.Size} bytes)");
                }
            }

            return result.ToString();
        }



    }
}
