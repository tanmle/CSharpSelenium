using System;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class RadaPage : ControlFactory
    {
        public bool DoesAssessmentRadaDisplay(string assessment)
        {
            Control<Label>("dynamicRadaNameLabel", null, assessment).WaitForControlDisplay(40);
            return Control<Label>("dynamicRadaNameLabel", null, assessment).Exists;
        }
    }
}
