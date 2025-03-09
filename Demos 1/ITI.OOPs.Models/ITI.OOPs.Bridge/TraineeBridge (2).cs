
using ITI.OOPs.Models ;

namespace ITI.OOPs.Bridge
{
    class TraineeBridge
    {
        List<Trainee> Get()
        {
            return new List<Trainee>()
            {
                new Trainee
                (){Id=1, Name="Ahmed"},
                new Trainee(){Id=2, Name="Doaa"}
               
            };
        }
    }
}