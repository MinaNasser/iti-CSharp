
#include <stdio.h>
int main()
{
    int x = 80;

    int * pointerTox = &x;
    int &pp = x;

    printf("%d \n", x);
    printf("%d \n", *pointerTox);
    printf("%d", pp);


    return 0;
}
