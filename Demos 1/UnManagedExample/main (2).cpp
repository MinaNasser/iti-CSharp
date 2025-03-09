#include <iostream>
using namespace std;

#include "TraineeRepo.h"

int main()
{
    TraineeRepo *  repo
    = new TraineeRepo();


    Trainee * data  = repo->GetTrainees();
    cout <<
      ((Trainee)(data)[0]).Name;

    delete repo;
    delete[] data;



    return 0;
}
