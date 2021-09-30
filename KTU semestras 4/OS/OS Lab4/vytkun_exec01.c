/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_exec01.c */

#include <stdio.h>
#include <unistd.h>
#include <signal.h>
#include <unistd.h>

int vp_test();

int vp_test( int argc, char * argv[] )
{
	if(atoi(argv[1]) > 0)
	{
		printf("Process id: %d  Parent id: %d Argument: %d\n", (int)getpid(), (int)getppid(), atoi(argv[1]));
		char buf[5];
		sprintf(buf, "%d", atoi(argv[1]) - 1);
		execl("/export/home/vytkun/lab4/work/exec01", "exec01", buf, (char*)0);
	}
	
}

int main( int argc, char * argv[] )
{
   printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
   vp_test(argc, argv);
   return 0;
}