using System;
using System.IO;
using Xunit;
using OrderBot;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.ToLower().Contains("welcome"));
        }
        [Fact]
        public void TestName()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[1];
            Assert.True(sInput.ToLower().Contains("name"));
        }
        [Fact]
        public void TestSymptoms()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("sajid")[0];
            Assert.True(sInput.ToLower().Contains("symptoms"));
        }
        [Fact]
        public void TestDate()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("sajid");
            String sInput = oSession.OnMessage("fever")[0];
            Assert.True(sInput.ToLower().Contains("date"));
        }
        [Fact]
        public void TestConfirm()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("sajid");
            oSession.OnMessage("fever");
            String sInput = oSession.OnMessage("21 july 2022")[0];
            Assert.True(sInput.ToLower().Contains("appointment"));
        }

    }
}
