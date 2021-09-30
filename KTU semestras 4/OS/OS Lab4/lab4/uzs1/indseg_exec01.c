#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
int main( int argc, char *argv[] ){

	extern char **environ;
	char buf[5];
	if(argc!=2){
		perror("Blogas argumentu kiekis");
	}
	int a = atoi(argv[1]);
	a=a-1;
	sprintf(buf, "%d", a);
	char *file[] ={ "./a.out", buf, NULL};
	printf("PID: %d\n",  getpid());
	printf("PPID: %d\n",  getppid());
	printf("argv: %d\n",  a);




	if(a > 0){
		execv(file[0], file);
	}
	return 0;
}
//kaip int paverst i char????
