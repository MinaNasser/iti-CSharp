
#include "Trainee.h"
class TraineeRepo
{
    public:
        TraineeRepo(){}
        Trainee * GetTrainees(){

          Trainee * data
               =    new Trainee[10];
          data[0].Id=1;
          data[0]. Name = "Ahmed";

          return data;
        }
};
