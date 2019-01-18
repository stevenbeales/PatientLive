using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class VariableGridView : GridView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var viewModel = item as HubTile;

            element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, viewModel.ColSpan);

            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
