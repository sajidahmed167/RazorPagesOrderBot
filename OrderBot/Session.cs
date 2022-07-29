using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, SYMPTOMS, DATE, CONFIRM
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to Flu Fighters!");
                    aMessages.Add("Please enter your Full name? (example: John Doe)");
                    this.nCur = State.SYMPTOMS;
                    break;
                case State.SYMPTOMS:
                    this.oOrder.Patientname = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("What are the Symptoms for your visit, for multiple symptoms write each with comma in between? (Example: Fever,Coughing,Backaches)");
                    this.nCur = State.DATE;
                    break;
                case State.DATE:
                    this.oOrder.Symptoms = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("What date are you available to visit us? (Example: 8 July 2022)");
                    this.nCur = State.CONFIRM;
                    break;
                case State.CONFIRM:
                    this.oOrder.Appointmentdate = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Thank for booking an appointment");
                    this.nCur = State.WELCOMING;
                    break;


            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
