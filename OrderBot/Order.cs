using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _name = String.Empty;
        private string _phone = String.Empty;
        private string _symptoms = String.Empty;
        private DateOnly? _appointmentdate = null;

        public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string Name{
            get => _name;
            set => _name = value;
        }

        public string Symptoms{
            get => _symptoms;
            set => _symptoms = value;
        }

        public DateOnly? Appointmentdate{
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
        SET name = $name , symptoms = $symptoms, appointmentdate = $appointmentdate
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$name", Name);
                commandUpdate.Parameters.AddWithValue("$symptoms", Symptoms);
                commandUpdate.Parameters.AddWithValue("$appointmentdate", Appointmentdate);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(name, phone, appointmentdate, symptoms)
            VALUES($name, $phone, $appointmentdate, $symptoms)
        ";
                    commandInsert.Parameters.AddWithValue("$name", Name);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    commandUpdate.Parameters.AddWithValue("$appointmentdate", Appointmentdate);
                    commandUpdate.Parameters.AddWithValue("$symptoms", Symptoms);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
