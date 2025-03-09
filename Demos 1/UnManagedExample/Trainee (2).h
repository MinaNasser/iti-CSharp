

class Trainee
{
    public:
        int Id;
        string Name;
        Trainee(){}
        Trainee(int _id, string _name){
            Id =_id;
            Name=_name;
        }
        void Display(){
            cout << "ID: " << Id<<", Name:"
             << Name << endl;
        }
};
