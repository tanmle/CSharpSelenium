using System;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class CreateAssessmentPage : ControlFactory
    {
        public void CreateAssessment(string assessmentType, string name, string facilitator)
        {
            Control<ComboBox>("assessmentTypeCombobox").SelectByText(assessmentType);
            Control<TextBox>("nameTextbox").SendKeys(name);
            Control<TextBox>("facilitatorTextbox").SendKeys(facilitator);

            Control<Button>("selectTeamMembersButton").Click();
        }
    }
}
