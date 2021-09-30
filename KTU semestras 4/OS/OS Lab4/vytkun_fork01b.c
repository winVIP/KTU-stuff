/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_fork01b.c */

#include <stdio.h>
#include <unistd.h>

int vp_test();

int vp_test()
{
	pid_t child1, child2;
	child1 =  fork();
	if(child1 == 0)
	{
		printf("Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
	}
	else
	{
		printf("Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
		child2 = fork();
		if(child2 == 0)
		{
			printf("Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
		}
	}	
	return 0;
}

int main( int argc, char * argv[] )
{
   printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
   vp_test();
   return 0;
}