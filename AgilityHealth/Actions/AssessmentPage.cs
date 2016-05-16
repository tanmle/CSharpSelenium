using System;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class AssessmentPage : ControlFactory
    {
        public void ClickOnAddAnAssessment()
        {
            Control<Button>("addAnAssessmentButton").Click();
        }

        public void SelectAssessmentType(string type)
        {
            Control<Label>("dynamicAssessmentTypeLabel", null, type);
            Control<Button>("addAssessmentButton").Click();
        }

        public bool DoesAssessmentDisplay(string assessment)
        {
            Control<Label>("dynamicAssessmentLabel", null, assessment).WaitForControlDisplay(40);
            return Control<Label>("dynamicAssessmentLabel", null, assessment).Exists;
        }

        public bool DoesParticipantDisplayCorrectly(string assessment, string respondent, string contact)
        {
            return Control<Label>("dynamicAssessmentParticipantLabel", null, assessment).GetAttribute("textContent").Trim().Contains(respondent + " out of " + contact);
        }

        public bool DoesAssessmentDateDisplayCorrectly(string assessment, string date)
        {
            return Control<Label>("dynamicAssessmentDateLabel", null, assessment).Text.Trim().Contains(date);
        }

        public bool DoesAssessmentStateDisplayCorrectly(string assessment, string status)
        {
            return Control<Div>("dynamicAssessmentStatusDiv", null, assessment, status).Exists;
        }

        public void ClickAssessmentLogo(string assessment)
        {
            Control<Hyperlink>("dynamicAssessmentLogoLink", null, assessment).Click();
        }
    }
}
