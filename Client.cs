namespace HomeWork_29_04
{
    class Client{
        public string Firstname{get;set;}
        public string Middlename{get;set;}
        public string Lastname{get;set;}
        public int Id{get;set;}
        public decimal Balance{get;set;}
        public Client(string firstname, string middlename, string lastname, int id, decimal balance){
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
            Id = id;
            Balance = balance;
        }
        public Client(){

        }
    }
}