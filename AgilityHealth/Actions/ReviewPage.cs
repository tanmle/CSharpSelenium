using System;
using System.Security.Claims;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class ReviewPage : ControlFactory
    {
        public bool DoesSurveyTypeDisplay(string surveyType)
        {
            return Control<Label>("surveyTypeLabel").Text == surveyType;
        }

        public bool DoesAssessmentNameDisplay(string assessmentName)
        {
            return Control<Label>("assessmentNameLabel").Text == assessmentName;
        }

        public bool DoesFacilitatorDisplay(string facilitator)
        {
            return Control<Label>("facilitatorLabel").Text == facilitator;
        }

        public bool DoesTeamMemberDisplay(string name, string role, string email)
        {
            return Control<Label>("dynamicTeamMemberLabel", null, name, role, email).Exists;
        }

        public bool DoesStakeDisplay(string name, string role, string email)
        {
            return Control<Label>("dynamicStakeLabel", null, name, role, email).Exists;
        }

        public void SelectSendToOption(string sendTo)
        {
            Control<RadioButton>("dynamicSendToRadioButton", null, sendTo).Click();
        }

        public void ClickOnPublish()
        {
            Control<Button>("publishButton").Click();
        }
    }
}
