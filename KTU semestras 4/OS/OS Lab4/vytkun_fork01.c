/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_fork01.c */

#include <stdio.h>
#include <unistd.h>

int vp_test();

int vp_test()
{

   fork();
   fork();

   printf("Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
   return 0;
}

int main( int argc, char * argv[] )
{
   printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
   vp_test();
   return 0;
}