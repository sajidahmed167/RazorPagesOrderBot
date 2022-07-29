using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _patientname = String.Empty;
        private string _phone = String.Empty;
        private string _symptoms = String.Empty;
        private string _appointmentdate = String.Empty;

        public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string Patientname{
            get => _patientname;
            set => _patientname = value;
        }

        public string Symptoms{
            get => _symptoms;
            set => _symptoms = value;
        }

        public string Appointmentdate{
            get => _appointmentdate;
            set => _appointmentdate = value;
        } 


        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET patientname = $patientname , symptoms = $symptoms, appointmentdate = $appointmentdate
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$patientname", Patientname);
                commandUpdate.Parameters.AddWithValue("$symptoms", Symptoms);
                commandUpdate.Parameters.AddWithValue("$appointmentdate", Appointmentdate);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(patientname, phone, appointmentdate, symptoms)
            VALUES($patientname, $phone, $appointmentdate, $symptoms)
        ";
                    commandInsert.Parameters.AddWithValue("$patientname", Patientname);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    commandInsert.Parameters.AddWithValue("$appointmentdate", Appointmentdate);
                    commandInsert.Parameters.AddWithValue("$symptoms", Symptoms);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
