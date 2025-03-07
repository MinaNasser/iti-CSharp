
namespace P1
{
    public class CoplexNumber
    {
        int real;
        int img;
        public int GetReal(){return real;}
        public int GetImg(){return img;}
        
        public void SetReal(int real){this.real=real;}
        public void SetImg(int img){this.img=img;}

        public CoplexNumber()
        {
            real = 0;
            img = 0;
        }
        public CoplexNumber(int _r ,int _i)
        {   
            real = _r;
            img = _i;
        }

        public static void Demo(CoplexNumber coplex)
        {
            coplex.real=coplex.img=2;
        }




    }
}
