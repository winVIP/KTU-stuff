/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_fork01a.c */

#include <stdio.h>
#include <unistd.h>

int vp_test();

int vp_test()
{
	pid_t child, grandchild;
	child = fork();
	if(child == 0)
	{
		printf("Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
		
		grandchild = fork();
		if(grandchild == 0)
		{
			printf("Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
		}
	}
	else
	{
		printf("Process id: %d  Parent id: %d\n", (int)getpid(), (int)getppid());
	}

	
	return 0;
}

int main( int argc, char * argv[] ){
	printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
	vp_test();
	return 0;
}