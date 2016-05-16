using System;
using AgilityHealth.Actions;
using Framework.Common;
using NUnit.Framework;

namespace AgilityHealth.Scenarios
{
    [TestFixture]
    public class SCR01_AddAnAssessment
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        private AssessmentPage assessmentPage;
        private CreateAssessmentPage createAssessmentPage;
        private TeamMemberPage teamMemberPage;
        private StakeHoldersPage stakeHoldersPage;
        private ReviewPage reviewPage;
        private RadaPage radaPage;

        public SCR01_AddAnAssessment()
        {
            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
            assessmentPage = new AssessmentPage();
            createAssessmentPage = new CreateAssessmentPage();
            teamMemberPage = new TeamMemberPage();
            stakeHoldersPage = new StakeHoldersPage();
            reviewPage = new ReviewPage();
            radaPage = new RadaPage();
        }

        [SetUp]
        public void Init()
        {
            Browser.GoToDefaultUrl();
        }

        [Test]
        public void AddAnAssessment()
        {
            var email = "tanle@mailinator.com";
            var password = "asdfasdf";
            string assessmentType = "Team Health";
            string assessmentName = "Sample " + new Random().Next(100000, 999999);
            string facilitator = "Tan Le";
            string today = DateTime.Now.ToString("M/d/yyyy");

            //Login to Agility Health
            loginPage.Login(email, password);

            //Switch to grid view
            dashboardPage.SwitchToGridView();

            //Select Selenium Team
            dashboardPage.ClickOnTeam("Selenium Team");

            //Click on Add An Assessment
            assessmentPage.ClickOnAddAnAssessment();

            //Select Team assessment type
            assessmentPage.SelectAssessmentType("Team");

            //Create an assessment
            createAssessmentPage.CreateAssessment(assessmentType, assessmentName, facilitator);

            //Select Mem 1, Mem 2, Mem 3
            teamMemberPage.SelectTeamMember("Mem 1", "Business Analyst", "Mem1@mailinator.com");
            teamMemberPage.SelectTeamMember("Mem 2", "", "Mem2@mailinator.com");
            teamMemberPage.SelectTeamMember("Mem 3", "", "Mem3@mailinator.com");

            //Click select Stake
            teamMemberPage.ClickOnStackHolders();

            //Select Stake 1, Stake 2, Stake 3
            stakeHoldersPage.SelectStake("Stake 1", "Executive", "stake1@mailinator.com");
            stakeHoldersPage.SelectStake("Stake 2", "Executive", "stake2@mailinator.com");
            stakeHoldersPage.SelectStake("Stake 3", "Manager", "stake3@mailinator.com");

            //Click Review and Finish
            stakeHoldersPage.ClickOnReviewAndFinish();

            //Verify assessment info
            Assert.IsTrue(reviewPage.DoesSurveyTypeDisplay(assessmentType), "Survey type is incorrect");
            Assert.IsTrue(reviewPage.DoesAssessmentNameDisplay(assessmentName), "Assessment name is incorrect");
            Assert.IsTrue(reviewPage.DoesTeamMemberDisplay("Mem  1", "Business Analyst", "Mem1@mailinator.com"), "Mem 1 is incorrect");
            Assert.IsTrue(reviewPage.DoesTeamMemberDisplay("Mem  2", "", "Mem2@mailinator.com"), "Mem 2 is incorrect");
            Assert.IsTrue(reviewPage.DoesTeamMemberDisplay("Mem  3", "", "Mem3@mailinator.com"), "Mem 3 is incorrect");
            Assert.IsTrue(reviewPage.DoesStakeDisplay("Stake 1", "Executive", "stake1@mailinator.com"), "Stake 1 is incorrect");
            Assert.IsTrue(reviewPage.DoesStakeDisplay("Stake 2", "Executive", "stake2@mailinator.com"), "Stake 2 is incorrect");
            Assert.IsTrue(reviewPage.DoesStakeDisplay("Stake 3", "Manager", "stake3@mailinator.com"), "Stake 3 is incorrect");

            //Select Send to every one
            reviewPage.SelectSendToOption("Send To Everyone");

            //Click Publish button
            reviewPage.ClickOnPublish();

            //Verify assessment displays correctly
            Assert.IsTrue(assessmentPage.DoesAssessmentDisplay(assessmentName), "Assessment does not display");
            Assert.IsTrue(assessmentPage.DoesParticipantDisplayCorrectly(assessmentName, "0", "6"), "Participant displays incorrectly");
            Assert.IsTrue(assessmentPage.DoesAssessmentStateDisplayCorrectly(assessmentName, "Open"), "Assessment status displays incorrectly");
            Assert.IsTrue(assessmentPage.DoesAssessmentDateDisplayCorrectly(assessmentName ,today), "Assessment data displays incorrectly");

            //Click on Assessment
            assessmentPage.ClickAssessmentLogo(assessmentName);
            
            //Verify that Assessment rada displays
            Assert.IsTrue(radaPage.DoesAssessmentRadaDisplay(assessmentName), "Assessment rada does not display");
        }

        [TearDown]
        public void CleanUp()
        {
            dashboardPage.LogOut();
            Browser.Close();
        }
    }
}