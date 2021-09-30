/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_fork03.c */

#include <stdio.h>
#include <unistd.h>
#include <signal.h>
#include <unistd.h>

int vp_test();

int vp_test()
{
	pid_t child;
	child = fork();
	if(child == 0)
	{
		printf("Child Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
	}
	else
	{
		printf("Parent Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
		execl("/bin/ps", "ps", "-f", "-a", (char*)0);
		wait(0);
	}
}

int main( int argc, char * argv[] )
{
   printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
   vp_test();
   return 0;
}